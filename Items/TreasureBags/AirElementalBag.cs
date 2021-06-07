using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Placeabless;
using ProvidenceMod.Items.Materials;

namespace ProvidenceMod.Items.TreasureBags
{
  public class AirElementalBag : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Air Elemental Bag");
      Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
    }

    public override void SetDefaults()
    {
      item.maxStack = 999;
      item.consumable = true;
      item.width = 56;
      item.height = 32;
      item.expertOnly = true;
      item.expert = true;
      item.rare = ItemRarityID.Expert;
    }

    public override bool CanRightClick() => true;

    public override void RightClick(Player player)
    {
      // 26 to 74 ore spawned
      player.QuickSpawnItem(ItemType<ZephyrOre>(), Main.rand.Next(26, 75));
    }
  }
}