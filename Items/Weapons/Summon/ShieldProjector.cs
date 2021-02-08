using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Summon;

namespace ProvidenceMod.Items.Weapons.Summon
{
  public class ShieldProjector : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.SpectreStaff;

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Shield Projector");
      Tooltip.SetDefault("Summons a shield that protects you from enemy projectiles!");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.SpectreStaff);
      item.shoot = ModContent.ProjectileType<ProjectorShield>();
      item.shootSpeed = 0f;
    }
    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      position = Main.MouseWorld;
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