﻿using Providence.Content.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.World
{
	public class WorldBuilding : ModSystem
	{
		/// <summary>
		/// Generates ore
		/// </summary>
		/// <param name="type">Type of ore</param>
		/// <param name="frequency">Frequency, where 1f is all tiles</param>
		/// <param name="steps">Steps to take</param>
		/// <param name="lowRich">Low cap for deposit</param>
		/// <param name="highRich">High cap for deposit</param>
		/// <param name="lowY">Lowest point to generate to</param>
		/// <param name="highY">Highest point to generate from</param>
		public static void BuildOre(int type, float frequency, int steps, int lowRich, int highRich, float lowY, float highY)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return;
			int maxTilesX = Main.maxTilesX;
			int maxTilesY = Main.maxTilesY;
			for (int i = 0; i < maxTilesX * maxTilesY * frequency; i++)
			{
				int posX = WorldGen.genRand.Next(0, maxTilesX);
				int posY = WorldGen.genRand.Next((int)(maxTilesY * lowY), (int)(maxTilesY * highY));
				WorldGen.OreRunner(posX, posY, WorldGen.genRand.Next(lowRich, highRich), steps, (ushort)type);
			}
		}
		public static void RemoveAllOres()
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return;
			int maxTilesX = Main.maxTilesX;
			int maxTilesY = Main.maxTilesY;
			for (int i = 0; i < maxTilesX; i++)
			{
				for (int j = 0; j < maxTilesY; j++)
				{
					if (Main.tile[i, j].TileType == TileType<ZephyrOre>())
					{
						WorldGen.KillTile(i, j);
						WorldGen.Place1x1(i, j, TileID.Dirt);
					}
				}
			}
		}
	}
}
