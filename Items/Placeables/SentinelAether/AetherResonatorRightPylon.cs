using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Placeables.SentinelAether
{
	public class AetherResonatorRightPylon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aether Resonator Right Pylon");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 46;
			item.rare = (int)ProvidenceRarity.Orange;
			item.value = Item.buyPrice(0, 0, 3, 0);
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Tiles.SentinelAether.AetherResonatorRightPylon>();
		}
	}
}
