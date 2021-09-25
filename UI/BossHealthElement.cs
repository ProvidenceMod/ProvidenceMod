using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.UI
{
	internal class BossHealthElement : UIElement
	{
		public int comboDMG;
		public float quotient;
		public float percentage;
		public float opacity;
		public bool comboVisible;

		public NPC boss;

		public Vector2 comboPos;

		public Rectangle healthRect = new Rectangle(0, 0, 924, 6);
		public Rectangle comboRect = new Rectangle(0, 0, 924, 6);

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			percentage = quotient * 100f;
			if (percentage > 0 && boss != null)
			{
				if (opacity < 1f)
				{
					opacity += 1f / 60f;
					if (opacity > 1f)
						opacity = 1f;
				}
			}
			else if (opacity > 0f)
			{
				opacity -= 1f / 60f;
				if (opacity < 0f)
					opacity = 0f;
			}
			SpriteBatch spriteBatch1 = new SpriteBatch(Main.graphics.GraphicsDevice);

			spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowF"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));

			string text = percentage.ToString("N1") + "%";

			Vector2 vPercent = boss != null ? ProvidenceMod.bossHealthFont.MeasureString(text) : default;
			Vector2 vTitle = boss != null ? ProvidenceMod.bossHealthFont.MeasureString(boss.FullName) : default;
			Vector2 vName = boss != null ? ProvidenceMod.bossHealthFont.MeasureString(boss.Providence().GetBossTitle(boss.FullName)) : default;

			if (ProvidenceMod.Instance.bossPercentage)
			{
				spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowR"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));
				DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, text, new Vector2(Left.Pixels + 970f - vPercent.X, Top.Pixels - 2), new Color((int)(200 * opacity), (int)(200 * opacity), (int)(200 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.8f);
			}

			if (ProvidenceMod.Instance.bossHP)
			{
				spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowL"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));
				DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, $"{boss?.life} / {boss?.lifeMax}", new Vector2(Left.Pixels + 50f, Top.Pixels + 3f), new Color((int)(220 * opacity), (int)(220 * opacity), (int)(220 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.5f);
			}

			DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss != null ? boss.FullName : "", new Vector2(Left.Pixels + 500f - (vTitle.X * 0.75f * 0.5f), Top.Pixels + 50), new Color((int)(200 * opacity), (int)(200 * opacity), (int)(200 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.75f);
			DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss != null ? boss.Providence().GetBossTitle(boss?.FullName) : "", new Vector2(Left.Pixels + 500f - (vName.X * 0.40f * 0.5f), Top.Pixels + 5), new Color((int)(220 * opacity), (int)(220 * opacity), (int)(220 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.40f);

			if (comboVisible)
			{
				spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowC"), new Vector2(comboPos.X - 55 + 15, comboPos.Y - 10), Color.White);
				DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont, $"{comboDMG}", comboPos, new Color(opacity, opacity, opacity, opacity), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.4f);
			}

			spriteBatch1.End();
		}
	}
}