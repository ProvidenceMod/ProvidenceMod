using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Boss
{
  public class RoyalFeather : ModProjectile
  {
    public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
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
      projectile.scale = 0.75f;
      projectile.damage = 25;
      projectile.hostile = false;
      projectile.friendly = true;
      projectile.Opacity = 0f;
      projectile.Providence().element = -1; // Typeless
      projectile.Providence().homingID = HomingID.Natural;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      const float speedCap = 8f, turnStrength = 20f;
      Player player = ClosestPlayer(projectile);
      projectile.Homing(player, speedCap, default, default, turnStrength, 1500);
      if(projectile.Opacity < 1f)
        projectile.Opacity += 0.01f;
        color.X += 0.01f;
        color.Y += 0.01f;
        color.Z += 0.01f;
        color.W += 0.01f;
      if(projectile.Opacity == 1)
        projectile.hostile = true;
        projectile.friendly = false;
      projectile.rotation = projectile.velocity.ToRotation();
      // if (++projectile.frameCounter >= 3) // Frame time
      // {
      //   projectile.frameCounter = 0;
      //   if (++projectile.frame >= 9) //Frame number
      //   {
      //     projectile.frame = 0;
      //   }
      // }
    }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      base.OnHitPlayer(target, damage, crit);
      projectile.active = false;
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return new Color(color.X, color.Y, color.Z, color.W);
    }
  }
}