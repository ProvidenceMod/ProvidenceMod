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
			item.height = 16;
			item.width = 8;
			item.value = Item.buyPrice(0, 0, 7, 53);
      item.shootSpeed = 32f;
      item.damage = 69;
      item.ranged = true;
      item.maxStack = 999;
      item.consumable = true;
      item.knockBack = 30f;
      //item.rare = (int)ProvidenceRarity.Celestial;
      item.shoot = ProjectileType<Projectiles.Ranged.AntimatterBullet>();
      item.ammo = AmmoID.Bullet;
		}
	}
}