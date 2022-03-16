using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Providence.Rarities
{
	public class Lament : ModRarity
	{
		public override Color RarityColor => ProvidenceColor.ColorShiftMultiple(new Color[4] { new Color(41, 122, 138), new Color(56, 196, 166), new Color(83, 242, 160), new Color(56, 196, 166) }, 0.5f);
	}
}
