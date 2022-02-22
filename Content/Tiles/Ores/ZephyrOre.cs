using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Tiles.Ores
{
	public class ZephyrOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 3000; // How often tiny dust appear off this tile. Larger is less frequently
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Zephyr Ore");
			AddMapEntry(new Color(174, 197, 231), name);

			DustType = DustID.Platinum;
			ItemDrop = ModContent.ItemType<Items.Placeables.Ores.ZephyrOre>();
			SoundType = SoundID.Tink;
			SoundStyle = 1;
			//mineResist = 4f;
			//minPick = 200;
		}
	}
}