using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ProvidenceMod.Items.Tools
{
  public class CosmicTerraformer : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cosmic Terraformer");
      Tooltip.SetDefault("A pickaxe forged from the pure essence of the Cosmos");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.NebulaPickaxe);
      item.width = 20;
      item.height = 20;
      item.value = Item.buyPrice(0, 10, 0, 0);
      item.useTime = 4;
      item.useAnimation = 10;
      item.scale = 1.0f;
      item.melee = true;
      item.autoReuse = true;
      item.useTurn = true;
      item.pick = 255;
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.DirtBlock, 10); //Adds ingredients
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }
  }
}