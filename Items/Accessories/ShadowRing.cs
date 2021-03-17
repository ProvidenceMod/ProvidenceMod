using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items;

namespace ProvidenceMod.Items.Accessories
{
	public class ShadowRing : ShadowItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Ring");
			Tooltip.SetDefault("Increases shadowmancy power by 25% and shadow capacity by 10.\nReduces shadow consumed on Shadow Amp usage by 5.\nOnly equippable when shadowmancy is enabled.");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Pink;
      item.accessory = true;
		}

    public override bool CanEquipAccessory(Player player, int slot) => player.Providence().shadowmancy;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.Providence().shadowDamage++;
      player.Providence().shadowConsumedOnUse -= 5;
      player.Providence().maxShadowStacks += 10;
    }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddIngredient(ItemID.FragmentVortex, 10);
			recipe.AddIngredient(ItemID.FragmentNebula, 10);
			recipe.AddIngredient(ItemID.FragmentStardust, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}