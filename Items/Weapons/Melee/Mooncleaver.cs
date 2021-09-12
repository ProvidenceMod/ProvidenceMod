using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Melee;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace ProvidenceMod.Items.Weapons.Melee
{
	public class Mooncleaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mooncleaver");
			Tooltip.SetDefault("A blade that once pierced the heavens");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.width = 90;
			item.height = 90;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.melee = true;
			item.shoot = ProjectileType<MoonBlast>();
			item.shootSpeed = 24f;
			item.autoReuse = true;
			item.rare = (int)ProvidenceRarity.Purple;
			item.Providence().customRarity = ProvidenceRarity.Developer;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Projectile.NewProjectile(player.Center + new Vector2(128f, 0f).RotatedBy(i), new Vector2(speedX, speedY), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
			}
			return false;
		}
	}
}
