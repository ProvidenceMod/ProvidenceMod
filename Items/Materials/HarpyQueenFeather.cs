using Terraria.ModLoader;

namespace UnbiddenMod.Items.Materials
{
  public class HarpyQueenFeather : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Harpy Queen Feather");
      Tooltip.SetDefault("\"Despite the softness on the flat edge, you could almost use this like a dagger!\"");
    }

    public override void SetDefaults()
    {
      item.maxStack = 999;
      item.width = 22;
      item.height = 50;
    }
  }
}