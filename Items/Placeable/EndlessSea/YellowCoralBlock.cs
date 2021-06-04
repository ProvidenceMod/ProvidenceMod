using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeable.EndlessSea
{
	public class YellowCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yellow Coral Block");
			Tooltip.SetDefault("Yellow (Gold) Coral like this are said to be among the most rare types.");
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
			item.createTile = ModContent.TileType<Tiles.EndlessSea.YellowCoralBlock>();
			item.width = 16;
			item.height = 16;
			item.value = 30;
			item.rare = ItemRarityID.Orange;
		}
	}
}