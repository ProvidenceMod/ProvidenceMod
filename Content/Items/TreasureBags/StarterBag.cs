using Terraria;
using Terraria.DataStructures;
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
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronBroadsword);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronBow);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronPickaxe);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronAxe);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronHammer);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.SwiftnessPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.IronskinPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.BuilderPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.MiningPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.RecallPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.SpelunkerPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.GillsPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.LesserHealingPotion, 5);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.Torch, 100);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.WoodenArrow, 100);
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemID.Bomb, 10);
		}
	}
}
