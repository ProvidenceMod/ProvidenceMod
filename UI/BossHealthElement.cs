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
			if (percentage > 0 && boss?.life > 0 && boss != null)
			{
				if (opacity < 1f)
				{
					opacity += 1f / 30f;
					if (opacity > 1f)
						opacity = 1f;
				}

				Vector2 vPercent = ProvidenceMod.bossHealthFont.MeasureString(PercentageFormatter(percentage));
				Vector2 vTitle = ProvidenceMod.bossHealthFont.MeasureString(boss.FullName);
				Vector2 vName = ProvidenceMod.bossHealthFont.MeasureString(boss.Providence().GetBossTitle(boss.FullName));

				//spriteBatch.Draw(GetTexture("ProvidenceMod/UI/BossUnderline"), new Vector2(Left.Pixels + 500f - (vTitle.X / 2f), Top.Pixels), new Rectangle(0, 0, (int) vTitle.X, 3), Color.White);
				
				SpriteBatch spriteBatch1 = new SpriteBatch(Main.graphics.GraphicsDevice);
				spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

				spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowF"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));
				
				if (ProvidenceMod.Instance.bossPercentage)
					spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowR"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));
				
				if (ProvidenceMod.Instance.bossHP)
					spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowL"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(1f * opacity, 1f * opacity, 1f * opacity, 1f * opacity));
				


				DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss.FullName, new Vector2(Left.Pixels + 500f - (vTitle.X * 0.75f * 0.5f), Top.Pixels + 50), new Color((int)(200 * opacity), (int)(200 * opacity), (int)(200 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.75f);
				DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss?.Providence().GetBossTitle(boss.FullName), new Vector2(Left.Pixels + 500f - (vName.X * 0.40f * 0.5f), Top.Pixels + 5), new Color((int)(220 * opacity), (int)(220 * opacity), (int)(220 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.40f);
				
				if (ProvidenceMod.Instance.bossPercentage)
					DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, PercentageFormatter(percentage), new Vector2(Left.Pixels + 970f - vPercent.X, Top.Pixels - 2), new Color((int)(200 * opacity), (int)(200 * opacity), (int)(200 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.8f);
				
				if (ProvidenceMod.Instance.bossHP)
					DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, $"{boss?.life} / {boss?.lifeMax}", new Vector2(Left.Pixels + 50f, Top.Pixels + 3f), new Color((int)(220 * opacity), (int)(220 * opacity), (int)(220 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.5f);
				
				spriteBatch1.End();
				
				if (comboVisible)
				{
					spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
					spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowC"), new Vector2(comboPos.X - 55 + 15, comboPos.Y - 10), Color.White);
					DrawBorderStringEightWay(spriteBatch1, ProvidenceMod.bossHealthFont, $"{comboDMG}", comboPos, new Color(opacity, opacity, opacity, opacity), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.4f);
					spriteBatch1.End();
				}
			}
			else if (opacity > 0f)
			{
				opacity -= 1f / 30f;
				if (opacity < 0f)
					opacity = 0f;
			}
		}
		public string PercentageFormatter(float percentage)
		{
			if (percentage == 100f)
				return "100.0%";
			else if (percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 != 0)
				return $"{percentage.Round(1)}%";
			else if (percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 == 0)
				return $"{percentage.Round(1)}.0%";
			else if (percentage.Round(1) < 10f && percentage.Round(1) % 1 != 0)
				return $"{percentage.Round(1)}%";
			else if (percentage.Round(1) < 10f && percentage.Round(1) % 1 == 0)
				return $"{percentage.Round(1)}.0%";
			else
				return $"{percentage.Round(1)}%";
		}
	}
}