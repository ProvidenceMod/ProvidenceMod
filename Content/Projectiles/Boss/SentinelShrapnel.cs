using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Dusts;

namespace Providence.Content.Projectiles.Boss
{
	public class SentinelShrapnel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Shrapnel");
		}
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.scale = 1f;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.penetrate = 1;
		}
		public override void AI()
		{
			Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), Vector2.Zero, default, default, 5f);
			if (Projectile.ai[0] == 1)
				Projectile.friendly = true;
			else
				Projectile.hostile = true;
			Projectile.velocity.Y += 0.3f;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Request<Texture2D>("Providence/Projectiles/Boss/SentinelShrapnel").Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, (int)Projectile.ai[1] * 10, 10, 10), lightColor, Projectile.velocity.ToRotation(), new Vector2(5f, 5f), 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver2)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), speed, default, default, 5f);
			}
			return true;
		}
	}
}
