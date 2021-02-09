using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Materials;

namespace ProvidenceMod.Items.Weapons.Cleric
{
  public class BronzeShortsword : ClericItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bronze Shortsword");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.CopperShortsword);
      base.SetDefaults();
      item.damage = 7;
      item.autoReuse = true;
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemType<BronzeBar>(), 6);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}