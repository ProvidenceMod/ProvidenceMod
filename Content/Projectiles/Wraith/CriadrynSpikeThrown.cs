using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Projectiles.Wraith
{
	public class CriadrynSpikeThrown : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ciradryn Spike");
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.ai[0] = 0;
		}
		public override void AI()
		{
			Projectile.velocity.Y += 0.2f;
			if (Projectile.velocity.Y > 20f)
				Projectile.velocity.Y = 20f;
			Projectile.rotation = Projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item48, Projectile.position);
		}
	}
}
