using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UnbiddenMod.Projectiles.Healing
{
  public class HealProjectile : ModProjectile
  {
    public override void SetDefaults()
    {
      projectile.width = 6;
      projectile.height = 6;
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      projectile.timeLeft = 6000;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.hostile = false;
      projectile.friendly = true;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = -1; // Typeless
      projectile.Unbidden().deflectable = false;
    }

    public override void AI()
    {
      Player player = Main.LocalPlayer;
      Vector2 offset = player.position - projectile.position;
      const float speedCap = 8f;
      const float gainStrength = 0.2f;
      const float slowStrength = 1.1f;
      UnbiddenGlobalProjectile.IsHomingPlayer(projectile, offset, speedCap, gainStrength, slowStrength);
      if (projectile.getRect().Intersects(player.getRect()))
      {
        player.statLife += 25;
      }
    }
  }
}