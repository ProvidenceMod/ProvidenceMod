using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModNet;

namespace ProvidenceMod.Subworld
{
	public class TestingSubworldEnter : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.Extractinator;

		public override void SetStaticDefaults() => Tooltip.SetDefault("Use to enter a subworld. Only works with 'SubworldLibrary' Mod enabled");

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 34;
			item.height = 38;
			item.rare = 12;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item1;
		}

		public override bool UseItem(Player player)
		{
			// Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && !SubworldManager.IsActive<BrinewastesSubworld>())
				SubworldManager.Enter<BrinewastesSubworld>(!ProvidenceMod.Instance.subworldVote);
			if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && SubworldManager.IsActive<BrinewastesSubworld>())
				SubworldManager.Exit();
			return true;
		}

		public override void AddRecipes()
		{
		}
	}
}