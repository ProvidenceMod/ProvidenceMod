using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;
using UnbiddenMod.Dusts;

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
      projectile.timeLeft = 600;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.hostile = false;
      projectile.friendly = true;
      projectile.Unbidden().element = -1; // Typeless
      projectile.Unbidden().deflectable = false;
      projectile.Unbidden().homingID = HomingID.Natural;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      Texture2D tex = ModContent.GetTexture("UnbiddenMod/Projectiles/Healing/HealProjectile");
      Player player = ClosestPlayer(projectile);
      Dust.NewDustPerfect(projectile.position, ModContent.DustType<ParryShieldDust>(), null, default, new Color(219, 240, 45), 1f);
      UnbiddenGlobalProjectile.AfterImage(projectile, Color.White, tex, 10);
      projectile.Homing(player, 30f, default, default, 20);
      if (projectile.getRect().Intersects(player.getRect()))
      {
        player.statLife += 25;
        player.HealEffect(25);
      }
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return Color.White;
    }
  }
}