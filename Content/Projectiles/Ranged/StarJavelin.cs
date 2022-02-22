using ProvidenceMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Particles;
using Terraria.Audio;
using ParticleLibrary;

namespace ProvidenceMod.Projectiles.Ranged
{
	public class StarJavelin : ModProjectile
	{
		public Vector2 pos;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Javelin");
		}
		public override void SetDefaults()
		{
			Projectile.width = 60;
			Projectile.height = 18;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			AIType = 0;
			Projectile.timeLeft = 300;
			Projectile.penetrate = 5;
			Projectile.scale = 1f;
			Projectile.Opacity = 0f;
		}
		public override void AI()
		{
			Projectile.UpdateCenterCache();
			Projectile.UpdatePositionCache();
			Projectile.UpdateRotationCache();
			if (Projectile.ai[0] % Main.rand.Next(100, 201) == 0 && Projectile.ai[0] != 0)
				ParticleManager.NewParticle(Projectile.Center, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-10f, 11f) / 100f) / 3f, new MoonBlastParticle(), Color.White, Main.rand.NextFloat(5f, 11f) / 10f);
			if (Projectile.Providence().extraAI[0] == 0)
			{
				pos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
				Projectile.ai[0] = 0;
				Projectile.Providence().extraAI[0]++;
			}
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 30)
			{
				Projectile.Opacity += 1f / 30f;
				if (Projectile.Opacity >= 0)
					Projectile.Opacity = 1f;
			}
			if (Projectile.timeLeft <= 30)
			{
				Projectile.Opacity -= 1f / 30f;
				if (Projectile.Opacity <= 0)
					Projectile.Kill();
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 640;
				SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
			}
			Projectile.rotation = Projectile.velocity.ToRotation();
			NPC target = Projectile.ClosestNPC();
			if (target?.Distance(Projectile.Center) <= 750f)
			{
				Vector2 unitY = Projectile.DirectionTo(target.Center);
				Projectile.velocity = ((Projectile.velocity * 20f) + (unitY * 40f)) / (20f + 1f);
			}
			else if (Projectile.ai[0] <= 10)
			{
				Vector2 unitY = Projectile.DirectionTo(pos);
				Projectile.velocity = ((Projectile.velocity * 20f) + (unitY * 40f)) / (20f + 1f);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			float amount = MathHelper.Lerp(0f, 1f, ((Main.GlobalTimeWrappedHourly * 64f) % 360) / 360);
			Color hsl = Main.hslToRgb(amount, 1f, 0.75f);
			Color color = new Color((int)(hsl.R * Projectile.Opacity), (int)(hsl.G * Projectile.Opacity), (int)(hsl.B * Projectile.Opacity), 0);

			for (int i = 0; i < Projectile.Providence().oldCen.Length - 1; i++)
			{
				float alpha = 1f - (i * 0.1f);
				//for (int k = 0; k < 3; k++)
				//{
				//	Color color2 = new Color((int)(hsl.R * alpha * projectile.Opacity), (int)(hsl.G * alpha * projectile.Opacity), (int)(hsl.B * alpha * projectile.Opacity), 0);

				//	spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Ranged/StarJavelin"), projectile.Providence().oldCen[i] + new Vector2(k == 0 ? 0f : 2.5f, k == 0 ? 0f : k == 1 ? -2.5f : 2.5f).RotatedBy(projectile.Providence().oldCen[i].ToRotation()) - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);
				//}
				Color color2 = new Color((int)(hsl.R * alpha * Projectile.Opacity), (int)(hsl.G * alpha * Projectile.Opacity), (int)(hsl.B * alpha * Projectile.Opacity), 0);
				Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Ranged/StarJavelin").Value, Projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), Projectile.oldRot[i], new Vector2(Projectile.width / 2, Projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);

			}
			//spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			//spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), -MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/ExtraTextures/Glow").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 512, 512), Color.Multiply(color, 0.25f), Projectile.rotation, new Vector2(256, 256), 0.25f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/Projectiles/Ranged/StarJavelin").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, Projectile.width, Projectile.height), color, Projectile.rotation, new Vector2(Projectile.width / 2, Projectile.height / 2), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
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

			Projectile.penetrate--;
			target.immune[Projectile.owner] = 1;
		}
	}
}
