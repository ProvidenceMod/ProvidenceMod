using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod.Items.Accessories
{
  public class MicitBangle : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Micit Bangle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
      Tooltip.SetDefault("Increases parried projectile damage by 250%.\n\"A bangle made of Micit. It exudes immense power.\"");
    }
    public override void SetDefaults()
    {
      item.width = 30;
      item.height = 24;
      item.value = 10000;
      item.rare = 12;
      item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.Unbidden().micitBangle = true;
      player.Unbidden().parryCapable = true;
      player.Unbidden().parryType = ParryTypeID.Support;
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