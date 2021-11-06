using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Boss;
using ProvidenceMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Ranged
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
			item.scale = 1f;
			item.width = 70;
			item.damage = 25;
			item.height = 118;
			item.useTime = 15;
			item.useAnimation = 15;
			item.shootSpeed = 15f;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.knockBack = 4.5f;
			item.useAmmo = AmmoID.Arrow;
			item.UseSound = SoundID.Item102;
			item.rare = ItemRarityID.Orange;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shoot = ProjectileType<ZephyrDart>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (float i = -MathHelper.PiOver4 * 0.125f; i <= MathHelper.PiOver4 * 0.125f; i += MathHelper.PiOver4 * 0.125f)
			{
				Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedBy(i), ProjectileType<SentinelShard>(), damage, knockBack, player.whoAmI, Main.rand.Next(0, 8), 1f);
			}
			if (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.WoodenArrowHostile)
			{
				Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileType<ZephyrDart>(), damage, knockBack, player.whoAmI, 1f);
				return false;
			}
			return true;
		}
	}
}