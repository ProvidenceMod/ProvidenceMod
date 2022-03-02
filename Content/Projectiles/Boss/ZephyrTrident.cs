using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Projectiles.Boss
{
	public class ZephyrTrident : ModProjectile
	{
		public Vector2 velocity;

		public float opacity = 1f;
		public float mult = 1.1f;
		public int frame;
		public int frameCounter;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Trident");
		}
		public override void SetDefaults()
		{
			Projectile.width = 104;
			Projectile.height = 26;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 420;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.Opacity = 0f;
			Projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			if (Projectile.ai[0] == 0)
			{
				if (Projectile.ai[1] > 0)
					Projectile.rotation = Projectile.AngleTo(Projectile.ClosestPlayer().Center);
				if (Projectile.ai[1] == 0)
					Projectile.velocity = new Vector2(32f, 0f).RotatedBy(Projectile.rotation);
				Projectile.ai[1]--;
				Projectile.UpdateCenterCache();
				Projectile.UpdateRotationCache();
				Lighting.AddLight(Projectile.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
				if (Projectile.timeLeft >= (180 + (Projectile.ai[1] * 90)) - 30)
				{
					opacity += 1f / 30f;
					if (opacity >= 0)
						opacity = 1f;
				}
				if (Projectile.timeLeft <= 30)
				{
					opacity -= 1f / 30f;
					if (opacity <= 0)
						Projectile.Kill();
				}
			}
			else
			{
				Projectile.UpdateCenterCache();
				Projectile.UpdateRotationCache();
				if (Projectile.ai[1] == 0)
				{
					Projectile.penetrate = 5;
					velocity = Projectile.velocity;
				}
				Projectile.ai[1]++;
				Projectile.hostile = false;
				Projectile.friendly = true;
				Projectile.penetrate = -1;
				Projectile.rotation = Projectile.velocity.ToRotation();
				velocity.X *= 1.05f;
				velocity.Y *= 1.05f;
				float sin = (float)((Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.375f) + 0.625f);
				Projectile.velocity.X = velocity.X * sin;
				Projectile.velocity.Y = velocity.Y * sin;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			float sine = (float)((Math.Sin(Main.GlobalTimeWrappedHourly * 12f) * 0.25f) + 1f);
			for (int i = 0; i < Projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(158, 186, 226, 0), new Vector4(54, 16, 53, 0), i / (float)(Projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X = colorV.X * alpha * opacity;
				colorV.Y = colorV.Y * alpha * opacity;
				colorV.Z = colorV.Z * alpha * opacity;
				colorV.W = colorV.W * alpha * opacity;
				Color color = new(colorV.X * sine, colorV.Y * sine, colorV.Z * sine, colorV.W * sine);
				Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Boss/ZephyrTrident").Value, Projectile.Providence().oldCen[i] - Main.screenPosition, Projectile.AnimationFrame(ref frame, ref frameCounter, 30, 11, true), color, Projectile.oldRot[i], new Vector2(Projectile.width, Projectile.height) * 0.5f, 1f, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Boss/ZephyrTrident").Value, Projectile.Center - Main.screenPosition, Projectile.AnimationFrame(ref frame, ref frameCounter, 30, 11, true), new Color(1f * sine, 1f * sine, 1f * sine, 1f * sine), Projectile.rotation, new Vector2(Projectile.width, Projectile.height) * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			Projectile.hostile = false;
			Projectile.timeLeft = 30;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 10;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
