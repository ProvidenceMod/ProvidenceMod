using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Melee;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

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
			Item.damage = 500;
			Item.width = 84;
			Item.height = 84;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item60;
			Item.DamageType = DamageClass.Melee;
			Item.shoot = ProjectileType<MoonBlast>();
			Item.shootSpeed = 24f;
			Item.autoReuse = true;
			Item.rare = (int)ProvidenceRarity.Purple;
			Item.Providence().customRarity = ProvidenceRarity.Developer;
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Vector2 pos = player.Center + new Vector2(Main.rand.NextFloat(-512f, 513f), -394f);
				Vector2 dir = new Vector2(Main.MouseWorld.X - pos.X, Main.MouseWorld.Y - pos.Y).RotatedBy(Main.rand.NextFloat(-360, 361) * 0.0003f);
				dir.Normalize();
				float mag = Main.rand.NextFloat(20f, 29f);

				// Sky shot.
				Projectile.NewProjectile(source, pos, dir * mag, ProjectileType<MoonBlast>(), damage, knockback, player.whoAmI);
				// Spray shot.
				//Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedBy(Main.rand.NextFloat(-15f, 16f).InRadians()), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
				// Octogon shot.
				//Projectile.NewProjectile(player.Center + new Vector2(128f, 0f).RotatedBy(i), new Vector2(speedX, speedY), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
			}
			return false;
		}
	}
}
