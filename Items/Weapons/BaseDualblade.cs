using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Weapons
{
	public abstract class BaseDualblade : ModItem
	{
		public virtual float Speed => 24f;
		public virtual int Fadeout => 60;
		public virtual int Projectiles => 2;
		public Projectile[] projectiles;
		public override void SetDefaults()
		{
			projectiles = new Projectile[Projectiles];
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.melee = false; // Cleric is true? Then true, otherwise false
			item.ranged = false;
			item.magic = false;
			item.summon = false;
			item.thrown = false;
			item.Providence().cleric = true;
			SetExtraDefaults();
		}
		public virtual void SetExtraDefaults() { }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float ai1Step = MathHelper.TwoPi / projectiles.Length;
			for (int i = 0; i < projectiles.Length; i++)
			{
				if (projectiles[i]?.active != true)
					projectiles[i] = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, item.shoot, damage, knockBack, player.whoAmI, 0f, ai1Step * i)];
				else
					projectiles[i].timeLeft = Fadeout;
			}
			return false;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (item.Providence().cleric)
			{
				TooltipLine damagetip = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
				if (damagetip != null)
				{
					string[] array = damagetip.text.Split(' ');
					damagetip.text = array[0] + " parity " + array.Last();
				}
			}
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			mult = player.Providence().clericDamage;
			float globalDmg = player.Providence().clericDamage - 1;
			if (player.meleeDamage - 1 < globalDmg)
				globalDmg = player.meleeDamage - 1;
			if (player.magicDamage - 1 < globalDmg)
				globalDmg = player.magicDamage - 1;
			if (player.rangedDamage - 1 < globalDmg)
				globalDmg = player.rangedDamage - 1;
			if (player.minionDamage - 1 < globalDmg)
				globalDmg = player.minionDamage - 1;
			if (player.thrownDamage - 1 < globalDmg)
				globalDmg = player.thrownDamage - 1;
			if (globalDmg > 1)
				mult += globalDmg;
		}
	}
}
