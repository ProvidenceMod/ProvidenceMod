using Microsoft.Xna.Framework;
using ParticleLibrary;
using Providence.Particles;
using Providence.Particles.Portals;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Providence.ProvidenceUtils;

namespace Providence.Content.Tiles.SentinelAether
{
	public class AetherResonatorCore : ModTile
	{
		public override void SetStaticDefaults()
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
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			//// Change what we need.
			//TileObjectData.newTile.Width = 2;
			//TileObjectData.newTile.Height = 3;
			//TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
			//TileObjectData.newTile.CoordinateWidth = 16;
			//TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(179, 146, 107));
			DustType = DustID.Platinum;
			ItemDrop = ModContent.ItemType<Providence.Content.Items.Placeables.SentinelAether.AetherResonatorCore>();
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			//if (!WorldGen.gen && Main.netMode != NetmodeID.MultiplayerClient)
			//	Item.NewItem(i * 16, j * 16, 48, 28, ModContent.ItemType<Items.Placeables.SentinelAether.AetherResonator>(), 1);
		}
		public override bool RightClick(int i, int j)
		{
			ParticleManager.NewParticle(new Vector2(i * 16, j * 16) + new Vector2(24f, -32f), Vector2.Zero, new SentinelAetherPortal(), Color.White, Main.rand.NextFloat(10f, 16f) / 10f);
			//// Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && !SubworldManager.IsActive<Subworld.SentinelAetherSubworld>())
			//	SubworldManager.Enter<Subworld.SentinelAetherSubworld>(!Providence.Instance.subworldVote);
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && SubworldManager.IsActive<Subworld.SentinelAetherSubworld>())
			//	SubworldManager.Exit();
			return true;
		}
	}
}
