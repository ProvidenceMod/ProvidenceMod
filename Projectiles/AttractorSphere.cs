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
        }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      if (!target.Unbidden().kbInversedA && !target.Unbidden().kbInversedB) // If not currently in the process of getting inversed
      {
        target.Unbidden().InverseKnockback(target);
      }
    }



        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.Cyan;
            return (color);
        }
    }
}