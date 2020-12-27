using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Dusts;

namespace UnbiddenMod.Projectiles
{
  public class AcidBlob : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Acid Blob");
      Main.projFrames[projectile.type] = 6;
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 7;
      projectile.height = 15;
      projectile.aiStyle = 1;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      aiType = 0;
      projectile.timeLeft = 300;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Acid
    }

    public override void AI()
    {
      // Gravity
      projectile.ai[0]++;
      // Rotation
      if (projectile.velocity.X > 0.0)
      {
        projectile.rotation -= 1.57f * (float)projectile.direction;
      }
      else
      {
        projectile.rotation += 1.57f * (float)projectile.direction;
      }
    }
  }
}