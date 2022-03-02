using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Items.Armor;
namespace Providence.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZephyrHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Helm");
			Tooltip.SetDefault("+5% melee damage\n2% melee crit chance");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.05f;
			player.GetCritChance(DamageClass.Melee) += 2;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) =>
			body.type == ItemType<ZephyrBreastplate>() &&
			legs.type == ItemType<ZephyrLeggings>() &&
			head.type == ItemType<ZephyrHelm>();
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+3 defense";
			player.statDefense += 3;
		}
		public override void AddRecipes()
		{
		}
	}
}
