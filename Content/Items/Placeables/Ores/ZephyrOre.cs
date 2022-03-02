using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Items.Placeables.Ores
{
	public class ZephyrOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Ore");
			Tooltip.SetDefault("Roughly hewn, lightweight air crystal");
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
			Item.createTile = ModContent.TileType<Tiles.Ores.ZephyrOre>();
			Item.width = 22;
			Item.height = 22;
			Item.value = 3000;
			Item.material = true;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
