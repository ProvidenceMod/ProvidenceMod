﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Providence.Content.Tiles.SentinelAether
{
	internal class ZephyrPlatform : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.Platforms[Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			AddMapEntry(new Color(179, 146, 107));
			DustType = DustID.Dirt;
			ItemDrop = ModContent.ItemType<Providence.Content.Items.Placeables.SentinelAether.ZephyrPlatform>();
			AdjTiles = new int[] { TileID.Platforms };
			//// Properties
			//Main.tileLighted[Type] = true;
			//Main.tileFrameImportant[Type] = true;
			//Main.tileSolidTop[Type] = true;
			//Main.tileSolid[Type] = true;
			//Main.tileNoAttach[Type] = true;
			//Main.tileTable[Type] = true;
			//Main.tileLavaDeath[Type] = true;
			//TileID.Sets.Platforms[Type] = true;

			//AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			//AddMapEntry(new Color(117, 126, 199));

			//dustType = DustID.Platinum;
			//drop = ModContent.ItemType<Items.Placeables.SentinelAether.ZephyrPlatform>();
			//adjTiles = new int[] { TileID.Platforms };
			//disableSmartCursor = true;

			//TileObjectData.newTile.CoordinateHeights = new[] { 16 };
			//TileObjectData.newTile.CoordinateWidth = 16;
			//TileObjectData.newTile.CoordinatePadding = 2;
			//TileObjectData.newTile.StyleHorizontal = true;
			//TileObjectData.newTile.StyleMultiplier = 27;
			//TileObjectData.newTile.StyleWrapLimit = 27;
			//TileObjectData.newTile.UsesCustomCanPlace = false;
			//TileObjectData.newTile.LavaDeath = true;
			//TileObjectData.addTile(Type);
		}

		public override void PostSetDefaults() => Main.tileNoSunLight[Type] = false;
		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
	}
}
