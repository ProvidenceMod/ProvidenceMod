using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Providence.Content.Dusts;
using Providence.Content.Items.Placeables.Ores;
using Providence.Systems;
using Terraria.DataStructures;

namespace Providence.Content.Items.TreasureBags
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
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 56;
			Item.height = 32;
			Item.expertOnly = true;
			Item.expert = true;
			Item.rare = ItemRarityID.Expert;
		}

		public override bool CanRightClick() => true;

		public override void RightClick(Player player)
		{
			if (WorldFlags.lament && !WorldFlags.wrath)
			{
				player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemType<ZephyrOre>(), Main.rand.Next(36, 76));
				return;
			}
			if (WorldFlags.wrath)
			{
				player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemType<ZephyrOre>(), Main.rand.Next(46, 91));
				return;
			}
			player.QuickSpawnItem(new EntitySource_ItemOpen(Item, Item.type), ItemType<ZephyrOre>(), Main.rand.Next(26, 61));
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			SpriteBatch spriteBatch1 = new(Main.graphics.GraphicsDevice);
			spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			if (Main.GlobalTimeWrappedHourly % 30 == 0)
			{
				Vector2 pos = new Vector2(0f, 16f).RotatedBy(Main.rand.NextFloat(0f, MathHelper.TwoPi));
				Dust.NewDustPerfect(Item.Center + pos, DustType<CloudDust>(), -pos * 0.5f);
			}
			if (Item.Providence().highlight)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = 1f - (i * 0.05f);
					float newScale = 1f + ((i + 1f) * 0.05f);
					Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / 10f).RGBAIntToFloat();
					colorV.X *= alpha;
					colorV.Y *= alpha;
					colorV.Z *= alpha;
					colorV.W *= alpha;
					Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);
					spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), color, rotation, new Vector2(Item.width / 2, Item.height / 2), newScale, SpriteEffects.None, 0f);
				}
				float sin = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 12f) * 4f;
				float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 12f) * 4f;
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition + new Vector2(4f, 0f) + new Vector2(cos, sin), new Rectangle(0, 0, Item.width, Item.height), new Color(1f, 1f, 1f, 0.25f), rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition + new Vector2(0f, 4f) + new Vector2(cos, sin), new Rectangle(0, 0, Item.width, Item.height), new Color(1f, 1f, 1f, 0.25f), rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition + new Vector2(-4f, 0f) + new Vector2(cos, sin), new Rectangle(0, 0, Item.width, Item.height), new Color(1f, 1f, 1f, 0.25f), rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition + new Vector2(0f, -4f) + new Vector2(cos, sin), new Rectangle(0, 0, Item.width, Item.height), new Color(1f, 1f, 1f, 0.25f), rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/TreasureBags/CaelusBag").Value, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), lightColor, rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
				spriteBatch1.End();
				return false;
			}
			else
			{
				spriteBatch1.End();
				return true;
			}
		}
	}
}
