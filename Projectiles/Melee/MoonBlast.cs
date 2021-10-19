using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Buffs.DamageOverTime;

namespace ProvidenceMod.Projectiles.Melee
{
	public class MoonBlast : ModProjectile
	{
		public Vector4 color = new Vector4(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
		public Vector2[] oldPos = new Vector2[5] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Blast");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			aiType = 0;
			projectile.timeLeft = 300;
			projectile.penetrate = 3;
			projectile.scale = 1f;
			projectile.Opacity = 0f;
		}
		public override void AI()
		{
			projectile.UpdateCenterCache();
			projectile.UpdatePositionCache();
			projectile.UpdateRotationCache();
			Lighting.AddLight(projectile.Center, new Vector3(Main.DiscoR, Main.DiscoG, Main.DiscoB).ColorRGBIntToFloat());
			projectile.ai[0]++;
			if (projectile.ai[0] < 30)
			{
				projectile.Opacity += 1f / 30f;
				if (projectile.Opacity >= 0)
					projectile.Opacity = 1f;
			}
			if (projectile.soundDelay == 0)
			{
				projectile.soundDelay = 640;
				Main.PlaySound(SoundID.Item9, projectile.position);
			}
			projectile.rotation += projectile.velocity.X * 0.05f;
			//projectile.rotation = projectile.velocity.ToRotation();
			NPC target = (NPC)ClosestEntity(projectile, true);
			if (target?.Distance(projectile.Center) <= 750f)
			{
				if (projectile.timeLeft > 120)
				{
					if (projectile.position.IsInRadiusOf(target.position, 500f) && projectile.ai[0] % 25 == 0)
					{
						Projectile.NewProjectile(projectile.Center, new Vector2(4f, 0f).RotatedBy(projectile.AngleTo(target.Center)).RotatedBy(Main.rand.NextFloat(0f, MathHelper.TwoPi)), ProjectileType<MoonBeam>(), 100, 0f, projectile.owner);
					}
					Vector2 position = new Vector2(target.Center.X + ((float)Math.Sin(Main.GlobalTime) * 300f),
																				 target.Center.Y + ((float)Math.Sin(Main.GlobalTime) * 300f));
					Vector2 unitY = projectile.DirectionTo(position);
					projectile.velocity = ((projectile.velocity * 25f) + (unitY * 50f)) / (25f + 1f);
				}
				else
				{
					Vector2 unitY = projectile.DirectionTo(target.Center);
					projectile.velocity = ((projectile.velocity * 15f) + (unitY * 20f)) / (15f + 1f);
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int i = 0; i < projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = new Vector4(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f).ColorRGBAIntToFloat();
				colorV.X = colorV.X * projectile.Opacity * alpha;
				colorV.Y = colorV.Y * projectile.Opacity * alpha;
				colorV.Z = colorV.Z * projectile.Opacity * alpha;
				//colorV.W = colorV.W * projectile.Opacity * alpha;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), new Color(Main.DiscoR * projectile.Opacity, Main.DiscoG * projectile.Opacity, Main.DiscoB * projectile.Opacity, 0f).ColorRGBAIntToFloat(), projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = projectile.OwnerPlayer();
			target.AddBuff(BuffID.OnFire, 600);
			target.AddBuff(BuffID.Oiled, 600);
			target.AddBuff(BuffID.Ichor, 600);
			target.AddBuff(BuffID.CursedInferno, 600);
			target.AddBuff(BuffID.Poisoned, 600);
			target.AddBuff(BuffID.Venom, 600);
			target.AddBuff(BuffType<PressureSpike>(), 600);
			target.AddBuff(BuffID.Frostburn, 600);
			target.AddBuff(BuffID.Electrified, 600);
			target.AddBuff(BuffID.Daybreak, 600);

			int healingAmount = damage / 60 >= player.statLifeMax * 0.5f ? player.statLifeMax / 2 : damage / 60;
			player.statLife += healingAmount;
			player.HealEffect(healingAmount, true);
			projectile.penetrate--;
			target.immune[projectile.owner] = 3;
		}
		//public override Color? GetAlpha(Color lightColor) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f);
	}
}