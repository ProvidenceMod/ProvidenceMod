using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class DragonTomb : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Dragon Tomb");
      Tooltip.SetDefault("An unbreakable sword housing a dragon's soul. \nIt will only be freed once the sword is cut by its better.");
    }

    public override void SetDefaults()
    {
      item.useTime = 20;
      item.useAnimation = 20;
      item.useStyle = ItemUseStyleID.SwingThrow;
      item.melee = true;
      item.damage = 150;
      item.autoReuse = true;
      item.rare = ItemRarityID.Purple;
      item.UseSound = SoundID.Item1;
      item.Providence().element = -1; // Typeless
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);
      /////
      recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
      recipe.AddIngredient(ItemID.Stinger, 20);
      recipe.AddIngredient(ItemID.JungleSpores, 20);
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
                          /////
      ModRecipe recipe2 = new ModRecipe(mod);
      /////
      recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
      recipe2.AddIngredient(ItemID.Stinger, 20);
      recipe2.AddIngredient(ItemID.JungleSpores, 20);
      recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe2.SetResult(this); //Sets the result of this recipe to this item
      recipe2.AddRecipe(); //Adds the recipe to the mod
                           /////
    }
  }
}