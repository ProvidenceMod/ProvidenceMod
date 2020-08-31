using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Code.Dusts;

namespace UnbiddenMod.Code.Projectiles
{
  public class StarBlast : ModProjectile
  {
    public int projectileAI = 1;
    public int projectileCycle = 1;
    public int projectileAICount = 1;
    public int r = 0;
    public int g = 0;
    public int b = 0;

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Star Blast");
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 30;
      projectile.height = 30;
      projectile.aiStyle = 0;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      aiType = 0;
      projectile.timeLeft = 300;
      projectile.penetrate = 3;
      projectile.scale = 1f;

    }

    public override void AI()
    {
      Player player = Main.player[projectile.owner];
      double r2 = ((100 / 255) * r) * 0.01;
      double g2 = ((100 / 255) * g) * 0.01;
      double b2 = ((100 / 255) * b) * 0.01;
      Lighting.AddLight(projectile.Center,(float) r2, (float) g2, (float) b2);
      projectile.ai[0] += 1f;
      Color color = new Color (r, g, b, 0);
      Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("StarBlastDust"), 0, 0, 0, color, 0.7f);
      if((projectileAI % 4) / 1 == 0)
      {
        projectileAI = 0;
        projectileAICount += 1;
        if(projectileAICount > 15)
        {
          projectileAICount = 1;
          projectileCycle += 1;
        }
      }
      projectileAI += 1;
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += projectile.velocity.X * 0.1f;
      /*if ((float) ((Vector2) ((Entity) projectile).velocity).X > 0.0)
      {
        projectile.rotation += 0.8f;
      }
      else
      {
        projectile.rotation -= 0.8f;
      }*/
      for (int i = 0; i < 200; i++)
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
          if (distance < 80f && !target.friendly && target.active)
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
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      Player player = Main.player[projectile.owner];
      int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
      player.statLife += healingAmount;
      player.HealEffect(healingAmount, true);
      projectile.penetrate--;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
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

    public override Color? GetAlpha(Color lightColor)
    {
      if(projectileCycle == 1) //Add to green
      {
        r = 255;
        if(projectileAI == 1)
        {
          g += 4;
        }
        else if(projectileAI == 2)
        {
          g += 4;
        }
        else if(projectileAI == 3)
        {
          g += 4;
        }
        else if(projectileAI == 4)
        {
          g += 5;
        }
      }
      else if(projectileCycle == 2) //Subtract from red
      {
        if(projectileAI == 1)
        {
          r -= 4;
        }
        else if(projectileAI == 2)
        {
          r -= 4;
        }
        else if(projectileAI == 3)
        {
          r -= 4;
        }
        else if(projectileAI == 4)
        {
          r -= 5;
        }
      }
      else if(projectileCycle == 3) //Add to blue
      {
        if(projectileAI == 1)
        {
          b += 4;
        }
        else if(projectileAI == 2)
        {
          b += 4;
        }
        else if(projectileAI == 3)
        {
          b += 4;
        }
        else if(projectileAI == 4)
        {
          b += 5;
        }
      }
      else if(projectileCycle == 4) //Subtract from green
      {
        if(projectileAI == 1)
        {
          g -= 4;
        }
        else if(projectileAI == 2)
        {
          g -= 4;
        }
        else if(projectileAI == 3)
        {
          g -= 4;
        }
        else if(projectileAI == 4)
        {
          g -= 5;
        }
      }
      else if(projectileCycle == 5) //Add to red
      {
        if(projectileAI == 1)
        {
          r += 4;
        }
        else if(projectileAI == 2)
        {
          r += 4;
        }
        else if(projectileAI == 3)
        {
          r += 4;
        }
        else if(projectileAI == 4)
        {
          r += 5;
        }
      }
      else if(projectileCycle == 6) //Subtract from blue
      {
        if(projectileAI == 1)
        {
          b -= 4;
        }
        else if(projectileAI == 2)
        {
          b -= 4;
        }
        else if(projectileAI == 3)
        {
          b -= 4;
        }
        else if(projectileAI == 4)
        {
          b -= 5;
        }
        projectileCycle = 1;
      }
      Color color = new Color (r, g, b, 0);
      return(color);
    }

    public override void Kill(int timeLeft)
    {

    }
  }
}