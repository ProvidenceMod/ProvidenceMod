using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Melee;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class EightGodsBlade : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Eight-God Blade");
      Tooltip.SetDefault("This sword was made from the tears of the eight fallen angels.");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.WoodenSword);
      item.damage = 8000;
      item.width = 90;
      item.height = 90;
      item.value = Item.buyPrice(0, 10, 0, 0);
      item.rare = 12;
      item.useTime = 13;
      item.useAnimation = 13;
      item.scale = 1.0f;
      item.melee = true;
      item.autoReuse = true;
      item.useTurn = true;
      item.shoot = ProjectileType<MoonBlast>();
      item.shootSpeed = 16f;
      // item.shoot = true; // Commenting this until we have a projectile to shoot
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      const int numberProjectiles = 3; // 4 or 5 shots
      for (int i = 0; i < numberProjectiles; i++)
      {
        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(15f.InRadians());
        // If you want to randomize the speed to stagger the projectiles
        float scale = 1f - (Main.rand.NextFloat() * .1f);
        perturbedSpeed *= scale;
        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
      }
      return false; // return false because we don't want tModContent to shoot projectile
    }

    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      // Caps potential healing at 1% of max health per hit.
      int healingAmount = damage / 60 >= player.statLifeMax / 100 ? player.statLifeMax / 100 : damage / 60;

      // Actually heals, and gives the little green numbers pop up
      player.statLife += healingAmount;
      player.HealEffect(healingAmount, true);
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