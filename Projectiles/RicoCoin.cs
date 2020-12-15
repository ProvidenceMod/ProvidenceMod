using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles
{
  public class RicoCoin : ModProjectile
  {
    public bool ricocheted = false;
    public override string Texture => "Terraria/Projectile_" + ProjectileID.GoldCoin;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Rico-Coin");
    }

    public override void SetDefaults()
    {
      projectile.aiStyle = 0;
      projectile.hide = false;
      projectile.arrow = true;
      projectile.width = 16;
      projectile.height = 16;
      projectile.friendly = true;
      projectile.ranged = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = false;
      projectile.scale = 1f;
      projectile.damage = 0;
      projectile.timeLeft = 600;
    }


    // Making the AI so it'll home in on enemies
    public override void AI()
    {
      Lighting.AddLight(projectile.position, new Vector3(1, 1, 1));
      projectile.velocity.Y += 0.05f;
      if (projectile.velocity.Y > 16f)
      {
        projectile.velocity.Y = 16f;
      }
      projectile.rotation += 0.25f;

      foreach (Projectile proj2 in Main.projectile)
      {
        if (proj2.active && !proj2.hostile && proj2.friendly && proj2 != projectile && projectile.Hitbox.Intersects(proj2.Hitbox))
        {
          if (projectile.type == ModContent.ProjectileType<RicoCoin>() && projectile.type == proj2.type)
          {
            continue;
          }
          else
          {
            ricocheted = true;
            projectile.velocity = Vector2.Add(projectile.velocity, proj2.velocity);
            projectile.timeLeft = 30;
            if (ClosestEnemyNPC(proj2) != null)
            {
              Main.PlaySound(SoundID.Item37, proj2.position);
              proj2.damage *= 2;
              proj2.velocity = proj2.velocity.RotateTo(ClosestEnemyNPC(proj2).AngleFrom(proj2.position));
            }
            else {
              proj2.active = false;
            }
          }
        }
      }
    }
    // This will allow them to bounce off of surfaces a set amount of times
    // This also would reduce the amount of NPCs it can hit, but that's neither here nor there

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return false;
    }
  }
}