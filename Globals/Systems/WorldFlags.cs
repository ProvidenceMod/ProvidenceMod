using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProvidenceMod.Globals.Systems
{
	// Set a flag like this in your bosses OnKill hook:
	//    NPC.SetEventFlagCleared(ref DownedBossSystem.downedMinionBoss, -1);
	public class WorldFlags : ModSystem
	{
		// Difficulty Modifiers
		public static bool lament = false;
		public static bool wrath = false;

		// Downed Bosses
		public static bool downedCaelus = false;
		public static bool downedVerglasLeviathan = false;
		public static bool downedFireAncient = false;
		public static bool downedAstrid = false;
		public static bool downedLysandria = false;

		// World Building
		public static bool zephyrGenned = false;

		public override void OnWorldLoad()
		{
			lament = false;
			wrath = false;
			downedCaelus = false;
			downedVerglasLeviathan = false;
			downedFireAncient = false;
			downedAstrid = false;
			downedLysandria = false;
			zephyrGenned = false;
		}

		public override void OnWorldUnload()
		{
			lament = false;
			wrath = false;
			downedCaelus = false;
			downedVerglasLeviathan = false;
			downedFireAncient = false;
			downedAstrid = false;
			downedLysandria = false;
			zephyrGenned = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			var flags = new List<string>();
			flags.AddIfTrue(lament, "lament");
			flags.AddIfTrue(wrath, "wrath");
			flags.AddIfTrue(downedCaelus, "downedCaelus");
			flags.AddIfTrue(downedVerglasLeviathan, "downedVerglasLeviathan");
			flags.AddIfTrue(downedFireAncient, "downedFireAncient");
			flags.AddIfTrue(downedAstrid, "downedAstrid");
			flags.AddIfTrue(downedLysandria, "downedLysandria");
			flags.AddIfTrue(zephyrGenned, "zephyrGenned");
		}
		public override void LoadWorldData(TagCompound tag)
		{
			var flags = tag.GetList<string>("flags");

			lament = flags.Contains("lament");
			wrath = flags.Contains("wrath");
			downedCaelus = flags.Contains("downedCaelus");
			downedVerglasLeviathan = flags.Contains("downedVerglasLeviathan");
			downedFireAncient = flags.Contains("downedFireAncient");
			downedAstrid = flags.Contains("downedAstrid");
			downedLysandria = flags.Contains("downedLysandria");
			zephyrGenned = flags.Contains("zephyrGenned");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte
			(
				lament,
				wrath,
				downedCaelus,
				downedVerglasLeviathan,
				downedFireAncient,
				downedAstrid,
				downedLysandria,
				zephyrGenned
			);

			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();

			lament = flags[0];
			wrath = flags[1];
			downedCaelus = flags[2];
			downedVerglasLeviathan = flags[3];
			downedFireAncient = flags[4];
			downedAstrid = flags[5];
			downedLysandria = flags[6];
			zephyrGenned = flags[7];
		}
	}
}
