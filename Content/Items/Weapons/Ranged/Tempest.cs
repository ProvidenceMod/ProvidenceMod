using Microsoft.Xna.Framework;
using Providence.Content.Projectiles.Boss;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Weapons.Ranged
{
	public class Tempest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tempest");
			Tooltip.SetDefault("Converts regular arrows into zephyr darts\nFires a fan of sentinel shards\nLaunch Velocity: 15\n\"They asked for a storm...and the gods gave them a tempest.\"");
		}
		public override void SetDefaults()
		{
			Item.scale = 1f;
			Item.width = 70;
			Item.damage = 25;
			Item.height = 118;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.shootSpeed = 15f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.knockBack = 4.5f;
			Item.useAmmo = AmmoID.Arrow;
			Item.UseSound = SoundID.Item102;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 10, 0, 0);
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileType<ZephyrDart>();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (float i = -MathHelper.PiOver4 * 0.125f; i <= MathHelper.PiOver4 * 0.125f; i += MathHelper.PiOver4 * 0.125f)
			{
				Projectile.NewProjectile(source, player.Center, velocity.RotatedBy(i), ProjectileType<SentinelShard>(), damage, knockback, player.whoAmI, Main.rand.Next(0, 8), 1f);
			}
			if (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.WoodenArrowHostile)
			{
				Projectile.NewProjectile(source, player.Center, velocity, ProjectileType<ZephyrDart>(), damage, knockback, player.whoAmI, 1f);
				return false;
			}
			return true;
		}
	}
}
