using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.UI
{
	internal class ParityUI : UIState
	{
		public static bool visible = true;
		private bool set;
		private bool arraySet;
		public float oldScale = Main.inventoryScale;
		private int radiantCooldown = 30;
		private int shadowCooldown = 30;
		// private float oldRadiant;
		// private float oldShadow;
		private ParityElement area;
		private UIImage ParityFrame;
		private UIImageFramed RadiantUse, ShadowUse, RadiantBar, ShadowBar;
		private Rectangle RadiantUseRect, ShadowUseRect, RadiantBarRect, ShadowBarRect;
		private readonly float[] ShadowArray = new float[3] { 0, 0, 0 };
		private readonly float[] RadiantArray = new float[3] { 0, 0, 0 };
		public override void OnInitialize()
		{
			area = new ParityElement();
			area.Left.Set(1350f, 0f);
			area.Top.Set(30f, 0f);
			area.Width.Set(220, 0f);
			area.Height.Set(38, 0f);

			ParityFrame = new UIImage(Request<Texture2D>("ProvidenceMod/UI/ParityUIFrame"));
			ParityFrame.Width.Set(220, 0f);
			ParityFrame.Height.Set(38, 0f);

			RadiantUseRect = new Rectangle(0, 0, 0, 6);
			RadiantUse = new UIImageFramed(Request<Texture2D>("ProvidenceMod/UI/ParityUIRadiantUse"), RadiantUseRect);
			RadiantUse.Top.Set(16, 0f);
			RadiantUse.Left.Set(32, 0f);
			RadiantUse.Width.Set(70, 0f);
			RadiantUse.Height.Set(6, 0f);

			RadiantBarRect = new Rectangle(0, 0, 0, 6);
			RadiantBar = new UIImageFramed(Request<Texture2D>("ProvidenceMod/UI/ParityUIRadiantBar"), RadiantBarRect);
			RadiantBar.Top.Set(16, 0f);
			RadiantBar.Left.Set(32, 0f);
			RadiantBar.Width.Set(70, 0f);
			RadiantBar.Height.Set(6, 0f);

			ShadowUseRect = new Rectangle(70, 0, 70, 6);
			ShadowUse = new UIImageFramed(Request<Texture2D>("ProvidenceMod/UI/ParityUIShadowUse"), ShadowUseRect);
			ShadowUse.Top.Set(16, 0f);
			ShadowUse.Left.Set(120, 0f);
			ShadowUse.Width.Set(70, 0f);
			ShadowUse.Height.Set(6, 0f);

			ShadowBarRect = new Rectangle(70, 0, 70, 6);
			ShadowBar = new UIImageFramed(Request<Texture2D>("ProvidenceMod/UI/ParityUIShadowBar"), ShadowBarRect);
			ShadowBar.Top.Set(16, 0f);
			ShadowBar.Left.Set(120, 0f);
			ShadowBar.Width.Set(70, 0f);
			ShadowBar.Height.Set(6, 0f);

			area.Append(ParityFrame);
			area.Append(RadiantUse);
			area.Append(RadiantBar);
			area.Append(ShadowUse);
			area.Append(ShadowBar);
			Append(area);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			area.visible = visible;
			if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
			ProvidencePlayer proPlayer = LocalPlayer().Providence();
			visible = proPlayer.cleric;
			if (visible)
			{
				float radiantQuotient = LocalPlayer().Providence().radiantStacks / LocalPlayer().Providence().parityMaxStacks;
				float shadowQuotient = LocalPlayer().Providence().shadowStacks / LocalPlayer().Providence().parityMaxStacks;
				radiantQuotient = Utils.Clamp(radiantQuotient, 0f, 1f);
				shadowQuotient = Utils.Clamp(shadowQuotient, 0f, 1f);
				RadiantBarRect.Width = (int)(68 * radiantQuotient);
				ShadowBarRect.X = (int)(68 * shadowQuotient);
				ShadowBarRect.Width = (int)(68 * shadowQuotient);
				ShadowBar.Left.Pixels = 120 + (70 - ShadowBarRect.Width);
				RadiantBar.SetFrame(RadiantBarRect);
				ShadowBar.SetFrame(ShadowBarRect);

				if (!set)
				{
					RadiantUseRect.Width = 0;
					ShadowUseRect.Width = 0;
					RadiantUse.SetFrame(RadiantBarRect);
					ShadowUse.SetFrame(ShadowBarRect);
					set = true;
				}

				if (!arraySet)
				{
					ShadowArray[2] = ShadowArray[1];
					ShadowArray[1] = ShadowArray[0];
					ShadowArray[0] = proPlayer.shadowStacks;
					RadiantArray[2] = RadiantArray[1];
					RadiantArray[1] = RadiantArray[0];
					RadiantArray[0] = proPlayer.radiantStacks;
					arraySet = true;
				}
				// Shadow
				if (proPlayer.shadowStacks < ShadowArray[0])
				{
					ShadowArray[2] = ShadowArray[1];
					ShadowArray[1] = ShadowArray[0];
					ShadowArray[0] = proPlayer.shadowStacks;
					// oldShadow = proPlayer.shadowStacks;
					shadowCooldown = 30;
				}
				if (shadowCooldown > 0)
				{
					shadowCooldown--;
				}
				if (shadowCooldown == 0 && ShadowUseRect.Width != ShadowBarRect.Width)
				{
					if ((ShadowUseRect.Width - ShadowBarRect.Width) * 0.05f < 1)
					{
						ShadowUseRect.Width--;
						ShadowUseRect.X++;
						ShadowUse.Left.Pixels++;
					}
					else
					{
						ShadowUseRect.Width -= (int)((ShadowUseRect.Width - ShadowBarRect.Width) * 0.05f);
						ShadowUseRect.X += (int)((ShadowUseRect.X - ShadowBarRect.X) * 0.05f);
						ShadowUse.Left.Pixels += (int)((ShadowUse.Left.Pixels - ShadowUse.Left.Pixels) * 0.05f);
					}
					ShadowUse.SetFrame(ShadowUseRect);
				}
				if (ShadowBarRect.X < ShadowUseRect.X)
				{
					ShadowUseRect.X = ShadowBarRect.X;
					ShadowUse.SetFrame(ShadowUseRect);
				}
				// Radiant
				if (proPlayer.radiantStacks < RadiantArray[0])
				{
					RadiantArray[2] = RadiantArray[1];
					RadiantArray[1] = RadiantArray[0];
					RadiantArray[0] = proPlayer.radiantStacks;
					if (RadiantArray[1] < RadiantArray[0])
					{
						radiantCooldown = 120;
					}
				}
				if (radiantCooldown > 0)
				{
					radiantCooldown--;
				}
				if (radiantCooldown == 0 && RadiantUseRect.Width != RadiantBarRect.Width)
				{
					if ((RadiantUseRect.Width - RadiantBarRect.Width) * 0.05f < 1)
					{
						RadiantUseRect.Width--;
					}
					else
					{
						RadiantUseRect.Width -= (int)((RadiantUseRect.Width - RadiantBarRect.Width) * 0.05f);
					}
					RadiantUse.SetFrame(RadiantUseRect);
				}
				if (RadiantBarRect.Width > RadiantUseRect.Width)
				{
					RadiantUseRect.Width = RadiantBarRect.Width;
					RadiantUse.SetFrame(RadiantUseRect);
				}

				// if (prov.ShadowAmp) bLFrame.SetImage(Request<Texture2D>("ProvidenceMod/UI/ShadowUIFrameAmp"));
				// else bLFrame.SetImage(Request<Texture2D>("ProvidenceMod/UI/ShadowUIFrame"));
			}
		}
	}
}