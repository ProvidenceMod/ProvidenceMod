using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Placeables.SentinelAether
{
	public class ZephyrPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Platform");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 14;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.SentinelAether.ZephyrPlatform>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemType<ZephyrBricks>())
				.Register();
		}
	}
}
