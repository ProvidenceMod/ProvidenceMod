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
			Tooltip.SetDefault("Removes and then respawns all mod ores.\nDon't use on a world you wanna play normally");
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
		public override bool UseItem(Player player)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return false;
			Talk("Removing all ores...", new Color(218, 70, 70));
			WorldBuilding.RemoveAllOres();
			Talk("Building all ores...", new Color(218, 70, 70));
			WorldBuilding.BuildOre(TileType<Tiles.Ores.ZephyrOre>(), 0.00005f, 1, 10, 13, 0.35f, 0.6f);
			Talk("Complete.", new Color(218, 70, 70));
			return true;
		}
	}
}
