using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.SentinelAether
{
	public class ZephyrBricks : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Bricks");
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
			item.createTile = ModContent.TileType<Tiles.SentinelAether.ZephyrBricks>();
			item.width = 16;
			item.height = 16;
			item.value = 3000;
			item.material = true;
			item.rare = ItemRarityID.Orange;
		}
	}
}
