using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.Brinewastes
{
	public class CyanCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cyan Coral Block");
			Tooltip.SetDefault("Zooxanthellae are a type of algee that live symbiotically with coral. they help provide color with their chlorophyll!");
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
			Item.createTile = ModContent.TileType<Tiles.Brinewastes.CyanCoralBlock>();
			Item.width = 16;
			Item.height = 16;
			Item.value = 30;
			Item.rare = ItemRarityID.Orange;
		}
	}
}