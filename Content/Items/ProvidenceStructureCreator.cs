using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleLibrary;
using Providence.Particles;
using Providence.Rarities;
using Providence.Structures;
using Providence.UI.Developer;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Providence.ProvidenceUtils;

namespace Providence.Content.Items
{
	public class ProvidenceStructureCreator : ModItem
	{
		public int x;
		public int y;
		public int x2;
		public int y2;
		public bool flag1;
		public bool flag2;
		public bool append;
		public int divisions;
		public Vector2 cacheSize;
		public Tile[,] cachedTiles;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Providence Structure Creator");
			Tooltip.SetDefault("Don't use if you're not a developer. It could have unintended consequences.\n" +
												 "Holding alt while using this item will attempt to place your recent generation.\n" +
												 "Holding shift while using this item will toggle appending.\n" +
												 "Do NOT use appending if the file does not yet exist." +
												 "Enabling appending will write to the structure file instead of overwriting it.\n" +
												 "Disabling appending will overwrite the structure file." +
												 "Left-click will select coordinates. If two coordinates are already selected, it will overwrite them.\n" +
												 "RIght-click will clear the values if one or less coordinates is selected. Otherwise, it will generate the structure in that area.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.rare = (int)ProvidenceRarity.Purple;
			Item.rare = ModContent.RarityType<Developer>();
			StructureDev.clearAction = ClearValues;
			StructureDev.genAction = GenerateStructure;
		}
		public override bool AltFunctionUse(Player player) => true;
		public void ClearValues()
		{
			x = 0;
			y = 0;
			x2 = 0;
			y2 = 0;
			flag1 = false;
			flag2 = false;
			divisions = 0;
			for (int i = 0; i < 20; i++)
				ParticleManager.NewParticle(Main.MouseWorld, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-360, 361).InRadians()), new GlowParticle(), new Color(218, 70, 70, 0), 0.25f);
			Talk("Values discarded.", new Color(218, 70, 70));
		}
		public void GenerateStructure()
		{
			string filePath = StructureDev.filePath.text + "/" + StructureDev.fileName.text;
			filePath = filePath.Replace("\\", "/");
			Vector2 size = new Vector2(x > x2 ? x - x2 : x2 - x, y > y2 ? y - y2 : y2 - y);
			cacheSize = size;
			Tile[,] tiles = new Tile[(int)size.X + 1, (int)size.Y + 1];
			SerializableTile[,] serializableTiles = new SerializableTile[(int)size.X + 1, (int)size.Y + 1];
			for (int i = 0; i <= size.X; i++)
			{
				for (int j = 0; j <= size.Y; j++)
				{
					tiles[i, j] = Main.tile[flag1 ? x2 + i : x + i, flag2 ? y2 + j : y + j];
				}
			}
			for (int i = 0; i <= size.X; i++)
			{
				for (int j = 0; j <= size.Y; j++)
				{
					//serializableTiles[i, j] = new SerializableTile(tiles[i, j]);
				}
			}
			StructureManager.WriteToBinaryFile(filePath, serializableTiles, false);
			SerializableTile[,] result = StructureManager.ReadFromBinaryFile<SerializableTile[,]>(filePath);
			Tile[,] resultTiles = new Tile[(int)size.X + 1, (int)size.Y + 1];
			for (int i = 0; i <= size.X; i++)
			{
				for (int j = 0; j <= size.Y; j++)
				{
					//resultTiles[i, j] = SerializableTile.STileToTile(result[i, j]);
				}
			}
			cachedTiles = resultTiles;
		}
		public override bool? UseItem(Player player)
		{
			if (Main.keyState.IsKeyDown(Keys.LeftAlt) || Main.keyState.IsKeyDown(Keys.RightAlt))
			{
				int x3 = (int)(Main.MouseWorld.X / 16);
				int y3 = (int)(Main.MouseWorld.Y / 16);
				for (int i = 0; i <= cacheSize.X; i++)
				{
					for (int j = 0; j <= cacheSize.Y; j++)
					{
						WorldGen.PlaceTile(x3 + i, y3 + j, cachedTiles[i, j].TileType, false, false, default, (int) cachedTiles[i, j].BlockType);
						NetMessage.SendTileSquare(-1, x3 + i, y3 + j, 1);
					}
				}
				return true;
			}
			if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
			{
				if (append)
					Talk("Appending disabled. File will now be overwritten.", new Color(218, 70, 70));
				if (!append)
					Talk("Appending enabled. FIle will now be written to.", new Color(218, 70, 70));
				append = !append;
				return true;
			}
			if (player.altFunctionUse == 2 && x == 0 && y == 0 && x2 == 0 && y2 == 0)
			{
				for (int i = 0; i < 20; i++)
					ParticleManager.NewParticle(Main.MouseWorld, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-360, 361).InRadians()), new GlowParticle(), new Color(218, 70, 70, 0), 0.25f);
				Talk("No values to discard.", new Color(218, 70, 70));
				return true;
			}
			if (player.altFunctionUse == 2 && x != 0 && y != 0 && x2 == 0 && y2 == 0)
			{
				x = 0;
				y = 0;
				x2 = 0;
				y2 = 0;
				flag1 = false;
				flag2 = false;
				divisions = 0;
				for (int i = 0; i < 20; i++)
					ParticleManager.NewParticle(Main.MouseWorld, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-360, 361).InRadians()), new GlowParticle(), new Color(218, 70, 70, 0), 0.25f);
				Talk("Values discarded.", new Color(218, 70, 70));
				return true;
			}
			if (player.altFunctionUse == 2 && x != 0 && y != 0 && x2 != 0 && y2 != 0)
			{
				Talk($"Generating structure...", new Color(218, 70, 70));
				GenerateStructure();
				Dust.QuickBox(new Vector2(x + (flag1 ? 1 : 0), y + (flag2 ? 1 : 0)) * 16, new Vector2(x2 + (!flag1 ? 1 : 0), y2 + (!flag2 ? 1 : 0)) * 16, divisions, new Color(218, 70, 70), null);
				return true;
			}
			if (player.altFunctionUse != 2 && x != 0 && y != 0 && x2 != 0 && y2 != 0)
			{
				x = 0;
				y = 0;
				x2 = 0;
				y2 = 0;
				flag1 = false;
				flag2 = false;
				divisions = 0;
				for (int i = 0; i < 20; i++)
					ParticleManager.NewParticle(Main.MouseWorld, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-360, 361).InRadians()), new GlowParticle(), new Color(218, 70, 70, 0), 0.25f);
				Talk("Old values discarded.", new Color(218, 70, 70));
			}
			if (x == 0 && y == 0)
			{
				x = (int)(Main.MouseWorld.X / 16);
				y = (int)(Main.MouseWorld.Y / 16);
				Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, new Color(218, 70, 70), null);
				Talk($"First coordinate set to [{x}, {y}]. Right-click to discard.", new Color(218, 70, 70));
				return true;
			}
			if (x != 0 && y != 0 && x2 == 0 && y2 == 0)
			{
				x2 = (int)(Main.MouseWorld.X / 16);
				y2 = (int)(Main.MouseWorld.Y / 16);
				Dust.QuickBox(new Vector2(x2, y2) * 16, new Vector2(x2 + 1, y2 + 1) * 16, 2, new Color(218, 70, 70), null);
				Talk($"Final coordinate set to [{x2}, {y2}]. Right-click to generate structure. Left-click to discard and select new coordinates.", new Color(218, 70, 70));
				Vector2 v1 = new Vector2(x, y) * 16;
				Vector2 v2 = new Vector2(x2 + 1, y2 + 1) * 16;
				flag1 = v2.X <= v1.X;
				flag2 = v2.Y <= v1.Y;
				Vector2 v3 = !(flag1 || flag2) ? v2 - v1 : v1 - v2;
				divisions = Math.Abs(v3.X) > Math.Abs(v3.Y) ? (int)((Math.Abs(v3.X) / 2) + 1) : (int)((Math.Abs(v3.Y) / 2) + 1);
				Dust.QuickBox(new Vector2(x + (flag1 ? 1 : 0), y + (flag2 ? 1 : 0)) * 16, new Vector2(x2 + (!flag1 ? 1 : 0), y2 + (!flag2 ? 1 : 0)) * 16, divisions, new Color(218, 70, 70), null);
			}
			return true;
		}
	}
}
