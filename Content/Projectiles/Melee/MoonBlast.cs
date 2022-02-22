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
using ParticleLibrary;
using Terraria.Audio;

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
			Projectile.width = 70;
			Projectile.height = 26;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			AIType = 0;
			Projectile.timeLeft = 300;
			Projectile.penetrate = 3;
			Projectile.scale = 1f;
			Projectile.Opacity = 0f;
		}
		public override void AI()
		{
			Projectile.UpdateCenterCache();
			Projectile.UpdatePositionCache();
			Projectile.UpdateRotationCache();
			//Lighting.AddLight(projectile.Center, new Vector3(Main.DiscoR, Main.DiscoG, Main.DiscoB).RGBIntToFloat());
			if (Projectile.ai[0] % 60 == 0)
				ParticleManager.NewParticle(Projectile.Center, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-10f, 11f) / 100f) / 3f, new MoonBlastParticle(), Color.White, Main.rand.NextFloat(5f, 11f) / 10f);
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 30)
			{
				Projectile.Opacity += 1f / 30f;
				if (Projectile.Opacity >= 0)
					Projectile.Opacity = 1f;
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 640;
				SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
			}
			//projectile.rotation += projectile.velocity.X * 0.05f;
			Projectile.rotation = Projectile.velocity.ToRotation();
			NPC target = Projectile.ClosestNPC();
			if (target?.Distance(Projectile.Center) <= 750f)
			{
				if (Projectile.timeLeft > 120)
				{
					if (Projectile.position.IsInRadiusOf(target.position, 500f) && Projectile.ai[0] % 25 == 0)
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, new Vector2(4f, 0f).RotatedBy(Projectile.AngleTo(target.Center)).RotatedBy(Main.rand.NextFloat(0f, MathHelper.TwoPi)), ProjectileType<MoonBeam>(), 100, 0f, Projectile.owner);
					Vector2 position = new Vector2(target.Center.X + ((float)Math.Sin(Main.GlobalTimeWrappedHourly) * 300f),
																				 target.Center.Y + ((float)Math.Sin(Main.GlobalTimeWrappedHourly) * 300f));
					Vector2 unitY = Projectile.DirectionTo(position);
					Projectile.velocity = ((Projectile.velocity * 25f) + (unitY * 50f)) / (25f + 1f);
				}
				else
				{
					Vector2 unitY = Projectile.DirectionTo(target.Center);
					Projectile.velocity = ((Projectile.velocity * 15f) + (unitY * 20f)) / (15f + 1f);
				}
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			float amount = MathHelper.Lerp(0f, 1f, ((Main.GlobalTimeWrappedHourly * 64f) % 360) / 360);
			Color hsl = Main.hslToRgb(amount, 1f, 0.75f);
			Color color = new Color(hsl.R, hsl.G, hsl.B, 0);

			for (int i = 0; i < Projectile.Providence().oldCen.Length - 1; i++)
			{
				float alpha = 1f - (i * 0.1f);
				for (int k = 0; k < 3; k++)
				{
					Color color2 = new Color((int)(hsl.R * alpha), (int)(hsl.G * alpha), (int)(hsl.B * alpha), 0);

					Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Melee/MoonBlast").Value, Projectile.Providence().oldCen[i] + new Vector2(k == 0 ? 0f : 5f, k == 0 ? 0f : k == 1 ? -5f : 5f).RotatedBy(Projectile.Providence().oldCen[i].ToRotation()) - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), Projectile.oldRot[i], new Vector2(Projectile.width / 2, Projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);
				}
			}
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Flare").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Flare").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), -MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Glow").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 512, 512), Color.Multiply(color, 0.25f), Projectile.rotation, new Vector2(256, 256), 0.25f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Melee/MoonBlast").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), color, Projectile.rotation, new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Projectile.OwnerPlayer();
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
			Projectile.penetrate--;
			target.immune[Projectile.owner] = 1;
		}
		//public override Color? GetAlpha(Color lightColor) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f);
	}
}