using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UnbiddenMod.NPCs.FireAncient;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles.Boss
{
  public class AbyssalHellblast : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Abyssal Hellblast");
      Main.projFrames[projectile.type] = 9;
    }

    public override void SetDefaults()
    {
      projectile.width = 45;
      projectile.height = 22;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      projectile.timeLeft = 180;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.damage = 100;
      projectile.hostile = true;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Fire
      projectile.tileCollide = false;
      projectile.Unbidden().homingID = HomingID.Natural;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      projectile.rotation = projectile.velocity.ToRotation();
      if (++projectile.frameCounter >= 3) // Frame time
      {
        projectile.frameCounter = 0;
        if (++projectile.frame >= 9) //Frame number
        {
          projectile.frame = 0;
        }
      }
      const float speedCap = 8f;
      const float radius = 500f;
      Player player = ClosestPlayer(projectile);
      projectile.Homing(player, 8f, default, default, 20);
    }
    public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
    {
      target.AddBuff(BuffID.OnFire, 10);
      target.CalcEleDamageFromProj(projectile, ref damage);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.White;
      return color;
    }
  }
}