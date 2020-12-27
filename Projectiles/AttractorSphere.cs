using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UnbiddenMod.Projectiles
{
  public class AttractorSphere : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ice Sphere");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.Flames);
      projectile.Unbidden().inverseKB = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.Cyan;
      return color;
    }
  }
}