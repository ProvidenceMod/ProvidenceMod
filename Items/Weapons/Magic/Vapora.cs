using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Magic
{
	public class Vapora : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vapora");
			Tooltip.SetDefault("Fires clouds that linger a short distance away from where they were shot\nThese clouds asphyxiate enemies, causing damage over time.\n\"Shatter the diamond wall. Scatter it like dust in the wind.\"");
		}
		public override void SetDefaults()
		{
			item.scale = 1f;
			item.width = 70;
			item.damage = 25;
			item.height = 118;
			item.useTime = 15;
			item.useAnimation = 15;
			item.shootSpeed = 15f;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.knockBack = 4.5f;
			item.UseSound = SoundID.Item102;
			item.rare = ItemRarityID.Orange;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shoot = ProjectileType<VaporaMiasma>();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			item.FireXProjectiles(5, ref speedX, ref speedY, 10f);
			return false;
		}
	}
}