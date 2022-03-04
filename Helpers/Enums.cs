using System;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace Providence
{
	public static partial class ProvidenceUtils
	{
		public enum ElementID
		{
			Typeless = -1,
			Fire = 0,
			Ice = 1,
			Lightning = 2,
			Water = 3,
			Earth = 4,
			Air = 5,
			Radiant = 6,
			Shadow = 7
		}
	}
}
