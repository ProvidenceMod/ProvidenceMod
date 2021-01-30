using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles.Magic
{
  public class FriendlyHarpyFeather : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Friendly Harpy Feather");
    }

    public override void SetDefaults()
    {
      projectile.width = 50;
      projectile.height = 22;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      projectile.timeLeft = 600;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.damage = 25;
      projectile.friendly = true;
      projectile.hostile = false;
      projectile.Unbidden().element = ElementID.Air; // Typeless
      projectile.Unbidden().homingID = HomingID.Gravity;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      projectile.rotation = projectile.velocity.ToRotation();

      const float speedCap = 6f, turnStrength = 0.3f;
      NPC target = ClosestEnemyNPC(projectile);
      if (target != null)
        projectile.Homing(target, speedCap, default, default, turnStrength);
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return new Color(255, 255, 255);
    }
  }
}