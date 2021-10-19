using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Projectiles.Boss;
using static ProvidenceMod.Projectiles.ProvidenceGlobalProjectileAI;

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
			item.scale = 1f;
			item.width = 70;
			item.height = 70;
			item.damage = 46;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shootSpeed = 16f;
			item.autoReuse = true;
			item.material = true;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<ZephyrDart>();
			item.useStyle = ItemUseStyleID.SwingThrow;
		}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
	  Main.PlaySound(SoundID.Item45, player.position);
	  Vector2 velocity = new Vector2(speedX, speedY);
		Projectile.NewProjectile(position, velocity, type, damage, knockBack, player.whoAmI, (int)ZephyrDartAI.Friendly);
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