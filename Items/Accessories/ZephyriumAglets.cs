using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod.Items.Accessories
{
  public class ZephyriumAglets : ModItem
  {
    public override string Texture => $"Terraria/Item_{ItemID.Aglet}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyrium Aglets");
      Tooltip.SetDefault("+2 Air defense\nProvides a jump boost and increased vertical acceleration with wings and rocket boots");
    }

    public override void SetDefaults()
    {
      item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
      player.jumpBoost = true;
      UnbiddenPlayer unbiddenPlayer = player.Unbidden();
      unbiddenPlayer.resists[ElementID.Air] += 2;
      unbiddenPlayer.zephyriumAglet = true;
    }
  }
}