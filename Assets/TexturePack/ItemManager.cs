using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Providence.TexturePack
{
	public static class ItemManager
	{
		public static Texture2D[] originalTextures = (Texture2D[])Terraria.GameContent.TextureAssets.Item.Clone();
		public static void Load()
		{
			Terraria.GameContent.TextureAssets.Item[ItemID.MagmaStone] = Request<Texture2D>("Providence/Assets/TexturePack/Items/Accessories/MagmaStone");
		}

		public static void InitializeItemGlowMasks(this Item item)
		{
			switch (item.type)
			{
				case ItemID.MagmaStone:
					//item.Providence().glowMaskTexture = Request<Texture2D>("Providence/Assets/TexturePack/Items/Accessories/MagmaStoneGlow");
					//item.Providence().overrideGlowMaskPositionX = -2;
					break;
			}
		}

		public static void Unload()
		{
			originalTextures = null;
		}
	}
}