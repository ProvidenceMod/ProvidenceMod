using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Melee
{
  public class ClockworkCog : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Clockwork Cog");
    }

    public override void SetDefaults()
    {
      projectile.width = 26;
      projectile.height = 26;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      projectile.penetrate = 4;
      projectile.scale = 1f;
      projectile.damage = 30;
      projectile.aiStyle = 3;
      drawOffsetX = -11;
      drawOriginOffsetY = -10;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      // Bouncing effect!
      projectile.penetrate--;
			if (projectile.penetrate <= 0) {
				projectile.Kill();
			}
			else {
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				if (projectile.velocity.X != oldVelocity.X) {
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y) {
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
    }
  }
}