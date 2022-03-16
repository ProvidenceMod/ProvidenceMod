using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Projectiles.Ranged
{
	public class AntimatterBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Antimatter Bullet");
		}
		public override void SetDefaults()
		{
			Projectile.width = 100;
			Projectile.height = 2;
			Projectile.penetrate = 2;
			Projectile.friendly = true;
			Projectile.scale = 1f;
			Projectile.hide = false;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 10f.InTicks();
			Projectile.damage = 70;
		}
		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
			Lighting.AddLight(Projectile.position, new Vector3(109, 242, 196).RGBIntToFloat());
			Player p = Main.player[Projectile.owner];
			p.noKnockback = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			SoundEngine.PlaySound(SoundID.Item54);
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			foreach (NPC npc in Main.npc)
			{
				if (!npc.friendly && !npc.boss)
				{
					float distance = Vector2.Distance(Projectile.position, npc.position);
					if (distance <= 256)
					{
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(Projectile.position));
					}
				}
			}
			target.AddBuff(BuffID.Ichor, 8f.InTicks());
			//target.immune[projectile.owner] = 1;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Item54);
			Projectile.penetrate -= 10;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			foreach (NPC npc in Main.npc)
			{
				if (!npc.friendly && !npc.boss)
				{
					float distance = Vector2.Distance(Projectile.position, npc.position);
					if (distance <= 256)
					{
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(Projectile.position));
					}
				}
			}
			return false;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
