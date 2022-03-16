using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class ZephyrLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Leggings");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
		}

		public override void AddRecipes()
		{
		}
	}
}
