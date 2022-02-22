using System;
using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Structures
{
	[Serializable]
	public class SerializableTile
	{
		public ushort type;
		public ushort wall;
		public byte liquid;
		public ushort sTileHeader;
		public byte bTileHeader;
		public byte bTileHeader2;
		public byte bTileHeader3;
		public short frameX;
		public short frameY;
		public int style = 0;
		public bool isModTile;
		public bool isModWall;
		public string tileName;
		public string wallName;
		//public SerializableTile(Tile copy)
		//{
		//	type = copy.TileType;
		//	wall = copy.wall;
		//	liquid = copy.liquid;
		//	sTileHeader = copy.sTileHeader;
		//	bTileHeader = copy.bTileHeader;
		//	bTileHeader2 = copy.bTileHeader2;
		//	bTileHeader3 = copy.bTileHeader3;
		//	frameX = copy.frameX;
		//	frameY = copy.frameY;
		//	style = copy.collisionType;
		//	ModTile modTile = ModContent.GetModTile(copy.type);
		//	isModTile = modTile != null;
		//	tileName = isModTile ? modTile.Name : "";
		//	ModWall modWall = ModContent.GetModWall(copy.wall);
		//	isModWall = modWall != null;
		//	wallName = isModWall ? modWall.Name : "";
		//}
		//public static Tile STileToTile(SerializableTile tile)
		//{
		//	return new Tile
		//	{
		//		type = tile.isModTile && ProvidenceMod.Instance.GetTile(tile.tileName) != null ? ProvidenceMod.Instance.GetTile(tile.tileName).Type : tile.type,
		//		wall = tile.isModWall && ProvidenceMod.Instance.GetWall(tile.wallName) != null ? ProvidenceMod.Instance.GetTile(tile.wallName).Type : tile.wall,
		//		liquid = tile.liquid,
		//		sTileHeader = tile.sTileHeader,
		//		bTileHeader = tile.bTileHeader,
		//		bTileHeader2 = tile.bTileHeader2,
		//		bTileHeader3 = tile.bTileHeader3,
		//		frameX = tile.frameX,
		//		frameY = tile.frameY
		//	};
		//}
	}
}
