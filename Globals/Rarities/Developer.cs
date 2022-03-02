using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;

namespace Providence.Rarities
{
	public class Developer : ModRarity
	{
		public override Color RarityColor => ColorShiftMultiple(new Color[6] { new Color(77, 40, 132), new Color(160, 16, 193), new Color(255, 25, 153), new Color(255, 181, 62), new Color(255, 25, 153), new Color(160, 16, 193) }, 0.5f);
	}
}
