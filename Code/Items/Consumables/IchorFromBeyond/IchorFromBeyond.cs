using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod.Code.Items.Consumables.IchorFromBeyond
{
    public class IchorFromBeyond : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor From Beyond");
            Tooltip.SetDefault("\"It glows illustriously, and smells like ozone.\"\n+20 max Health, to a limit of 1000\nMust have consumed all life fruits first");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
        }

        public override bool CanUseItem(Player player) {
          return !player.ichor && player.statLifeMax >= 500;
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.DirtBlock, 10); //Adds ingredients
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}