using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Materials
{
  public class Starfiber : ModItem
  {
    public int frame;
    public int frameNumber;
    public int frameTick;
    public int frameTime = Main.rand.Next(18, 23);
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starfiber");
      Tooltip.SetDefault("It sparkles like the night sky!");
    }

    public override void SetDefaults()
    {
      item.width = 34;
      item.height = 32;
      item.material = true;
      item.maxStack = 999;
      item.noUseGraphic = true;
      frameNumber = frame;
      item.rare = ItemRarityID.Lime;
      // item.Providence().glowmaskTexture = GetTexture("ProvidenceMod/Items/Materials/StarfiberGlow");
      // item.Providence().glowmask = true;
      // item.Providence().animated = true;
      // item.Providence().frameTime = 20;
      // item.Providence().frameCount = 5;
      // item.Providence().frameTickIncrease = true;
      // item.Providence().overrideGlowmaskPositionX = 0;
      // item.Providence().overrideGlowmaskPositionY = 0;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.FallenStar, 1);
      recipe.AddIngredient(ItemID.Silk, 1);
      recipe.AddTile(TileID.Loom);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
      Texture2D texture = GetTexture("ProvidenceMod/Items/Materials/StarfiberAnimated");
      spriteBatch.Draw(texture, position, item.AnimationFrame(ref frameNumber, ref frameTick, frameTime, 5, true), Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
      Texture2D texture = GetTexture("ProvidenceMod/Items/Materials/StarfiberGlow");
      spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X, item.position.Y - Main.screenPosition.Y + 2), item.AnimationFrame(ref frame, ref frameTick, frameTime, 5, true), Color.White, 0.0f, Vector2.Zero , 1f, SpriteEffects.None, 0.0f);
    }
  }
}