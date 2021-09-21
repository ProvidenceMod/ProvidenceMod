using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Placeables.Ores;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Materials
{
	public class ZephyrBar : ModItem
	{
		public int frameNumber;
		public int frameTick;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Bar");
			Tooltip.SetDefault("Even condensed, it's still as light as air.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 15));
		}

		public override void SetDefaults()
		{
			item.material = true;
			item.width = 66;
			item.height = 66;
			item.rare = (int)ProvidenceRarity.Orange;
		}
		//public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		//{
		//	spriteBatch.Draw(GetTexture("ProvidenceMod/Items/Materials/ZephyrBarSheet"), position - new Vector2(item.width * scale * 0.5f, item.height * scale * 0.5f), item.AnimationFrame(ref frameNumber, ref frameTick, 6, 15, true), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
		//	return false;
		//}
		//public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		//{
		//	spriteBatch.Draw(GetTexture("ProvidenceMod/Items/Materials/ZephyrBarSheet"), item.Center - Main.screenPosition, item.AnimationFrame(ref frameNumber, ref frameTick, 6, 15, true), Color.White, 0f, new Vector2(item.width * 0.5f, item.height * 0.5f), 1f, SpriteEffects.None, 0f);
		//	return false;
		//}
		public override void AddRecipes()
		{
			ModRecipe r = new ModRecipe(mod);
			r.AddIngredient(ItemType<ZephyrOre>(), 4);
			r.AddTile(TileID.SkyMill);
			r.SetResult(this);
			r.AddRecipe();
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
