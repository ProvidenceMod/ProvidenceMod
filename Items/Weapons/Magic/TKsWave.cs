using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Projectiles.Magic;

namespace ProvidenceMod.Items.Weapons.Magic
{
  public class TKsWave : ModItem
  {
    public override string Texture => $"Terraria/Item_{ItemID.Boomstick}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("TK's Wave");
      Tooltip.SetDefault("Ride the wave, dude!");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Boomstick);
      item.ranged = false;
      item.magic = true;
      item.mana = 10;
      item.useAmmo = AmmoID.None;
      item.shoot = ProjectileType<TKWave>();
      item.shootSpeed = 16f;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      for (int i = 0; i < 5; i++)
      {
        Vector2 speed = new Vector2(speedX, speedY);
        _ = Projectile.NewProjectile(position.RandomPointNearby(56f), speed, type, damage, knockBack, item.owner);
      }
      return false;
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.DirtBlock, 10); //Adds ingredients
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }
  }
}