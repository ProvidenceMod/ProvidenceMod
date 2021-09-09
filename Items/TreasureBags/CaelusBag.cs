using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Placeables.Ores;
using ProvidenceMod.Items.Materials;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProvidenceMod.Items.TreasureBags
{
	public class CaelusBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 56;
			item.height = 32;
			item.expertOnly = true;
			item.expert = true;
			item.rare = ItemRarityID.Expert;
		}

		public override bool CanRightClick() => true;

		public override void RightClick(Player player)
		{
			// 26 to 74 ore spawned
			player.QuickSpawnItem(ItemType<ZephyrOre>(), Main.rand.Next(26, 75));
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (item.Providence().highlight)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = 1f - (i * 0.05f);
					float newScale = 1f + ((i + 1f) * 0.05f);
					Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / 10f).ColorRGBAIntToFloat();
					colorV.X *= alpha;
					colorV.Y *= alpha;
					colorV.Z *= alpha;
					colorV.W *= alpha;
					Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
					spriteBatch.Draw(GetTexture("ProvidenceMod/Items/TreasureBags/CaelusBag"), item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), color, rotation, new Vector2(item.width / 2, item.height / 2), newScale, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(GetTexture("ProvidenceMod/Items/TreasureBags/CaelusBag"), item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), lightColor, rotation, new Vector2(item.width / 2, item.height / 2), 1f, SpriteEffects.None, 0f);
				return false;
			}
			else
				return true;
		}
	}
}