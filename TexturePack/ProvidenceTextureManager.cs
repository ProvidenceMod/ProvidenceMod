using Terraria;
using Terraria.ModLoader;
using ProvidenceMod;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.TexturePack;
using ReLogic.Graphics;

namespace ProvidenceMod.TexturePack
{
  public static class ProvidenceTextureManager
  {
		public static DynamicSpriteFont origMouseTextFont;
		public static DynamicSpriteFont origItemStackFont;
		public static DynamicSpriteFont origDeathTextFont;
		public static void Load()
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
		public static void LoadFonts()
		{
			origMouseTextFont = Main.fontMouseText;
			origItemStackFont = Main.fontItemStack;
			origDeathTextFont = Main.fontDeathText;
			Main.fontMouseText = ProvidenceMod.mouseTextFont;
			Main.fontItemStack = ProvidenceMod.mouseTextFont;
			Main.fontDeathText = ProvidenceMod.mouseTextFont;
		}
		public static void Unload()
		{
			BigStyleManager.Unload();
			BuffManager.Unload();
			PlayerManager.Unload();
			ItemManager.Unload();
			NPCManager.Unload();
			ProjectileManager.Unload();
			TileManager.Unload();
			UIManager.Unload();
			WallManager.Unload();
		}
		public static void UnloadFonts()
		{
			Main.fontMouseText = origMouseTextFont;
			Main.fontItemStack = origMouseTextFont;
			Main.fontDeathText = origDeathTextFont;
			origMouseTextFont = null;
			origItemStackFont = null;
			origDeathTextFont = null;
		}
  }
}