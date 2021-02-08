using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace ProvidenceMod.Items.Accessories
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
      ProvidencePlayer ProvidencePlayer = player.Providence();
      ProvidencePlayer.bastionsAegis = true;
      ProvidencePlayer.hasClericSet = true;
      ProvidencePlayer.burnAura = true;
      ProvidencePlayer.dashMod = 1;
    }
  }
}