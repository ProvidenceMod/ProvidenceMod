using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace ProvidenceMod.Projectiles.Melee
{
	public class MoonBeam : ModProjectile
	{
		public override string Texture => "ProvidenceMod/ExtraTextures/EmptyPixel";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Beam");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.Opacity = 0f;
			projectile.extraUpdates = 100;
		}
		public override void AI()
		{
			projectile.ai[0]++;
			if (projectile.ai[0] % 2.5f == 0)
			{
				Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Red, default, default, default, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Green);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Blue);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Yellow);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Pink);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
				projectile.Kill();
		}
	}
}
