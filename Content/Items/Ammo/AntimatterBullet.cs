using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Ammo
{
	public class AntimatterBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Antimatter Bullet");
			Tooltip.SetDefault("Stabilized Anti-Baryonic matter in a vial. Shoot it at things to anhiliate them.");
		}
		public override void SetDefaults()
		{
			Item.height = 16;
			Item.width = 8;
			Item.value = Item.buyPrice(0, 0, 7, 53);
			Item.shootSpeed = 32f;
			Item.damage = 69;
			Item.DamageType = DamageClass.Ranged;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 30f;
			//item.rare = (int)ProvidenceRarity.Celestial;
			Item.shoot = ProjectileType<Projectiles.Ranged.AntimatterBullet>();
			Item.ammo = AmmoID.Bullet;
		}
	}
}