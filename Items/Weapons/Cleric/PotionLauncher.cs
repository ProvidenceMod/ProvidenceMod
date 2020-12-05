using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UnbiddenMod.Items.Weapons.Cleric
{
  public class PotionLauncher : ClericItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Potion Launcher");
      Tooltip.SetDefault("Launch potions at allies to heal them!\nSplits healing and cooldown across multiple targets evenly");
    }

    public override void SetDefaults()
    {
      item.width = 40;
      item.height = 16;
      item.useStyle = 5;
      item.knockBack = 1f;
      item.damage = 100;
      item.rare = 4;
      item.autoReuse = true;
      item.useAnimation = 30;
      item.useTime = 30;
      item.useTurn = true;
      item.shoot = mod.ProjectileType("YeetPotion");
      item.shootSpeed = 16f;
      item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.DirtBlock, 1);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}