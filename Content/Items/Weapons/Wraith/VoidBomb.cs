using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using Providence.Rarities;

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
			Item.useTime = 10;
			Item.useAnimation = 10;
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
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			return true;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			return true;
		}
	}
}
