using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProvidenceMod.Tiles.SentinelAether
{
	public class AetherResonatorRightPylon : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = false;
			Main.tileLighted[Type] = true;
			Main.tileObsidianKill[Type] = true;
			Main.tileNoFail[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;
			// You need the AnchorData to determine whether the tile can be placed
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 2);
			// This allows the player to place it from above.
			TileObjectData.newTile.Origin = new Point16(1, 0);
			// Copy the data from 3x3, and then...
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			// Change what we need.
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(179, 146, 107));
			dustType = DustID.Platinum;
			drop = ModContent.ItemType<Items.Placeables.SentinelAether.AetherResonatorRightPylon>();
		}
	}
}
