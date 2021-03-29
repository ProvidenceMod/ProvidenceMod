using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.NPCs.HarpyQueen;

namespace ProvidenceMod.Items.Accessories
{
  public class VampireFang : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vampire's Fang");
      Tooltip.SetDefault("While this is equipped and visible, double-tap your 'Use Chaos Magic' hotkey to consume Chaos.\nConsuming Chaos restores 35% of your max HP with reduced potion sickness.");
    }

    public override void SetDefaults()
    {
      item.width = 12;
      item.height = 32;
      item.maxStack = 1;
      item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      if (!hideVisual) player.Providence().vampFang = true;
    }
  }
}