using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using System;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrDart : ModProjectile
	{
		public float opacity = 1f;
		public double mult = 0.1d;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Dart");
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 14;
			projectile.timeLeft = 420;
			projectile.scale = 1f;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.hostile = true;
			projectile.friendly = false;
		}
		public override void AI()
		{
			projectile.UpdatePositionCache();
			projectile.UpdateCenterCache();
			projectile.UpdateRotationCache();
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
			switch (projectile.ai[0])
			{
				// Alternate between AI states periodically for a cool effect?

				// Friendly
				case 1:
					projectile.hostile = false;
					projectile.friendly = true;
					break;
				// Immediate weak homing
				case 2:
					if (projectile.timeLeft <= 300)
					{
						opacity -= 1f / 30f;
						if (opacity <= 0)
							projectile.Kill();
					}
					NaturalHoming(projectile, projectile.ClosestPlayer(), 100f, 10f);
					break;
				// Accelerate homing
				case 3:
					if (projectile.timeLeft > 360)
					{
						projectile.velocity.X *= 0.85f;
						projectile.velocity.Y *= 0.85f;
					}
					if (projectile.timeLeft <= 360 && projectile.timeLeft > 240)
					{
						projectile.velocity.X *= 1.1f + ((float)mult / 10f);
						projectile.velocity.Y *= 1.1f + ((float)mult / 10f);
					}
					if ((projectile.timeLeft <= 180 && projectile.velocity.Length() < 8f))
					{
						projectile.hostile = false;
						opacity -= 1f / 30f;
						if (opacity <= 0)
							projectile.Kill();
					}
					NaturalHoming(projectile, projectile.ClosestPlayer(), 25f, projectile.velocity.Length());
					break;
				// Spiral
				case 4:
					if (projectile.timeLeft <= 330)
					{
						opacity -= 1f / 30f;
						if (opacity <= 0)
							projectile.Kill();
					}
					mult *= 1.05d;
					Vector2 v = new Vector2((float)(Math.Cos(Main.GlobalTime * (6f * projectile.Providence().extraAI[0])) * (mult * projectile.Providence().extraAI[0])),
																	(float)(Math.Sin(Main.GlobalTime * (6f * projectile.Providence().extraAI[0])) * (mult * projectile.Providence().extraAI[0])))
																	.RotatedBy(projectile.ai[1]);
					projectile.velocity.X = v.X;
					projectile.velocity.Y = v.Y;
					break;
				// Helix
				case 5:
					Vector2 v2 = new Vector2(10f, (float)(Math.Sin(Main.GlobalTime * 18f) * 4f * projectile.Providence().extraAI[0])).RotatedBy(projectile.ai[1]);
					projectile.velocity.X = v2.X;
					projectile.velocity.Y = v2.Y;
					break;
				// Gravity
				case 6:
					projectile.velocity.Y += 0.3f;
					break;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int i = 0; i < projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / (float)(projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X = colorV.Y * alpha * opacity;
				colorV.Y = colorV.X * alpha * opacity;
				colorV.Z = colorV.Z * alpha * opacity;
				colorV.W = colorV.W * alpha * opacity;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Boss/ZephyrDart"), projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, 40, 14), color, projectile.oldRot[i], new Vector2(projectile.width, projectile.height) * 0.5f, 1.0f - (0.15f * (i / 10f)), SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Boss/ZephyrDart"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 40, 14), new Color(1f * opacity, 1f * opacity, 1f * opacity, 0f), projectile.rotation, new Vector2(projectile.width, projectile.height) * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
