using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.UI
{
	public static class BossHealthBarManager
	{
		public static bool visible;
		public static int barNum;
		public static int npcNum;
		public readonly static int barMax = 5;
		public readonly static int maxNPCs = 5;
		public static List<NPC> npcs;

		public static Texture2D frameTexture;
		public static Texture2D healthTexture;
		public static Texture2D comboTexture;
		public static Texture2D shadowLeftTexture;
		public static Texture2D shadowRightTexture;
		public static Texture2D shadowMiddleTexture;
		public static Texture2D shadowComboTexture;
		public static Texture2D underlineTexture;
		public static Texture2D bloomTexture;

		public static Vector2 anchor;
		public static Vector2 screenSize;

		public static SpriteBatch spriteBatch;


		public enum BossHealthBarType
		{
			MainBossHealthBar = 0,
			MiniBossHealthBar1 = 1,
			MiniBossHealthBar2 = 2,
			MiniBossHealthBar3 = 3,
			MiniBossHealthBar4 = 4
		}

		public static void Initialize()
		{
			npcs = new List<NPC>();
			spriteBatch = new SpriteBatch(Main.graphics.GraphicsDevice);
			frameTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossFrame").Value;
			healthTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossHP").Value;
			comboTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossCombo").Value;
			shadowLeftTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossShadowL").Value;
			shadowRightTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossShadowR").Value;
			shadowMiddleTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossShadowF").Value;
			shadowComboTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossShadowC").Value;
			underlineTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossUnderline").Value;
			bloomTexture = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/BossBloom").Value;
		}
		public static void Update()
		{
			for (int i = 0; i < npcs.Count; i++)
			{
				if (npcs[i]?.life <= 0)
				{
					npcs.RemoveAt(i);
					ClearHealthBarData(i);
				}
			}
			npcNum = 0;
			foreach (NPC npc in Main.npc)
			{
				if (npc.boss && npcs.Count < maxNPCs && !npcs.Contains(npc))
				{
					npcs.Add(npc);
				}
			}
			for (int k = 0; k < npcs.Count; k++)
			{
				if (npcs[k] != null)
				{
					npcNum++;
					if (k == 0)
					{
						shouldFadeOut1 = false;
						npc1 = npcs[k];
					}
					else if (k == 1)
					{
						shouldFadeOut2 = false;
						npc2 = npcs[k];
					}
					else if (k == 2)
					{
						shouldFadeOut3 = false;
						npc3 = npcs[k];
					}
					else if (k == 3)
					{
						shouldFadeOut4 = false;
						npc4 = npcs[k];
					}
					else if (k == 4)
					{
						shouldFadeOut5 = false;
						npc5 = npcs[k];
					}
				}
			}
			screenSize.X = Main.screenWidth;
			screenSize.Y = Main.screenHeight;
			anchor.X = ((Main.screenWidth - 1000f) / 2);
			anchor.Y = (Main.screenHeight - 100);
			//if (npcs[0]?.life > 0 || opacity1 > 0)
			//	DrawMainHealthBar();
			//if (npcs[1]?.life > 0 || opacity2 > 0)
			//	DrawMiniHealthBar1();
			//if (npcs[2]?.life > 0 || opacity3 > 0)
			//	DrawMiniHealthBar2();
			//if (npcs[3]?.life > 0 || opacity4 > 0)
			//	DrawMiniHealthBar3();
			//if (npcs[4]?.life > 0 || opacity5 > 0)
			//	DrawMiniHealthBar4();
		}

		public static int comboDMG1;
		public static int comboDMCCounter1;
		public static int comboRectCooldown1;
		public static int oldLife1;

		public static float quotient1;
		public static float opacity1;

		public static bool shouldFadeOut1;
		public static bool comboVisible1;

		public static Rectangle healthRect1;
		public static Rectangle comboRect1;

		public static NPC npc1;
		public static void DrawMainHealthBar()
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			if (!shouldFadeOut1)
			{
				if (oldLife1 == 0)
					oldLife1 = npc1.lifeMax;
				if (opacity1 < 1f)
				{
					opacity1 += 1f / 60f;
					if (opacity1 > 1f)
						opacity1 = 1f;
				}
				quotient1 = (float)npc1.life / (float)npc1.lifeMax;
				if (comboRect1.Width < healthRect1.Width)
					comboRect1.Width = healthRect1.Width;
				Vector2 comboPos = Vector2.Zero;
				if (comboDMG1 >= 0)
					comboPos = new Vector2(anchor.X + 34 + healthRect1.Width + ((comboRect1.Width - healthRect1.Width) / 2), anchor.Y + 7);
				if (npc1.life < oldLife1)
				{
					comboDMG1 += oldLife1 - npc1.life;
					oldLife1 = npc1.life;
					comboRectCooldown1 = 75;
					comboVisible1 = true;
					comboDMCCounter1 = 0;
				}
				else if (npc1.life == oldLife1)
				{
					if (comboRectCooldown1 > 0)
						comboRectCooldown1--;
				}
				if (comboRectCooldown1 == 0 && comboRect1.Width != healthRect1.Width)
				{
					if ((comboRect1.Width - healthRect1.Width) * 0.05f < 1)
						comboRect1.Width--;
					else
						comboRect1.Width -= (int)((comboRect1.Width - healthRect1.Width) * 0.05f);
				}
				if ((comboRectCooldown1 == 0 && comboDMG1 != 0) || (healthRect1.Width == comboRect1.Width && comboDMG1 != 0))
					comboDMG1 -= (int)(comboDMG1 * 0.05f) + 1;
				if (comboDMG1 == 0 && comboDMCCounter1 != 120)
					comboDMCCounter1++;
				if (comboDMCCounter1 == 120)
					comboVisible1 = false;

				spriteBatch.Draw(frameTexture, anchor, new Rectangle(0, 0, 1000, 46), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(comboTexture, anchor + new Vector2(34f, 22f), comboRect1, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(healthTexture, anchor + new Vector2(34f, 22f), healthRect1, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(bloomTexture, anchor + new Vector2(31f + healthRect1.Width, 19f), new Rectangle(0, 0, 1000, 46), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

				spriteBatch.Draw(shadowMiddleTexture, new Vector2(anchor.X, anchor.Y - 13), new Color(1f * opacity1, 1f * opacity1, 1f * opacity1, 1f * opacity1));

				string text = (quotient1 * 100f).ToString("N1") + "%";

				Vector2 vPercent = ProvidenceMod.bossHealthFont.MeasureString(text);
				Vector2 vTitle = ProvidenceMod.bossHealthFont.MeasureString(npcs[0].FullName);
				Vector2 vName = ProvidenceMod.bossHealthFont.MeasureString(npcs[0].Providence().GetBossTitle(npcs[0].FullName));

				if (ProvidenceMod.Instance.bossPercentage)
				{
					spriteBatch.Draw(shadowRightTexture, new Vector2(anchor.X, anchor.Y - 13), new Color(1f * opacity1, 1f * opacity1, 1f * opacity1, 1f * opacity1));
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Terraria.GameContent.FontAssets.ItemStack.Value, text, new Vector2(anchor.X + 970f - vPercent.X, anchor.Y - 2), new Color((int)(200 * opacity1), (int)(200 * opacity1), (int)(200 * opacity1), (int)(255 * opacity1)), new Color((int)(23 * opacity1), (int)(23 * opacity1), (int)(23 * opacity1), (int)(255 * opacity1)), 0.8f);
				}

				if (ProvidenceMod.Instance.bossHP)
				{
					spriteBatch.Draw(shadowLeftTexture, new Vector2(anchor.X, anchor.Y - 13), new Color(1f * opacity1, 1f * opacity1, 1f * opacity1, 1f * opacity1));
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Terraria.GameContent.FontAssets.ItemStack.Value, $"{npcs[0]?.life} / {npcs[0]?.lifeMax}", new Vector2(anchor.X + 50f, anchor.Y + 3f), new Color((int)(220 * opacity1), (int)(220 * opacity1), (int)(220 * opacity1), (int)(255 * opacity1)), new Color((int)(23 * opacity1), (int)(23 * opacity1), (int)(23 * opacity1), (int)(255 * opacity1)), 0.5f);
				}

				DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Terraria.GameContent.FontAssets.ItemStack.Value, npcs[0] != null ? npcs[0].FullName : "", new Vector2(anchor.X + 500f - (vTitle.X * 0.75f * 0.5f), anchor.Y + 50), new Color((int)(200 * opacity1), (int)(200 * opacity1), (int)(200 * opacity1), (int)(255 * opacity1)), new Color((int)(23 * opacity1), (int)(23 * opacity1), (int)(23 * opacity1), (int)(255 * opacity1)), 0.75f);
				DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont ?? Terraria.GameContent.FontAssets.ItemStack.Value, npcs[0] != null ? npcs[0].Providence().GetBossTitle(npcs[0]?.FullName) : "", new Vector2(anchor.X + 500f - (vName.X * 0.40f * 0.5f), anchor.Y + 5), new Color((int)(220 * opacity1), (int)(220 * opacity1), (int)(220 * opacity1), (int)(255 * opacity1)), new Color((int)(23 * opacity1), (int)(23 * opacity1), (int)(23 * opacity1), (int)(255 * opacity1)), 0.40f);

				if (comboVisible1)
				{
					spriteBatch.Draw(shadowComboTexture, new Vector2(comboPos.X - 55 + 15, comboPos.Y - 10), Color.White);
					DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont, $"{comboDMG1}", comboPos, new Color(opacity1, opacity1, opacity1, opacity1), new Color((int)(23 * opacity1), (int)(23 * opacity1), (int)(23 * opacity1), (int)(255 * opacity1)), 0.4f);
				}
			}
			else
			{
				if (opacity1 > 0f)
				{
					opacity1 -= 1f / 60f;
					if (opacity1 < 0f)
						opacity1 = 0f;
				}
			}
			spriteBatch.End();
		}
		public static int comboDMG2;
		public static int comboDMCCounter2;
		public static int comboRectCooldown2;
		public static int oldLife2;

		public static float quotient2;
		public static float opacity2;

		public static bool shouldFadeOut2;
		public static bool comboVisible2;

		public static Rectangle healthRect2;
		public static Rectangle comboRect2;

		public static NPC npc2;
		public static void DrawMiniHealthBar1()
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch.End();
		}
		public static int comboDMG3;
		public static int comboDMCCounter3;
		public static int comboRectCooldown3;
		public static int oldLife3;

		public static float quotient3;
		public static float opacity3;

		public static bool shouldFadeOut3;
		public static bool comboVisible3;

		public static Rectangle healthRect3;
		public static Rectangle comboRect3;

		public static NPC npc3;
		public static void DrawMiniHealthBar2()
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch.End();
		}
		public static int comboDMG4;
		public static int comboDMCCounter4;
		public static int comboRectCooldown4;
		public static int oldLife4;

		public static float quotient4;
		public static float opacity4;

		public static bool shouldFadeOut4;
		public static bool comboVisible4;

		public static Rectangle healthRect4;
		public static Rectangle comboRect4;

		public static NPC npc4;
		public static void DrawMiniHealthBar3()
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch.End();
		}
		public static int comboDMG5;
		public static int comboDMCCounter5;
		public static int comboRectCooldown5;
		public static int oldLife5;

		public static float quotient5;
		public static float opacity5;

		public static bool shouldFadeOut5;
		public static bool comboVisible5;

		public static Rectangle healthRect5;
		public static Rectangle comboRect5;

		public static NPC npc5;
		public static void DrawMiniHealthBar4()
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch.End();
		}
		public static void ClearHealthBarData(int type)
		{
			switch (type)
			{
				case 0:
					comboDMG1 = 0;
					comboDMCCounter1 = 0;
					comboRectCooldown1 = 0;
					oldLife1 = 0;
					quotient1 = 0;
					shouldFadeOut1 = true;
					comboVisible1 = false;
					healthRect1 = Rectangle.Empty;
					comboRect1 = Rectangle.Empty;
					npc1 = null;
					break;
				case 1:
					comboDMG2 = 0;
					comboDMCCounter2 = 0;
					comboRectCooldown2 = 0;
					oldLife2 = 0;
					quotient2 = 0;
					shouldFadeOut2 = true;
					comboVisible2 = false;
					healthRect2 = Rectangle.Empty;
					comboRect2 = Rectangle.Empty;
					npc2 = null;
					break;
				case 2:
					comboDMG3 = 0;
					comboDMCCounter3 = 0;
					comboRectCooldown3 = 0;
					oldLife3 = 0;
					quotient3 = 0;
					shouldFadeOut3 = true;
					comboVisible3 = false;
					healthRect3 = Rectangle.Empty;
					comboRect3 = Rectangle.Empty;
					npc3 = null;
					break;
				case 3:
					comboDMG4 = 0;
					comboDMCCounter4 = 0;
					comboRectCooldown4 = 0;
					oldLife4 = 0;
					quotient4 = 0;
					shouldFadeOut4 = true;
					comboVisible4 = false;
					healthRect4 = Rectangle.Empty;
					comboRect4 = Rectangle.Empty;
					npc4 = null;
					break;
				case 4:
					comboDMG5 = 0;
					comboDMCCounter5 = 0;
					comboRectCooldown5 = 0;
					oldLife5 = 0;
					quotient5 = 0;
					shouldFadeOut5 = true;
					comboVisible5 = false;
					healthRect5 = Rectangle.Empty;
					comboRect5 = Rectangle.Empty;
					npc5 = null;
					break;
			}
		}
	}
}
