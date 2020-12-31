using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Accessories
{
  public class ClarityHeadband : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.Blindfold;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Clarity Headband");
      Tooltip.SetDefault("\"A headband to help clear the mind, and focus\"\nBoosts focus gain");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Blindfold);
    }
    public override void UpdateEquip(Player player)
    {
      player.Unbidden().bonusFocusGain += 0.005f;
    }
    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.Ichor, 25);
      recipe.AddIngredient(ItemID.LifeCrystal, 5);
      recipe.AddTile(TileID.LunarCraftingStation); // Ancient Manipulator
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }
  }
}