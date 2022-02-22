using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items;
namespace ProvidenceMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZephyrCirclet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Circlet");
			Tooltip.SetDefault("+6% parity damage");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.Providence().clericDamage += 0.06f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<ZephyrBreastplate>() &&
				legs.type == ItemType<ZephyrLeggings>() &&
				head.type == ItemType<ZephyrCirclet>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 defense";
			player.statDefense += 2;
		}
		public override void AddRecipes()
		{
		}
	}
}