using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria.Localization;
using UnbiddenMod.Projectiles.Ranged;
using Terraria.Audio;

namespace UnbiddenMod.Items.Weapons.Ranged
{
  public class RicochetRevolver : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.Revolver;

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Trickshot Revolver");
      Tooltip.SetDefault("Right-click to flip a coin. Shoot said coins to ricochet your shot into an enemy!\nYou can even chain ricochets together!");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Revolver);
    }

    public override bool AltFunctionUse(Player player)
    {
      return true;
    }

    public override bool ConsumeAmmo(Player player)
    {
      return player.altFunctionUse != 2;
    }
    public override bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        item.shoot = ModContent.ProjectileType<RicoCoin>();
        item.useAmmo = AmmoID.None;
        item.shootSpeed = 5f;
        item.UseSound = new LegacySoundStyle(38, 1, Terraria.Audio.SoundType.Sound); // Coin Pickup sound
      }
      else
      {
        item.useAmmo = AmmoID.Bullet;
        item.shootSpeed = 10f;
        item.UseSound = SoundID.Item41;
      }
      return true;
    }
    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.IceTorch, 1);
      recipe.AddIngredient(ItemID.Switch, 1);
      recipe.AddIngredient(ItemID.CobaltBar, 10);
      recipe.AddIngredient(ItemID.IllegalGunParts, 1);
      recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }

    /*public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Texture2D tex = ModContent.GetTexture("Items/Weapons/MoonCleaver/MoonCleaverGlow"); //loads our glowmask
        spriteBatch.Draw(tex, position, tex.Frame(), Color.White, 0, origin, scale, 0, 0); //draws our glowmask in the inventory. To see how to draw it in the world, see the ModifyDrawLayers method in ExamplePlayer.
    }*/
  }
}