using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Items.Placeables.Moon
{
	public class Anorthosite : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anorthosite");
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
			Item.createTile = ModContent.TileType<Tiles.Moon.Anorthosite>();
			Item.width = 16;
			Item.height = 16;
			Item.value = 30;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
