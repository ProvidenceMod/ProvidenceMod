using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Boss
{
	public class SentinelShrapnel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Shrapnel");
		}
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.scale = 1f;
			projectile.timeLeft = 300;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.penetrate = 1;
		}
		public override void AI()
		{
			Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), Vector2.Zero, default, default, 5f);
			if (projectile.ai[0] == 1)
				projectile.friendly = true;
			else
				projectile.hostile = true;
			projectile.velocity.Y += 0.3f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = GetTexture("ProvidenceMod/Projectiles/Boss/SentinelShrapnel");
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, (int)projectile.ai[1] * 10, 10, 10), lightColor, projectile.velocity.ToRotation(), new Vector2(5f, 5f), 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver2)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), speed, default, default, 5f);
			}
			return true;
		}
	}
}
