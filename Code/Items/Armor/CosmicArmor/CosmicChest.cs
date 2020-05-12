using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Code.Items.Armor.CosmicArmor;

namespace UnbiddenMod.Code.Items.Armor.CosmicArmor
{
	[AutoloadEquip(EquipType.Body)]
	public class CosmicChest : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Cosmic Breastplate");
			Tooltip.SetDefault("\"The power of the cosmos protects you.\"\n+40 Mana, +2 Max Minions");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.defense = 60;
		}

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.MoonLeech] = true;
			player.statManaMax2 += 40;
			player.maxMinions++;
			player.maxMinions++;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}