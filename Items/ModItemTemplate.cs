using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items
{
  public abstract class ProvidenceItemNameHere : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("");
      Tooltip.SetDefault("");
    }

    public override void SetDefaults()
    {
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.DirtBlock, 1);
      recipe.AddTile(TileID.Anvils);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}