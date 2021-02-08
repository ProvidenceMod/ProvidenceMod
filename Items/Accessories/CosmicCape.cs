using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Accessories
{
  [AutoloadEquip(EquipType.Wings)]
  public class CosmicCape : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cosmic Cape");
      Tooltip.SetDefault("The galaxy brings you closer to the sun");
    }

    public override void SetDefaults()
    {
      item.width = 22;
      item.height = 20;
      item.value = 10000;
      item.rare = ItemRarityID.Green;
      item.accessory = true;
    }
    //these wings use the same values as the solar wings
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.wingTimeMax = 240;
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
      ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
      ascentWhenFalling = 0.85f;
      ascentWhenRising = 0.15f;
      maxCanAscendMultiplier = 1f;
      maxAscentMultiplier = 3f;
      constantAscend = 0.135f;
    }

    public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
      speed = 12f;
      acceleration *= 2.5f;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.DirtBlock);
      recipe.AddTile(TileID.WorkBenches);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}