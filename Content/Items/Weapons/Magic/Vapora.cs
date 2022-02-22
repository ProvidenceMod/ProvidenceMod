using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Magic;
using Terraria;
using Terraria.DataStructures;
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
			Item.scale = 1f;
			Item.width = 52;
			Item.height = 52;
			Item.damage = 25;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.shootSpeed = 15f;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item102;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 10, 0, 0);
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileType<VaporaMiasma>();
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return false;
		}
	}
}