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
      Tooltip.SetDefault("\"What did you expect smashing a bunch of scrap together?\"");  
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Minishark);
      item.useAmmo = AmmoID.Bullet;
      item.ranged = true;
      item.damage = 20;
      item.scale = 1f;
      item.useTime = 6;
      item.useAnimation = 6;
      item.reuseDelay = 3;
      item.autoReuse = true;
      item.knockBack = 2;
      item.width = 30;
      item.height = 16;
      item.rare = ItemRarityID.Blue;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(9f.InRadians());
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
      recipe.AddIngredient(ItemID.FlintlockPistol, 1);
      recipe.AddIngredient(ItemID.Gel, 50);
      recipe.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe.AddIngredient(ItemID.LeadBar, 10);
      recipe.AddIngredient(ItemID.IceBlock, 25);
      recipe.AddTile(TileID.Anvils);
      recipe.SetResult(this);
      recipe.AddRecipe();

      ModRecipe recipe2 = new ModRecipe(mod);
      recipe2.AddIngredient(ItemID.FlintlockPistol, 1);
      recipe2.AddIngredient(ItemID.Gel, 50);
      recipe2.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe2.AddIngredient(ItemID.IronBar, 10);
      recipe2.AddIngredient(ItemID.IceBlock, 25);
      recipe2.AddTile(TileID.Anvils);
      recipe2.SetResult(this);
      recipe2.AddRecipe();
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
    {
      target.AddBuff(BuffID.Frostburn, 240);
    }
  }
}
//pre-wof alternative to minishark, may need tweaking.