using Terraria.ModLoader;
using Terraria.ModLoader.IO;
namespace ProvidenceMod
{
	public class ProvidenceWorld : ModWorld
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
	}
}
