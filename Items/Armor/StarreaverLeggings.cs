using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Armor;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Legs)]
  public class StarreaverLeggings : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starreaver Leggings");
      Tooltip.SetDefault("The talons of the gods");
    }

    public override void SetDefaults()
    {
      item.width = 22;
      item.height = 18;
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