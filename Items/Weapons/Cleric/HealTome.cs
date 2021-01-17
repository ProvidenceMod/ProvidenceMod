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
  public class HealTome : ClericItem
  {
    private int missingManaCooldown = 10;
    public bool urgentHeal = false;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Heal Tome");
      Tooltip.SetDefault("Heal allies around your cursor!\nIf out of mana, right-click to sacrifice health to heal at double rate");
    }

    public override void SetDefaults()
    {
      item.width = 28;
      item.height = 30;
      item.useStyle = ItemUseStyleID.HoldingOut;
      item.noMelee = true;
      item.damage = 6; // "damage"
      item.rare = ItemRarityID.LightRed;
      item.autoReuse = true;
      item.useAnimation = 10;
      item.useTime = 10;
      item.useTurn = true;
      item.UseSound = SoundID.Item1;
      item.mana = 2;
      item.Unbidden().cleric = true;
    }

    public override bool CanRightClick()
    {
      return true;
    }

    public override void RightClick(Player player)
    {
      urgentHeal = !urgentHeal;
    }

    private void RegisterRadius(Player player)
    {
      float mX = Main.screenPosition.X + Main.mouseX,
            mY = Main.screenPosition.Y + Main.mouseY;
      const int explosionRadius = 2 * 16; // Not explosion, per se...

      float leftEdgeX = mX - explosionRadius, // Grabbing the bounds of the AoE
            rightEdgeX = mX + explosionRadius,
            upperEdgeY = mY - explosionRadius,
            lowerEdgeY = mY + explosionRadius;

      Item tome = player.inventory[player.selectedItem]; // Referencing the item that cast this

      // Urgently healing? Double rate.
      int healing = tome.damage;
      for (int i = 0; i < Main.player.Length; i++) // For every player on Main. Not the most optimal, but it's a start.
      {
        Player iteratedPlayer = Main.player[i]; // Reference for later
        // If the player is active and within the bounds of the explosion radius AND not the original caster
        if (iteratedPlayer != Main.player[player.whoAmI] && iteratedPlayer.active && (iteratedPlayer.position.X >= leftEdgeX && iteratedPlayer.position.X <= rightEdgeX) && (iteratedPlayer.position.Y <= lowerEdgeY && iteratedPlayer.position.Y >= upperEdgeY))
        {
          iteratedPlayer.statLife += healing;
          iteratedPlayer.HealEffect(healing, true);
        }
      }
    }

    public override void OnConsumeMana(Player player, int manaConsumed)
    {
      RegisterRadius(player);
    }

    public override void OnMissingMana(Player player, int neededMana)
    {
      // Limiter to not call it every damn tick
      if (missingManaCooldown == 0 && player.statLife > 2)
      {
        Item tome = player.inventory[player.selectedItem];
        player.statLife -= tome.mana;
        RegisterRadius(player);
        missingManaCooldown = 10;
      }
      else
      {
        missingManaCooldown--;
      }
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