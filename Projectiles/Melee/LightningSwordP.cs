using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UnbiddenMod.Projectiles.Melee
{
  public class LightningSwordP : ModProjectile
  {
    public override string Texture => "Terraria/Projectile_" + ProjectileID.WoodenArrowFriendly;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lightning Bolt");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
      projectile.damage = 10;
      projectile.timeLeft = 180;
      projectile.Unbidden().element = 2;
    }
  }
}