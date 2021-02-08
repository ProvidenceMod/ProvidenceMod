using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Melee;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class Confluence : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Confluence");
      Tooltip.SetDefault("\"The culmination of the wild energies of the elements.\"");
    }

    public override string Texture => $"Terraria/Item_{ItemID.TerraBlade}";
    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.TerraBlade);
      item.shoot = ProjectileType<ConfluenceBeam>();
      item.damage = 42;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      damage = item.damage;
      return true;
    }
    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      /////
      recipe.AddIngredient(ItemType<FireSword>());
      recipe.AddIngredient(ItemType<IceSword>());
      recipe.AddIngredient(ItemType<LightningSword>());
      // recipe.AddIngredient(ItemType<WaterSword>());
      recipe.AddIngredient(ItemType<EarthSword>());
      recipe.AddIngredient(ItemType<AirSword>());
      recipe.AddIngredient(ItemType<RadiantSword>());
      recipe.AddIngredient(ItemType<CorruptSword>());
      recipe.AddTile(TileID.Anvils);
      recipe.SetResult(this);
      recipe.AddRecipe();

      ModRecipe recipe2 = new ModRecipe(mod);
      /////
      recipe2.AddIngredient(ItemType<FireSword>());
      recipe2.AddIngredient(ItemType<IceSword>());
      recipe2.AddIngredient(ItemType<LightningSword>());
      // recipe.AddIngredient(ItemType<WaterSword>());
      recipe2.AddIngredient(ItemType<EarthSword>());
      recipe2.AddIngredient(ItemType<AirSword>());
      recipe2.AddIngredient(ItemType<RadiantSword>());
      recipe2.AddIngredient(ItemType<Crimsword>());
      recipe2.AddTile(TileID.Anvils);
      recipe2.SetResult(this);
      recipe2.AddRecipe();
    }
  }
}