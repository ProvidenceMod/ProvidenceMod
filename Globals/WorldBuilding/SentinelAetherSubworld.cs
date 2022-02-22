//using System.Collections.Generic;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.World.Generation;
//using Terraria.GameContent.Generation;
//using static Terraria.ModLoader.ModContent;
//using ProvidenceMod.Tiles.Ores;
//using Terraria.ID;
//using System;

//namespace ProvidenceMod.Subworld
//{
//	public class SentinelAetherSubworld : SubworldLibrary.Subworld
//	{
//		public override int height => 2400;
//		public override int width => 4200;
//		public override List<GenPass> tasks => SentinelAetherGenPass();
//		public override ModWorld modWorld => GetInstance<SentinelAetherWorld>();
//		public override ushort votingDuration => 600;
//		public override bool noWorldUpdate => false;
//		public override bool saveSubworld => false;
//		public static bool enteredWorld;

//		public static List<GenPass> SentinelAetherGenPass()
//		{
//			return new List<GenPass>
//			{
//				// First pass
//				new PassLegacy("Adjusting",
//				(GenerationProgress progress) => {
//					progress.Message = "Adjusting world levels"; // Sets the text above the worldgen progress bar
//					Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
//					Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds
//				},
//				1f),
//				new PassLegacy("GeneratingBorder",
//				(GenerationProgress progress) => {
//				 	progress.Message = "Generating Border";
//				 	for (int j = 0 ; j < Main.maxTilesX ; j++)
//				 	{
//				 		for (int i = 0 ; i < Main.maxTilesY ; i++)
//						{
//							if(j == 0 || j == 4199)
//							{
//								WorldGen.PlaceTile(i, j, TileType<ZephyrOre>());
//								progress.Value += 1f / 13196f;
//							}
//							if(i == 0 || i == 2399)
//							{
//								WorldGen.PlaceTile(i, j, TileType<ZephyrOre>());
//								progress.Value += 1f / 13196f;
//							}
//						}
//				 	}
//				},
//				1f),
//				new PassLegacy("GeneratingLand",
//				(GenerationProgress progress) => {
//					progress.Message = "Generating Land";
//					int remainder = 10;
//					int heightMod = 10;
//					int heightModTarget = 10;
//					int sin = 0;
//					for(int i = 0; i < Main.maxTilesX; i++)
//					{
//						sin++;
//						if(i % remainder == 0)
//						{
//							remainder = WorldGen.genRand.Next(10, 20);
//							heightModTarget = WorldGen.genRand.Next(10, 20);
//						}
//						if(i % WorldGen.genRand.Next(1, 3) == 0)
//						heightMod = heightMod < heightModTarget ? heightMod + 1 : heightMod > heightModTarget ? heightMod - 1 : heightMod;
//						WorldGen.TileRunner(i, Main.maxTilesY - 1000 - heightMod, 10d, 1, TileID.Cloud, true, (float)Math.Sin(sin) * 10f, (float)Math.Sin(sin), default, true);
//						progress.Value = i / Main.maxTilesX;
//					}
//				},
//				1f)
//				// Add more passes here	
//			};
//		}
//	}
//}
