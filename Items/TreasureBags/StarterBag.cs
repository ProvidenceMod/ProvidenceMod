using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod.Items.TreasureBags
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
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Expert;
		}

		public override bool CanRightClick() 
        {
			return true;
		}

		public virtual void RightClick(Player player) 
        {
			player.QuickSpawnItem(ItemID.IronBroadsword);
            player.QuickSpawnItem(ItemID.IronBow);
            player.QuickSpawnItem(ItemID.IronPickaxe);
            player.QuickSpawnItem(ItemID.IronAxe);
            player.QuickSpawnItem(ItemID.IronHammer);
            player.QuickSpawnItem(ItemID.IronHelmet);
            player.QuickSpawnItem(ItemID.IronChainmail);
            player.QuickSpawnItem(ItemID.IronGreaves);
            player.QuickSpawnItem(ItemID.SwiftnessPotion, 5);
            player.QuickSpawnItem(ItemID.IronskinPotion, 5);
            player.QuickSpawnItem(ItemID.BuilderPotion, 5);
            player.QuickSpawnItem(ItemID.MiningPotion, 5);
            player.QuickSpawnItem(ItemID.RecallPotion, 5);
            player.QuickSpawnItem(ItemID.SpelunkerPotion, 5);
            player.QuickSpawnItem(ItemID.GillsPotion, 5);
            player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
        }
	}
}