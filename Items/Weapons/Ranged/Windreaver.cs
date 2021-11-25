using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Projectiles.Ranged;
namespace ProvidenceMod.Items.Weapons.Ranged
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
			item.damage = 10;
			item.width = 66;
			item.height = 28;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useTurn = false;
			item.autoReuse = true;
			item.scale = 1.0f;
			item.shootSpeed = 24f;
			item.ranged = true;
			item.noMelee = true;
			item.useAmmo = AmmoID.Bullet;
			item.rare = (int)ProvidenceRarity.Orange;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item11;
			item.value = Item.buyPrice(0, 0, 5, 0);
			item.shoot = ProjectileType<AirBullet>();
		}
		public override Vector2? HoldoutOffset() => new Vector2(-4f, 0f);
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(2f.InRadians());
			Projectile.NewProjectile(player.Center, spread, ProjectileType<AirBullet>(), item.damage, knockBack, player.whoAmI);
			return false;
		}
	}
}