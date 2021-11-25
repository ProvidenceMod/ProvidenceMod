using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Projectiles.Enemy;

namespace ProvidenceMod.NPCs.Desert
{
	public class Criadryn : ModNPC
	{
		private int volleyTimer;
		private int volleyCooldown;
		private Entity target;
		private bool HasTarget { get => target?.active == true; }
		private bool firingVolley;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Criadryn");
		}
		public override void SetDefaults()
		{
			npc.width = 104;
			npc.height = 64;
			npc.friendly = false;
			npc.defense = 30;
			npc.lifeMax = 350;
			npc.noTileCollide = false;
			npc.knockBackResist = 0.2f;
			npc.HitSound = SoundID.NPCHit1;
			npc.damage = 50;
		}
		private void CountTimers()
		{
			if (firingVolley)
			{
				if (volleyCooldown < 120) volleyCooldown++;
				if (HasTarget && volleyCooldown >= 120 && volleyTimer < 15) volleyTimer++;
				else volleyTimer = 0;
			}
		}
		public override void AI()
		{
			firingVolley = HasTarget;
			CountTimers();
			target = npc.ClosestPlayer();
			npc.TargetClosestUpgraded();
			float direction = target.Center.X - npc.Center.X > 0 ? 1 : -1;
			npc.velocity.X += 0.15f * direction;
			// Prevents air maneuvering.
			if (Collision.SolidCollision(npc.BottomLeft, npc.width, 1))
			{
				// Clamp velocity.
				//Utils.Clamp(npc.velocity.X, -1f, 1f);
				if (npc.velocity.X > 1f)
					npc.velocity.X = 1f;
				if (npc.velocity.X < -1f)
					npc.velocity.X = -1f;

				// Jump if stuck.
				//if (!Collision.SolidCollision(npc.BottomLeft + new Vector2(0f, 5f), 1, 1))
				//	npc.velocity.Y = -3f;
				//if (!Collision.SolidCollision(npc.BottomRight + new Vector2(0f, 5f), 1, 1))
				//	npc.velocity.Y = -3f;
			}

			// Jump over tiles in the way.
			if (Collision.SolidCollision(npc.TopLeft - new Vector2(8f, 8f), 8, npc.height))
				npc.velocity.Y = -4f;
			if (Collision.SolidCollision(npc.TopRight - new Vector2(-8f, 0f), 8, npc.height))
				npc.velocity.Y = -4f;
			if (Collision.SolidCollision(npc.BottomLeft - new Vector2(8f, 8f), 8, 8))
				npc.velocity.Y = -4f;
			if (Collision.SolidCollision(npc.BottomRight - new Vector2(-8f, 8f), 8, 8))
				npc.velocity.Y = -4f;

			if (firingVolley && volleyCooldown >= 120 && volleyTimer % 5 == 0)
			{
				Vector2 distance = target.Center - npc.Center;
				distance.X += target.velocity.X * 60f;
				float x = Utils.Clamp((distance.X + target.velocity.X) / 60f, -8f, 8f);
				float y = Main.rand.NextFloat(-140f, -120f) / 10f;

				Projectile.NewProjectile(npc.Center, new Vector2(x, y), ProjectileType<CriadrynSpike>(), npc.damage, 1f);
				volleyCooldown = volleyTimer >= 15 ? 0 : volleyCooldown;
			}
		}
	}
}