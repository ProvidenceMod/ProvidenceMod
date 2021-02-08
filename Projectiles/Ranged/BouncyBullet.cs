using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Projectiles.Ranged
{
  public class BouncyBullet : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bouncy Bullet");
    }

    public override void SetDefaults()
    {
      projectile.penetrate = 10;
      projectile.friendly = true;
      projectile.height = 4;
      projectile.width = 4;
      projectile.scale = 1f;
      projectile.hide = false;
      projectile.aiStyle = 0;
      projectile.ranged = true;
      projectile.restrikeDelay = 2;
      projectile.timeLeft = 5.InTicks();
      projectile.extraUpdates = 1;
    }
    public override void AI()
    {
      projectile.rotation = projectile.velocity.ToRotation();
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      // Making sure the next closest NPC is found, not this one
      target.active = false;
      NPC nextTarget = ClosestEnemyNPC(projectile);
      if (nextTarget != null)
      {
        projectile.velocity = projectile.velocity.RotateTo(projectile.AngleTo(nextTarget.position));
        projectile.penetrate -= 2;
      }
      target.active = true;
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