using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class ZephyriumCutlass : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyrium Cutlass");
      Tooltip.SetDefault("The talons on the hilt are the only things keeping this from floating away.");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.BeeKeeper);
      item.Providence().element = ElementID.Air;
      item.damage = 34;
      item.width = 41;
      item.height = 42;
      item.useTime = 20;
      item.useAnimation = 20;
      item.rare = ItemRarityID.Orange;
      item.autoReuse = true;
    }
    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);

      r.AddIngredient(ItemType<ZephyriumBar>(), 10);
      r.AddIngredient(ItemType<HarpyQueenFeather>(), 3);
      r.AddIngredient(ItemType<HarpyQueenTalon>(), 2);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}