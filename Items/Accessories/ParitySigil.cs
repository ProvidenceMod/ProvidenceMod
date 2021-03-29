using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Accessories
{
  public class ParitySigil : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Parity Sigil");
      Tooltip.SetDefault("Attunes you to Order and Chaos\nPassively generates Order or Chaos stacks\nPress C to consume Parity stacks to unleash your ability");
    }
    public override void SetDefaults()
    {
      item.material = true;
      item.width = 26;
      item.height = 32;
      item.accessory = true;
      item.defense = 10;
    }
    public override void UpdateEquip(Player player)
    {
      ProvidencePlayer proPlayer = player.Providence();
      proPlayer.cleric = true;
      proPlayer.paritySigil = true;
      proPlayer.maxParityStacks = 50;
      proPlayer.parityStackGeneration = 1f;
      proPlayer.parityCyclePenalty = 50f;
    }
  }
}