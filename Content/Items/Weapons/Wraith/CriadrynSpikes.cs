//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using ProvidenceMod.Projectiles.Wraith;
//using Terraria.DataStructures;

//namespace ProvidenceMod.Items.Weapons.Wraith
//{
//	public class CriadrynSpikes : WraithItem
//	{
//		public override void SetStaticDefaults()
//		{
//			DisplayName.SetDefault("Criadryn Spikes");
//		}
//		public override void SetDefaults()
//		{
//			Item.width = 24;
//			Item.height = 28;
//			Item.noMelee = true;
//			Item.useTime = 10;
//			Item.useAnimation = 10;
//			Item.useStyle = ItemUseStyleID.Swing;
//			Item.UseSound = SoundID.Item39;
//			Item.damage = 10;
//			Item.DamageType = DamageClass.Throwing;
//			Item.Providence().wraith = true;
//			Item.consumable = true;
//			Item.maxStack = 999;
//			Item.shoot = ModContent.ProjectileType<CriadrynSpikeThrown>();
//			Item.shootSpeed = 20f;
//			Item.autoReuse = true;
//		}
//		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//		{
//			int num = Main.rand.Next(1, 4);
//			for (int i = 0; i < num; i++)
//				Projectile.NewProjectile(source, position, velocity.RotatedBy((Main.rand.NextFloat(-100f, 101f) / 10f).InRadians()) * player.thrownVelocity, type, damage, knockback, player.whoAmI);
//			return false;
//		}
//	}
//}
