using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Enemy
{
	public class CriadrynSpike : ModProjectile
	{
		public const float gravConst = 0.3f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ciradryn Spike");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 10;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 10f.InTicks();
			aiType = 1; // Lobbed projectile
			projectile.ai[0] = 0;
		}

		public override void AI()
		{
			projectile.ai[0]++;
			if (projectile.ai[0] >= 20)
				projectile.velocity.Y += gravConst;
			if (projectile.velocity.Y > 16f) projectile.velocity.Y = 16f;
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item48, projectile.position);
		}
	}
}
