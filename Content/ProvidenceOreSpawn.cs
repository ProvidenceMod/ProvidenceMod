using Providence.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Microsoft.Xna.Framework;
using Providence.Systems;

using Providence.Rarities;

namespace Providence.Content
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
			Item.consumable = false;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.rare = RarityType<Developer>();
			Item.useStyle = ItemUseStyleID.HoldUp;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Talk("Resetting all variables...", new Color(218, 70, 70));
				WorldFlags.downedCaelus = false;
				WorldFlags.downedVerglasLeviathan = false;
				WorldFlags.downedAstrid = false;
				WorldFlags.downedFireAncient = false;
				WorldFlags.downedLysandria = false;
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
