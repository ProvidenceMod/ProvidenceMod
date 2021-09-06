using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Magic
{
	public class Galeshot : ModProjectile
	{
		private bool spent = false;
		public override string Texture => $"Terraria/Projectile_{ProjectileID.WaterBolt}";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galeshot");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.hide = true;
			projectile.magic = true;
			projectile.timeLeft = 300;
			projectile.damage = 8;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 101;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, new Vector3(98, 99, 129).ColorRGBIntToFloat());
      _ = Dust.NewDust(projectile.Center, 6, 6, DustID.Ice);
		}
		public override void Kill(int timeLeft)
		{
			for (float rot = 0f; rot < 360f; rot += 15f)
			{
				_ = Dust.NewDustPerfect(new Vector2(30f, 0f).RotatedBy(rot.InRadians()), DustID.Ice, new Vector2(4f, 0f).RotatedBy(rot.InRadians()), 255, Color.Red);
			}
		}
	}
}
