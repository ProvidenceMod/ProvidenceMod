using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace UnbiddenMod.Projectiles
{
  public class VampireKnifeClone : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vampire Knife");
    }

    public override void SetDefaults()
    {
      projectile.aiStyle = 0;
      projectile.hide = false;
      projectile.arrow = true;
      projectile.width = 16;
      projectile.height = 16;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      projectile.scale = 1f;
    }


    // Making the AI so it'll home in on enemies
    public override void AI()
    {
      Player player = Main.player[projectile.owner];
      Lighting.AddLight(projectile.Center, 0.5f, 0.25f, 0f);
      projectile.rotation += 0.4f * (float)projectile.direction;
      projectile.ai[0] += 1f;

      for (int i = 0; i < 20; i++)
      {
        NPC target = Main.npc[i];
        //This will allow the projectile to only target hostile NPC's by referencing the variable, "target", above
        if (target.active && !target.dontTakeDamage && !target.friendly)
        {
          //Finding the horizontal position of the target and adjusting trajectory accordingly
          float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
          //Finding the vertical position of the target and adjusting trajectory accordingly
          float shootToY = target.position.Y - projectile.Center.Y;
          //  √ shootToX² + shootToY², using the Pythagorean Theorem to calculate the distance from the target
          float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

          //f, in this scenario, is a measurement of Pixel Distance
          if (distance < 400f && !target.friendly && target.active)
          {
            distance = 3f / distance;
            shootToY *= distance * 5;
            shootToX *= distance * 5;

            projectile.velocity.Y = shootToY;
            projectile.velocity.X = shootToX;
          }
        }
      }
    }
    // This will allow them to bounce off of surfaces a set amount of times
    // This also would reduce the amount of NPCs it can hit, but that's neither here nor there
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      //If collide with tile, reduce the penetrate.
      projectile.penetrate--;
      if (projectile.penetrate <= 0)
      {
        projectile.Kill();
      }
      else
      {
        Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
        Main.PlaySound(SoundID.Item10, projectile.position);
        if (projectile.velocity.X != oldVelocity.X)
        {
          projectile.velocity.X = -oldVelocity.X;
        }
        if (projectile.velocity.Y != oldVelocity.Y)
        {
          projectile.velocity.Y = -oldVelocity.Y;
        }
      }
      return false;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
      projectile.penetrate--;
      if (projectile.penetrate <= 0) {
        projectile.Kill();
      }
    }
  }
}