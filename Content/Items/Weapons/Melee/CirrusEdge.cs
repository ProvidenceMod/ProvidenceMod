using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Projectiles.Boss;
using static ProvidenceMod.Projectiles.ProvidenceGlobalProjectileAI;
using Terraria.DataStructures;
using Terraria.Audio;

namespace ProvidenceMod.Items.Weapons.Melee
{
	public class CirrusEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cirrus Edge");
			Tooltip.SetDefault("Light as a feather");
		}

		public override void SetDefaults()
		{
			Item.scale = 1f;
			Item.width = 70;
			Item.height = 70;
			Item.damage = 46;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.shootSpeed = 16f;
			Item.autoReuse = true;
			Item.material = true;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<ZephyrDart>();
			Item.useStyle = ItemUseStyleID.Swing;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			SoundEngine.PlaySound(SoundID.Item45, player.position);
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, (int)ZephyrDartAI.Friendly);
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