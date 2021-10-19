using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Projectiles.Magic;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Projectiles.Boss;
using System;

namespace ProvidenceMod.Items.Weapons.Magic
{
	public class Galeburst : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galeburst");
			Tooltip.SetDefault("Wind shears those before you.");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.mana = 16;
			item.scale = 1f;
			item.width = 38;
			item.height = 60;
			item.damage = 10;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 1f;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item45;
			item.shoot = ProjectileType<ZephyrTrident>();
			item.value = Item.buyPrice(0, 0, 50, 0);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 1f);
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe r = new ModRecipe(mod);

			r.AddIngredient(ItemType<ZephyrBar>(), 10);
			r.AddTile(TileID.SkyMill);
			r.SetResult(this);
			r.AddRecipe();
		}
	}
}