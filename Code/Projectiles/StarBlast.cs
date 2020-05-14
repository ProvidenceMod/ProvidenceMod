using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Code.Dusts;

namespace UnbiddenMod.Code.Projectiles
{
  public class StarBlast : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Star Blast");
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 10;
      projectile.height = 10;
      projectile.aiStyle = 1;
      projectile.friendly = true;
      projectile.ranged = true;
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      aiType = 0;

    }

    public override void AI()
    {
      Player player = Main.player[projectile.owner];
      Lighting.AddLight(projectile.Center, 0.25f, 0f, 0.75f);
      projectile.rotation += 0.4f * (float)projectile.direction;
      projectile.ai[0] += 1f;
      Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("StarBlastDust"));
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 8;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      for (int i = 0 ; i < 200 ; i++)
      {
        NPC target = Main.npc[i];
        //This will allow the projectile to only target hostile NPC's by referencing the variable, "target", above
        if(target.active && !target.dontTakeDamage && !target.friendly)
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
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
    {
      Player player = Main.player[projectile.owner];
      int healingAmount = damage/30; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
      player.statLife +=healingAmount;
      player.HealEffect(healingAmount, true);
    }
  }
}