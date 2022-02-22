using ProvidenceMod.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Weapons.Melee
{
	public class StratusSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratus Spear");
			Tooltip.SetDefault("A mighty force");
		}

		public override void SetDefaults()
		{
			Item.damage = 29;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 18;
			Item.useTime = 24;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 76;
			Item.height = 76;
			Item.scale = 1f;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.StratusSpear>();
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}
}
