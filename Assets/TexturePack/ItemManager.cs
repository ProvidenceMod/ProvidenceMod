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
		public static Texture2D[] originalTextures = (Texture2D[])Terraria.GameContent.TextureAssets.Item.Clone();
		public static void Load()
		{
			Terraria.GameContent.TextureAssets.Item[ItemID.MagmaStone] = Request<Texture2D>("ProvidenceMod/TexturePack/Items/Accessories/MagmaStone");
		}

		public static void InitializeItemGlowMasks(this Item item)
		{
			switch (item.type)
			{
				case ItemID.MagmaStone:
					item.Providence().glowMask = true;
					//item.Providence().glowMaskTexture = Request<Texture2D>("ProvidenceMod/TexturePack/Items/Accessories/MagmaStoneGlow");
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