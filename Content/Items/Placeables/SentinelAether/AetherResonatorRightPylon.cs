using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Placeables.SentinelAether
{
	public class AetherResonatorRightPylon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aether Resonator Right Pylon");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 46;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.value = Item.buyPrice(0, 0, 3, 0);
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.SentinelAether.AetherResonatorRightPylon>();
		}
	}
}
