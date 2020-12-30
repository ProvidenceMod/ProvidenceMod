using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Items.Armor;

namespace UnbiddenMod.Items.Armor
{
  [AutoloadEquip(EquipType.Legs)]
  public class AcolyteLegs : ClericItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Acolyte Shoes");
      Tooltip.SetDefault("+3% Crit Chance");
    }

    public override void SetDefaults()
    {
      item.width = 18;
      item.height = 18;
      item.defense = 1;
    }

    public override void UpdateEquip(Player player)
    {
      player.magicCrit += 3;
      player.meleeCrit += 3;
      player.rangedCrit += 3;
      player.thrownCrit += 3;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.DirtBlock, 1);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}