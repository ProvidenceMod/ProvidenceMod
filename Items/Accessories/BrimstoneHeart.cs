using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Accessories
{
  public class BrimstoneHeart : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Brimstone Heart");
      Tooltip.SetDefault("\"A stone-like heart that pulses with lava.\"\n+15 resistance to fire damage\nImmune to lava, as well as \"On Fire!\" debuff\nReduces mana cost for fire magic by 15%");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.ObsidianSkull);
    }
    public override void UpdateEquip(Player player)
    {
      player.Providence().brimHeart = true;
      player.Providence().resists[0] += 15;
      player.Providence().affinities[0]++;
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