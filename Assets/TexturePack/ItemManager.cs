using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;

namespace Providence.TexturePack
{
	public static class ItemManager
	{
		public static Texture2D[] originalTextures = (Texture2D[])Terraria.GameContent.TextureAssets.Item.Clone();
		public static void Load()
		{
			Terraria.GameContent.TextureAssets.Item[ItemID.MagmaStone] = Request<Texture2D>("Providence/TexturePack/Items/Accessories/MagmaStone");
		}

		public static void InitializeItemGlowMasks(this Item item)
		{
			switch (item.type)
			{
				case ItemID.MagmaStone:
					//item.Providence().glowMaskTexture = Request<Texture2D>("Providence/TexturePack/Items/Accessories/MagmaStoneGlow");
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