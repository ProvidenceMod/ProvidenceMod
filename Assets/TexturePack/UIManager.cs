using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using ReLogic.Content;

namespace Providence.TexturePack
{
	public static class UIManager
	{
		public static Asset<Texture2D>[] originalTextures = new Asset<Texture2D>[19];
		public static void Load()
		{
			originalTextures = new Asset<Texture2D>[21];

			originalTextures[0] = TextureAssets.InventoryBack;
			originalTextures[1] = TextureAssets.InventoryBack2;
			originalTextures[2] = TextureAssets.InventoryBack3;
			originalTextures[3] = TextureAssets.InventoryBack4;
			originalTextures[4] = TextureAssets.InventoryBack5;
			originalTextures[5] = TextureAssets.InventoryBack6;
			originalTextures[6] = TextureAssets.InventoryBack7;
			originalTextures[7] = TextureAssets.InventoryBack8;
			originalTextures[8] = TextureAssets.InventoryBack9;
			originalTextures[9] = TextureAssets.InventoryBack10;
			originalTextures[10] = TextureAssets.InventoryBack11;
			originalTextures[11] = TextureAssets.InventoryBack12;
			originalTextures[12] = TextureAssets.InventoryBack13;
			originalTextures[13] = TextureAssets.InventoryBack14;
			originalTextures[14] = TextureAssets.InventoryBack15;
			originalTextures[15] = TextureAssets.InventoryBack16;
			originalTextures[16] = TextureAssets.Heart;
			originalTextures[17] = TextureAssets.Heart2;
			originalTextures[18] = TextureAssets.Mana;
			originalTextures[19] = TextureAssets.Hb1;
			originalTextures[20] = TextureAssets.Hb2;
			TextureAssets.Hb1 = Request<Texture2D>("Providence/TexturePack/UI/HB1");
			TextureAssets.Hb2 = Request<Texture2D>("Providence/TexturePack/UI/HB2");
			TextureAssets.InventoryBack = Request<Texture2D>("Providence/TexturePack/UI/InventoryPanel"); // Normal Panel
			TextureAssets.InventoryBack2 = Request<Texture2D>("Providence/TexturePack/UI/ChestPanel"); // Piggy Bank
			TextureAssets.InventoryBack3 = Request<Texture2D>("Providence/TexturePack/UI/AccessoryPanel"); // Accessory and Armor Panels
			TextureAssets.InventoryBack4 = Request<Texture2D>("Providence/TexturePack/UI/CraftingPanel"); // Crafting Panel
			TextureAssets.InventoryBack5 = Request<Texture2D>("Providence/TexturePack/UI/ChestPanel"); // Chest Panel
			TextureAssets.InventoryBack6 = Request<Texture2D>("Providence/TexturePack/UI/ShopPanel"); // Shop Panel
			TextureAssets.InventoryBack7 = Request<Texture2D>("Providence/TexturePack/UI/TrashPanel"); // Trash Panel
			TextureAssets.InventoryBack8 = Request<Texture2D>("Providence/TexturePack/UI/CosmeticPanel"); // Cosmetic Panel
			TextureAssets.InventoryBack9 = Request<Texture2D>("Providence/TexturePack/UI/InventoryPanel"); // Held Item Panels
			TextureAssets.InventoryBack10 = Request<Texture2D>("Providence/TexturePack/UI/InventoryPanelFavorited"); // Favorited Panel
																														// Main.inventoryBack11Texture = Request<Texture2D>("Providence/TexturePack/UI/InventoryPanel"); // Unknown
			TextureAssets.InventoryBack12 = Request<Texture2D>("Providence/TexturePack/UI/DyePanel"); // Dye Panels
			TextureAssets.InventoryBack13 = Request<Texture2D>("Providence/TexturePack/UI/MenuPanel"); // Settings Menu Panel
			TextureAssets.InventoryBack14 = Request<Texture2D>("Providence/TexturePack/UI/HeldItemPanel"); // Held Item Panel
			TextureAssets.InventoryBack15 = Request<Texture2D>("Providence/TexturePack/UI/NewItemPanel"); // New Item Panel
																											 // Main.inventoryBack16Texture = Request<Texture2D>("Providence/TexturePack/UI/ChestPanel"); // Unknown
			TextureAssets.Heart = Request<Texture2D>("Providence/TexturePack/UI/HeartRed"); // Normal Heart Texture
			TextureAssets.Heart2 = Request<Texture2D>("Providence/TexturePack/UI/HeartGold"); // Life Fruit Heart Texture
			TextureAssets.Mana  = Request<Texture2D>("Providence/TexturePack/UI/Mana");
		}

		public static void Unload()
		{
			if (Main.dedServ)
			{
				TextureAssets.InventoryBack = originalTextures[0];
				TextureAssets.InventoryBack2 = originalTextures[1];
				TextureAssets.InventoryBack3= originalTextures[2];
				TextureAssets.InventoryBack4= originalTextures[3];
				TextureAssets.InventoryBack5= originalTextures[4];
				TextureAssets.InventoryBack6= originalTextures[5];
				TextureAssets.InventoryBack7= originalTextures[6];
				TextureAssets.InventoryBack8= originalTextures[7];
				TextureAssets.InventoryBack9= originalTextures[8];
				TextureAssets.InventoryBack10= originalTextures[9];
				TextureAssets.InventoryBack11 = originalTextures[10];
				TextureAssets.InventoryBack12= originalTextures[11];
				TextureAssets.InventoryBack13= originalTextures[12];
				TextureAssets.InventoryBack14= originalTextures[13];
				TextureAssets.InventoryBack15= originalTextures[14];
				TextureAssets.InventoryBack16 = originalTextures[15];
				TextureAssets.Heart = originalTextures[16];
				TextureAssets.Heart2 = originalTextures[17];
				TextureAssets.Mana  = originalTextures[18];
				TextureAssets.Hb1  = originalTextures[19];
				TextureAssets.Hb2  = originalTextures[20];
			}
			originalTextures = null;
		}
	}
}