using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Consumables;

namespace ProvidenceMod.Entities
{
  public class AbyssalPortal : Portal
  {
    public override void SetDefaults()
    {
      animated = true;
      texture = ModContent.GetTexture("AbyssalPortal");
      activationItem = ModContent.GetModItem(ModContent.ItemType<PortalSpawner>());
      frameTime = 10;
      frameCount = 10;
    }
  }
}