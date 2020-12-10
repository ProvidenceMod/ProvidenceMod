using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UnbiddenMod.Projectiles
{
  public class LightningSwordP : ModProjectile
  {
    public override string Texture => "Terraria/Projectile_" + ProjectileID.SandBallGun;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lightning Bolt");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.CultistBossLightningOrb);
      projectile.damage = 10;
      projectile.timeLeft = 180;
      projectile.hostile = false;
      projectile.friendly = true;
      projectile.Unbidden().element = 2;
    }
  }
}