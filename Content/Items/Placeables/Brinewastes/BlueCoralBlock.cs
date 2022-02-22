using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.Brinewastes
{
	public class BlueCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Coral Block");
			Tooltip.SetDefault("Micro-Organisms slowly excrete limestone to create colorful structures ontop of the remnants of other dead formations");
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
			Item.createTile = ModContent.TileType<Tiles.Brinewastes.BlueCoralBlock>();
			Item.width = 16;
			Item.height = 16;
			Item.value = 30;
			Item.rare = ItemRarityID.Orange;
		}
	}
}