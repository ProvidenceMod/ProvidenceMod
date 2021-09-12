using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.TexturePack
{
  public static class UIManager
  {
    public static Texture2D[] originalTextures = new Texture2D[19] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
    public static void InitializeUITextures()
    {
      originalTextures = new Texture2D[21];
      originalTextures[0] = Main.inventoryBackTexture;
      originalTextures[1] = Main.inventoryBack2Texture;
      originalTextures[2] = Main.inventoryBack3Texture;
      originalTextures[3] = Main.inventoryBack4Texture;
      originalTextures[4] = Main.inventoryBack5Texture;
      originalTextures[5] = Main.inventoryBack6Texture;
      originalTextures[6] = Main.inventoryBack7Texture;
      originalTextures[7] = Main.inventoryBack8Texture;
      originalTextures[8] = Main.inventoryBack9Texture;
      originalTextures[9] = Main.inventoryBack10Texture;
      originalTextures[10] = Main.inventoryBack11Texture;
      originalTextures[11] = Main.inventoryBack12Texture;
      originalTextures[12] = Main.inventoryBack13Texture;
      originalTextures[13] = Main.inventoryBack14Texture;
      originalTextures[14] = Main.inventoryBack15Texture;
      originalTextures[15] = Main.inventoryBack16Texture;
      originalTextures[16] = Main.heartTexture;
      originalTextures[17] = Main.heart2Texture;
			originalTextures[18] = Main.manaTexture;
			originalTextures[19] = Main.hbTexture1;
			originalTextures[20] = Main.hbTexture2;
			Main.hbTexture1 = GetTexture("ProvidenceMod/TexturePack/UI/HB1");
			Main.hbTexture2 = GetTexture("ProvidenceMod/TexturePack/UI/HB2");
			Main.inventoryBackTexture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Normal Panel
      Main.inventoryBack2Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Piggy Bank
      Main.inventoryBack3Texture = GetTexture("ProvidenceMod/TexturePack/UI/AccessoryPanel"); // Accessory and Armor Panels
      Main.inventoryBack4Texture = GetTexture("ProvidenceMod/TexturePack/UI/CraftingPanel"); // Crafting Panel
      Main.inventoryBack5Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Chest Panel
      Main.inventoryBack6Texture = GetTexture("ProvidenceMod/TexturePack/UI/ShopPanel"); // Shop Panel
      Main.inventoryBack7Texture = GetTexture("ProvidenceMod/TexturePack/UI/TrashPanel"); // Trash Panel
      Main.inventoryBack8Texture = GetTexture("ProvidenceMod/TexturePack/UI/CosmeticPanel"); // Cosmetic Panel
      Main.inventoryBack9Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Held Item Panels
      Main.inventoryBack10Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanelFavorited"); // Favorited Panel
      // Main.inventoryBack11Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Unknown
      Main.inventoryBack12Texture = GetTexture("ProvidenceMod/TexturePack/UI/DyePanel"); // Dye Panels
      Main.inventoryBack13Texture = GetTexture("ProvidenceMod/TexturePack/UI/MenuPanel"); // Settings Menu Panel
      Main.inventoryBack14Texture = GetTexture("ProvidenceMod/TexturePack/UI/HeldItemPanel"); // Held Item Panel
      Main.inventoryBack15Texture = GetTexture("ProvidenceMod/TexturePack/UI/NewItemPanel"); // New Item Panel
      // Main.inventoryBack16Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Unknown
      Main.heartTexture = GetTexture("ProvidenceMod/TexturePack/UI/HeartRed"); // Normal Heart Texture
      Main.heart2Texture = GetTexture("ProvidenceMod/TexturePack/UI/HeartGold"); // Life Fruit Heart Texture
      Main.manaTexture = GetTexture("ProvidenceMod/TexturePack/UI/Mana");
    }

    public static void Unload()
    {
      if (Main.dedServ)
      {
        Main.inventoryBackTexture = originalTextures[0];
        Main.inventoryBack2Texture = originalTextures[1];
        Main.inventoryBack3Texture = originalTextures[2];
        Main.inventoryBack4Texture = originalTextures[3];
        Main.inventoryBack5Texture = originalTextures[4];
        Main.inventoryBack6Texture = originalTextures[5];
        Main.inventoryBack7Texture = originalTextures[6];
        Main.inventoryBack8Texture = originalTextures[7];
        Main.inventoryBack9Texture = originalTextures[8];
        Main.inventoryBack10Texture = originalTextures[9];
        Main.inventoryBack11Texture = originalTextures[10];
        Main.inventoryBack12Texture = originalTextures[11];
        Main.inventoryBack13Texture = originalTextures[12];
        Main.inventoryBack14Texture = originalTextures[13];
        Main.inventoryBack15Texture = originalTextures[14];
        Main.inventoryBack16Texture = originalTextures[15];
        Main.heartTexture = originalTextures[16];
        Main.heart2Texture = originalTextures[17];
        Main.manaTexture = originalTextures[18];
				Main.hbTexture1 = originalTextures[19];
				Main.hbTexture2 = originalTextures[20];
			}
      originalTextures = null;
    }
  }
}