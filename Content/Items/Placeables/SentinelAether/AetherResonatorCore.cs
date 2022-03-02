using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Placeables.SentinelAether
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
			Item.width = 48;
			Item.height = 28;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.value = Item.buyPrice(0, 0, 3, 0);
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.SentinelAether.AetherResonatorCore>();
		}
	}
}
