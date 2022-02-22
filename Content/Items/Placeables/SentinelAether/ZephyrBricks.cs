using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.SentinelAether
{
	public class ZephyrBricks : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Bricks");
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.SentinelAether.ZephyrBricks>();
			Item.width = 16;
			Item.height = 16;
			Item.value = 3000;
			Item.material = true;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
