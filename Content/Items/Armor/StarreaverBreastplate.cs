using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;

namespace Providence.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class StarreaverBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Starreaver Breasplate");
			Tooltip.SetDefault("The hide of the Gods");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.defense = 100;
		}

		public override void UpdateEquip(Player player)
		{
		}

		public override void AddRecipes()
		{
		}
	}
}
