using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Providence.ProvidenceUtils;
using Terraria.Audio;

namespace Providence.Content.Projectiles.Ranged
{
	public class AirBullet : ModProjectile
	{
		public bool fadeOut;
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Air Bullet");
		}
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 2;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.aiStyle = -1;
			Projectile.hide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 300;
			Projectile.damage = 3;
			Projectile.Opacity = 1f;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
			Lighting.AddLight(Projectile.Center, new Vector3(98, 99, 129).RGBIntToFloat());
			if (Projectile.ai[1] < 20)
			{
				Projectile.ai[1]++;
				Projectile.Opacity += 0.2f;
				color.X += 0.2f;
				color.Y += 0.2f;
				color.Z += 0.2f;
				color.W += 0.2f;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			Collision.HitTiles(Projectile.Center + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.Kill();
			return false;
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
