using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

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
		}
		public override void AI()
		{
			oldPos[4] = oldPos[3];
			oldPos[3] = oldPos[2];
			oldPos[2] = oldPos[1];
			oldPos[1] = oldPos[0];
			oldPos[0] = projectile.Center;
			Lighting.AddLight(projectile.Center, (float)Main.DiscoR / 400f, (float)Main.DiscoG / 400f, (float)Main.DiscoB / 400f);
			projectile.ai[0]++;
			if (projectile.soundDelay == 0)
			{
				projectile.soundDelay = 640;
				Main.PlaySound(SoundID.Item9, projectile.position);
			}
			projectile.rotation += projectile.velocity.X * 0.05f;
			NPC target = (NPC)ClosestEntity(projectile, true);
			if (target != null && target.Distance(projectile.Center) >= 300f)
			{
				Vector2 unitY = projectile.DirectionTo(target.Center);
				projectile.velocity = ((projectile.velocity * 15f) + (unitY * 20f)) / (15f + 1f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast");
			const int counter = 5;
			ProvidenceGlobalProjectile.AfterImage(projectile, lightColor, tex, counter);
			//Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
			//float alpha = 1f;
			//for (int i = 0; i <= 4; i++)
			//{
			//	alpha = 255 - (i * 51);
			//	spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), oldPos[i] - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), new Color(alpha, alpha, alpha, alpha), projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
			//}
			//spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, projectile.width, projectile.height), new Color(255, 255, 255, color.W), projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale, SpriteEffects.None, 0f);
			//Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			//for (int k = 0; k < projectile.oldPos.Length; k++)
			//{
			//	float alpha = 1f - (k * (1f / projectile.oldPos.Length));
			//	Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
			//	Color color = projectile.GetAlpha(lightColor * alpha);
			//	spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			//}
			return true;
			////return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = projectile.OwnerPlayer();
			// Caps potential healing at 1% of max health per hit.
			int healingAmount = damage / 60 >= player.statLifeMax / 100 ? player.statLifeMax / 100 : damage / 60;
			// Actually heals, and gives the little green numbers pop up
			player.statLife += healingAmount;
			player.HealEffect(healingAmount, true);

			projectile.penetrate--;
			target.immune[projectile.owner] = 3;

			// int trueDmg = crit ? damage * 2 : damage;
			// if (target.life - trueDmg <= 0)
			// {
			//   NPC newTarget = ClosestEntity(projectile);
			//   if (newTarget?.active == true)
			//     projectile.velocity = projectile.velocity.RotateTo(projectile.AngleTo(newTarget.position));
			// }
		}
		public override Color? GetAlpha(Color lightColor) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		public override void Kill(int timeLeft)
		{
		}
	}
}