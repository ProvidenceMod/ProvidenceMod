using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;

namespace Providence.Rarities
{
	public class Wrath : ModRarity
	{
		public override Color RarityColor => ColorShiftMultiple(new Color[4] { new Color(158, 47, 63), new Color(218, 70, 70), new Color(243, 132, 95), new Color(218, 70, 70) }, 0.5f);
	}
}
