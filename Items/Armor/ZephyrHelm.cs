using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items;
namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Head)]
  public class ZephyrHelm : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyr Helm");
      Tooltip.SetDefault("");
    }

    public override void SetDefaults()
    {
      item.width = 24;
      item.height = 20;
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