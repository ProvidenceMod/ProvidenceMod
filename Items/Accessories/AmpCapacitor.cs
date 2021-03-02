using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Accessories
{
  public class AmpCapacitor : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amp Capacitor");
      Tooltip.SetDefault("Amplifies friendly projectiles that\n pass through the aura barrier!");
    }

    public override void SetDefaults()
    {
      item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
      ProvidencePlayer ProvidencePlayer = player.Providence();
      ProvidencePlayer.auraType = AuraType.AmpCapacitorAura;
    }
  }
}