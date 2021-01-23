using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static UnbiddenMod.UnbiddenUtils;
using UnbiddenMod.Items.Materials;
using UnbiddenMod.NPCs.FireAncient;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace UnbiddenMod.Items.Consumables
{
  public class ScintilatingObsidian : ModItem
  {
    public int frame = Main.itemFrameCounter[0];
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Scintilating Obsidian");
      Tooltip.SetDefault($"\"It pulses with an eerie fiery glow.\"{frame}");
      Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 13));
    }

    public override void SetDefaults()
    {
      item.width = 40;
      item.height = 40;
      item.CloneDefaults(ItemID.SuspiciousLookingEye);
      item.maxStack = 1;
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
    public override void PostDrawInInventory(
      SpriteBatch spriteBatch,
      Vector2 position,
      Rectangle frame,
      Color drawColor,
      Color itemColor,
      Vector2 origin,
      float scale) => item.DrawGlowmask(spriteBatch, 13, default, GetTexture("UnbiddenMod/Items/Consumables/ScintilatingObsidianGlow"));
    public override void PostDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
       Color alphaColor,
       float rotation,
       float scale,
       int whoAmI) => item.DrawGlowmask(spriteBatch, 13, rotation, GetTexture("UnbiddenMod/Items/Consumables/ScintilatingObsidianGlow"));
    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.Feather, 3);
      recipe.AddIngredient(ItemID.SunplateBlock, 20);
      recipe.AddTile(TileID.SkyMill); // Ancient Manipulator
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }
  }
}