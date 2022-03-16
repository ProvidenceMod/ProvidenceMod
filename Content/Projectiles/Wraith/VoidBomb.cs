using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Projectiles.Wraith
{
	public class VoidBomb : ModProjectile
	{
		private NPC contactTarget;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Bomb");
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 36;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 3f.InTicks();
			Projectile.penetrate = 1;
			//projectile.damage = 50; A soft damage, don't want the proj itself dealing damage
		}
		public override void AI()
		{
			Projectile.velocity.Y += 0.3f;
			if (Projectile.velocity.Y > 16f)
				Projectile.velocity.Y = 16f;
			Projectile.ai[0]++;
		}
		public override bool PreDraw(ref Color color) => false;
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			contactTarget = target;
			damage += (int)Math.Floor((decimal)(Projectile.timeLeft / 10));
			Projectile.Kill();
			Projectile.active = false;
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) => projHitbox.Intersects(targetHitbox);
		public override void Kill(int timeLeft)
		{
			for (byte i = 0; i < 15; i++)
			{
				Vector2 randPointWI100f = Projectile.Center;
				randPointWI100f.X += Main.rand.NextFloat(-50f, 50f);
				randPointWI100f.Y += Main.rand.NextFloat(-50f, 50f);
			}
			Player owner = Projectile.OwnerPlayer();
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.townNPC && npc.Center.IsInRadiusOf(Projectile.position, 100f) && npc != contactTarget)
				{
					npc.StrikeNPC(50 + (int)Math.Floor((decimal)(timeLeft / 10)), 1f, -npc.direction, owner.GetCritChance(DamageClass.Throwing).PercentChance());
				}
			}
		}
	}
}
