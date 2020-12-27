using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using Microsoft.Xna.Framework;

namespace UnbiddenMod.Items.Weapons.Cleric
{
  public class Serrator : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Serrator"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
      Tooltip.SetDefault("Overkill damage is added to next hit");
    }
    // Defines a global variable overkill. You'll see why.
    public static int overkill = 0;
    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.WoodenSword);

      item.damage = 40;
      item.autoReuse = true;
      item.useTime = 15;
      item.useAnimation = 15;
      item.value = 1500;
      item.rare = ItemRarityID.Blue;
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
      // Pre-lim check to limit overkill as to not snap the game in half
      if (overkill > 100)
      {
        overkill = 100;
      }
      // If we have a value in overkill that is above 0, Make a second hit with overkill damage and
      // set overkill to 0
      if (overkill > 0)
      {
        damage += overkill;
        overkill = 0;
      }
      // Defining variables to shorthand coding below
      int totalCrit = (damage * 2) + overkill, total = damage + overkill;

      /* 
       * If this damage tick when registered would kill the NPC, set the overkill damage to the remaining
       * damage after it's been deducted by the remaining target's life.
       * Also works with crits! 
       */

      if (crit)
      {
        if (target.life - totalCrit <= 0)
        {
          overkill = totalCrit - target.life;
        }
      }
      else
      {
        if (target.life - total <= 0)
        {
          overkill = total - target.life;
        }
      }
    }
    public override void AddRecipes()
    {
      ModRecipe recipeUS = new ModRecipe(mod);
      recipeUS.AddIngredient(ItemID.Wood, 5);
      recipeUS.AddTile(TileID.WorkBenches);
      recipeUS.SetResult(this);
      recipeUS.AddRecipe();
    }
  }
}