using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Tiles.Moon
{
	public class Anorthosite : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileMergeDirt[Type] = true; // Allows the tile to merge with dirt, meaning it will draw the parts of the spritesheet with dirt merging
			Main.tileSolid[Type] = true; // Makes sure the tile counts as solid
										 // Main.tileBlockLight[Type] = true;
			ModTranslation name = CreateMapEntryName(); // Gives the tile a name to display when hovered over on the map
			name.SetDefault("Anorthosite"); // Sets the map hover name
			AddMapEntry(new Color(152, 255, 241), name); // Adds the map entry for this tile with a hover name
			DustType = DustID.Platinum; // TODO: Make a dedicated dust for this tile // The type of dust to create when hit
			ItemDrop = ModContent.ItemType<Items.Placeables.Moon.Anorthosite>(); // The item to drop when this tile is mined or broken
			SoundType = SoundID.Tink; // The sound to play when this tile is mined
			SoundStyle = 1; // The same as sound type, basically
							//mineResist = 4f;
							//minPick = 200;
		}
	}
}