using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items.Materials;

namespace UnbiddenMod.Items.Consumables
{
    public class AngelTear : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angel's Tear");
            Tooltip.SetDefault("\"The physical manifest of an angel's suffering.\"\n+20 max Health, to a limit of 1000\nMust have consumed all life fruits first");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
            item.maxStack = 99;
        }

        public override bool CanUseItem(Player player) {
          return player.statLifeMax2 >= 500 && player.GetModPlayer<UnbiddenPlayer>().tearCount < 25; // If you have all life fruits AND haven't gained up to 500 extra HP total from it
        }

        public override bool UseItem(Player player)
        {
          if (player.statLifeMax2 >= 500 && player.statLifeMax2 < 1000) {
            player.statLifeMax2 += 20;
            player.statLife += 20;
            player.HealEffect(20, true);
            player.GetModPlayer<UnbiddenPlayer>().angelTear = true;
            player.GetModPlayer<UnbiddenPlayer>().tearCount++;
            return true;
          } else {
            return false;
          }
        }
        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient(ItemType<LuminousFragment>(), 5);
            recipe.AddTile(412); // Ancient Manipulator
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}