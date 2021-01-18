using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Buffs.Cooldowns;
using UnbiddenMod.Buffs.StatBuffs;

namespace UnbiddenMod
{
  public static class UnbiddenUtils
  {
    // Summary:
    // References the UnbiddenPlayer instance. Shorthand for ease of use.
    // UnbiddenPlayer unbiddenPlayer = player.Unbidden();
    public static UnbiddenPlayer Unbidden(this Player player) => player.GetModPlayer<UnbiddenPlayer>();
    // 
    // Summary:
    // References the UnbiddenGlobalNPC instance. Shorthand for ease of use.
    // UnbiddenGlobalNPC unbiddenNPC = npc.Unbidden();
    public static UnbiddenGlobalNPC Unbidden(this NPC npc) => npc.GetGlobalNPC<UnbiddenGlobalNPC>();
    // 
    // Summary:
    // References the UnbiddenGlobalItem instance. Shorthand for ease of use.
    // UnbiddenGlobalItem unbiddenItem = item.Unbidden();
    public static UnbiddenGlobalItem Unbidden(this Item item) => item.GetGlobalItem<UnbiddenGlobalItem>();
    // 
    // Summary:
    // References the UnbiddenGlobalProjectile instance. Shorthand for ease of use.
    // UnbiddenGlobalProjectile unbiddenProjectile = projectile.Unbidden();
    public static UnbiddenGlobalProjectile Unbidden(this Projectile proj) => proj.GetGlobalProjectile<UnbiddenGlobalProjectile>();
    public static float InRadians(this float degrees) => MathHelper.ToRadians(degrees);
    public static float InDegrees(this float radians) => MathHelper.ToDegrees(radians);
    public static float[,] elemAffDef = new float[2, 15]
    {  // Defense score (middle), Damage mult (bottom)
      {     1,      2,      3,      5,      7,      9,     12,     15,     18,     22,     26,     30,     35,     45,     50},
      {1.010f, 1.022f, 1.037f, 1.056f, 1.080f, 1.110f, 5.000f, 1.192f, 1.246f, 1.310f, 1.397f, 1.497f, 1.611f, 1.740f, 1.885f}
    };
    public static void CalcElemDefense(this Player player)
    {
      UnbiddenPlayer unPlayer = player.GetModPlayer<UnbiddenPlayer>();
      for (int k = 0; k < 8; k++)
      {
        int index = unPlayer.affinities[k] - 1;
        if (index != -1)
          unPlayer.resists[k] += (int)elemAffDef[0, index];
      }
    }
    public static float[] GetAffinityBonuses(this Player player, int e)
    {
      return new float[2] { elemAffDef[0, player.Unbidden().affinities[e]], elemAffDef[1, player.Unbidden().affinities[e]] };
    }
    // 
    // Summary:
    // Calculates elemental item damage based on UnbiddenGlobalNPC resists.
    // UnbiddenUtils.CalcEleDamage(Item item, NPC npc, ref int damage);
    public static int CalcEleDamage(this Item item, NPC npc, ref int damage)
    {
      int weapEl = item.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (weapEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.Unbidden().resists[weapEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
        // UnbiddenPlayer modPlayer = Main.player[item.owner].Unbidden();
        // if (modPlayer.affExpCooldown <= 0)
        // {
        //   modPlayer.affExp[weapEl] += 1;
        //   modPlayer.affExpCooldown = 120;
        // }
      }
      return damage;
    }
    // 
    // Summary:
    // Calculates elemental projectile damage based on UnbiddenGlobalNPC resists.
    // UnbiddenUtils.CalcEleDamage(Projectile projectile, NPC npc, ref int damage);
    public static int CalcEleDamage(this Projectile projectile, NPC npc, ref int damage)
    {
      int projEl = projectile.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.Unbidden().resists[projEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
        // UnbiddenPlayer modPlayer = Main.player[projectile.owner].Unbidden();
        // if (modPlayer.affExpCooldown <= 0)
        // {
        //   modPlayer.affExp[projEl] += 1;
        //   modPlayer.affExpCooldown = 120;
        // }
      }
      return damage;
    }
    // 
    // Summary:
    // Calculates elemental damage based on UnbiddenPlayer resists.
    // UnbiddenUtils.CalcEleDamageFromNPC(Player player, NPC npc, ref int damage);
    public static int CalcEleDamageFromNPC(this Player player, NPC npc, ref int damage)
    {
      int npcEl = npc.Unbidden().contactDamageEl;
      if (npcEl != -1)
      {
        int resistMod = player.Unbidden().resists[npcEl];
        damage -= (int)(Main.expertMode ? resistMod * 0.75 : resistMod * 0.5);
      }
      return damage;
    }

    // Summary:
    // Calculates elemental projectile damage based on UnbiddenPlayer resists.
    // UnbiddenUtils.CalcEleDamageFromProj(Player player, Projectile proj, ref int damage);
    public static int CalcEleDamageFromProj(this Player player, Projectile proj, ref int damage)
    {
      int projEl = proj.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        int resistMod = player.Unbidden().resists[projEl];
        damage -= (int)(Main.expertMode ? resistMod * 0.75 : resistMod * 0.5); // set the damage to the int version of the new float, implicitly rounding down to the lower int
      }
      return damage;
    }

    public static bool IsParry(this Projectile currProj, Player player, Rectangle hitbox, ref int parryShield)
    {
      return currProj.active && currProj.whoAmI != parryShield && !player.HasBuff(ModContent.BuffType<CantDeflect>()) && currProj.Unbidden().deflectable && currProj.hostile && hitbox.Intersects(currProj.Hitbox);
    }
    // 
    // Summary:
    // Allows players to parry. Call this when executing a swing.
    public static void StandardParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          // Add your melee damage multiplier to the damage so it has a little more oomph
          currProj.damage = (int)(currProj.damage * player.meleeDamageMult);

          // If Micit Bangle is equipped, add that multiplier.
          currProj.damage = player.Unbidden().micitBangle ? (int)(currProj.damage * 2.5) : currProj.damage;
          // Convert the proj so you own it and reverse its trajectory
          currProj.owner = player.whoAmI;
          currProj.hostile = false;
          currProj.friendly = true;
          currProj.Unbidden().deflected = true;
          currProj.velocity.X = -currProj.velocity.X;
          currProj.velocity.Y = -currProj.velocity.Y;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs = affectedProjs;
    }
    public static void TankParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      int DRBoost = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          DRBoost = currProj.damage / 100;
          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs = affectedProjs;
      if (DRBoost != 0)
      {
        player.Unbidden().tankParryPWR = DRBoost;
        // 5 secs of time, -1 second for each hit tanked with this active
        player.AddBuff(BuffType<TankParryBoost>(), 600);
      }
    }

    public static void DPSParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          _ = Projectile.NewProjectile(
            currProj.position,
            Vector2.Negate(currProj.velocity),
            ProjectileID.ChlorophyteBullet,
            (int)(player.Unbidden().micitBangle ? currProj.damage * 2 * 2.5 : currProj.damage * 5),
            currProj.knockBack,
            player.whoAmI
            );

          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs = affectedProjs;
    }
    // 
    // Summary:
    // Generates dust particles based on Aura size. Call when adding an Aura buff.
    // UnbiddenUtils.GenerateAuraField(Player player, int dust, float radiusBoost);
    public static void GenerateAuraField(Player player, int dust, float radiusBoost)
    {
      UnbiddenPlayer mP = player.Unbidden();
      for (float rotation = 0f; rotation < 360f; rotation += 8f)
      {
        Vector2 spawnPosition = player.MountedCenter + new Vector2(0f, mP.clericAuraRadius + radiusBoost).RotatedBy(rotation.InRadians());
        Dust d = Dust.NewDustPerfect(spawnPosition, dust, null, 90, new Color(255, 255, 255), 1f);
        d.noLight = true;
        d.noGravity = true;
      }
    }
    public static NPC ClosestEnemyNPC(Projectile projectile)
    {
      float shortest = -1f;
      NPC chosenNPC = null;
      foreach (NPC npc in Main.npc)
      {
        if (npc.active && !npc.townNPC && !npc.friendly)
        {
          float dist = Vector2.Distance(projectile.position, npc.position);
          if (dist < shortest || shortest == -1f)
          {
            shortest = dist;
            chosenNPC = npc;
          }
        }
      }
      return chosenNPC;
    }
    public static bool IsInRadius(this Vector2 targetPos, Vector2 center, float radius) => Vector2.Distance(center, targetPos) <= radius;
    public static int GrabProjCount(int type)
    {
      int count = 0;
      foreach (Projectile proj in Main.projectile)
      {
        if (proj.type == type)
          count++;
      }
      return count;
    }
    public static Vector2 RotateTo(this Vector2 v, float rotation)
    {
      float oldVRotation = v.ToRotation();
      return v.RotatedBy(rotation - oldVRotation);
    }
    // 
    // Summary:
    // Add to a list if condition returns true.
    // UnbiddenUtils.AddWithCondition(List<T> list, T type, bool condition);
    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
      if (!condition)
        return;
      list.Add(type);
    }
    public static Color ColorShift(Color firstColor, Color secondColor, float seconds)
    {
      float amount = (float)((Math.Sin(Math.PI * Math.PI / seconds * Main.GlobalTime) + 1.0) * 0.5);
      return Color.Lerp(firstColor, secondColor, amount);
    }

    public static void SetElementalTraits(this Item item, int elID, int elDef = 0, int weakElID = -1, int weakElDef = 0)
    {
      item.Unbidden().element = elID;
      item.Unbidden().elementDef = elDef;
      if (weakElID != -1)
      {
        item.Unbidden().weakEl = weakElID;
        item.Unbidden().weakElDef = weakElDef;
      }
    }
    public static Tuple<bool, int> IsThereABoss()
    {
      bool bossExists = false;
      int bossID = -1;
      foreach (NPC npc in Main.npc)
      {
        if (npc.active && npc.boss)
          bossExists = true;
        bossID = npc.type;
      }
      return Tuple.Create(bossExists, bossID);
    }
    // Tuple in this order: Damage, DR, Regen, Speed
    public static Tuple<int, decimal, decimal, decimal> FocusBonuses(this Player player)
    {
      UnbiddenPlayer mP = player.Unbidden();
      int focusPercent = (int)(mP.focus * 100);
      int damageBoost = focusPercent / 5;
      decimal DR = (decimal)((mP.focus / 4 >= 0.25f ? 0.25f : mP.focus / 4) * 100);
      decimal regen = focusPercent;
      decimal moveSpeedBoost = focusPercent / 2;
      return new Tuple<int, decimal, decimal, decimal>(damageBoost, DR, regen, moveSpeedBoost);
    }
  }

  public static class ParryTypeID
  {
    public const int Universal = 0;
    public const int Tank = 1;
    public const int DPS = 2;
    public const int Support = 3;

  }
  public static class ElementID
  {
    public const int Fire = 0;
    public const int Ice = 1;
    public const int Lightning = 2;
    public const int Water = 3;
    public const int Earth = 4;
    public const int Air = 5;
    public const int Radiant = 6;
    public const int Necrotic = 7;
  }
}