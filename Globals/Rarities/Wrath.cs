using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Providence.Rarities
{
	public class Wrath : ModRarity
	{
		public override Color RarityColor => ProvidenceColor.ColorShiftMultiple(new Color[4] { new Color(158, 47, 63), new Color(218, 70, 70), new Color(243, 132, 95), new Color(218, 70, 70) }, 0.5f);
	}
}
