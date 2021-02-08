using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProvidenceMod.Items.Weapons.Cleric
{
  public class Syringe : ClericItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Syringe");
      Tooltip.SetDefault("Stab allies to boost their next potion\nStab enemies to increase their damage taken by 20% for 5 seconds");
    }

    public override void SetDefaults()
    {
      item.width = 45;
      item.height = 45;
      item.useStyle = ItemUseStyleID.Stabbing;
      item.knockBack = 1f;
      item.damage = 65;
      item.rare = ItemRarityID.LightRed;
      item.autoReuse = true;
      item.useAnimation = 30;
      item.useTime = 30;
      item.useTurn = true;
      item.UseSound = SoundID.Item1;
      item.Providence().cleric = true;
    }

    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      player.AddBuff(mod.BuffType("BoosterShot"), 3600);
      target.AddBuff(mod.BuffType("Hypodermia"), 300);
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