 using Terraria;
 using Terraria.ID;
 using Terraria.ModLoader;
 using Microsoft.Xna.Framework;
 using System.Collections.Generic;
 using System;

 namespace UnbiddenMod.Projectiles.Ranged
 {
     public class CoronachtArrow : ModProjectile
     {
        
         public override void SetStaticDefaults()
         {
             DisplayName.SetDefault("Coro Arrow");
         }

         public override void SetDefaults()
         {
             projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
             projectile.arrow = true;
         }
     }
}
