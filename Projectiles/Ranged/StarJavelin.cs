using ProvidenceMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Particles;

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
			projectile.width = 60;
			projectile.height = 18;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			aiType = 0;
			projectile.timeLeft = 300;
			projectile.penetrate = 5;
			projectile.scale = 1f;
			projectile.Opacity = 0f;
		}
		public override void AI()
		{
			projectile.UpdateCenterCache();
			projectile.UpdatePositionCache();
			projectile.UpdateRotationCache();
			if (projectile.ai[0] % Main.rand.Next(100, 201) == 0 && projectile.ai[0] != 0)
				NewParticle(projectile.Center, projectile.velocity.RotatedBy(Main.rand.NextFloat(-10f, 11f) / 100f) / 3f, new MoonBlastParticle(), Color.White, Main.rand.NextFloat(5f, 11f) / 10f);
			if(projectile.Providence().extraAI[0] == 0)
			{
				pos = new Vector2(projectile.ai[0], projectile.ai[1]);
				projectile.ai[0] = 0;
				projectile.Providence().extraAI[0]++;
			}
			projectile.ai[0]++;
			if (projectile.ai[0] < 30)
			{
				projectile.Opacity += 1f / 30f;
				if (projectile.Opacity >= 0)
					projectile.Opacity = 1f;
			}
			if (projectile.timeLeft <= 30)
			{
				projectile.Opacity -= 1f / 30f;
				if (projectile.Opacity <= 0)
					projectile.Kill();
			}
			if (projectile.soundDelay == 0)
			{
				projectile.soundDelay = 640;
				Main.PlaySound(SoundID.Item9, projectile.position);
			}
			projectile.rotation = projectile.velocity.ToRotation();
			NPC target = projectile.ClosestNPC();
			if (target?.Distance(projectile.Center) <= 750f)
			{
				Vector2 unitY = projectile.DirectionTo(target.Center);
				projectile.velocity = ((projectile.velocity * 20f) + (unitY * 40f)) / (20f + 1f);
			}
			else if(projectile.ai[0] <= 10)
			{
				Vector2 unitY = projectile.DirectionTo(pos);
				projectile.velocity = ((projectile.velocity * 20f) + (unitY * 40f)) / (20f + 1f);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			float amount = MathHelper.Lerp(0f, 1f, ((Main.GlobalTime * 64f) % 360) / 360);
			Color hsl = Main.hslToRgb(amount, 1f, 0.75f);
			Color color = new Color((int) (hsl.R * projectile.Opacity),(int) (hsl.G * projectile.Opacity), (int) (hsl.B * projectile.Opacity), 0);

			for (int i = 0; i < projectile.Providence().oldCen.Length - 1; i++)
			{
				float alpha = 1f - (i * 0.1f);
				//for (int k = 0; k < 3; k++)
				//{
				//	Color color2 = new Color((int)(hsl.R * alpha * projectile.Opacity), (int)(hsl.G * alpha * projectile.Opacity), (int)(hsl.B * alpha * projectile.Opacity), 0);

				//	spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Ranged/StarJavelin"), projectile.Providence().oldCen[i] + new Vector2(k == 0 ? 0f : 2.5f, k == 0 ? 0f : k == 1 ? -2.5f : 2.5f).RotatedBy(projectile.Providence().oldCen[i].ToRotation()) - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);
				//}
				Color color2 = new Color((int)(hsl.R * alpha * projectile.Opacity), (int)(hsl.G * alpha * projectile.Opacity), (int)(hsl.B * alpha * projectile.Opacity), 0);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Ranged/StarJavelin"), projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), Color.Multiply(color2, alpha + (i * 0.05f)), projectile.oldRot[i], new Vector2(projectile.width / 2, projectile.height / 2), 1f * alpha, SpriteEffects.None, 0f);

			}
			//spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			//spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 142, 42), Color.Multiply(color, 0.5f), -MathHelper.PiOver4, new Vector2(71, 21), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Glow"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 512, 512), Color.Multiply(color, 0.25f), projectile.rotation, new Vector2(256, 256), 0.25f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Ranged/StarJavelin"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
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

			projectile.penetrate--;
			target.immune[projectile.owner] = 1;
		}
	}
}
