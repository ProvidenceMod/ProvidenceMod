using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Summon
{
	public class TwistingRelic : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twisting Relic");
			Tooltip.SetDefault("\"A piece of the sky come forth to your aid.\"");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ImpStaff);
			Item.width = 26;
			Item.height = 30;
			//item.shoot = ProjectileType<>();
		}
	}
}
