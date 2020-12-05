using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using UnbiddenMod;
using UnbiddenMod.Buffs;
using UnbiddenMod.Dusts;

namespace UnbiddenMod
{
  public static class UnbiddenUtils
  {
    public static UnbiddenPlayer Unbidden(this Player player) => (UnbiddenPlayer)player.GetModPlayer<UnbiddenPlayer>();
    public static UnbiddenGlobalNPC Unbidden(this NPC npc) => (UnbiddenGlobalNPC)npc.GetGlobalNPC<UnbiddenGlobalNPC>();
    public static UnbiddenGlobalItem Unbidden(this Item item) => (UnbiddenGlobalItem)item.GetGlobalItem<UnbiddenGlobalItem>();
    public static UnbiddenGlobalProjectile Unbidden(this Projectile proj) => (UnbiddenGlobalProjectile)proj.GetGlobalProjectile<UnbiddenGlobalProjectile>();

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
      }
      return damage;
    }
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
      }
      return damage;
    }

    public static int CalcEleDamageFromNPC(this Player player, NPC npc, ref int damage)
    {
      int npcEl = npc.Unbidden().contactDamageEl;
      if (npcEl != -1)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
        resistMod = player.Unbidden().resists[npcEl];
        if (resistMod > 0f) // If you don't have immunity (meaning your resist number is 0 or lower)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else // If not, you're damage is set to 1
        {
          damage = 1;
        }
      }
      return damage;
    }
    public static int CalcEleDamageFromProj(this Player player, Projectile proj, ref int damage)
    {
      int projEl = proj.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
        resistMod = player.Unbidden().resists[projEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
      }
      return damage;
    }

    public static void Parry(Player player, Rectangle hitbox)
    {
      int NoOfProj = Main.projectile.Length;
      int affectedProjs = 0;
      for (int i = 0; i < NoOfProj; i++)
      {
        Projectile currProj = Main.projectile[i];
        if (!player.HasBuff(ModContent.BuffType<CantDeflect>()) && currProj.active && currProj.hostile && hitbox.Intersects(currProj.Hitbox))
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
      if (affectedProjs > 0)
      {
        // Give a cooldown; 1 second per projectile reflected
        // CantDeflect is a debuff, separate from this code block
        player.AddBuff(ModContent.BuffType<CantDeflect>(), affectedProjs * 60, true);
      }
    }
    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
      if (!condition)
        return;
      list.Add(type);
    }
  }
}