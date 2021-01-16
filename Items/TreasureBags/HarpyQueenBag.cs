using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items.Placeable;

namespace UnbiddenMod.Items.TreasureBags
{
  public class HarpyQueenBag : ModItem
  {
    public override string Texture => $"Terraria/Item_{ItemID.EyeOfCthulhuBossBag}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Harpy Queen Bag");
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

    public override void RightClick(Player player)
    {
      // 26 to 74 ore spawned
      player.QuickSpawnItem(ItemType<ZephyriumOre>(), Main.rand.Next(26, 75));
    }
  }
}