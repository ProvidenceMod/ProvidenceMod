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
		private const int volleyTimerUpper = 15;
		private const int volleyCDUpper = 120;
		private int volleyTimer;
		private int volleyCD = 120;
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
				if (volleyCD > 0) volleyCD--;
				if (HasTarget && volleyCD <= 0 && volleyTimer < volleyTimerUpper) volleyTimer++;
				else volleyTimer = 0;
			}
		}
		public override void AI()
		{
			CountTimers();
			target = npc.ClosestPlayer();
			npc.TargetClosestUpgraded();
			float direction = target.Center.X - npc.Center.X > 0 ? 1 : -1;
			npc.velocity.X += 0.15f * direction;
			// Prevents air maneuvering.
			if (Collision.SolidCollision(npc.BottomLeft, npc.width, 1))
			{
				// Clamp velocity.
				Utils.Clamp(npc.velocity.X, -3f, 3f);

				// Jump if stuck.
				if (!Collision.SolidCollision(npc.BottomLeft + new Vector2(0f, 5f), 1, 1))
					npc.velocity.Y = -3f;
				if (!Collision.SolidCollision(npc.BottomRight + new Vector2(0f, 5f), 1, 1))
					npc.velocity.Y = -3f;
			}

			// Jump over tiles in the way.
			//if (Collision.SolidCollision(npc.TopLeft, 1, npc.height))
			//	npc.velocity.Y = -3f;
			//if (Collision.SolidCollision(npc.TopRight, 1, npc.height))
			//	npc.velocity.Y = -3f;

			firingVolley = HasTarget;
			if (firingVolley && volleyCD <= 0 && volleyTimer % 5 == 0)
			{
				Vector2 distance = target.Center - npc.Center;
				float x = Utils.Clamp(distance.X / 60f, -8f, 8f);
				float y = Main.rand.NextFloat(-140f, -120f) / 10f;

				Projectile.NewProjectile(npc.Center, new Vector2(x, y), ProjectileType<CriadrynSpike>(), npc.damage, 1f);
				volleyCD = volleyTimer >= volleyTimerUpper ? volleyCDUpper : volleyCD;
			}
		}
	}
}