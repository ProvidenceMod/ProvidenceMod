using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static UnbiddenMod.UnbiddenUtils;
using UnbiddenMod.NPCs.FireAncient;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace UnbiddenMod.Items.Consumables
{
  public class ScintilatingObsidian : ModItem
  {
    public int frame;
    public int frameCounter;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Scintilating Obsidian");
      Tooltip.SetDefault("It pulses with an eerie fiery glow.");
      Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 13));
    }

    public override void SetDefaults()
    {
      item.width = 40;
      item.height = 40;
      item.CloneDefaults(ItemID.SuspiciousLookingEye);
      item.maxStack = 1;
      item.rare = ItemRarityID.Lime;
    }

    public override bool CanUseItem(Player player)
    {
      // No other bosses active and on at least the surface layer
      return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
    }

    public override bool UseItem(Player player)
    {
      _ = NPC.NewNPC((int)player.position.X, (int)(player.position.Y - (37 * 16)), NPCType<FireAncient>());
      return true;
    }
    public override bool ConsumeItem(Player player)
    {
      return false;
    }
    public override bool PreDrawInInventory(
      SpriteBatch spriteBatch,
      Vector2 position,
      Rectangle frameI,
      Color drawColor,
      Color itemColor,
      Vector2 origin,
    float scale)
    {
      Texture2D texture = GetTexture("UnbiddenMod/Items/Consumables/ScintilatingObsidianAnimated");
      spriteBatch.Draw(texture, position, new Rectangle?(item.AnimationFrame(ref frame, ref frameCounter, 8, 13)), Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
      return false;
    }
    public override bool PreDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      ref float rotation,
      ref float scale,
      int whoAmI)
    {
      Texture2D texture = GetTexture("UnbiddenMod/Items/Consumables/ScintilatingObsidianAnimated");
      spriteBatch.Draw(texture, item.position - Main.screenPosition, new Rectangle?(item.AnimationFrame(ref frame, ref frameCounter, 8, 13)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      return false;
    }
    public override void PostDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      float rotation,
      float scale,
      int whoAmI)
    {
      Texture2D texture = GetTexture("UnbiddenMod/Items/Consumables/ScintilatingObsidianGlow");
      spriteBatch.Draw(texture, item.position - Main.screenPosition, new Rectangle?(item.AnimationFrame(ref frame, ref frameCounter, 8, 13, false)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
    }
    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.Feather, 3);
      recipe.AddIngredient(ItemID.SunplateBlock, 20);
      recipe.AddTile(TileID.SkyMill);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}