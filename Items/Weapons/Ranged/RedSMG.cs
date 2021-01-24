using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles.Ranged;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Weapons.Ranged
{
  public class RedSMG : ModItem
  {

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("RedSMG");
      Tooltip.SetDefault("\"The Perfect SMG...\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Minishark);
      item.useAmmo = AmmoID.Bullet;
      item.ranged = true;
      item.damage = 80;
      item.scale = 1f;
      item.useTime = 4;
      item.useAnimation = 4;
      item.reuseDelay = 2;
      item.autoReuse = true;
      item.knockBack = 2;
      item.width = 30;
      item.height = 16;
      item.rare = ItemRarityID.Red;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(5f.InRadians());
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
      recipe.AddIngredient(ItemType<BluSMG>(), 1);
      recipe.AddIngredient(ItemType<GreSMG>(), 1);
      recipe.AddIngredient(ItemID.ShadowFlameBow, 1);
      recipe.AddIngredient(ItemID.SoulofFright, 1);
      recipe.AddIngredient(ItemID.SoulofMight, 1);
      recipe.AddIngredient(ItemID.SoulofSight, 1);
      recipe.AddTile(TileID.MythrilAnvil);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
    {
      target.AddBuff(BuffID.Frostburn, 600);
      target.AddBuff(BuffID.CursedInferno, 600);
      target.AddBuff(BuffID.OnFire, 600);
      target.AddBuff(BuffID.ShadowFlame, 600);
    }
  }
}
//intended to be Post-mech boss, possibly even post Plantera. 