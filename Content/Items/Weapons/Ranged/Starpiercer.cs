using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Projectiles.Ranged;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Weapons.Ranged
{
	public class Starpiercer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starpiercer");
			Tooltip.SetDefault("The arrows from this bow streaking across the sky once heard many wishes uttered in secret by mortals");
		}
		public override void SetDefaults()
		{
			Item.damage = 750;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 46;
			Item.height = 114;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTurn = false;
			Item.rare = (int)ProvidenceRarity.Purple;
			Item.Providence().customRarity = ProvidenceRarity.Developer;
			Item.UseSound = ProvidenceSound.TerraBeam;
			Item.value = Item.buyPrice(10, 0, 0, 0);
			Item.autoReuse = true;
			Item.shootSpeed = 40f;
			Item.shoot = ProjectileType<StarJavelin>();
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 10; i++)
			{
				Projectile.NewProjectile(source, player.Center, velocity.RotatedByRandom(25f.InRadians()), ProjectileType<StarJavelin>(), damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
			}
			return false;
		}
	}
}
