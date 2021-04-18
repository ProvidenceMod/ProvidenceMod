using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Armor;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Legs)]
  public class ZephyrLeggings : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyr Leggings");
      Tooltip.SetDefault("");
    }

    public override void SetDefaults()
    {
      item.width = 22;
      item.height = 16;
      item.defense = 1;
    }

    public override void UpdateEquip(Player player)
    {
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.DirtBlock, 1);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}