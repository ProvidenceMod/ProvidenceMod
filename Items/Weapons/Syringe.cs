using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UnbiddenMod.Items.Weapons {
  public class Syringe : SupportItem {
    public override void SetStaticDefaults () {
      DisplayName.SetDefault ("Syringe");
      Tooltip.SetDefault ("\"Stab allies to boost their next potion\"\n\"Stab enemies to increase their damage taken by 20%\"");
    }

    public override void SetDefaults () {
      item.width = 45;
      item.height = 45;
      item.useStyle = 3;
      item.knockBack = 1f;
      item.damage = 125;
      item.rare = 4;
      item.autoReuse = true;
      item.useAnimation = 30;
      item.useTime = 30;
      item.useTurn = true;
      item.UseSound = (LegacySoundStyle) 2;
    }

    public override void OnHitNPC (Player player, NPC target, int damage, float knockBack, bool crit) {
      // TODO: Make a debuff to increase their damage taken
    }

    public override void AddRecipes () {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe (mod);

      recipe.AddIngredient (ItemID.PlatinumBroadsword, 1);
      recipe.AddIngredient (ItemID.Torch, 25);
      recipe.AddIngredient (ItemID.Gel, 99);
      recipe.AddTile (TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult (this); //Sets the result of this recipe to this item
      recipe.AddRecipe (); //Adds the recipe to the mod

      ModRecipe recipe2 = new ModRecipe (mod);

      recipe2.AddIngredient (ItemID.GoldBroadsword, 1);
      recipe2.AddIngredient (ItemID.Torch, 25);
      recipe2.AddIngredient (ItemID.Gel, 99);
      recipe2.AddTile (TileID.Anvils); //The tile you craft this sword at
      recipe2.SetResult (this); //Sets the result of this recipe to this item
      recipe2.AddRecipe (); //Adds the recipe to the mod
    }
  }
}