using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.TexturePack
{
  public static class UIManager
  {
    public static void InitializeUITextures()
    {
      Main.inventoryBackTexture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Normal Panel
      Main.inventoryBack2Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Piggy Bank
      Main.inventoryBack3Texture = GetTexture("ProvidenceMod/TexturePack/UI/AccessoryPanel"); // Accessory and Armor Panels
      Main.inventoryBack4Texture = GetTexture("ProvidenceMod/TexturePack/UI/CraftingPanel"); // Crafting Panel
      Main.inventoryBack5Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Chest Panel
      Main.inventoryBack6Texture = GetTexture("ProvidenceMod/TexturePack/UI/ShopPanel"); // Shop Panel
      Main.inventoryBack7Texture = GetTexture("ProvidenceMod/TexturePack/UI/TrashPanel"); // Trash Panel
      Main.inventoryBack8Texture = GetTexture("ProvidenceMod/TexturePack/UI/CosmeticPanel"); // Cosmetic Panel
      Main.inventoryBack9Texture = GetTexture("ProvidenceMod/TexturePack/UI/HeldItemPanel"); // Held Item Panels
      Main.inventoryBack10Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanelFavorited"); // Favorited Panel
      Main.inventoryBack11Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Unknown
      Main.inventoryBack12Texture = GetTexture("ProvidenceMod/TexturePack/UI/DyePanel"); // Dye Panels
      Main.inventoryBack13Texture = GetTexture("ProvidenceMod/TexturePack/UI/MenuPanel"); // Settings Menu Panel
      Main.inventoryBack14Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Unknown
      Main.inventoryBack15Texture = GetTexture("ProvidenceMod/TexturePack/UI/InventoryPanel"); // Unknown
      Main.inventoryBack16Texture = GetTexture("ProvidenceMod/TexturePack/UI/ChestPanel"); // Unknown
      Main.heartTexture = GetTexture("ProvidenceMod/TexturePack/UI/HeartRed"); // Normal Heart Texture
      Main.heart2Texture = GetTexture("ProvidenceMod/TexturePack/UI/HeartGold"); // Life Fruit Heart Texture
    }
  }
}