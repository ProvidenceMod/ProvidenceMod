using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Armor;

namespace ProvidenceMod.Items.Armor
{
  [AutoloadEquip(EquipType.Head)]
  public class CosmicHead : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cosmic Helmet");
      Tooltip.SetDefault("\"The power of the cosmos enlightens you.\"\n+20 Mana, +1 Max Minion");
    }

    public override void SetDefaults()
    {
      item.width = 18;
      item.height = 18;
      item.defense = 30;
    }
    public override void UpdateEquip(Player player)
    {
      player.buffImmune[BuffID.Darkness] = true;
      player.buffImmune[BuffID.Confused] = true;
      player.buffImmune[BuffID.Silenced] = true;
      player.statManaMax2 += 20;
      player.maxMinions++;
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ItemType<CosmicChest>() && legs.type == ItemType<CosmicLegs>();
    }

    public override void UpdateArmorSet(Player player)
    {
      player.setBonus = "+20% Damage";
      player.allDamage += (player.allDamage * 0.2f);
      /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
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