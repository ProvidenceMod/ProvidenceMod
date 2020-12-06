using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items;

namespace UnbiddenMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AcolyteHead : ClericItem
	{
		public override void SetStaticDefaults() {
      DisplayName.SetDefault("Acolyte Hood");
			Tooltip.SetDefault("Increases cleric damage by 5%");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.defense = 1;
		}
    public override void UpdateEquip(Player player) {
			player.Unbidden().cleric += 0.05f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}