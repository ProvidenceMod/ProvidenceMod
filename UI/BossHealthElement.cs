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
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			percentage = quotient * 100f;
			if (percentage > 0 && boss != null)
			{
				Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.providenceFont ?? Main.fontItemStack, PercentageFormatter(percentage), Left.Pixels + 500, Top.Pixels - 30, new Color(104, 237, 195), new Color(55, 62, 106), Vector2.Zero, 0.5f);
				//Utils.DrawBorderStringFourWay(spriteBatch, ProvidenceMod.providenceFont, $"11111111", Left.Pixels, Top.Pixels + 40, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
				spriteBatch.Draw(GetTexture("ProvidenceMod/UI/BossHealthUIAccent"), new Vector2(Left.Pixels + 12, Top.Pixels + 14), new Rectangle(0, 0, 654, 2), new Color(255, 255, 255, 255), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}
		public string PercentageFormatter(float percentage)
		{
			string percentageString;
			if(percentage == 100f)
			{
				return percentageString = "100.0%";
			}
			else if(percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 != 0)
			{
				return percentageString = $"    {percentage.Round(1)}%";
			}
			else if(percentage.Round(1) < 100f && percentage.Round(1) > 10f && percentage.Round(1) % 1 == 0)
			{
				return percentageString = $"    {percentage.Round(1)}.0%";
			}
			else if(percentage.Round(1) < 10f && percentage.Round(1) % 1 != 0)
			{
				return percentageString = $"        {percentage.Round(1)}%";
			}
			else if(percentage.Round(1) < 10f && percentage.Round(1) % 1 == 0)
			{
				return percentageString = $"        {percentage.Round(1)}.0%";
			}
			else
			{
				return percentageString = $"{percentage.Round(1)}%";
			}
		}
	}
}