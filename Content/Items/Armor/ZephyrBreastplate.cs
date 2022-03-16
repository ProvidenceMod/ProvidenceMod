using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ZephyrBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zephyr Breastplate");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 22;
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
