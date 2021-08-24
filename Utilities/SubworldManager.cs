using System;
using System.Collections.Generic;
using ProvidenceMod.Subworld;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using SubworldLibrary;
using static ProvidenceMod.ProvidenceServerConfig;

namespace ProvidenceMod
{
	// This class showcases how to organize your SubworldLibrary reference
	// **
	// For ProvidenceMod, we've implemented a strong mod reference. That
	// means we've downloaded the DLL from the SubworldLibrary GitHub and
	// added it to our mod solution, and included a modReference to
	// SubworldLibrary in our build.txt. https://github.com/jjohnsnaill/SubworldLibrary
	// If you would like this direct functionality without needing mod calls,
	// first add this to your build.txt :
	//
	// modReferences = SubworldLibrary
	//
	// Then download the DLL in the GitHub, open your mod project in Visual Studio,
	// right-click Dependencies at the top of the solution explorer, click
	// " Add COM Reference ", then browse to the DLL and select it. You should
	// now be able to use the SubworldLibrary functions without needing mod calls.
	// It should be noted that this changes how you should register worlds.
	//
	// Instead of your Subworld inheriting from ModWorld, it should inherit from
	// SubworldLibrary.Subworld. Then you must implement the abstract members of
	// that class, which you can do by overriding them and assigning them a value.
	// Reference our subworlds for these values.
	// **
	public static class SubworldManager
	{
		//How we identify our world
		public static string endlessSeaID = string.Empty; //An empty string will not cause any problems in Enter, IsActive etc. calls

		public static void Enter(string id)
		{
			SubworldLibrary.Subworld.Enter(id, ProvidenceMod.Instance.subworldVote);
		}

		public static void Exit()
		{
			SubworldLibrary.Subworld.Exit(ProvidenceMod.Instance.subworldVote);
		}

		public static void IsActive(string id)
		{
			SubworldLibrary.Subworld.IsActive(id);
		}

		public static bool AnyActive()
		{
			return SubworldLibrary.Subworld.AnyActive(ProvidenceMod.Instance);
		}

		//Call this in ProvidenceMod.PostSetupContent()
		public static void Load()
		{
			Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			object result = subworldLibrary.Call(
				"Register",
				/*Mod mod*/ ModContent.GetInstance<ProvidenceMod>(),
				/*string name*/ "MySubworld",
				/*int width*/ 8400,
				/*int height*/ 2400,
				/*List<GenPass> tasks*/ EndlessSeaGenPass(),
				/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
				/*Action load*/ (Action)LoadWorld,
				/*Action unload*/ null,
				/*ModWorld modWorld*/ ModContent.GetInstance<EndlessSea>()
				);

			if (result != null && result is string id)
			{
				endlessSeaID = id;
			}
		}

		//Call this in ProvidenceMod.Unload()
		public static void Unload()
		{
			endlessSeaID = string.Empty;
		}

		//Passed into subworldLibrary.Call()
		public static void LoadWorld()
		{
			Main.dayTime = true;
			Main.time = 27000;
		}

		//Called in subworldLibrary.Call()
		public static List<GenPass> EndlessSeaGenPass()
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
				//Second pass
				new PassLegacy("GeneratingBorders",
				(GenerationProgress progress) => {
					progress.Message = "Generating subworld borders";
					//Create three tiles for the player to stand on when he spawns
					for (int i = -1; i < 2; i++)
					{
						WorldGen.PlaceTile(Main.spawnTileX - i,  Main.spawnTileY + 2, TileID.Dirt, true, true);
					}
					//Create a wall of lihzard bricks around the world. 41, 42 and 43 are magic numbers from the game regarding world boundaries
					for (int i = 0; i < Main.maxTilesX; i++)
					{
						for (int j = 0; j < Main.maxTilesY; j++)
						{
							progress.Value = (((float)i * Main.maxTilesY) + j) / (Main.maxTilesX * Main.maxTilesY);
							if (i < 42 || i >= Main.maxTilesX - 43 || j <= 41 || j >= Main.maxTilesY - 43)
							{
								WorldGen.PlaceTile(i, j, TileID.LihzahrdBrick, true, true);
							}
						}
					}
				},
				1f),
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
