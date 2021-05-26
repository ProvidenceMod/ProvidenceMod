using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Projectiles.Melee
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
      projectile.Providence().homingID = (int)HomingID.Natural;
    }
    public override void AI()
    {
      Lighting.AddLight(projectile.Center, (float)Main.DiscoR / 400f, (float)Main.DiscoG / 400f, (float)Main.DiscoB / 400f);
      projectile.ai[0]++;
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += projectile.velocity.X * 0.05f;
			NPC target = (NPC)ClosestEntity(projectile, true);
      projectile.Homing(target, 20f, default, default, 15f, 300f, default, default, default, default);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D tex = GetTexture("ProvidenceMod/Projectiles/Melee/MoonBlast");
      const int counter = 5;
      ProvidenceGlobalProjectile.AfterImage(projectile, lightColor, tex, counter);

      return false;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      Player player = projectile.OwnerPlayer();
      // Caps potential healing at 1% of max health per hit.
      int healingAmount = damage / 60 >= player.statLifeMax / 100 ? player.statLifeMax / 100 : damage / 60;
      // Actually heals, and gives the little green numbers pop up
      player.statLife += healingAmount;
      player.HealEffect(healingAmount, true);

      projectile.penetrate--;
      target.immune[projectile.owner] = 3;

      // int trueDmg = crit ? damage * 2 : damage;
      // if (target.life - trueDmg <= 0)
      // {
      //   NPC newTarget = ClosestEntity(projectile);
      //   if (newTarget?.active == true)
      //     projectile.velocity = projectile.velocity.RotateTo(projectile.AngleTo(newTarget.position));
      // }
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