using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.ID;
namespace ProvidenceMod.Projectiles.Ranged
{
	public class AntimatterBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Antimatter Bullet");
		}
		public override void SetDefaults()
		{
			projectile.width = 100;
			projectile.height = 2;
			projectile.penetrate = 2;
			projectile.friendly = true;
			projectile.scale = 1f;
			projectile.hide = false;
			projectile.aiStyle = 0;
			projectile.ranged = true;
			projectile.timeLeft = 10f.InTicks();
			projectile.damage = 70;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.position, new Vector3(109, 242, 196).RGBIntToFloat());
			Player p = Main.player[projectile.owner];
			p.noKnockback = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.Item54);
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			foreach (NPC npc in Main.npc)
			{
				if (!npc.friendly && !npc.boss)
				{
					float distance = Vector2.Distance(projectile.position, npc.position);
					if (distance <= 256)
					{
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
					}
				}
			}
			target.AddBuff(BuffID.Ichor, 8f.InTicks());
			//target.immune[projectile.owner] = 1;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item54);
			projectile.penetrate -= 10;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			foreach (NPC npc in Main.npc)
			{
				if (!npc.friendly && !npc.boss)
				{
					float distance = Vector2.Distance(projectile.position, npc.position);
					if (distance <= 256)
					{
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
					}
				}
			}
			return false;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}