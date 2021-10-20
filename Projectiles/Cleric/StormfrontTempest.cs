using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Cleric
{
	public class StormfrontTempest : ModProjectile
	{
		public float angleMod;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormfront Tempest");
		}
		public override void SetDefaults()
		{
			projectile.width = 104;
			projectile.height = 80;
			projectile.friendly = true;
			projectile.timeLeft = 120;
			projectile.Opacity = 0f;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			// AI 0 will be the rotation angle
			// AI 1 will be the modifier for the angle
			if (projectile.timeLeft > 30)
			{
				projectile.Opacity += 1f / 90f;
				if (projectile.Opacity >= 1f)
					projectile.Opacity = 1f;
			}
			Player player = Main.player[projectile.owner];
			projectile.UpdateCenterCache();
			projectile.UpdateRotationCache();
			projectile.UpdatePositionCache();
			if (projectile.timeLeft > 30)
				projectile.ai[0] += (angleMod < 24f ? angleMod += 0.01f * (1f + angleMod) : angleMod);
			Vector2 pos = new Vector2(100f, 0f).RotatedBy(projectile.ai[1]).RotatedBy(projectile.ai[0].InRadians());
			//if (angleMod > 18f)
			//{
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos), DustType<CloudDust>(), (pos / 25f).RotatedBy(-MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4 * 0.5f), DustType<CloudDust>(), (pos / 25f).RotatedBy(-MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4), DustType<CloudDust>(), (pos / 25f).RotatedBy(-MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4 * 1.5f), DustType<CloudDust>(), (pos / 25f).RotatedBy(-MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos), DustType<CloudDust>(), (pos / 25f).RotatedBy(MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4 * 0.5f), DustType<CloudDust>(), (pos / 25f).RotatedBy(MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4), DustType<CloudDust>(), (pos / 25f).RotatedBy(MathHelper.PiOver2));
			//	Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + (pos).RotatedBy(MathHelper.PiOver4 * 1.5f), DustType<CloudDust>(), (pos / 25f).RotatedBy(MathHelper.PiOver2));
			//}
			projectile.rotation = pos.ToRotation();
			projectile.Center = new Vector2(player.Center.X, player.Center.Y) + pos;
			if (projectile.timeLeft < 31)
			{
				angleMod *= 0.9f;
				projectile.ai[0] += angleMod;
				projectile.Opacity -= 1f / 30f;
				if (projectile.Opacity <= 0f)
				{
					projectile.Opacity = 0;
					projectile.Kill();
				}
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 2;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int i = 0; i < projectile.oldRot.Length; i++)
			{
				float alpha = 0.5f - (i * 0.05f);
				Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / (float)(projectile.oldRot.Length - 1)).ColorRGBAIntToFloat();
				colorV.X = colorV.Y * alpha * projectile.Opacity;
				colorV.Y = colorV.X * alpha * projectile.Opacity;
				colorV.Z = colorV.Z * alpha * projectile.Opacity;
				colorV.W = colorV.W * alpha * projectile.Opacity;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				//Color color = new Color(1f * projectile.Opacity * alpha, 1f * projectile.Opacity * alpha, 1f * projectile.Opacity * alpha, 0f);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Cleric/StormfrontTempest"), projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Cleric/StormfrontTempest"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), new Color(1f * projectile.Opacity, 1f * projectile.Opacity, 1f * projectile.Opacity, 0f), projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
