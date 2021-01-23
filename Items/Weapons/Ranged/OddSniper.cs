using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles.Ranged;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Weapons.Ranged
{
  public class OddSniper : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.SniperRifle;

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("OddSniper");
      Tooltip.SetDefault("\"Accurately Inaccurate!\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Musket);
      item.useAmmo = AmmoID.Bullet;
      item.ranged = true;
      item.expert = true;
      item.damage = 300000;
      item.scale = 1f;
      item.useTime = 1;
      item.useAnimation = 1;
      item.reuseDelay = 180;
      item.autoReuse = false;
      item.knockBack = 15;
      item.crit = 99;      
      item.rare = ItemRarityID.Expert;
      item.useStyle = 1;
      item.value = Item.buyPrice (0, 0, 0, 1);
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(359f.InRadians());
      speedX = speed.X; speedY = speed.Y;
      return true;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.SniperRifle, 1);
      recipe.AddIngredient(ItemID.SniperScope, 20);
      recipe.AddTile(TileID.LunarCraftingStation);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}