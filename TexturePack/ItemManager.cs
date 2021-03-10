using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.TexturePack
{
  public static class ItemManager
  {
    public static Texture2D[] originalTextures = new Texture2D[1] { null };
    public static void InitializeItemTextures()
    {
      originalTextures[0] = Main.itemTexture[ItemID.MagmaStone];
      Main.itemTexture[ItemID.MagmaStone] = GetTexture("ProvidenceMod/TexturePack/Items/Accessories/MagmaStone");
    }

    public static void InitializeItemGlowMasks(this Item item)
    {
      switch (item.type)
      {
        case ItemID.MagmaStone:
          item.Providence().glowmask = true;
          item.Providence().glowmaskTexture = GetTexture("ProvidenceMod/TexturePack/Items/Accessories/MagmaStoneGlow");
          item.Providence().overrideGlowmaskPositionX = -2;
          break;
      }
    }

    public static void Unload()
    {
      //Main.itemTexture[ItemID.MagmaStone] = originalTextures[0];
      //Main.item[ItemID.MagmaStone].Providence().glowmaskTexture = null;
      originalTextures[0] = null;
    }
  }
}