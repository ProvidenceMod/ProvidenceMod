using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class CounterSwing : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Counterswing"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
      Tooltip.SetDefault("\"Swing at projectiles to bounce them back!\"");
    }
    public override void SetDefaults()
    {
      item.damage = 150;
      item.crit = 45;
      item.melee = true;
      item.width = 30;
      item.height = 30;
      item.scale = 2.25f;
      item.useTime = 12;
      item.useAnimation = 12;
      item.useStyle = ItemUseStyleID.SwingThrow;
      item.knockBack = 6;
      item.value = 10000;
      item.rare = 12;
      item.UseSound = SoundID.Item1;
      item.autoReuse = true;
      item.useTurn = true;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
      // Run the parry util (In TynUtils)
      StandardParry(player, hitbox, ref player.Providence().parryProjID);
    }
    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.DirtBlock, 10);
      recipe.AddTile(TileID.LunarCraftingStation);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}