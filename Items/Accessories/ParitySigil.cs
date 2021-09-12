using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Accessories
{
	public class ParitySigil : ClericItem
	{
		public override string Texture => $"Terraria/Item_{ItemID.DiamondRing}";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Parity Sigil");
			Tooltip.SetDefault("TODO");
		}
		public override void SetDefaults()
		{
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.Providence().cleric = true;
			player.Providence().paritySigil = true;
			player.Providence().parityStackGen += 0.5f;
			player.Providence().parityMaxStacks += 100f;
		}
	}
}
