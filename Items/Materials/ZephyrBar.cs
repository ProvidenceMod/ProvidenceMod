using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Placeables.Ores;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Materials
{
	public class ZephyrBar : ModItem
	{
		public override void SetStaticDefaults()
		{
      DisplayName.SetDefault("Zephyr Bar");
      Tooltip.SetDefault("Even condensed, it's still as light as air.");
		}

		public override void SetDefaults()
		{
      item.CloneDefaults(ItemID.IronBar);
      item.width = 30;
      item.height = 24;
		}

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemType<ZephyrOre>(), 4);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
	}
}
