using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Buffs.DamageOverTime;
using ProvidenceMod.Particles;

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
			projectile.width = 70;
			projectile.height = 26;
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
			//Lighting.AddLight(projectile.Center, new Vector3(Main.DiscoR, Main.DiscoG, Main.DiscoB).RGBIntToFloat());
			if (projectile.ai[0] % Main.rand.Next(100, 201) == 0 && projectile.ai[0] != 0)
				NewParticle(projectile.Center, projectile.velocity.RotatedBy(Main.rand.NextFloat(-10f, 11f) / 100f) / 3f, new MoonBlastParticle(), Color.White, Main.rand.NextFloat(5f, 11f) / 10f);
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
			//projectile.rotation += projectile.velocity.X * 0.05f;
			projectile.rotation = projectile.velocity.ToRotation();
			NPC target = projectile.ClosestNPC();
			if (target?.Distance(projectile.Center) <= 750f)
			{
				if (projectile.timeLeft > 120)
				{
					if (projectile.position.IsInRadiusOf(target.position, 500f) && projectile.ai[0] % 25 == 0)
						Projectile.NewProjectile(projectile.Center, new Vector2(4f, 0f).RotatedBy(projectile.AngleTo(target.Center)).RotatedBy(Main.rand.NextFloat(0f, MathHelper.TwoPi)), ProjectileType<MoonBeam>(), 100, 0f, projectile.owner);
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
			float amount = MathHelper.Lerp(0f, 1f, ((Main.GlobalTime * 64f) % 360) / 360);
			Color hsl = Main.hslToRgb(amount, 1f, 0.75f);
			Color color = new Color(hsl.R, hsl.G, hsl.B, 0);

			for (int i = 0; i < projectile.Providence().oldCen.Length - 1; i++)
			{
				float alpha = 1f - (i * 0.1f);
				for (int k = 0; k < 3; k++)
				{
					Color color2 = new Color((int)(hsl.R * alpha), (int)(hsl.G * alpha), (int)(hsl.B * alpha), 0);

					spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), projectile.Providence().oldCen[i] + new Vector2(k == 0 ? 0f : 5f, k == 0 ? 0f : k == 1 ? -5f : 5f).RotatedBy(projectile.Providence().oldCen[i].ToRotation()) - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);
				}
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), -MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Glow"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 512, 512), Color.Multiply(color, 0.25f), projectile.rotation, new Vector2(256, 256), 0.25f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
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
			target.immune[projectile.owner] = 1;
		}
		//public override Color? GetAlpha(Color lightColor) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f);
	}
}