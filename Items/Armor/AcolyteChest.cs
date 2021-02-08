using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Body)]
  public class AcolyteChest : ClericItem
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      DisplayName.SetDefault("Acolyte Robe");
      Tooltip.SetDefault("Slightly increased healing when equipped");
    }

    public override void SetDefaults()
    {
      item.width = 18;
      item.height = 18;
      item.defense = 2;
    }

    public override void UpdateEquip(Player player)
    {
      player.lifeRegen++;
      player.Providence().resists[6] += 2;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ItemType<AcolyteChest>() &&
        head.type == ItemType<AcolyteHead>() &&
        legs.type == ItemType<AcolyteLegs>();
    }
    public override void UpdateArmorSet(Player player)
    {
      player.setBonus = "Generate an aura around yourself. Players inside this aura gain extra healing based on your cleric multiplier.";
      base.UpdateArmorSet(player);
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