using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.Brinewastes
{
	public class YellowCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yellow Coral Block");
			Tooltip.SetDefault("Yellow (Gold) Coral like this are said to be among the most rare types.");
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
			Item.createTile = ModContent.TileType<Tiles.Brinewastes.YellowCoralBlock>();
			Item.width = 16;
			Item.height = 16;
			Item.value = 30;
			Item.rare = ItemRarityID.Orange;
		}
	}
}