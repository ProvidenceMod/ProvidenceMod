using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.ToggleableModifiers
{
	public class Lament : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lament");
			Tooltip.SetDefault("Activates Lament Mode\nRequires an Expert world\nToggleable\nDO NOT USE while fighting a boss");
		}
		public override void SetDefaults()
		{
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.width = 38;
			item.height = 78;
			item.consumable = false;
			item.maxStack = 1;
			item.Providence().customRarity = ProvidenceRarity.Lament;
		}
		public override bool ConsumeItem(Player player) => false;
		public override bool CanUseItem(Player player) => Main.expertMode && !ProvidenceWorld.wrath;
		public override bool UseItem(Player player)
		{
			// Read the description lol
			Main.PlaySound(SoundID.DD2_BetsyDeath);
			if (IsThereABoss().Item1)
			{
				for (int i = 0; i < Main.player.Length; i++)
				{
					if (Main.player[i]?.active == true && !Main.player[i].dead)
						Main.player[i]?.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("You tempted fate."), 999999, 0);
				}
			}
			ProvidenceWorld.lament = !ProvidenceWorld.lament;
			BrinewastesWorld.lament = !BrinewastesWorld.lament;
			if (ProvidenceWorld.wrath && !ProvidenceWorld.lament)
			{
				ProvidenceWorld.wrath = false;
				BrinewastesWorld.wrath = false;
			}
			if (ProvidenceWorld.lament)
				Talk("You feel hollow.", Color.Purple, player.whoAmI);
			else
				Talk("Warmth returns.", Color.Purple, player.whoAmI);
			return true;
		}
	}
}
