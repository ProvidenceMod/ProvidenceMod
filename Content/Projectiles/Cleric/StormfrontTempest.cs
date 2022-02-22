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
			Projectile.width = 104;
			Projectile.height = 80;
			Projectile.friendly = true;
			Projectile.timeLeft = 120;
			Projectile.Opacity = 0f;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (angleMod < 12f)
				return false;
			return null;
		}
		public override void AI()
		{
			// AI 0 will be the rotation angle
			// AI 1 will be the modifier for the angle
			if (Projectile.timeLeft > 30)
			{
				Projectile.Opacity += 1f / 90f;
				if (Projectile.Opacity >= 1f)
					Projectile.Opacity = 1f;
			}
			Player player = Main.player[Projectile.owner];
			if (Projectile.timeLeft > 30)
				Projectile.ai[0] += (angleMod < 24f ? angleMod += 0.02f * (1f + angleMod) : angleMod);
			Vector2 pos = new Vector2(100f, 0f).RotatedBy(Projectile.ai[1]).RotatedBy(Projectile.ai[0].InRadians());
			Projectile.rotation = pos.ToRotation();
			Projectile.Center = new Vector2(player.Center.X, player.Center.Y) + pos;
			if (Projectile.timeLeft < 31)
			{
				angleMod *= 0.9f;
				Projectile.ai[0] += angleMod;
				Projectile.Opacity -= 1f / 30f;
				if (Projectile.Opacity <= 0f)
				{
					Projectile.Opacity = 0;
					Projectile.Kill();
				}
			}
			Projectile.UpdateCenterCache();
			Projectile.UpdateRotationCache();
			Projectile.UpdatePositionCache();
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 5;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			for (int i = 0; i < Projectile.oldRot.Length; i++)
			{
				float alpha = 0.5f - (i * 0.05f);
				Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / (float)(Projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X = colorV.Y * alpha * Projectile.Opacity;
				colorV.Y = colorV.X * alpha * Projectile.Opacity;
				colorV.Z = colorV.Z * alpha * Projectile.Opacity;
				colorV.W = colorV.W * alpha * Projectile.Opacity;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				//Color color = new Color(1f * projectile.Opacity * alpha, 1f * projectile.Opacity * alpha, 1f * projectile.Opacity * alpha, 0f);
				Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Cleric/StormfrontTempest").Value, Projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), color, Projectile.oldRot[i], new Vector2(Projectile.width / 2, Projectile.height / 2), 1f, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Cleric/StormfrontTempest").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), new Color(1f * Projectile.Opacity, 1f * Projectile.Opacity, 1f * Projectile.Opacity, 0f), Projectile.rotation, new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
