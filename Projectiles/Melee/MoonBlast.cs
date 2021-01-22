using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles.Melee
{
  public class MoonBlast : ModProjectile
  {
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
      projectile.Unbidden().homingID = HomingID.Smart;
      projectile.Unbidden().entityType = EntityType.NPC;
    }

    public override void AI()
    {
      Lighting.AddLight(projectile.Center, (float)Main.DiscoR / 400f, (float)Main.DiscoG / 400f, (float)Main.DiscoB / 400f);
      projectile.ai[0]++;
      Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
      Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("ParryShieldDust"), 0, 0, 0, color, 0.7f);
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += projectile.velocity.X * 0.05f;
      //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MoonBlast"), projectile.damage, projectile.knockBack, projectile.owner);
      projectile.Homing(20f, 1.0f, 1.1f, 300f, false, 5f, true, 5f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D tex = mod.GetTexture("Projectiles/Melee/MoonBlast");
      const int counter = 5;
      UnbiddenGlobalProjectile.AfterImage(projectile, lightColor, tex, counter);

      return false;
    }

    public virtual void PostDraw()
    {
      projectile.rotation += projectile.velocity.X * 0.05f;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      Player player = Main.player[projectile.owner];
      int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
      player.statLife += healingAmount;
      player.HealEffect(healingAmount, true);
      projectile.penetrate--;
      target.immune[projectile.owner] = 3;

      int trueDmg = crit ? damage * 2 : damage;
      if (target.life - trueDmg <= 0)
      {
        NPC newTarget = ClosestEnemyNPC(projectile);
        if (newTarget?.active == true)
          projectile.velocity = projectile.velocity.RotateTo(projectile.AngleTo(newTarget.position));
      }
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
      return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
    }

    public override void Kill(int timeLeft)
    {
    }
  }
}