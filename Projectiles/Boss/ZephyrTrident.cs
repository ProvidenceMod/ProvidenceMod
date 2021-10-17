using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using System;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrTrident : ModProjectile
	{
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
			projectile.width = 104;
			projectile.height = 26;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 420;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.Opacity = 0f;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			if (projectile.ai[0] == 0)
			{
				if (projectile.ai[1] > 0)
					projectile.rotation = projectile.AngleTo(ClosestEntity(projectile, false).Center);
				if (projectile.ai[1] == 0)
					projectile.velocity = new Vector2(32f, 0f).RotatedBy(projectile.rotation);
				projectile.ai[1]--;
				projectile.UpdateCenterCache();
				projectile.UpdateRotationCache();
				Lighting.AddLight(projectile.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
				if (projectile.timeLeft >= (180 + (projectile.ai[1] * 90)) - 30)
				{
					opacity += 1f / 30f;
					if (opacity >= 0)
						opacity = 1f;
				}
				if (projectile.timeLeft <= 30)
				{
					opacity -= 1f / 30f;
					if (opacity <= 0)
						projectile.Kill();
				}
			}
			else
			{
				projectile.UpdateCenterCache();
				projectile.UpdateRotationCache();
				if (projectile.ai[1] == 0)
					projectile.penetrate = 5;
				projectile.ai[1]++;
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.rotation = projectile.velocity.ToRotation();
				projectile.velocity.X *= 1.05f;
				projectile.velocity.Y *= 1.05f;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			float sine = (float)((Math.Sin(Main.GlobalTime * 12f) * 0.375f) + 0.625f);
			for (int i = 0; i < projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / (float)(projectile.oldRot.Length - 1)).ColorRGBAIntToFloat();
				colorV.X = colorV.Y * alpha * opacity;
				colorV.Y = colorV.X * alpha * opacity;
				colorV.Z = colorV.Z * alpha * opacity;
				colorV.W = colorV.W * alpha * opacity;
				Color color = new Color(colorV.X * sine, colorV.Y * sine, colorV.Z * sine, colorV.W * sine);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Boss/ZephyrTrident"), projectile.Providence().oldCen[i] - Main.screenPosition, projectile.AnimationFrame(ref frame, ref frameCounter, 30, 11, true), color, projectile.oldRot[i], Vector2.Zero, 1.0f - (0.15f * (i / 10f)), SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Boss/ZephyrTrident"), projectile.Center - Main.screenPosition, projectile.AnimationFrame(ref frame, ref frameCounter, 30, 11, true), new Color(1f * sine, 1f * sine, 1f * sine, 1f * sine), projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			projectile.hostile = false;
			projectile.timeLeft = 30;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
