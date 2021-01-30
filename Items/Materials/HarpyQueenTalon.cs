using Terraria.ModLoader;

namespace UnbiddenMod.Items.Materials
{
  public class HarpyQueenTalon : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Harpy Queen Talon");
      Tooltip.SetDefault("\"The point is really, REALLY sharp!\"");
    }

    public override void SetDefaults()
    {
      item.maxStack = 999;
      item.width = 14;
      item.height = 20;
    }
  }
}