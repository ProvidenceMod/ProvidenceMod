using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles.Ranged;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Weapons.Ranged
{
  public class GreSMG : ModItem
  {

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("GreSMG");
      Tooltip.SetDefault("\"Wait, this doesn't even fit!\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Minishark);
      item.useAmmo = AmmoID.Bullet;
      item.ranged = true;
      item.damage = 40;
      item.scale = 1f;
      item.useTime = 5;
      item.useAnimation = 5;
      item.reuseDelay = 3;
      item.autoReuse = true;
      item.knockBack = 2;
      item.width = 30;
      item.height = 16;
      item.rare = ItemRarityID.Green;
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
      recipe.AddIngredient(ItemID.TitaniumBar, 10);
      recipe.AddIngredient(ItemID.CursedFlame, 15);
      recipe.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe.AddTile(TileID.MythrilAnvil);
      recipe.SetResult(this);
      recipe.AddRecipe();

      ModRecipe recipe2 = new ModRecipe(mod);
      recipe2.AddIngredient(ItemID.TitaniumBar, 10);
      recipe2.AddIngredient(ItemID.Ichor, 15);
      recipe2.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe2.AddTile(TileID.MythrilAnvil);
      recipe2.SetResult(this);
      recipe2.AddRecipe();

      ModRecipe recipe3 = new ModRecipe(mod);
      recipe3.AddIngredient(ItemID.AdamantiteBar, 10);
      recipe3.AddIngredient(ItemID.CursedFlame, 15);
      recipe3.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe3.AddTile(TileID.MythrilAnvil);
      recipe3.SetResult(this);
      recipe3.AddRecipe();

      ModRecipe recipe4 = new ModRecipe(mod);
      recipe4.AddIngredient(ItemID.AdamantiteBar, 10);
      recipe4.AddIngredient(ItemID.Ichor, 15);
      recipe4.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe4.AddTile(TileID.MythrilAnvil);
      recipe4.SetResult(this);
      recipe4.AddRecipe();
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
    {
      target.AddBuff(BuffID.CursedInferno, 480);
    }
  }
}
//Intended to be Pre-Mech-Boss, potentially post if needed.