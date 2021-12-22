using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Projectiles.Ranged;

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
			item.damage = 750;
			item.ranged = true;
			item.noMelee = true;
			item.width = 46;
			item.height = 114;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTurn = false;
			item.rare = (int)ProvidenceRarity.Purple;
			item.Providence().customRarity = ProvidenceRarity.Developer;
			item.UseSound = ProvidenceSound.TerraBeam;
			item.value = Item.buyPrice(10, 0, 0, 0);
			item.autoReuse = true;
			item.shootSpeed = 40f;
			item.shoot = ProjectileType<StarJavelin>();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 10; i++)
			{
				Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(25f.InRadians()), ProjectileType<StarJavelin>(), damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
			}
			return false;
		}
	}
}
