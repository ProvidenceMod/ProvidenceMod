using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace ProvidenceMod.Projectiles.Magic
{
  public class TKWave : ModProjectile
  {
    private bool vectDone;
    private Vector2 forwardV, ampV;
    private int herzing;
    private Vector2 amplitude = new Vector2(0, 0.4f);
    public override string Texture => $"Terraria/Item_{ProjectileID.Flamelash}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("TK's Wave Wave");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.Flamelash);
      projectile.tileCollide = false;
      projectile.timeLeft = 300;
    }

    public override void AI()
    {
      if (!vectDone)
      {
        forwardV = projectile.velocity;
        ampV = forwardV.RotatedBy(90f.InRadians());
        amplitude.RotateTo(ampV.ToRotation());

        vectDone = true;
      }
      else
      {
        herzing++;
        if (herzing > 60)
        {
          herzing = 0;
        }
        if (herzing <= 15)
        {
          projectile.velocity += amplitude;
        }
        else if (herzing <= 45)
        {
          projectile.velocity -= amplitude;
        }
        else if (herzing <= 60)
        {
          projectile.velocity += amplitude;
        }
      }
    }
  }
}