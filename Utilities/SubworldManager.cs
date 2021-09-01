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

		public static void Enter<T>(bool noVote = false) where T : SubworldLibrary.Subworld
		{
			SubworldLibrary.Subworld.Enter<T>(noVote);
		}

		public static void Exit()
		{
			SubworldLibrary.Subworld.Exit(ProvidenceMod.Instance.subworldVote);
		}

		public static bool IsActive<T>() where T : SubworldLibrary.Subworld
		{
			return SubworldLibrary.Subworld.IsActive<T>();
		}

		public static bool AnyActive<T>() where T : Mod
		{
			return SubworldLibrary.Subworld.AnyActive(ProvidenceMod.Instance);
		}

		//Call this in ProvidenceMod.PostSetupContent()
		public static void Load()
		{
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
	}
}
