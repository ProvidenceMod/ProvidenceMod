using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.World;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.ToggleableModifiers
{
	public class EyeOfWrath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Wrath");
			Tooltip.SetDefault("This crystallized eyeball burns with immense fury...\nActivates Wrath Mode when in an Expert world with Lament Mode active\nCan be toggled at any time before defeating any boss.");
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
			if (Main.expertMode && !IsThereABoss().Item1 && !DownedAnyBoss() && ProvidenceWorld.lament) { return true; }
			else
			{
				if (!Main.expertMode)
					Talk("The eye is dim; there is not enough chaos in this world (Expert Mode world only).", Color.Red, player.whoAmI);
				if (DownedAnyBoss())
					Talk("The eye is tarnished; this world has already been partially restored (Activate before killing any bosses).", Color.Red, player.whoAmI);
				if (IsThereABoss().Item1)
					Talk("The eye is searing; you are fighting the chaos already (Activate out of combat with a boss).", Color.Red, player.whoAmI);
				if (!ProvidenceWorld.lament)
					Talk("The eye is ready, but you don't dare gaze into it yet (Activate with Lament Mode active).", Color.Red, player.whoAmI);
				return false;
			}
		}
		public override bool UseItem(Player player)
		{
			Main.PlaySound(SoundID.DD2_BetsyDeath);
			ProvidenceWorld.wrath = !ProvidenceWorld.wrath;
			Brinewastes.wrath = !Brinewastes.wrath;
			if (ProvidenceWorld.wrath)
				Talk("You gaze into the eye, and your lament turns to wrath... (Wrath Mode ON)", Color.Purple, player.whoAmI);
			else
				Talk("You gaze into the eye, and you return to lamenting. (Wrath Mode OFF)", Color.Purple, player.whoAmI);
			return true;
		}
	}
}
