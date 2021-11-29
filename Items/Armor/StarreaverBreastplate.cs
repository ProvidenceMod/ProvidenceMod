using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Body)]
  public class StarreaverBreastplate : ModItem
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      DisplayName.SetDefault("Starreaver Breasplate");
      Tooltip.SetDefault("The hide of the Gods");
    }

    public override void SetDefaults()
    {
      item.width = 30;
      item.height = 24;
      item.defense = 100;
    }

    public override void UpdateEquip(Player player)
    {
    }

    public override void AddRecipes()
    {
    }
  }
}