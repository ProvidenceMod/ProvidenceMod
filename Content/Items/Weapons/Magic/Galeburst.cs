using Microsoft.Xna.Framework;
using Providence.Content.Items.Materials;
using Providence.Content.Projectiles.Boss;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Weapons.Magic
{
	public class Galeburst : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galeburst");
			Tooltip.SetDefault("Wind shears those before you.");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.mana = 16;
			Item.scale = 1f;
			Item.width = 38;
			Item.height = 60;
			Item.damage = 10;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 1f;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item45;
			Item.shoot = ProjectileType<ZephyrTrident>();
			Item.value = Item.buyPrice(0, 0, 50, 0);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1f);
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemType<ZephyrBar>(), 10)
				.AddTile(TileID.SkyMill)
				.Register();
		}
	}
}
