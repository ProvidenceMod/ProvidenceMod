using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.Audio;

namespace ProvidenceMod.Projectiles.Enemy
{
	public class CriadrynSpike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ciradryn Spike");
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 10;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
		}
		public override void AI()
		{
			Projectile.velocity.Y += 0.4f;
			if (Projectile.velocity.Y > 10f)
				Projectile.velocity.Y = 10f;
			Projectile.rotation = Projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item48, Projectile.position);
		}
	}
}
