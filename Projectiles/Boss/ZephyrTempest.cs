using Microsoft.Xna.Framework;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrTempest : ModProjectile
	{
		public bool fadeOut;
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Tempest");
		}
		public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 90;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 600;
			projectile.penetrate = 1;
			projectile.scale = 2f;
			projectile.damage = 35;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.Opacity = 0f;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			projectile.rotation += 0.1f;
			projectile.ai[0]++;
			projectile.ai[2]++;
			if (projectile.ai[0] == 3)
			{
				projectile.ai[0] = 0;
				Dust.NewDust(projectile.Center, 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
			if (projectile.ai[1] < 15)
			{
				projectile.ai[1]++;
				projectile.Opacity += 0.05f;
				color.X += 0.05f;
				color.Y += 0.05f;
				color.Z += 0.05f;
				color.W += 0.05f;
			}
			if (projectile.ai[1] == 15)
			{
				projectile.damage = 40;
			}
			if(projectile.timeLeft == 15)
			{
				fadeOut = true;
			}
			if (fadeOut)
			{
				projectile.ai[1]++;
				projectile.Opacity -= 0.05f;
				color.X -= 0.05f;
				color.Y -= 0.05f;
				color.Z -= 0.05f;
				color.W -= 0.05f;
			}
			if (color.W == 0f)
			{
				projectile.Kill();
			}
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
