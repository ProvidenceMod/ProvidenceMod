using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod.Items.Weapons.Melee
{
  public class RadiantSword : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Radiant Sword");
      Tooltip.SetDefault("\"An exalted sword\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.PlatinumBroadsword);
      item.GetGlobalItem<UnbiddenGlobalItem>().element = 6; // Radiant
      item.autoReuse = true;
      // item.shoot = true; // Commenting this until we have a projectile to shoot
    }

    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      if (Main.rand.Next(8) == 0) // 12.5% chance
      {
        target.AddBuff(189, 15, true); // Daybroken for 1 tick (extra 25 damage)
      }
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);
      /////
      recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
      recipe.AddIngredient(ItemID.FallenStar, 25);
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
                          /////
      ModRecipe recipe2 = new ModRecipe(mod);
      /////
      recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
      recipe2.AddIngredient(ItemID.FallenStar, 25);
      recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe2.SetResult(this); //Sets the result of this recipe to this item
      recipe2.AddRecipe(); //Adds the recipe to the mod
                           /////
    }
  }
}