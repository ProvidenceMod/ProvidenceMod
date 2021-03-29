using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items;

namespace ProvidenceMod.Items.Accessories
{
	public class ShadowRing : ChaosItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Ring");
			Tooltip.SetDefault("Increases Chaos power by 25% and Chaos capacity by 10.\nReduces Chaos consumed on Chaos Amp usage by 5.\nOnly equippable when Chaos is enabled.");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Pink;
      item.accessory = true;
		}

    public override bool CanEquipAccessory(Player player, int slot) => player.Providence().chaos;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.Providence().maxParityStacks += 10;
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