using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Materials;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Accessories
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
      ProvidencePlayer ProvidencePlayer = player.Providence();
      ProvidencePlayer.resists[ElementID.Air] += 2;
      ProvidencePlayer.zephyriumAglet = true;
    }
    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemID.AnkletoftheWind);
      r.AddIngredient(ItemID.Aglet);
      r.AddIngredient(ItemType<ZephyriumBar>(), 5);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}