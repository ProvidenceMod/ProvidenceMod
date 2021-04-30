﻿using System;
using System.Collections.Generic;
using ProvidenceMod.Developer;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace ProvidenceMod
{
	//This class showcases how to organize your SubworldLibrary reference
	public static class SubworldManager
	{
		//How we identify our world
		public static string mySubworldID = string.Empty; //An empty string will not cause any problems in Enter, IsActive etc. calls

		public static Mod subworldLibrary;

		public static bool Loaded => subworldLibrary != null;

		public static bool? Enter(string id)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("Enter", id) as bool?;
		}

		public static bool? Exit()
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("Exit") as bool?;
		}

		public static bool? IsActive(string id)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("IsActive", id) as bool?;
		}

		public static bool? AnyActive(Mod mod)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("AnyActive", mod) as bool?;
		}

		//Call this in ProvidenceMod.PostSetupContent()
		public static void Load()
		{
			subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null)
			{
				object result = subworldLibrary.Call(
					"Register",
					/*Mod mod*/ ModContent.GetInstance<ProvidenceMod>(),
					/*string name*/ "MySubworld",
					/*int width*/ 8400,
					/*int height*/ 2400,
					/*List<GenPass> tasks*/ TestingSubworldGenPass(),
					/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
					/*Action load*/ (Action)LoadWorld,
					/*Action unload*/ null,
					/*ModWorld modWorld*/ ModContent.GetInstance<TestingSubworld>()
					);

				if (result != null && result is string id)
				{
					mySubworldID = id;
				}
			}
		}

		//Call this in ProvidenceMod.Unload()
		public static void Unload()
		{
			subworldLibrary = null;
			mySubworldID = string.Empty;
		}

		//Passed into subworldLibrary.Call()
		public static void LoadWorld()
		{
			Main.dayTime = true;
			Main.time = 27000;
		}

		//Called in subworldLibrary.Call()
		public static List<GenPass> TestingSubworldGenPass()
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
				1f)
				//Add more passes here
			};
		}
	}
}
