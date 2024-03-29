using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Subworld
{
	public class TestingSubworldEnter : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.Extractinator;

		public override void SetStaticDefaults() => Tooltip.SetDefault("Use to enter a subworld. Only works with 'SubworldLibrary' Mod enabled");

		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.width = 34;
			Item.height = 38;
			Item.rare = 12;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item1;
		}

		public override bool? UseItem(Player player)
		{
			// Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && !SubworldManager.IsActive<BrinewastesSubworld>())
			//	SubworldManager.Enter<BrinewastesSubworld>(!Providence.Instance.subworldVote);
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && SubworldManager.IsActive<BrinewastesSubworld>())
			//	SubworldManager.Exit();
			return true;
		}

		public override void AddRecipes()
		{
		}
	}
}