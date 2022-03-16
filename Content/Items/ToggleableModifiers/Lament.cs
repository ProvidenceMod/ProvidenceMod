using Microsoft.Xna.Framework;
using Providence.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.ToggleableModifiers
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
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.width = 38;
			Item.height = 78;
			Item.consumable = false;
			Item.maxStack = 1;
			Item.rare = RarityType<Rarities.Lament>();
		}
		public override bool ConsumeItem(Player player) => false;
		public override bool CanUseItem(Player player) => Main.expertMode && !WorldFlags.wrath;
		public override bool? UseItem(Player player)
		{
			// Read the description lol
			SoundEngine.PlaySound(SoundID.DD2_BetsyDeath);
			if (IsThereABoss().Item1)
			{
				for (int i = 0; i < Main.player.Length; i++)
				{
					if (Main.player[i]?.active == true && !Main.player[i].dead)
						Main.player[i]?.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("You tempted fate."), 999999, 0);
				}
			}
			WorldFlags.lament = !WorldFlags.lament;
			if (WorldFlags.wrath && !WorldFlags.lament)
				WorldFlags.wrath = false;
			if (WorldFlags.lament)
				Talk("You feel hollow.", Color.Purple, player.whoAmI);
			else
				Talk("Warmth returns.", Color.Purple, player.whoAmI);
			return true;
		}
	}
}
