using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.World;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.ToggleableModifiers
{
	public class TearOfLament : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tear of Lament");
			Tooltip.SetDefault("This crystallized teardrop feels like it's about to shred reality right in your hand...\nActivates Lament Mode when in an Expert world\nCan be toggled at any time before defeating any boss.");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LifeCrystal);
			item.healLife = 0;
			item.width = 32;
			item.height = 32;
			item.consumable = false;
			item.maxStack = 1;
		}
		public override bool ConsumeItem(Player player) => false;

		public override bool CanUseItem(Player player)
		{
			if (Main.expertMode && !IsThereABoss().Item1 && !DownedAnyBoss()) { return true; }
			else
			{
				if (!Main.expertMode)
					Talk("The teardrop is dim; there is not enough chaos in this world (Expert Mode world only).", Color.Purple, player.whoAmI);
				if (DownedAnyBoss())
					Talk("The teardrop is tarnished; this world has already been partially restored (Activate before killing any bosses).", Color.Purple, player.whoAmI);
				if (IsThereABoss().Item1)
					Talk("The teardrop is searing; you are fighting the chaos already (Activate out of combat with a boss).", Color.Purple, player.whoAmI);
				return false;
			}
		}
		public override bool UseItem(Player player)
		{
			Main.PlaySound(SoundID.DD2_BetsyDeath);
			ProvidenceWorld provWrld = GetInstance<ProvidenceWorld>();
			Brinewastes brine = GetInstance<Brinewastes>();
			provWrld.lament = !provWrld.lament;
			brine.lament = !brine.lament;
			if (provWrld.wrath && !provWrld.lament) { provWrld.wrath = false; brine.wrath = false; }
			if (provWrld.lament)
				Talk("You gaze into the teardrop, and feel the unending dread of lament... (Lament Mode ON)", Color.Purple, player.whoAmI);
			else
				Talk("You gaze into the teardrop, and feel notably calmer. (Lament Mode OFF)", Color.Purple, player.whoAmI);
			return true;
		}
	}
}
