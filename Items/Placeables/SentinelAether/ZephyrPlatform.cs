using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Placeables.SentinelAether
{
	public class ZephyrPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Platform");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 14;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Tiles.SentinelAether.ZephyrPlatform>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<ZephyrBricks>());
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
}
