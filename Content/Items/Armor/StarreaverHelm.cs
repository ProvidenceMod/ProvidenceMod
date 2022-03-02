using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Items.Armor;
namespace Providence.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class StarreaverHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starreaver Helm");
			Tooltip.SetDefault("The visage of the Gods");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 22;
			Item.defense = 100;
		}
		public override void UpdateEquip(Player player)
		{
			player.Providence().starreaverArmor = true;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) =>
			body.type == ItemType<StarreaverHelm>() &&
			legs.type == ItemType<StarreaverBreastplate>() &&
			head.type == ItemType<StarreaverLeggings>();
		public override void UpdateArmorSet(Player player)
		{
		}
		public override void AddRecipes()
		{
		}
	}
}
