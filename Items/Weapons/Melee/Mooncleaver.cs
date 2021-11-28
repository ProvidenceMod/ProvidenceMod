using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Melee;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;

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
			item.damage = 500;
			item.width = 84;
			item.height = 84;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item60;
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
				// Spawn position.
				Vector2 pos = player.Center + new Vector2(Main.rand.NextFloat(-512f, 513f), -394f);
				// Create offset triangle.
				float xOffset = Main.MouseWorld.X - pos.X;
				float yOffset = Main.MouseWorld.Y - pos.Y;
				// Hypotenuse of offset triangle.
				float hyp = (float)Math.Sqrt(xOffset * xOffset + yOffset * yOffset);
				// Magnitude (Vector Length) multiplier. speed spread / hypotenuse
				float mag = Main.rand.NextFloat(20f, 29f) / hyp;
				// Speed X. X offset * magnitude + spread
				float x = (xOffset * mag) + (Main.rand.NextFloat(-360, 361) * 0.01f);
				// Speed Y. Y offset * magnitude + spread
				float y = (yOffset * mag) + (Main.rand.NextFloat(-360, 361) * 0.01f);

				// Sky shot.
				Projectile.NewProjectile(pos, new Vector2(x, y), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
				// Spray shot.
				//Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedBy(Main.rand.NextFloat(-15f, 16f).InRadians()), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
				// Octogon shot.
				//Projectile.NewProjectile(player.Center + new Vector2(128f, 0f).RotatedBy(i), new Vector2(speedX, speedY), ProjectileType<MoonBlast>(), damage, knockBack, player.whoAmI);
			}
			return false;
		}
	}
}
