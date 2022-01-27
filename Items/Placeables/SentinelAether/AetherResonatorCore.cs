using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Placeables.SentinelAether
{
	public class AetherResonatorCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aether Resonator Core");
			Tooltip.SetDefault("Resonates a rift to the Sentinel Aether\nMust be placed directly adjacent to the left and right pylons");
		}
		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 28;
			item.rare = (int)ProvidenceRarity.Orange;
			item.value = Item.buyPrice(0, 0, 3, 0);
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Tiles.SentinelAether.AetherResonatorCore>();
		}
	}
}
