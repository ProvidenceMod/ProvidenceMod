using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Placeables.EndlessSea
{
	public class RedCoralBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Coral Block");
			Tooltip.SetDefault("A Living stone made of complex micro-organisms. Formations this large are as rare as they are beautiful.");
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
			item.createTile = ModContent.TileType<Tiles.EndlessSea.RedCoralBlock>();
			item.width = 16;
			item.height = 16;
			item.value = 30;
			item.rare = ItemRarityID.Orange;
		}
	}
}