using Microsoft.Xna.Framework;
using ProvidenceMod.Dusts;
using System;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrWhirlwind : ModProjectile
	{
		public bool fadeOut;
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Whirlwind");
			Main.projFrames[projectile.type] = 7;
		}
		public override void SetDefaults()
		{
			projectile.width = 58;
			projectile.height = 18;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.damage = 0;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.Opacity = 0f;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			projectile.ai[0]++;
			if (projectile.ai[0] == 2)
			{
				projectile.ai[0] = 0;
				float sin1 = (float)((float)(Math.Sin(Main.GlobalTime * 6d))) * 30f;
				float sin2 = ((float)((float)(Math.Sin(Main.GlobalTime * 6d))) * 30f) * -1f;
				Vector2 alpha1 = new Vector2(0f, sin1).RotatedBy(projectile.rotation);
				Vector2 alpha2 = new Vector2(0f, sin2).RotatedBy(projectile.rotation);
				Dust.NewDust(new Vector2(projectile.Center.X + alpha1.X, projectile.Center.Y + alpha1.Y), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
				Dust.NewDust(new Vector2(projectile.Center.X + alpha2.X, projectile.Center.Y + alpha2.Y), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 1f).ToVector3());
			if (projectile.ai[1] < 20)
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
				projectile.damage = 35;
			}
			if(projectile.timeLeft == 20)
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
			if (++projectile.frameCounter >= 6) // Frame time
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 7) //Frame number
				{
					projectile.frame = 0;
				}
			}
			if (color.W == 0f)
			{
				projectile.Kill();
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			projectile.damage = 0;
			fadeOut = true;
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
