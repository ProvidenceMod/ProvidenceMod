using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Providence.Content.Projectiles.Ranged;

namespace Providence.Content.Items.Weapons.Ranged
{
	public class Windreaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Windreaver");
			Tooltip.SetDefault("Bullets become high velocity air bullet projectiles.");
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.width = 66;
			Item.height = 28;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useTurn = false;
			Item.autoReuse = true;
			Item.scale = 1.0f;
			Item.shootSpeed = 24f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.useAmmo = AmmoID.Bullet;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item11;
			Item.value = Item.buyPrice(0, 0, 5, 0);
			Item.shoot = ProjectileType<AirBullet>();
		}
		public override Vector2? HoldoutOffset() => new Vector2(-4f, 0f);
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spread = velocity.RotatedByRandom(2f.InRadians());
			Projectile.NewProjectile(source, player.Center, spread, ProjectileType<AirBullet>(), Item.damage, knockback, player.whoAmI);
			return false;
		}
	}
}
