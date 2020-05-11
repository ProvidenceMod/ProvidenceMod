using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Code.Items.Armor;

namespace UnbiddenMod.Code.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class StardustArmorLegs : ModItem
	{
		public override void SetStaticDefaults() {
      DisplayName.SetDefault("Stardust Leggings");
			Tooltip.SetDefault("\"The power of the cosmos emboldens you.\"\n+10% movement speed\n+20 mana, +1 Max Minions");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.defense = 30;
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.1f;
      player.buffImmune[BuffID.Frozen] = true;
      player.buffImmune[BuffID.Burning] = true;
      player.buffImmune[BuffID.Stoned] = true;
      player.statManaMax2 += 20;
      player.maxMinions++;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(ItemID.Workbench, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}