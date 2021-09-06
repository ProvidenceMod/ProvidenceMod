using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.World.Generation;
using ProvidenceMod.World;
using Terraria.GameContent.Generation;

namespace ProvidenceMod.Subworld
{
	public class BrinewastesSubworld : SubworldLibrary.Subworld
	{
		public override int height => 2400;
		public override int width => 8400;
		public override List<GenPass> tasks => BrinewastesGenPass();
		public override ModWorld modWorld => ModContent.GetInstance<Brinewastes>();
		public override ushort votingDuration => 600;
		public override bool noWorldUpdate => false;
		public override bool saveSubworld => true;
		public static bool enteredWorld;

		//Called in subworldLibrary.Call()
		public static List<GenPass> BrinewastesGenPass()
		{
			return new List<GenPass>
			{
				//First pass
				new PassLegacy("Adjusting",
				(GenerationProgress progress) => {
					progress.Message = "Adjusting world levels"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY - 42; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds
				},
				1f),
				////Second pass
				//new PassLegacy("GeneratingBradiants",
				//(GenerationProgress progress) => {
				//	progress.Message = "Generating subworld bradiants";
				//	//Create three tiles for the player to stand on when he spawns
				//	for (int i = -1; i < 2; i++)
				//	{
				//		WorldGen.PlaceTile(Main.spawnTileX - i,  Main.spawnTileY + 2, TileID.Dirt, true, true);
				//	}
				//	//Create a wall of lihzard bricks around the world. 41, 42 and 43 are magic numbers from the game regarding world boundaries
				//	for (int i = 0; i < Main.maxTilesX; i++)
				//	{
				//		for (int j = 0; j < Main.maxTilesY; j++)
				//		{
				//			progress.Value = (((float)i * Main.maxTilesY) + j) / (Main.maxTilesX * Main.maxTilesY);
				//			if (i < 42 || i >= Main.maxTilesX - 43 || j <= 41 || j >= Main.maxTilesY - 43)
				//			{
				//				WorldGen.PlaceTile(i, j, TileID.LihzahrdBrick, true, true);
				//			}
				//		}
				//	}
				//},
				//1f),
				new PassLegacy("AddingWater",
				(GenerationProgress progress) => {
				 	progress.Message = "Generating water";
				 	for (int i = 256 ; i < Main.maxTilesY ; i++)
				 	{
				 		for (int j = 0 ; j < Main.maxTilesX ; j++)
						{
							Main.tile[j, i].liquidType(0);
							Main.tile[j, i].liquid = 255;
							WorldGen.SquareTileFrame(j, i, false);
						}
				 	}
				},
				1f)
				//Add more passes here
			};
		}
	}
}