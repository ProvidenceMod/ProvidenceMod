using ProvidenceMod.Subworld;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Subworld
{
	public class AetherResonator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aether Resonator");
			Tooltip.SetDefault("Creates a rift to the Sentinel Aether");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 26;
			item.height = 38;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.rare = (int) ProvidenceRarity.Orange;
		}
		public override bool UseItem(Player player)
		{
			// Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && !SubworldManager.IsActive<SentinelAetherSubworld>())
				SubworldManager.Enter<SentinelAetherSubworld>(!ProvidenceMod.Instance.subworldVote);
			if (Main.netMode != NetmodeID.MultiplayerClient && !ProvidenceUtils.IsThereABoss().bossExists && SubworldManager.IsActive<SentinelAetherSubworld>())
				SubworldManager.Exit();
			return true;
		}
	}
}
