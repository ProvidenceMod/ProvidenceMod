using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items.Materials;

namespace UnbiddenMod.Items.Accessories
{
  public class ZephyriumAglets : ModItem
  {
    // Temporary sprite, delete when we have an actual one
    public override string Texture => $"Terraria/Item_{ItemID.Aglet}";
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyrium Aglets");
      Tooltip.SetDefault("+2 Air defense\nProvides a jump boost and increased vertical acceleration with wings and rocket boots");
    }

    public override void SetDefaults()
    {
      item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
      player.jumpBoost = true;
      UnbiddenPlayer unbiddenPlayer = player.Unbidden();
      unbiddenPlayer.resists[ElementID.Air] += 2;
      unbiddenPlayer.zephyriumAglet = true;
    }
    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemID.AnkletoftheWind);
      r.AddIngredient(ItemType<ZephyriumBar>(), 5);
      r.SetResult(this);
      r.AddRecipe();

      ModRecipe r2 = new ModRecipe(mod);
      r2.AddIngredient(ItemID.Aglet);
      r2.AddIngredient(ItemType<ZephyriumBar>(), 5);
      r2.SetResult(this);
      r2.AddRecipe();
    }
  }
}