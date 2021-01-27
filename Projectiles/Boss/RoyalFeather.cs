using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles.Boss
{
  public class RoyalFeather : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Royal Feather");
      // Main.projFrames[projectile.type] = 9;
    }

    public override void SetDefaults()
    {
      projectile.width = 50;
      projectile.height = 22;
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      projectile.timeLeft = 300;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.damage = 25;
      projectile.hostile = true;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = -1; // Typeless
      projectile.Unbidden().deflectable = false;
      projectile.Unbidden().homingID = HomingID.Gravity;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      projectile.rotation = projectile.velocity.ToRotation();
      // if (++projectile.frameCounter >= 3) // Frame time
      // {
      //   projectile.frameCounter = 0;
      //   if (++projectile.frame >= 9) //Frame number
      //   {
      //     projectile.frame = 0;
      //   }
      // }
      const float speedCap = 8f, turnStrength = 0.3f;
      Player player = ClosestPlayer(projectile);
      projectile.Homing(player, speedCap, default, default, turnStrength);
    }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      base.OnHitPlayer(target, damage, crit);
      projectile.active = false;
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return new Color(255, 255, 255);
    }
  }
}