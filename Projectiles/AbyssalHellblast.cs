using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UnbiddenMod.NPCs.FireAncient;

namespace UnbiddenMod.Projectiles
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
    }

    public override void AI()
    {
      NPC npc = Main.npc[(int)projectile.ai[0]];
      projectile.ai[1]++;
      projectile.localAI[0]++;
      IList<int> targets = ((FireAncient)npc.modNPC).targets;
      int player2 = targets[0];
      Player player = Main.player[player2];
      if (++projectile.frameCounter >= 3) // Frame time
      {
        projectile.frameCounter = 0;
        if (++projectile.frame >= 9) //Frame number
        {
          projectile.frame = 0;
        }
      }
      Vector2 offset = Main.player[player2].position - projectile.position;
      const float speedCap = 8f;
      const float gainStrength = 0.2f;
      const float slowStrength = 1.1f;
      UnbiddenGlobalProjectile.IsHomingPlayer(projectile, offset, player, speedCap, gainStrength, slowStrength);
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