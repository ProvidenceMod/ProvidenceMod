using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria.Localization;

namespace ProvidenceMod.Items.Weapons.Ranged
{
  public class BulletMixer : ModItem
  {
    public override string Texture => $"Terraria/Item_{ItemID.DartRifle}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bullet Mixer");
      Tooltip.SetDefault("Every bullet fired is a random choice of the bullets you have in your inventory.");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.Megashark);
    }

    // This is handled in the Shoot function
    public override bool ConsumeAmmo(Player player)
    {
      return false;
    }
    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      // Making a list
      var bulletTypes = new List<int>();
      var bulletTypesInds = new List<int>();
      // Looping through inventory
      for (int ind = 0; ind < player.inventory.Length; ind++)
      {
        Item i = player.inventory[ind];
        // Testing if items are bullets, and not already added to list
        if (i.ammo == AmmoID.Bullet && !bulletTypes.Contains(i.shoot))
        {
          // Add to list if true
          bulletTypes.Add(i.shoot);
          bulletTypesInds.Add(ind);
        }
      }
      int rand = Main.rand.Next(bulletTypes.Count);
      // Grab a random type within the List and set bullet type to that
      type = bulletTypes[rand];
      player.inventory[bulletTypesInds[rand]].stack--;
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