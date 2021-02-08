using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles;
using static Terraria.ModLoader.ModContent;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class StellarTransfusion : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Stellar Transfusion");
      Tooltip.SetDefault("Throw knives that steal health\nKnives bounce off walls and penetrate one (1) target");
    }

    public override void SetDefaults()
    {
      item.damage = 60;
      item.value = Item.buyPrice(0, 10, 0, 0);
      item.rare = 12;
      item.autoReuse = true;
      item.useTurn = true;
      item.shoot = mod.ProjectileType("VampireKnifeClone");
      item.useTime = 13;
      item.useAnimation = 13;
      item.useStyle = ItemUseStyleID.HoldingOut;
      item.scale = 1.0f;
      item.melee = true;
      item.autoReuse = true;
      item.shootSpeed = 16f;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      const int spread = 10;
      const float spreadMult = 0.2f;
      int numOfKnives = Main.rand.Next(5) + 5; // Determines how many knives to shoot (6-12 range)
      for (int i = 0; i < numOfKnives; i++)
      {
        float vX = speedX + ((float)Main.rand.Next(-spread, spread + 1) * spreadMult);
        float vY = speedY + ((float)Main.rand.Next(-spread, spread + 1) * spreadMult);
        Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
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