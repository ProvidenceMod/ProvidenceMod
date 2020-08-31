using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnbiddenMod.Code.Projectiles
{
  public class StarBlast : ModProjectile
  {
    //public int projectileAI = 1;
    //public int projectileCycle = 1;
    //public int projectileAICount = 1;
    //public int r = 0;
    //public int g = 0;
    //public int b = 0;

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
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      aiType = 0;
      projectile.timeLeft = 300;
      projectile.penetrate = 3;
      projectile.scale = 1f;

    }

    public override void AI()
    {
      Player player = Main.player[projectile.owner];
      Lighting.AddLight(projectile.Center,(float) Main.DiscoR / 400f, (float) Main.DiscoG / 400f, (float) Main.DiscoB / 400f);
      projectile.ai[0] += 1f;
      Color color = new Color (Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
      Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("StarBlastDust"), 0, 0, 0, new Color (Main.DiscoR, Main.DiscoG, Main.DiscoB, 0), 0.7f);
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += projectile.velocity.X * 0.1f;
      //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("StarBlast"), projectile.damage, projectile.knockBack, projectile.owner);
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
    public override void PostAI()
    {
      Player player = Main.player[projectile.owner];
      for (int i = projectile.oldPos.Length - 1; i > 0; i--)
      {
        projectile.oldPos[i] = projectile.oldPos[i - 1];
      }
      projectile.oldPos[0] = projectile.position;
    }
    public static void PostDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            Color color = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            SpriteEffects effects = SpriteEffects.None;
            int a = 0;
            int b = 0;
            float num100 = (float)(Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
            for(int i = 0; i < 10; i++)
            {
                Color alpha = projectile.GetAlpha(color);
                float num127 = (float)(9 - i) / 9f;
                alpha.R = (byte)((float)alpha.R * num127);
                alpha.G = (byte)((float)alpha.G * num127);
                alpha.B = (byte)((float)alpha.B * num127);
                alpha.A = (byte)((float)alpha.A * num127);
                float num128 = (float)(9 - i) / 9f;
                Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.oldPos[i].X - Main.screenPosition.X + num100 + (float)b, projectile.oldPos[i].Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), alpha, projectile.rotation, new Vector2(num100, (float)(projectile.height / 2 + a)), num128 * projectile.scale, effects, 0f);
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

    /*public override bool OnTileCollide(Vector2 oldVelocity)
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
    }*/

    public override Color? GetAlpha(Color lightColor)
    {
      Color color = new Color (Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
      return(color);
    }

    public override void Kill(int timeLeft)
    {

    }
  }
}