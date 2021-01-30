using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Items.Placeable;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Materials
{
	public class ZephyriumBar : ModItem
	{
    // Delete this later, when we have a texture
		public override void SetStaticDefaults()
		{
      DisplayName.SetDefault("Zephyrium Bar");
      Tooltip.SetDefault("\"Even condensed, it's still as light as air.\"");
		}

		public override void SetDefaults()
		{
      item.CloneDefaults(ItemID.IronBar);
		}

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemType<ZephyriumOre>(), 4);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
	}
}
