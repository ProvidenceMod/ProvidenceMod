using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using System;

namespace Providence.Content.Projectiles.Boss
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
			Projectile.width = 40;
			Projectile.height = 14;
			Projectile.timeLeft = 420;
			Projectile.scale = 1f;
			Projectile.hostile = true;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 1;
			Projectile.hostile = true;
			Projectile.friendly = false;
		}
		public override void AI()
		{
			Projectile.UpdatePositionCache();
			Projectile.UpdateCenterCache();
			Projectile.UpdateRotationCache();
			Projectile.rotation = Projectile.velocity.ToRotation();
			Lighting.AddLight(Projectile.Center, ProvidenceColor.ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
			switch (Projectile.ai[0])
			{
				// Alternate between AI states periodically for a cool effect?

				// Friendly
				case 1:
					Projectile.hostile = false;
					Projectile.friendly = true;
					break;
				// Immediate weak homing
				case 2:
					if (Projectile.timeLeft <= 300)
					{
						opacity -= 1f / 30f;
						if (opacity <= 0)
							Projectile.Kill();
					}
					NaturalHoming(Projectile, Projectile.ClosestPlayer(), 100f, 10f);
					break;
				// Accelerate homing
				case 3:
					if (Projectile.timeLeft > 360)
					{
						Projectile.velocity.X *= 0.85f;
						Projectile.velocity.Y *= 0.85f;
					}
					if (Projectile.timeLeft <= 360 && Projectile.timeLeft > 240)
					{
						Projectile.velocity.X *= 1.1f + ((float)mult / 10f);
						Projectile.velocity.Y *= 1.1f + ((float)mult / 10f);
					}
					if ((Projectile.timeLeft <= 180 && Projectile.velocity.Length() < 8f))
					{
						Projectile.hostile = false;
						opacity -= 1f / 30f;
						if (opacity <= 0)
							Projectile.Kill();
					}
					NaturalHoming(Projectile, Projectile.ClosestPlayer(), 25f, Projectile.velocity.Length());
					break;
				// Spiral
				case 4:
					if (Projectile.timeLeft <= 330)
					{
						opacity -= 1f / 30f;
						if (opacity <= 0)
							Projectile.Kill();
					}
					mult *= 1.05d;
					Vector2 v = new Vector2((float)(Math.Cos(Main.GlobalTimeWrappedHourly * (6f * Projectile.Providence().extraAI[0])) * (mult * Projectile.Providence().extraAI[0])),
																	(float)(Math.Sin(Main.GlobalTimeWrappedHourly * (6f * Projectile.Providence().extraAI[0])) * (mult * Projectile.Providence().extraAI[0])))
																	.RotatedBy(Projectile.ai[1]);
					Projectile.velocity.X = v.X;
					Projectile.velocity.Y = v.Y;
					break;
				// Helix
				case 5:
					Vector2 v2 = new Vector2(10f, (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 18f) * 4f * Projectile.Providence().extraAI[0])).RotatedBy(Projectile.ai[1]);
					Projectile.velocity.X = v2.X;
					Projectile.velocity.Y = v2.Y;
					break;
				// Gravity
				case 6:
					Projectile.velocity.Y += 0.3f;
					break;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			for (int i = 0; i < Projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(158, 186, 226, 0), new Vector4(54, 16, 53, 0), i / (float)(Projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X = colorV.Y * alpha * opacity;
				colorV.Y = colorV.X * alpha * opacity;
				colorV.Z = colorV.Z * alpha * opacity;
				colorV.W = colorV.W * alpha * opacity;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Boss/ZephyrDart").Value, Projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, 40, 14), color, Projectile.oldRot[i], new Vector2(Projectile.width, Projectile.height) * 0.5f, 1.0f - (0.15f * (i / 10f)), SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Boss/ZephyrDart").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 40, 14), new Color(1f * opacity, 1f * opacity, 1f * opacity, 0f), Projectile.rotation, new Vector2(Projectile.width, Projectile.height) * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
