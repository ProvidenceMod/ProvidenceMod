using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Wraith
{
	public class CriadrynSpikeThrown : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ciradryn Spike");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.ai[0] = 0;
		}
		public override void AI()
		{
			projectile.velocity.Y += 0.2f;
			if (projectile.velocity.Y > 20f)
				projectile.velocity.Y = 20f;
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item48, projectile.position);
		}
	}
}
