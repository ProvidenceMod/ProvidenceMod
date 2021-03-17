using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items
{
  public abstract class ShadowItem : ModItem
  {
    public override void SetDefaults()
    {
      item.melee = false;
      item.ranged = false;
      item.magic = true;
      item.summon = false;
      item.thrown = false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
      var tt = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
      if (tt != null)
      {
        tooltips.Insert(
          tooltips.FindIndex(x => x.Name == "Damage" && x.mod == "Terraria"),
          new TooltipLine(mod, "Shadowmancy", "Shadowmancer's item")
          );
      }
    }
    public virtual void ModifyWeaponDamage(Player player, ref int damage)
    {
      ProvidencePlayer modPlayer = player.Providence();
      int originalDmg = damage;
      damage = (int)(damage * modPlayer.shadowDamage);
      float globalDmg = player.meleeDamage - 1;
      if (player.magicDamage - 1 < globalDmg) { globalDmg = player.magicDamage - 1; }
      if (player.rangedDamage - 1 < globalDmg) { globalDmg = player.rangedDamage - 1; }
      if (player.minionDamage - 1 < globalDmg) { globalDmg = player.minionDamage - 1; }
      if (player.thrownDamage - 1 < globalDmg) { globalDmg = player.thrownDamage - 1; }
      if (globalDmg > 1) { damage += (int)(originalDmg * globalDmg); }
    }
  }
}