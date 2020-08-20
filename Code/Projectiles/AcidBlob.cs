using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Code.Dusts;

namespace UnbiddenMod.Code.Projectiles
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
      projectile.GetGlobalProjectile<UnbiddenProjectile>().element = 4; // Acid

    }

    public override void AI()
    {
      // ID'ing player for future reference
      Player player = Main.player[projectile.owner];
      // Gravity
      projectile.ai[0] += 1f;
      // Rotation
      if ((float) ((Vector2) ((Entity) projectile).velocity).X > 0.0)
      {
        projectile.rotation -= 1.57f * (float)projectile.direction;
      }
      else
      {
        projectile.rotation += 1.57f * (float)projectile.direction;
      }
      // Animation AI
      if (++projectile.frameCounter >= 3)
      {
        projectile.frameCounter = 0;
        if (++projectile.frame >= 6)
        {
          projectile.frame = 0;
        }
      }
    }
  }
}