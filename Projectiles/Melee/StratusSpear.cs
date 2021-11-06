using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Melee
{
	public class StratusSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Spear");
		}

		public override void SetDefaults()
			{
			projectile.width = 146;
			projectile.height = 42;
			projectile.aiStyle = 19;
			projectile.penetrate = -1;
			projectile.scale = 1.3f;
			projectile.alpha = 0;

			projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

		public float MovementFactor
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void AI()
		{
			Player projOwner = Main.player[projectile.owner];
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			projectile.position.X = projOwner.Center.X - (projectile.width * 0.5f);
			projectile.position.Y = projOwner.Center.Y - (projectile.height * 0.5f);

			if (!projOwner.frozen)
			{
				if (MovementFactor == 0f)
				{
					MovementFactor = 3f;
					projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
					MovementFactor -= 2.4f;
				else
					MovementFactor += 2.1f;
			}

			projectile.position += projectile.velocity * MovementFactor;

			if (projOwner.itemAnimation == 0)
				projectile.Kill();

			projectile.rotation = projectile.velocity.ToRotation();

			Vector2 pos = projectile.Center + new Vector2(Main.rand.NextFloat(-projectile.width * 0.5f, projectile.width * 0.5f), -2.5f).RotatedBy(projectile.rotation);

			Dust.NewDustPerfect(pos, DustType<CloudDust>(), projectile.velocity.RotatedBy(Main.rand.NextFloat(MathHelper.ToRadians(-20f), MathHelper.ToRadians(21f))) * 2f);

			projectile.UpdateCenterCache();
			projectile.UpdateRotationCache();
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/StratusSpear"), projectile.Center - Main.screenPosition, new Rectangle(0, 0, 146, 42), Color.White, projectile.rotation, new Vector2(projectile.width, projectile.height) * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			for (int i = 0; i < projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(158, 186, 226, 0), new Vector4(54, 16, 53, 0), i / (float)(projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X *= alpha;
				colorV.Y *= alpha;
				colorV.Z *= alpha;
				colorV.W *= alpha;
				Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Projectiles/Melee/StratusSpearGlow"), projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, 146, 42), color, projectile.oldRot[i], new Vector2(projectile.width, projectile.height) * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 7;
		}
	}
}
