using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ProvidenceMod.Projectiles.Melee;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class LightningSword : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lightning Sword");
      Tooltip.SetDefault("\"A sword of pure Lightning\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.PlatinumBroadsword);
      item.Providence().element = 2; // Lightning
      item.autoReuse = true;
      item.shootSpeed = 10f;
      // item.shoot = true; // Commenting this until we have a projectile to shoot
    }

    public override bool AltFunctionUse(Player player)
    {
      return true;
    }

    public override bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        item.shoot = ModContent.ProjectileType<LightningSwordP>();
      }
      else
      {
        item.shoot = ProjectileID.None;
      }
      return base.CanUseItem(player);
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
      recipe.AddIngredient(ItemID.RainCloud, 50);
      recipe.AddIngredient(ItemID.Topaz, 10);
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod

      ModRecipe recipe2 = new ModRecipe(mod);

      recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
      recipe2.AddIngredient(ItemID.RainCloud, 50);
      recipe2.AddIngredient(ItemID.Topaz, 10);
      recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe2.SetResult(this); //Sets the result of this recipe to this item
      recipe2.AddRecipe(); //Adds the recipe to the mod
    }
  }
}