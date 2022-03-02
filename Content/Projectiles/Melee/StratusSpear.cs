using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Dusts;

namespace Providence.Content.Projectiles.Melee
{
	public class StratusSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Spear");
		}

		public override void SetDefaults()
		{
			Projectile.width = 146;
			Projectile.height = 42;
			Projectile.aiStyle = 19;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			Projectile.alpha = 0;

			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

		public float MovementFactor
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = projOwner.Center.X - (Projectile.width * 0.5f);
			Projectile.position.Y = projOwner.Center.Y - (Projectile.height * 0.5f);

			if (!projOwner.frozen)
			{
				if (MovementFactor == 0f)
				{
					MovementFactor = 3f;
					Projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
					MovementFactor -= 2.4f;
				else
					MovementFactor += 2.1f;
			}

			Projectile.position += Projectile.velocity * MovementFactor;

			if (projOwner.itemAnimation == 0)
				Projectile.Kill();

			Projectile.rotation = Projectile.velocity.ToRotation();

			Vector2 pos = Projectile.Center + new Vector2(Main.rand.NextFloat(-Projectile.width * 0.5f, Projectile.width * 0.5f), -2.5f).RotatedBy(Projectile.rotation);

			Dust.NewDustPerfect(pos, DustType<CloudDust>(), Projectile.velocity.RotatedBy(Main.rand.NextFloat(MathHelper.ToRadians(-20f), MathHelper.ToRadians(21f))) * 2f);

			Projectile.UpdateCenterCache();
			Projectile.UpdateRotationCache();
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Melee/StratusSpear").Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 146, 42), Color.White, Projectile.rotation, new Vector2(Projectile.width, Projectile.height) * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
			for (int i = 0; i < Projectile.oldRot.Length; i++)
			{
				float alpha = 1f - (i * 0.1f);
				Vector4 colorV = Vector4.Lerp(new Vector4(158, 186, 226, 0), new Vector4(54, 16, 53, 0), i / (float)(Projectile.oldRot.Length - 1)).RGBAIntToFloat();
				colorV.X *= alpha;
				colorV.Y *= alpha;
				colorV.Z *= alpha;
				colorV.W *= alpha;
				Color color = new(colorV.X, colorV.Y, colorV.Z, colorV.W);
				Main.spriteBatch.Draw(Request<Texture2D>("Providence/Projectiles/Melee/StratusSpearGlow").Value, Projectile.Providence().oldCen[i] - Main.screenPosition, new Rectangle(0, 0, 146, 42), color, Projectile.oldRot[i], new Vector2(Projectile.width, Projectile.height) * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 7;
		}
	}
}
