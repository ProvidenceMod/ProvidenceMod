using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using System.Collections.Generic;
using Terraria.Localization;
using Microsoft.Xna.Framework;
namespace ProvidenceMod.Subworld
{
	public class EndlessSea : SubworldLibrary.Subworld
	{
		public override int height => 2400;
		public override int width => 8400;
		public override List<GenPass> tasks => SubworldManager.EndlessSeaGenPass();
		public static bool enteredWorld;
	}
}