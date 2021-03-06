using Terraria;
using Terraria.ModLoader;
using ProvidenceMod;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.TexturePack;

namespace ProvidenceMod.TexturePack
{
  public static class ProvidenceTextureManager
  {
    public static void Initialize()
    {
      BigStyleManager.InitializeBigStyleTextures();
      BuffManager.InitializeBuffTextures();
      PlayerManager.InitializePlayerTextures();
      ItemManager.InitializeItemTextures();
      NPCManager.InitializeNPCTextures();
      ProjectileManager.InitializeProjectileTextures();
      TileManager.InitializeTileTextures();
      UIManager.InitializeUITextures();
      WallManager.InitializeWallTextures();
    }
  }
}