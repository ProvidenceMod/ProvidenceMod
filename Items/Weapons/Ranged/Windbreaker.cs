using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Projectiles.Ranged;
namespace ProvidenceMod.Items.Weapons.Ranged
{
	public class TempestBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tempest Blaster");
			Tooltip.SetDefault("haha blow. dont worry this text isn't perminant, i just cant think of anything better atm.");
		}
		public override void SetDefaults()
		{
			item.damage = 12;
			item.width = 13;
			item.height = 10;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = (int)ProvidenceRarity.Purple;
			item.useTime = 13;
			item.useAnimation = 26;
			item.useTurn = false;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.scale = 1.0f;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileType<Projectiles.Ranged.TempestWave>();
			item.shootSpeed = 20f;
			item.UseSound = SoundID.DD2_PhantomPhoenixShot;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 2; i++){
        Vector2 pSpeed = new Vector2(speedX, speedY).RotatedByRandom(2f.InRadians());
        int projectile = Projectile.NewProjectile(player.position, pSpeed, ProjectileType<Projectiles.Ranged.TempestWave>(), damage, knockBack, player.whoAmI );
        Projectile Projectile2 = Main.projectile[projectile];
        Projectile2.penetrate = 3;
      }
			return false;
		}
	}
}