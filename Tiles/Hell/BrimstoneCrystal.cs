using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Tiles.Hell
{
	public class BrimstoneCrystal : ModTile
	{
		public Texture2D texture = GetTexture("ProvidenceMod/Tiles/Hell/BrimstoneCrystal");
		public Vector3 color;
		public float time;
		public int frame;
		public int frameCounter;
		public int type;
		public override void SetDefaults()
		{
			// Main.tileSolid[Type] = false;
			// Main.tileNoAttach[Type] = true;
			// Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			// Main.tileValue[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
			// Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			// Main.tileShine[Type] = 3000; // How often tiny dust appear off this tile. Larger is less frequently
			// Main.tileMergeDirt[Type] = false;
			// TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			soundType = SoundID.Shatter;
			soundStyle = SoundID.Shatter;
			dustType = DustType<BurnDust>();
			drop = ItemType<Items.Placeables.Hell.BrimstoneCrystal>();
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			Main.tileNoFail[Type] = true;
			animationFrameHeight = 72;
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			time = (i % 2 == 0 ? 3 : i % 3 == 0 ? 4 : i % 4 == 0 ? 5 : i % 5 == 0 ? 6 : i % 6 == 0 ? 7 : i % 7 == 0 ? 8 : i % 8 == 0 ? 9 : 2) +
						 (j % 2 == 0 ? 3 : j % 3 == 0 ? 4 : j % 4 == 0 ? 5 : j % 5 == 0 ? 6 : j % 6 == 0 ? 7 : j % 7 == 0 ? 8 : j % 8 == 0 ? 9 : 2);
			color = ColorShift(new Color(54, 30, 53), new Color(166, 46, 61), time).ToVector3();
			color.RGBIntToFloat();
			r = color.X;
			g = color.Y;
			b = color.Z;
		}
		public override bool CanPlace(int i, int j) => (Main.tile[i, j + 1].slope() == 0 && !Main.tile[i, j + 1].halfBrick() && Main.tile[i, j + 1].active())
																								|| (Main.tile[i, j - 1].slope() == 0 && !Main.tile[i, j - 1].halfBrick() && Main.tile[i, j - 1].active())
																								|| (Main.tile[i + 1, j].slope() == 0 && !Main.tile[i + 1, j].halfBrick() && Main.tile[i + 1, j].active())
																								|| (Main.tile[i - 1, j].slope() == 0 && !Main.tile[i - 1, j].halfBrick() && Main.tile[i - 1, j].active());
		public override void PlaceInWorld(int i, int j, Item item)
		{
			if (Main.tile[i, j + 1].active() && Main.tileSolid[Main.tile[i, j + 1].type] && Main.tile[i, j + 1].slope() == 0 && !Main.tile[i, j + 1].halfBrick())
				Main.tile[i, j].frameY = 0;
			else if (Main.tile[i, j - 1].active() && Main.tileSolid[Main.tile[i, j - 1].type] && Main.tile[i, j - 1].slope() == 0 && !Main.tile[i, j - 1].halfBrick())
				Main.tile[i, j].frameY = 18;
			else if (Main.tile[i + 1, j].active() && Main.tileSolid[Main.tile[i + 1, j].type] && Main.tile[i + 1, j].slope() == 0 && !Main.tile[i + 1, j].halfBrick())
				Main.tile[i, j].frameY = 36;
			else if (Main.tile[i - 1, j].active() && Main.tileSolid[Main.tile[i - 1, j].type] && Main.tile[i - 1, j].slope() == 0 && !Main.tile[i - 1, j].halfBrick())
				Main.tile[i, j].frameY = 54;
			type = Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(9) * 18);
		}
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			// Animate tile.
			//Main.tileFrameCounter[Type]++;
			//if (Main.tileFrameCounter[Type] > time * 12)
			//{
			//	Main.tileFrameCounter[Type] = 0;
			//	Main.tileFrame[Type]++;
			//	if (Main.tileFrame[Type] > 3)
			//	{
			//		Main.tileFrame[Type] = 0;
			//	}
			//}
		}
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			drawColor = new Color(255, 255, 255, 200);
		}
	}
}