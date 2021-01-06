using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UnbiddenMod.Projectiles.Ranged
{
  public class IceSphere : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ice Sphere");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.Flames);
      projectile.Unbidden().element = 1; // Ice
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      // Every hit has a 10% chance to either slow down the target or extend their slow time.
      // If they're slowed for long enough, they'll freeze outright!
      if (0 == 0)
      { // 100% chance
        if (target.Unbidden().freezing && !target.Unbidden().frozen)
        {
          int buffChill = target.FindBuffIndex(mod.BuffType("Freezing"));
          if (target.buffTime[buffChill] > 600)
          {
            target.DelBuff(buffChill);
            target.AddBuff(mod.BuffType("Frozen"), 300);
          }
          else
          {
            target.buffTime[buffChill] += 60;
          }
        }
        else
        {
          target.AddBuff(mod.BuffType("Freezing"), 180); // Slows for 3 seconds
        }
      }
    }
    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.Cyan;
      return color;
    }
  }
}