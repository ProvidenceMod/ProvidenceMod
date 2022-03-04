using Providence.Rarities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Providence.Content.Items.BossSpawners;
using Providence.Content.NPCs.Caelus;

namespace Providence.Content.ModSupport
{
	public static class ModCalls
	{
		public static class BossChecklistProgressionValues
		{
			public const float KingSlime = 1f;
			public const float EyeOfCthulhu = 2f;
			public const float EaterOfWorlds = 3f;
			public const float QueenBee = 4f;
			public const float Skeletron = 5f;

			public const float Caelus = 5.5f;

			public const float WallOfFlesh = 6f;
			public const float TheTwins = 7f;
			public const float TheDestroyer = 8f;
			public const float SkeletronPrime = 9f;
			public const float Plantera = 10f;

			public const float FireAncient = 10.5f;

			public const float Golem = 11f;
			public const float DukeFishron = 12f;
			public const float LunaticCultist = 13f;
			public const float Moonlord = 14f;
		}
		public static void BossChecklist()
		{
			//Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			//List<int> caelusID = new List<int> { ModContent.NPCType<Caelus>() };
			//List<int> caelusSummons = new List<int> { ModContent.ItemType<ZephyrStone>() };
			//List<int> caelusCollection = new List<int> { };
			//List<int> caelusLoot = new List<int> { ModContent.ItemType<ZephyrStone>() };
			//bossChecklist?.Call(
			//		"AddBoss", // Method to call
			//		5.5f, // See BossChecklistProgressionValues
			//		caelusID, // NPC ID
			//		ProvidenceMod.Instance, // Mod
			//		"Primordial Caelus", // Name
			//		() => WorldFlags.downedCaelus, // Downed?
			//		caelusSummons, // Spawn items
			//		caelusCollection, // Trophies, collectables
			//		caelusLoot, // Loot table
			//		"Sky Islands will usually contain every material needed to craft the spawner. " +
			//		"A Skymill can be crafted. Must be spawned on the surface. Cannot spawn when another boss is active.",
			//		"The winds calm.", // Death message
			//		"Providence/NPCs/Caelus/Caelus", // Texture
			//		"Providence/NPCs/Caelus/Caelus_Head", // Icon
			//		() => true // Available
			//	);
		}
	}
}
