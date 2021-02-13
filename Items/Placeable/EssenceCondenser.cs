using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeable
{
  public class EssenceCondenser : ModItem
  {
    public override void SetStaticDefaults()
    {
      ItemID.Sets.SortingPriorityMaterials[item.type] = 58;
    }

    public override void SetDefaults()
    {
      item.useStyle = ItemUseStyleID.SwingThrow;
      item.useTurn = true;
      item.useAnimation = 15;
      item.useTime = 10;
      item.autoReuse = true;
      item.maxStack = 999;
      item.consumable = true;
      item.createTile = ModContent.TileType<Tiles.EssenceCondenser>();
      item.width = 16;
      item.height = 16;
      item.value = 3000;
      item.rare = ItemRarityID.Orange;
    }
    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.IronBar, 5);
      recipe.AddIngredient(ItemID.GoldBar, 1);
      recipe.AddIngredient(ItemID.LeadBar, 5);
      recipe.AddTile(TileID.WorkBenches);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}
