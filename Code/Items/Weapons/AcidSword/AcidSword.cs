using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod.Code.Items.Weapons.AcidSword
{
    public class AcidSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Sword");
            Tooltip.SetDefault("\"A sword that flings acid\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumBroadsword);
            item.GetGlobalItem<UnbiddenItem>().element = 4; // Acid
            item.shoot = mod.ProjectileType("AcidBlob");
            item.shootSpeed = 8f;
        }


        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe.AddIngredient(ItemID.Deathweed, 15);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod

            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient(ItemID.Deathweed, 15);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}