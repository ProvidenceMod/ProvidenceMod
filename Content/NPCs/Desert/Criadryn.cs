using Microsoft.Xna.Framework;
using Providence.Content.Projectiles.Enemy;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.NPCs.Desert
{
	public class Criadryn : ModNPC
	{
		private int volleyTimer;
		private int volleyCooldown;
		private Entity target;
		private bool firingVolley;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Criadryn");
		}
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.width = 104;
			NPC.height = 64;
			NPC.friendly = false;
			NPC.defense = 30;
			NPC.lifeMax = 150;
			NPC.noTileCollide = false;
			NPC.knockBackResist = 0.2f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.damage = 50;
		}
		private void CountTimers()
		{
			if (firingVolley)
			{
				if (volleyCooldown < 120) volleyCooldown++;
				if (volleyCooldown >= 120 && volleyTimer < 15) volleyTimer++;
				else volleyTimer = 0;
			}
		}
		public override void AI()
		{
			firingVolley = target?.active ?? false;
			CountTimers();
			target = NPC.ClosestPlayer();
			NPC.TargetClosestUpgraded();
			float direction = target.Center.X - NPC.Center.X > 0 ? 1 : -1;
			NPC.velocity.X += 0.15f * direction;
			NPC.velocity.X = Terraria.Utils.Clamp(NPC.velocity.X, -1f, 1f);
			// Prevents air maneuvering.
			if (Collision.SolidCollision(NPC.BottomLeft, NPC.width, 1))
			{
				// Clamp velocity.
				//Utils.Clamp(npc.velocity.X, -1f, 1f);
				if (NPC.velocity.X > 1f)
					NPC.velocity.X = 1f;
				if (NPC.velocity.X < -1f)
					NPC.velocity.X = -1f;

				// Jump if stuck.
				//if (!Collision.SolidCollision(npc.BottomLeft + new Vector2(0f, 5f), 1, 1))
				//	npc.velocity.Y = -3f;
				//if (!Collision.SolidCollision(npc.BottomRight + new Vector2(0f, 5f), 1, 1))
				//	npc.velocity.Y = -3f;
			}

			// Jump over tiles in the way.
			if (Collision.SolidCollision(NPC.TopLeft - new Vector2(8f, 8f), 8, NPC.height))
				NPC.velocity.Y = -4f;
			if (Collision.SolidCollision(NPC.TopRight - new Vector2(-8f, 0f), 8, NPC.height))
				NPC.velocity.Y = -4f;
			if (Collision.SolidCollision(NPC.BottomLeft - new Vector2(8f, 8f), 8, 8))
				NPC.velocity.Y = -4f;
			if (Collision.SolidCollision(NPC.BottomRight - new Vector2(-8f, 8f), 8, 8))
				NPC.velocity.Y = -4f;

			if (firingVolley && volleyCooldown >= 120 && volleyTimer % 5 == 0)
			{
				Vector2 distance = target.Center - NPC.Center;
				distance.X += target.velocity.X * 60f;
				float x = Terraria.Utils.Clamp((distance.X + target.velocity.X) / 60f, -8f, 8f);
				float y = Main.rand.NextFloat(-140f, -120f) / 10f;

				Projectile.NewProjectile(new EntitySource_Parent(NPC), NPC.Center, new Vector2(x, y), ProjectileType<CriadrynSpike>(), NPC.damage, 1f);
				volleyCooldown = volleyTimer >= 15 ? 0 : volleyCooldown;
			}
		}
		public override void OnKill()
		{
			//if (Main.rand.Next(1, 5) > 1)
			//	Item.NewItem(NPC.Center, ItemType<CriadrynSpikes>(), Main.rand.Next(5, 16));
		}
	}
}
