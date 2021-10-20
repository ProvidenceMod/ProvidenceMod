using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Cleric;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Weapons
{
	public abstract class BaseDualblade : ModItem
	{
		public Projectile[] projs;
		protected int projFadeoutTime;
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.noMelee = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float ai1Step = MathHelper.TwoPi / projs.Length;
			for (int i = 0; i < projs.Length; i++)
			{
				Projectile proj = projs[i];
				if (proj == null || !proj.active)
					projs[i] = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, item.shoot, damage, knockBack, player.whoAmI, 0f, ai1Step * i)];
				else
					proj.timeLeft = projFadeoutTime;
			}
			return false;
		}
	}
}
