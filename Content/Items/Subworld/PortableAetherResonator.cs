using Providence.Subworld;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Items.Subworld
{
	public class PortableAetherResonator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Portable Aether Resonator");
			Tooltip.SetDefault("Creates a rift to the Sentinel Aether");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.width = 26;
			Item.height = 38;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = (int)ProvidenceRarity.Orange;
		}
		public override bool? UseItem(Player player)
		{
			// Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && !SubworldManager.IsActive<SentinelAetherSubworld>())
			//	SubworldManager.Enter<SentinelAetherSubworld>(!Providence.Instance.subworldVote);
			//if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && SubworldManager.IsActive<SentinelAetherSubworld>())
			//	SubworldManager.Exit();
			return true;
		}
	}
}
