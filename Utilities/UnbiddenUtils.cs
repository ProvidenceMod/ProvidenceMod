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
using UnbiddenMod.Buffs;

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


    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
      if (!condition)
        return;
      list.Add(type);
    }
  }
}