using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Accessories
{
  [AutoloadEquip(EquipType.Wings)]
  public class ClockworkWings : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Clockwork Wings");
      Tooltip.SetDefault("Wings made with plates, cogs, and chains of bronze.\nOne of Hephanis's earlier inventions. They seem to work well despite the immense tarnish.");
    }

    public override void SetDefaults()
    {
      item.width = 18;
      item.height = 20;
      item.value = Item.buyPrice(0, 50, 0, 0);
      item.rare = ItemRarityID.Green;
      item.accessory = true;
    }
    //these wings use the same values as the solar wings
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.wingTimeMax = 2.17f.InTicks();
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
      ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
      ascentWhenFalling = 0.5f;
      ascentWhenRising = 0.15f;
      maxCanAscendMultiplier = 1f;
      maxAscentMultiplier = 1.5f;
      constantAscend = 0.135f;
    }

    public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
      speed = 8f;
      acceleration *= 2f;
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