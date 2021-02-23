using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class Crimsword : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Necrotic Sword");
      Tooltip.SetDefault("A depraved sword\nCan very rarely deal triple damage!");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.PlatinumBroadsword);
      item.Providence().element = 7; // Necrotic
      item.autoReuse = true;
      // item.shoot = true; // Commenting this until we have a projectile to shoot
    }

    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      if (Main.rand.Next(100) == 0) // 1% chance
      {
        target.StrikeNPC(damage * 2, knockBack, player.direction); // Hit them with double damage, totalling to triple
        Main.PlaySound(SoundID.Item107, target.position);
      }
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);
      /////
      recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
      recipe.AddIngredient(ItemID.Torch, 25);
      recipe.AddIngredient(ItemID.Gel, 99);
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
                          /////
      ModRecipe recipe2 = new ModRecipe(mod);
      /////
      recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
      recipe2.AddIngredient(ItemID.Torch, 25);
      recipe2.AddIngredient(ItemID.Gel, 99);
      recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe2.SetResult(this); //Sets the result of this recipe to this item
      recipe2.AddRecipe(); //Adds the recipe to the mod
                           /////
    }
  }
}