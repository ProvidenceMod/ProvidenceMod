using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Items.TreasureBags
{
	public class StarterBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starter Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Blue;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ItemID.IronBroadsword);
			player.QuickSpawnItem(ItemID.IronBow);
			player.QuickSpawnItem(ItemID.IronPickaxe);
			player.QuickSpawnItem(ItemID.IronAxe);
			player.QuickSpawnItem(ItemID.IronHammer);
			player.QuickSpawnItem(ItemID.SwiftnessPotion, 5);
			player.QuickSpawnItem(ItemID.IronskinPotion, 5);
			player.QuickSpawnItem(ItemID.BuilderPotion, 5);
			player.QuickSpawnItem(ItemID.MiningPotion, 5);
			player.QuickSpawnItem(ItemID.RecallPotion, 5);
			player.QuickSpawnItem(ItemID.SpelunkerPotion, 5);
			player.QuickSpawnItem(ItemID.GillsPotion, 5);
			player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
			player.QuickSpawnItem(ItemID.Torch, 100);
			player.QuickSpawnItem(ItemID.WoodenArrow, 100);
			player.QuickSpawnItem(ItemID.Bomb, 10);
		}
	}
}
