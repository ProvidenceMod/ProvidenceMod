using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Projectiles.Magic;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Weapons.Magic
{
	public class Cumulus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cumulus");
			Tooltip.SetDefault("Conjures a localized tempest that steadily follows the cursor and slowly pulls in enemies");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 52;
			Item.height = 52;
			Item.value = Item.buyPrice(0, 10, 0, 0);
			Item.rare = 12;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.scale = 1.0f;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item45;
			Item.shoot = ProjectileType<CumulusCloud>();
			Item.shootSpeed = 0f;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player) => player.altFunctionUse != 2;
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position = Main.MouseWorld;
			return true;
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