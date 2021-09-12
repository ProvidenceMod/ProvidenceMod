using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Accessories
{
	public class HeartOfReality : ClericItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heart of Reality");
			Tooltip.SetDefault("+10 life regen\n+500 HP\nEnables the generation of Parity stacks\n+0.5 Parity stack generation\n+100 maximum Parity stacks\nYou're not a mortal...");
		}
		public override void SetDefaults()
		{
			item.rare = ItemRarityID.Purple;
			item.Providence().customRarity = ProvidenceRarity.Developer;
			item.accessory = true;
			item.width = 78;
			item.height = 74;
			item.defense = 100;
			item.lifeRegen = 10;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.Providence().cleric = true;
			player.Providence().heartOfReality = true;
			player.Providence().parityStackGen += 0.5f;
			player.Providence().parityMaxStacks += 100f;
			player.statLifeMax2 += 500;
		}
	}
}
