using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.NPCs.FireAncient;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Consumables
{
  public class ScintillatingObsidian : ModItem
  {
    public int frame;
    public int frameNumber;
    public int frameTick;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Scintillating Obsidian");
      Tooltip.SetDefault("It pulses with an eerie fiery glow.");
    }

    public override void SetDefaults()
    {
      item.width = 40;
      item.height = 40;
      item.maxStack = 1;
      item.rare = ItemRarityID.Lime;
      frameNumber = frame;
      item.consumable = true;
      item.useAnimation = 45;
      item.useTime = 45;
      item.useStyle = ItemUseStyleID.HoldingUp;
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
    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
      Texture2D texture = GetTexture("ProvidenceMod/Items/Consumables/ScintillatingObsidianAnimated");
      spriteBatch.Draw(texture, position, item.AnimationFrame(ref frameNumber, ref frameTick, 8, 13, true), Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
      Texture2D texture = GetTexture("ProvidenceMod/Items/Consumables/ScintillatingObsidianGlow");
      spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X, item.position.Y - Main.screenPosition.Y + 2), item.AnimationFrame(ref frame, ref frameTick, 8, 13, true), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
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