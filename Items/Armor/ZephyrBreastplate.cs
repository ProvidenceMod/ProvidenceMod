using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Body)]
  public class ZephyrBreastplate : ModItem
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      DisplayName.SetDefault("Zephyr Breastplate");
      Tooltip.SetDefault("");
    }

    public override void SetDefaults()
    {
      item.width = 30;
      item.height = 22;
      item.defense = 4;
    }

    public override void UpdateEquip(Player player)
    {
    }

    public override void AddRecipes()
    {
    }
  }
}