using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Tiles.SentinelAether
{
	public class ZephyrBricks : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			DustType = DustID.Platinum;
			ItemDrop = ItemType<Items.Placeables.SentinelAether.ZephyrBricks>();
			SoundType = SoundID.Tink;
			SoundStyle = 1;
			//mineResist = 4f;
			//minPick = 200;
		}
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			CheckTiles(i, j, out bool up, out bool down, out bool left, out bool right, out bool leftUp, out bool leftDown, out bool rightUp, out bool rightDown);

			Texture2D tex = Request<Texture2D>("ProvidenceMod/Tiles/SentinelAether/ZephyrBricks").Value;

			if (up && down && left && right && leftUp && leftDown && rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(10 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && leftUp && leftDown && !rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(10 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && leftDown && rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(9 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && leftUp && !leftDown && rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(9 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}

			if (up && down && left && right && leftUp && leftDown && !rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(12 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && leftUp && !leftDown && !rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(12 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && !leftDown && rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(11 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && leftDown && rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(11 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}

			if (up && down && left && right && leftUp && !leftDown && !rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(14 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && !leftDown && !rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(14 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && !leftDown && rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(13 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && !leftUp && leftDown && !rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(13 * 18, 13 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}

			if (up && down && left && right && !leftUp && leftDown && !rightDown && rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(7 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			if (up && down && left && right && leftUp && !leftDown && rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(8 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}

			if (up && down && left && right && !leftUp && !leftDown && !rightDown && !rightUp)
			{
				spriteBatch.Draw(tex, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(192f, 192f), new Rectangle(15 * 18, 12 * 18, 16, 16), Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			return true;
		}
	}
}