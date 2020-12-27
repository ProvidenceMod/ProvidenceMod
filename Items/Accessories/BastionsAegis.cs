using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace UnbiddenMod.Items.Accessories
{
  public class BastionsAegis : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bastion's Aegis");
      Tooltip.SetDefault("Creates a burning aura!");
    }

    public override void SetDefaults()
    {
      item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
      UnbiddenPlayer unbiddenPlayer = player.Unbidden();
      unbiddenPlayer.bastionsAegis = true;
      unbiddenPlayer.hasClericSet = true;
      unbiddenPlayer.burnAura = true;
      unbiddenPlayer.dashMod = 1;
    }
  }
}