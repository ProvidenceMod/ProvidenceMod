using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Ranged
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
			projectile.width = 32;
			projectile.height = 2;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.aiStyle = -1;
			projectile.hide = false;
			projectile.ranged = true;
			projectile.timeLeft = 300;
			projectile.damage = 3;
			projectile.Opacity = 1f;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, new Vector3(98, 99, 129).RGBIntToFloat());
			if (projectile.ai[1] < 20)
			{
				projectile.ai[1]++;
				projectile.Opacity += 0.2f;
				color.X += 0.2f;
				color.Y += 0.2f;
				color.Z += 0.2f;
				color.W += 0.2f;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			Collision.HitTiles(projectile.Center + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			projectile.Kill();
			return false;
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
