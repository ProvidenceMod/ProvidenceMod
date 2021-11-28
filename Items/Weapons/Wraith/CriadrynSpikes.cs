using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Wraith;

namespace ProvidenceMod.Items.Weapons.Wraith
{
	public class CriadrynSpikes : WraithItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Criadryn Spikes");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.noMelee = true;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item39;
			item.damage = 10;
			item.thrown = true;
			item.Providence().wraith = true;
			item.consumable = true;
			item.maxStack = 999;
			item.shoot = ModContent.ProjectileType<CriadrynSpikeThrown>();
			item.shootSpeed = 20f;
			item.autoReuse = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = Main.rand.Next(1, 4);
			for (int i = 0; i < num; i++)
				Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy((Main.rand.NextFloat(-100f, 101f) / 10f).InRadians()) * player.thrownVelocity, type, damage, knockBack, player.whoAmI);
			return false;
		}
	}
}
