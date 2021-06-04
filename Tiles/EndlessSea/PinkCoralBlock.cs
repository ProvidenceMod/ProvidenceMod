using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Tiles.EndlessSea
{
	public class PinkCoralBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[Type] = true; // Allows the tile to merge with dirt, meaning it will draw the parts of the spritesheet with dirt merging
			Main.tileSolid[Type] = true; // Makes sure the tile counts as solid
																	 // Main.tileBlockLight[Type] = true;
			ModTranslation name = CreateMapEntryName(); // Gives the tile a name to display when hovered over on the map
			name.SetDefault("Pink Coral"); // Sets the map hover name
			AddMapEntry(new Color(255, 102, 153), name); // Adds the map entry for this tile with a hover name
			dustType = DustID.Platinum; // TODO: Make a dedicated dust for this tile // The type of dust to create when hit
			drop = ModContent.ItemType<Items.Placeable.EndlessSea.PinkCoralBlock>(); // The item to drop when this tile is mined or broken
			soundType = SoundID.Tink; // The sound to play when this tile is mined
			soundStyle = 0; // The same as sound type, basically
											//mineResist = 4f;
											//minPick = 200;
		}
	}
}