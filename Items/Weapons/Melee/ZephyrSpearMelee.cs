using ProvidenceMod.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Weapons.Melee
{
	public class ZephyrSpearMelee : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratus Spear");
			Tooltip.SetDefault("A mighty force");
		}

		public override void SetDefaults() {
			item.damage = 29;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 3.7f;
			item.knockBack = 6.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.rare = (int) ProvidenceRarity.Orange;
			item.value = Item.sellPrice(silver: 10);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<Projectiles.Melee.ZephyrSpearMelee>();
		}

		public override bool CanUseItem(Player player) {
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}
