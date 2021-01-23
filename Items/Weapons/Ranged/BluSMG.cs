using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles.Ranged;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Weapons.Ranged
{
  public class BluSMG : ModItem
  {

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("BluSMG");
      Tooltip.SetDefault("\"Missing the e, for some reason.\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Minishark);
      item.useAmmo = AmmoID.Bullet;
      item.ranged = true;
      item.damage = 42;
      item.scale = 1f;
      item.useTime = 4;
      item.useAnimation = 4;
      item.reuseDelay = 2;
      item.autoReuse = true;
      item.knockBack = 2;
      item.width = 30;
      item.height = 16;
      item.rare = ItemRarityID.Blue;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(7f.InRadians());
      speedX = speed.X; speedY = speed.Y;
      return true;
    }

    public override Vector2? HoldoutOffset()
    {
      return new Vector2(-16, 4);
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.Gel, 1);
      recipe.AddTile(TileID.WorkBenches);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}