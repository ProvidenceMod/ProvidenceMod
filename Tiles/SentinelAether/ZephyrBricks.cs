using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Tiles.SentinelAether
{
	public class ZephyrBricks : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			dustType = DustID.Platinum;
			drop = ModContent.ItemType<Items.Placeables.SentinelAether.ZephyrBricks>();
			soundType = SoundID.Tink;
			soundStyle = 1;
			//mineResist = 4f;
			//minPick = 200;
		}
	}
}