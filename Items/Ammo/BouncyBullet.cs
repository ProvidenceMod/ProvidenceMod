using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod;

namespace ProvidenceMod.Items.Ammo
{
	public class BouncyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bouncy Bullet");
			Tooltip.SetDefault("Pinball, anyone?");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.MusketBall);
			item.width = 8;
			item.height = 16;
			item.value = Item.buyPrice(0, 0, 1, 0);
			item.shoot = ProjectileType<Projectiles.Ranged.BouncyBullet>();
			item.shootSpeed = 8f;
			// item.damage = 10;
			// item.ranged = true;
			// item.width = 14;
			// item.height = 32;
			// item.maxStack = 999;
			// item.consumable = true;
			// item.knockBack = 1f; //Added with the weapon's knockback
			// item.value = 500;
			// item.rare = 2;
			// item.shoot = mod.ProjectileType("ExampleBulletA");
			// item.shootSpeed = 7f; //Added to the weapon's shoot speed
			// item.ammo = mod.ItemType("ExampleBulletA"); //Tells the game that it should be considered the same type of ammo as this item
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.PinkGel, 1);
			recipe.AddIngredient(ItemID.SilverBullet, 1);
			recipe.AddTile(TileID.Solidifier);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}