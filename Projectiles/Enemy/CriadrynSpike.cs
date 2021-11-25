﻿using System;
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
			projectile.timeLeft = 600;
		}
		public override void AI()
		{
			projectile.velocity.Y += 0.4f;
			if (projectile.velocity.Y > 10f)
				projectile.velocity.Y = 10f;
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item48, projectile.position);
		}
	}
}
