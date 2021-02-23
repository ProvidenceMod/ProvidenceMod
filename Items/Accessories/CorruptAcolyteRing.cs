using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace ProvidenceMod.Items.Accessories
{
  public class CorruptAcolyteRing : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Corrupt Acolyte's Ring");
      Tooltip.SetDefault("Creates a small aura that inflicts Cursed Flames.\nThe ring of an acolyte whose interests have wandered elsewhere.");
    }

    public override void SetDefaults()
    {
      item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
      ProvidencePlayer ProvidencePlayer = player.Providence();
      ProvidencePlayer.hasClericSet = true;
      ProvidencePlayer.cFlameAura = true;
      player.dash = 20;
    }
  }
}