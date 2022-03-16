using Microsoft.Xna.Framework;
using Providence.Content.Items.Materials;
using Providence.Content.Projectiles.Boss;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.Projectiles.ProvidenceGlobalProjectileAI;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.Weapons.Melee
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

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
