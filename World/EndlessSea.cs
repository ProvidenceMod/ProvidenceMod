using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProvidenceMod.Subworld;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.World
{
	public class EndlessSea : ModWorld
	{
		// Worldgen testing
		public static bool JustPressed(Keys key)
		{
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
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
			if (SubworldManager.IsActive<EndlessSeaSubworld>())
			{
				SubworldLibrary.SLWorld.noReturn = false;
				SubworldLibrary.SLWorld.drawMenu = false;
			}
			// Worldgen testing
			if (JustPressed(Keys.D1))
				TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
			//
		}
	}
}