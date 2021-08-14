using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.EndlessSea
{
	public class BlueCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Coral Block");
			Tooltip.SetDefault("Micro-Organisms slowly excrete limestone to create colorful structures ontop of the remnants of other dead formations");
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
			item.createTile = ModContent.TileType<Tiles.EndlessSea.BlueCoralBlock>();
			item.width = 16;
			item.height = 16;
			item.value = 30;
			item.rare = ItemRarityID.Orange;
		}
	}
}