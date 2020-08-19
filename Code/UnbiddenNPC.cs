using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.NPC;
using static Terraria.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.Mod;
using static UnbiddenMod.UnbiddenItem;
using static UnbiddenMod.UnbiddenProjectile;

namespace UnbiddenMod
{
  public class UnbiddenNPC : GlobalNPC
  {
    public override bool InstancePerEntity => true;
    public override bool CloneNewInstances => true;
    public int[] resists = new int[7] { 100, 100, 100, 100, 100, 100, 100 }; // Fire, Ice, Lightning, Poison, Acid, Holy, Unholy
    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
    {
      int weapEl = item.GetGlobalItem<UnbiddenItem>().element; // Determine the element (will always be between 0-6 for array purposes)
      if (weapEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = (float)(npc.GetGlobalNPC<UnbiddenNPC>().resists[weapEl]) / 100f;

        if (resistMod != 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
      }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      if (projectile.GetGlobalProjectile<UnbiddenProjectile>().element != -1) // if not typeless (and implicitly within 0-6)
      {
        int projEl = projectile.GetGlobalProjectile<UnbiddenProjectile>().element; // Determine the element (will always be between 0-6 for array purposes)
        float damageFloat = (float)damage; // And the damage we already have, converted to float
        damageFloat *= (float)(npc.GetGlobalNPC<UnbiddenNPC>().resists[projEl]) / 100f; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
        damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
      }
    }

  }
}