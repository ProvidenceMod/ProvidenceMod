using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;

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
      projectile.timeLeft = 6000;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.hostile = false;
      projectile.friendly = true;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = -1; // Typeless
      projectile.Unbidden().deflectable = false;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      Texture2D tex = ModContent.GetTexture("UnbiddenMod/Projectiles/Healing/HealProjectile");
      Player player = Main.LocalPlayer;
      Vector2 offset = player.position - projectile.position;
      projectile.Homing(8f, 0.2f, 200f, true, 5f, true);
      UnbiddenGlobalProjectile.AfterImage(projectile, Color.White, tex, 10);
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