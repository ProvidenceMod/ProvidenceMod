using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Dusts;

namespace UnbiddenMod.Projectiles
{
  public class YeetPotion : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Potion");
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 7;
      projectile.height = 15;
      projectile.aiStyle = 1;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      aiType = 0;
      projectile.timeLeft = 300;
      projectile.penetrate = 1;
      projectile.scale = 1f;
    }

    public override void AI()
    {
      // ID'ing player for future reference
      Player player = Main.player[projectile.owner];
      // Gravity
      projectile.ai[0] += 2f;
      // Rotation
      if (projectile.velocity.X > 0.0)
      {
        projectile.rotation -= 1.57f * (float)projectile.direction;
      }
      else
      {
        projectile.rotation += 1.57f * (float)projectile.direction;
      }
    }

    public override bool OnTileCollide(Vector2 oldVel)
    {
      projectile.Kill();
      return true;
    }
    public override void Kill(int timeLeft)
    {
      int explosionRadius = 5 * 6; // # of Tiles in one direction
      float leftEdgeX = projectile.position.X - explosionRadius,
            rightEdgeX = projectile.position.X + explosionRadius,
            upperEdgeY = projectile.position.Y - explosionRadius,
            lowerEdgeY = projectile.position.Y + explosionRadius;
      
      Player owner = Main.player[projectile.owner];
      Item launcher = owner.inventory[owner.selectedItem];

      for (int i = 0; i < Main.player.Length; i++)
      {
        Player player = Main.player[i];
        // If the player is active and within the bounds of the explosion radius
        if (player.active && (player.position.X >= leftEdgeX && player.position.X <= rightEdgeX) && (player.position.Y <= lowerEdgeY && player.position.Y >= upperEdgeY))
        {
          player.statLife += launcher.damage;
          player.HealEffect(launcher.damage, true);
          if (player.FindBuffIndex(21) != -1) // Potion Sick for 15 seconds. Already active debuff gets extended
          {
            player.buffTime[player.FindBuffIndex(21)] += 900;
          }
          else
          {
            player.AddBuff(21, 900, true);
          }
        }
      }

      for (int i = 0; i < Main.npc.Length; i++)
      {
        NPC npc = Main.npc[i];
        if (npc.active && (npc.position.X >= leftEdgeX && npc.position.X <= rightEdgeX) && (npc.position.Y <= lowerEdgeY && npc.position.Y >= upperEdgeY))
        {
          npc.StrikeNPC(launcher.damage, 1f, owner.direction, false);
        }
      }
    }
  }
}