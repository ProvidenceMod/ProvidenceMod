using ProvidenceMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;

namespace ProvidenceMod.Items
{
	public class ProvidenceOreSpawn : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Providence");
			Tooltip.SetDefault("Left-click removes and then respawns all mod ores.\nRight-click sets all downed boss variables to false.\nDon't use on a world you want to play normally");
		}
		public override void SetDefaults()
		{
			item.consumable = false;
			item.width = 32;
			item.height = 32;
			item.useTime = 20;
			item.useAnimation = 20;
			item.rare = (int)ProvidenceRarity.Purple;
			item.Providence().customRarity = ProvidenceRarity.Developer;
			item.useStyle = ItemUseStyleID.HoldingUp;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				Talk("Resetting all variables...", new Color(218, 70, 70));
				ProvidenceWorld.downedCaelus = false;
				ProvidenceWorld.downedVerglasLeviathan = false;
				ProvidenceWorld.downedAstrid = false;
				ProvidenceWorld.downedFireAncient = false;
				ProvidenceWorld.downedLysandria = false;
				BrinewastesWorld.downedCaelus = false;
				BrinewastesWorld.downedVerglasLeviathan = false;
				BrinewastesWorld.downedAstrid = false;
				BrinewastesWorld.downedFireAncient = false;
				BrinewastesWorld.downedLysandria = false;
				Talk("Complete.", new Color(218, 70, 70));
				return true;
			}
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return false;
			Talk("Removing all ores...", new Color(218, 70, 70));
			WorldBuilding.RemoveAllOres();
			Talk("Building all ores...", new Color(218, 70, 70));
			Talk("Powerful air suffuses into the ground...", new Color(158, 186, 226));
			WorldBuilding.BuildOre(TileType<Tiles.Ores.ZephyrOre>(), 0.00005f, 1, 10, 13, 0.35f, 0.6f);
			Talk("Complete.", new Color(218, 70, 70));
			return true;
		}
	}
}
