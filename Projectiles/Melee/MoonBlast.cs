using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Dusts;

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
      Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType<ParryShieldDust>(), 0, 0, 0, color, 0.7f);
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += projectile.velocity.X * 0.05f;
      //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MoonBlast"), projectile.damage, projectile.knockBack, projectile.owner);
      projectile.Homing(20f, 1.0f, 1.1f, 300f, true, 100f, true, 5f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D tex = mod.GetTexture("Projectiles/Melee/MoonBlast");
      const int counter = 5;
      UnbiddenGlobalProjectile.AfterImage(projectile, lightColor, tex, counter);

      return false;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      Player player = projectile.ProjectileOwnerPlayer();
      // Caps potential healing at 1% of max health per hit.
      int healingAmount = damage / 60 >= player.statLifeMax / 100 ? player.statLifeMax / 100 : damage / 60;
      // Actually heals, and gives the little green numbers pop up
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

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
    }

    public override void Kill(int timeLeft)
    {
    }
  }
}