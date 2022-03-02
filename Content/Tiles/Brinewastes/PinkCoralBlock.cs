using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Tiles.Brinewastes
{
	public class PinkCoralBlock : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileMergeDirt[Type] = true; // Allows the tile to merge with dirt, meaning it will draw the parts of the spritesheet with dirt merging
			Main.tileSolid[Type] = true; // Makes sure the tile counts as solid
										 // Main.tileBlockLight[Type] = true;
			ModTranslation name = CreateMapEntryName(); // Gives the tile a name to display when hovered over on the map
			name.SetDefault("Pink Coral"); // Sets the map hover name
			AddMapEntry(new Color(255, 102, 153), name); // Adds the map entry for this tile with a hover name
			DustType = DustID.Platinum; // TODO: Make a dedicated dust for this tile // The type of dust to create when hit
			ItemDrop = ModContent.ItemType<Providence.Content.Items.Placeables.Brinewastes.PinkCoralBlock>(); // The item to drop when this tile is mined or broken
			SoundType = SoundID.Tink; // The sound to play when this tile is mined
			SoundStyle = 0; // The same as sound type, basically
							//mineResist = 4f;
							//minPick = 200;
		}
	}
}
