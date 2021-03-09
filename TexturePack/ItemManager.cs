using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.TexturePack.UI
{
  public static class ItemManager
  {
    public static void InitializeItemTextures()
    {
      Main.itemTexture[1322] = GetTexture("ProvidenceMod/TexturePack/Items/Accessories/MagmaStone");
    }

    public static void InitializeItemGlowMasks(this Item item)
    {
      switch(item.type)
      {
        case ItemID.MagmaStone:
          item.Providence().glowmask = true;
          item.Providence().glowmaskTexture = GetTexture("ProvidenceMod/TexturePack/Items/Accessories/MagmaStoneGlow");
          item.Providence().overrideGlowmaskPositionX = -2;
          break;
      }
    }
  }
}