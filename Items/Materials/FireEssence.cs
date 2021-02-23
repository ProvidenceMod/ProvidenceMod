using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Tiles;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Materials
{
  public class FireEssence : ModItem
  {
    // {ID of item, amount of essence recieved}
    private readonly int[,] smeltableItems = new int[5, 3]
    {
      {ItemID.Hellstone,    9, 1 },
      {ItemID.HellstoneBar, 3, 1 },
      {ItemID.HelFire,      1, 10},
      {ItemID.Hellforge,    1, 10},
      {ItemID.HellwingBow,  1, 10}
    };
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Fire Essence");
      Tooltip.SetDefault("Material\nThis rune is scorching hot. Careful not to burn yourself!");
    }
    public override void SetDefaults()
    {
      item.width = 34;
      item.height = 32;
      item.material = true;
      item.maxStack = 999;
      item.noUseGraphic = true;
      item.rare = ItemRarityID.Lime;
      ItemID.Sets.ItemNoGravity[item.type] = true;
    }
    public override void AddRecipes()
    {
      for (int i = 0; i < smeltableItems.GetLength(0); i++)
      {
        ModRecipe r = new ModRecipe(mod);
        r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
        r.AddTile(TileType<EssenceCondenser>());
        r.SetResult(this, smeltableItems[i, 2]);
        r.AddRecipe();
      }
    }

  }
}