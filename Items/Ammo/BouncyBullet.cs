using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod;

namespace ProvidenceMod.Items.Ammo
{
  public class BouncyBullet : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bouncy Bullet");
      Tooltip.SetDefault("Pinball, anyone?");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.MusketBall);
      item.width = 8;
      item.height = 16;
      item.value = Item.buyPrice(0, 0, 1, 0);
      item.shoot = ProjectileType<Projectiles.Ranged.BouncyBullet>();
      item.shootSpeed = 8f;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.PinkGel, 1);
      recipe.AddIngredient(ItemID.SilverBullet, 1);
      recipe.AddTile(TileID.Solidifier);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}