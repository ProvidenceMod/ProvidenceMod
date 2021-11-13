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
		private const int volleyTimerUpper = 15, volleyCDUpper = 120;
		private int volleyTimer = 0, volleyCD = volleyCDUpper;
		private Entity fullTarget = null;
		private bool HasTarget { get => fullTarget != null && fullTarget.active; }
		private bool firingVolley = false;
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
			npc.HitSound = SoundID.NPCHit41;
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
			fullTarget = npc.ClosestPlayer();
			npc.target = fullTarget.whoAmI;
			float direction = fullTarget.position.X - npc.position.X > 0 ? 1 : -1;
			npc.velocity.X += direction * 0.15f;
			if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y + (npc.height / 2)), 1, 1))
			{
				if (npc.velocity.X > 3f)
					npc.velocity.X = 3f;
				if (npc.velocity.X < -3f)
					npc.velocity.X = -3f;
			}
			if (HasTarget) firingVolley = true;
			else firingVolley = false;
			if (firingVolley && volleyCD <= 0 && volleyTimer % 5 == 0)
			{
				Vector2 d = fullTarget.Center - npc.Center,
					normalized = fullTarget.Center - npc.Center;
				normalized.Normalize();
				float maxRange = Utils.Clamp(d.X, -360f, 360f);
				double angle = MathHelper.Lerp(180, 360, Math.Abs(maxRange) / 360f);
				Vector2 lobbingAngle = new Vector2(10f, 0f).RotateTo(npc.AngleTo(fullTarget.Center)).RotatedBy(d.X > 0 ? (volleyTimer - 5) * -2d.InRadians() : (volleyTimer - 5) * 2d.InRadians());
				Main.PlaySound(SoundID.Item5, npc.Center);
				Projectile.NewProjectile(npc.Center, lobbingAngle, ProjectileType<CriadrynSpike>(), npc.damage, 1f);
				if (volleyTimer >= volleyTimerUpper) volleyCD = volleyCDUpper;
			}
		}
	}
}