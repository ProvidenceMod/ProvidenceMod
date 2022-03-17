using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Providence.DamageClasses;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace Providence.Content.Items.Weapons.Wraith
{
	public class VoidBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Bomb");
			Tooltip.SetDefault("A bomb of the cosmos.\nDamage ramps up based on how much of its fuse remains");
		}
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.width = 90;
			Item.height = 90;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.DamageType = DamageClass.Throwing;
			Item.shoot = ProjectileType<Projectiles.Wraith.VoidBomb>();
			Item.shootSpeed = 12f;
			Item.autoReuse = true;
			Item.rare = (int)ProvidenceRarity.Developer;
			Item.maxStack = 999;
			Item.noMelee = true;
			Item.consumable = true;
			Item.rare = RarityType<Developer>();
			Item.damage = 50;
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			return true;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 3; i++)
				_ = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(10f.InRadians()), type, damage, knockback);
			return false;
		}
	}
}
