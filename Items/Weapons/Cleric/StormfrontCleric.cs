using Terraria;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Cleric;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Weapons.Cleric
{
	public class StormfrontCleric : ClericItem
	{
		public Projectile proj1;
		public Projectile proj2;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormfront");
		}
		public override void SetDefaults()
		{
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.damage = 50;
			item.autoReuse = true;
			item.width = 190;
			item.height = 124;
			item.material = true;
			item.Providence().cleric = true;
			item.rare = (int)ProvidenceRarity.Orange;
			item.shoot = ProjectileType<StormfrontTempest>();
			item.noUseGraphic = true;
			item.noMelee = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (proj1 == null || proj2 == null || !proj1.active || !proj2.active)
			{
				proj1 = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<StormfrontTempest>(), damage, knockBack, player.whoAmI, 0f, 0f)];
				proj2 = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<StormfrontTempest>(), damage, knockBack, player.whoAmI, 0f, 1f)];
			}
			else
			{
				proj1.timeLeft = 60;
				proj2.timeLeft = 60;
			}
			return false;
		}
	}
}
