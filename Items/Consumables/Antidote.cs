using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items.Materials;

namespace UnbiddenMod.Items.Consumables
{
    public class Antidote : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antidote");
            Tooltip.SetDefault("Cures \"Posioned\" and \"Venom\" debuffs\nAdds 5 seconds of potion sickness\n15 HP restored");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LesserHealingPotion);
            item.maxStack = 30;
            item.potion = false;
            item.healLife = 15;

        }

        public override bool CanUseItem(Player player) {
          return player.FindBuffIndex(20) != -1 || player.FindBuffIndex(70) != -1; // Poison or Venom
        }

        public override bool UseItem(Player player)
        {
          int poison = player.FindBuffIndex(20), venom = player.FindBuffIndex(70);
          Main.debuff[20] = false;
          Main.debuff[70] = false;
          if (poison != -1) {player.DelBuff(poison);}
          if (venom != -1) {player.DelBuff(venom);}
          player.AddBuff(21, 300, true); // Potion Sickness
          Main.debuff[20] = true;
          Main.debuff[70] = true;
          return true;
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