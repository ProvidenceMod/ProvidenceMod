using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Metaballs;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.Metaballs.MaskManager;

namespace ProvidenceMod.Items.Weapons.Rogue
{
	public class VoidBomb : ModItem, IGalaxySprite
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Bomb");
			Tooltip.SetDefault("A bomb of the cosmos.\nDamage ramps up based on how much of its fuse remains");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.width = 90;
			item.height = 90;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.thrown = true;
			item.shoot = ProjectileType<Projectiles.Rogue.VoidBomb>();
			item.shootSpeed = 12f;
			item.autoReuse = true;
			item.rare = (int)ProvidenceRarity.Developer;
			item.maxStack = 999;
			item.noMelee = true;
			item.consumable = true;
			item.Providence().customRarity = ProvidenceRarity.Developer;
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			DrawGalaxyMappedSprite(spriteBatch);
			return true;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			DrawGalaxyMappedSprite(spriteBatch);
			return true;
		}
		public void DrawGalaxyMappedSprite(SpriteBatch sB)
		{
			if (item.type == ItemType<VoidBomb>() && item.active)
			{
				Texture2D tex = GetTexture("ProvidenceMod/Projectiles/Rogue/VoidBomb");
				sB.Draw(tex, (item.Center - Main.screenPosition + new Vector2(0, 0)) / 2, new Rectangle(0, 0, item.width, item.height), Color.White, default, new Vector2(item.width / 2, item.height / 2), item.scale / 2f, SpriteEffects.None, 0);
			}
		}
	}
}
