using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProvidenceMod.Subworld;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProvidenceMod
{
	public class BrinewastesWorld : ModWorld
	{
		// Downed Bosses
		public static bool downedCaelus = false;
		public static bool downedVerglasLeviathan = false;
		public static bool downedFireAncient = false;
		public static bool downedAstrid = false;
		public static bool downedLysandria = false;

		// Difficulty Modifiers
		public static bool lament = false;
		public static bool wrath = false;
		// Worldgen testing
		public static bool JustPressed(Keys key)
		{
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		}
		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"lament", lament },
				{"wrath", wrath },
				{"downedCaelus", downedCaelus },
				{"downedVerglasLeviathan", downedVerglasLeviathan},
				{"downedFireAncient", downedFireAncient },
				{"downedAstrid", downedAstrid },
				{"downedLysandria", downedLysandria }
			};
		}
		public override void Load(TagCompound tag)
		{
			lament = tag.GetBool("lament");
			wrath = tag.GetBool("wrath");
			downedCaelus = tag.GetBool("downedCaelus");
			downedVerglasLeviathan = tag.GetBool("downedVerglasLeviathan");
			downedFireAncient = tag.GetBool("downedFireAncient");
			downedAstrid = tag.GetBool("downedAstrid");
			downedLysandria = tag.GetBool("downedLysandria");
		}
		private void TestMethod(int x, int y)
		{
			Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

			// Code to test placed here:
			//WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick);
			//WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick, true);
		}
		//
		public override void PostUpdate()
		{
			//if (SubworldManager.IsActive<BrinewastesSubworld>())
			//{
			//	SubworldLibrary.SLWorld.noReturn = false;
			//	SubworldLibrary.SLWorld.drawMenu = false;
			//}
			//// Worldgen testing
			if (JustPressed(Keys.D1))
				TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
			//
		}
	}
}