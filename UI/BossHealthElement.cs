using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.UI
{

	internal class BossHealthElement : UIElement
	{
		//
		//
		// This is the quotient, or, the value from 0.0f to 1.0f that we use to determine how much of the health bar to render
		public float quotient;
		public float percentage;
		public NPC boss;
		public int comboDMG;
		public bool comboVisible;
		public Vector2 comboPos;
		public float opacity;
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
				spriteBatch1.End();
				DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss.FullName, new Vector2(Left.Pixels + 500f - (vTitle.X * 0.75f * 0.5f), Top.Pixels + 50), new Color(200, 200, 200, 255), new Color(23, 23, 23, 255), 0.75f);
				DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss?.Providence().GetBossTitle(boss.FullName), new Vector2(Left.Pixels + 500f - (vName.X * 0.40f * 0.5f), Top.Pixels + 5), new Color(220, 220, 220, 255), new Color(23, 23, 23, 255), 0.40f);
				if (ProvidenceMod.Instance.bossPercentage)
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, PercentageFormatter(percentage), new Vector2(Left.Pixels + 970f - vPercent.X, Top.Pixels - 2), new Color(200, 200, 200, 255), new Color(23, 23, 23, 255), 0.8f);
				if (ProvidenceMod.Instance.bossHP)
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, $"{boss?.life} / {boss?.lifeMax}", new Vector2(Left.Pixels + 50f, Top.Pixels + 3f), new Color(220, 220, 220, 255), new Color(23, 23, 23, 255), 0.5f);
				if (comboVisible)
				{
					spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
					spriteBatch1.Draw(GetTexture("ProvidenceMod/ExtraTextures/UI/BossShadowC"), new Vector2(comboPos.X - 55 + 15, comboPos.Y - 10), Color.White);
					spriteBatch1.End();
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont, $"{comboDMG}", comboPos, new Color(255, 255, 255, 255), new Color(23, 23, 23, 255), 0.4f);
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
		public void DrawBorderStringEightWay( SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, Color border, float scale = 1f )
		{
			for (int x = -2; x <= 2; x++)
			{
				for (int y = -2; y <= 2; y++)
				{
					Vector2 vector2 = position + new Vector2(x, y);
					if (x != 0 || y != 0)
						spriteBatch.DrawString(font, text, vector2, border, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
				}
			}
			spriteBatch.DrawString(font, text, position, color, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
		}
	}
}