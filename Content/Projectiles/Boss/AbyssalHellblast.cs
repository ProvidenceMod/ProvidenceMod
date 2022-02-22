using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ProvidenceMod.NPCs.FireAncient;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Boss
{
	public class AbyssalHellblast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Hellblast");
			Main.projFrames[Projectile.type] = 9;
		}

		public override void SetDefaults()
		{
			Projectile.width = 45;
			Projectile.height = 22;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 180;
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.damage = 100;
			Projectile.hostile = true;
			Projectile.Providence().element = 0; // Fire
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			Projectile.ai[1]++;
			Projectile.localAI[0]++;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (++Projectile.frameCounter >= 3) // Frame time
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9) //Frame number
				{
					Projectile.frame = 0;
				}
			}
			Player player = Projectile.ClosestPlayer();
			if (player != null)
			{
				Vector2 unitY = Projectile.DirectionTo(player.Center);
				Projectile.velocity = ((Projectile.velocity * 8f) + (unitY * 20f)) / (8f + 1f);
			}
		}
		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(BuffID.OnFire, 10);
		}

		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}