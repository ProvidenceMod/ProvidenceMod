using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			percentage = quotient * 100f;
			if (boss != null)
			{

			}
			if (percentage > 0 && boss?.life > 0 && boss != null)
			{
				Vector2 vPercent = ProvidenceMod.bossHealthFont.MeasureString(PercentageFormatter(percentage));
				Vector2 vTitle = ProvidenceMod.bossHealthFont.MeasureString(boss.Providence().GetBossTitle(boss.FullName));
				Vector2 vName = ProvidenceMod.bossHealthFont.MeasureString(boss.FullName);
				//spriteBatch.Draw(GetTexture("ProvidenceMod/UI/BossUnderline"), new Vector2(Left.Pixels + 500f - (vTitle.X / 2f), Top.Pixels), new Rectangle(0, 0, (int) vTitle.X, 3), Color.White);
				SpriteBatch spriteBatch1 = new SpriteBatch(Main.graphics.GraphicsDevice);
				spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
				spriteBatch1.Draw(GetTexture("ProvidenceMod/UI/BossShadow"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(255, 255, 255, 255f));
				spriteBatch1.Draw(GetTexture("ProvidenceMod/UI/BossShadow"), new Vector2(Left.Pixels, Top.Pixels - 13), new Color(255, 255, 255, 255f));
				spriteBatch1.End();
				Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, PercentageFormatter(percentage), Left.Pixels + 970f - vPercent.X, Top.Pixels - 2, new Color(200, 200, 200), new Color(23, 23, 23), Vector2.Zero, 0.8f);
				Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, $"{boss?.life} / {boss?.lifeMax}", Left.Pixels + 50f, Top.Pixels + 3f, new Color(220, 220, 220), new Color(23, 23, 23), Vector2.Zero, 0.5f);
				Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss?.Providence().GetBossTitle(boss.FullName), Left.Pixels + 500f - (vTitle.X * 0.75f * 0.5f), Top.Pixels + 50, new Color(200, 200, 200), new Color(23, 23, 23), Vector2.Zero, 0.75f);
				Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Main.fontItemStack, boss.FullName, Left.Pixels + 500f - (vName.X * 0.40f * 0.5f), Top.Pixels + 5, new Color(220, 220, 220), new Color(23, 23, 23), Vector2.Zero, 0.40f);
				if (comboVisible)
				{
					spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
					spriteBatch1.Draw(GetTexture("ProvidenceMod/UI/BossCShadow"), new Vector2(comboPos.X - 55 + 15, comboPos.Y - 10), Color.White);
					spriteBatch1.End();
					Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.bossHealthFont, $"{comboDMG}", comboPos.X, comboPos.Y, new Color(255, 255, 255), new Color(23, 23, 23), Vector2.Zero, 0.4f);
				}
			}
		}
		public string PercentageFormatter(float percentage)
		{
			if (percentage == 100f)
			{
				return "100.0%";
			}
			else if (percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 != 0)
			{
				return $"{percentage.Round(1)}%";
			}
			else if (percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 == 0)
			{
				return $"{percentage.Round(1)}.0%";
			}
			else if (percentage.Round(1) < 10f && percentage.Round(1) % 1 != 0)
			{
				return $"{percentage.Round(1)}%";
			}
			else if (percentage.Round(1) < 10f && percentage.Round(1) % 1 == 0)
			{
				return $"{percentage.Round(1)}.0%";
			}
			else
			{
				return $"{percentage.Round(1)}%";
			}
		}
	}
}