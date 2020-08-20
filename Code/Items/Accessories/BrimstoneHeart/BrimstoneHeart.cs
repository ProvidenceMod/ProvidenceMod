using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;
using static UnbiddenMod.UnbiddenPlayer;
using static UnbiddenMod.UnbiddenItem;

namespace UnbiddenMod.Code.Items.Accessories.BrimstoneHeart
{
    public class BrimstoneHeart : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimstone Heart");
            Tooltip.SetDefault("\"A stone-like heart that pulses with lava.\"\nImmunity to fire damage and lava, as well as \"On Fire!\" debuff\nReduces mana cost for fire magic by 15%");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ObsidianSkull);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<UnbiddenPlayer>().brimHeart = true;
        }
        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Ichor, 25); 
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddTile(412); // Ancient Manipulator
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}