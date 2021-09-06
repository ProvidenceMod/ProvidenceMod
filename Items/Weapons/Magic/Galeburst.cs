using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Projectiles.Magic;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Magic
{
	public class Galeburst : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galeburst");
			Tooltip.SetDefault("\"Wind shears those before you.\"");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 20;
			item.useAnimation = 21;
			item.scale = 1f;
			item.magic = true;
			item.mana = 8;
			item.autoReuse = true;
			item.damage = 10;
			item.UseSound = Main.item[ItemID.WaterBolt].UseSound;
			item.shoot = ProjectileType<Galeshot>();
			item.shootSpeed = 10f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// On a right-click, combine into one beam so higher defense targets can still be dealt with
			if (player.altFunctionUse == 2)
			{
        damage = (int)(50 * (player.magicDamage + player.allDamage - 1));
				return true;
			}
			else
			{
				byte repeats = 5, counter = 0;
				while (counter < repeats)
				{
					_ = Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(2.5f.InRadians()), type, damage, knockBack, item.owner);
					counter++;
				}
				return false;
			}
		}
		public override bool AltFunctionUse(Player player) => true;
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