using Terraria.ModLoader;

namespace Providence
{
	public class BrinewastesWorld : ModSystem
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
		//// Worldgen testing
		//public static bool JustPressed(Keys key)
		//{
		//	return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		//}
		//private void TestMethod(int x, int y)
		//{
		//	Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

		//	// Code to test placed here:
		//	//WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick);
		//	//WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick, true);
		//}
		//
		//public override void PostUpdate()
		//{
		//if (SubworldManager.IsActive<BrinewastesSubworld>())
		//{
		//	SubworldLibrary.SLWorld.noReturn = true;
		//	//SubworldLibrary.SLWorld.drawMenu = true;
		//}
		////// Worldgen testing
		////if (JustPressed(Keys.D1))
		////	TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
		////
		//}
	}
}