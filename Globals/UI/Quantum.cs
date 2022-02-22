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
	internal class Quantum : UIElement
	{
		private bool OldSet;
		private int Timer = 30;
		private int frame;
		private int frameTick;
		private Player Player;
		private ProvidencePlayer ProPlayer;
		private Vector2 Position;
		private Texture2D QuantumFrame;
		private Texture2D QuantumUse;
		private Texture2D QuantumBar;
		private Texture2D QuantumLightning;
		private Rectangle QuantumUseRect;
		private Rectangle QuantumBarRect;
		private Rectangle QuantumLightningRect;
		private SpriteBatch sb;
		private float[] OldQuantum;
		public override void OnInitialize()
		{
			OldQuantum = new float[3];

			QuantumFrame = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/QuantumFrame").Value;

			QuantumUseRect = new Rectangle(0, 0, 0, 24);
			QuantumUse = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/QuantumUse").Value;

			QuantumBarRect = new Rectangle(0, 0, 0, 24);
			QuantumBar = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/QuantumBar").Value;

			QuantumLightningRect = new Rectangle(0, 0, 0, 22);
			QuantumLightning = Request<Texture2D>("ProvidenceMod/ExtraTextures/UI/QuantumLightning").Value;

			sb = new SpriteBatch(Main.graphics.GraphicsDevice);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (LocalPlayer() != null)
				Player = LocalPlayer();

			if (Player != null)
				ProPlayer = LocalPlayer().Providence();

			if (Player != null)
				Position = Player.position + new Vector2(-24f * Main.GameZoomTarget, (Player.height + 4f) * Main.GameZoomTarget) - Main.screenPosition;

			if (Position != Vector2.Zero && ProPlayer.wraith)
			{
				UpdateOldQuantum();
				UpdateQuantumUseRect();

				sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
				sb.Draw(QuantumFrame, Position, new Rectangle(0, 0, QuantumFrame.Width, QuantumFrame.Height), Color.White, 0f, Vector2.Zero, 1f * Main.GameZoomTarget, SpriteEffects.None, 0f);
				sb.Draw(QuantumUse, Position + new Vector2(8f * Main.GameZoomTarget, 10f * Main.GameZoomTarget), QuantumUseRect, Color.White, 0f, Vector2.Zero, 0.25f * Main.GameZoomTarget, SpriteEffects.None, 0f);
				sb.Draw(QuantumBar, Position + new Vector2(8f * Main.GameZoomTarget, 10f * Main.GameZoomTarget), QuantumBarRect, Color.White, 0f, Vector2.Zero, 0.25f * Main.GameZoomTarget, SpriteEffects.None, 0f);
				sb.End();
				sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
				if (ProPlayer.quantum == ProPlayer.quantumMax)
					sb.Draw(QuantumLightning, Position + new Vector2(-12f * Main.GameZoomTarget, 0f), QuantumLightning.AnimationFrame(ref frame, ref frameTick, 6, 10, true), new Color(1f, 1f, 1f, 0f), 0f, Vector2.Zero, 1f * Main.GameZoomTarget, SpriteEffects.None, 0f);
				sb.End();
			}

			if (Player == null)
				ResetValues();
		}
		private void UpdateOldQuantum()
		{
			if (!OldSet)
			{
				for (int i = 0; i < 3; i++)
					OldQuantum[i] = ProPlayer.quantum;
				OldSet = true;
			}
			if (ProPlayer.quantum != OldQuantum[0])
			{
				if (ProPlayer.quantum < OldQuantum[0])
					Timer = 75;
				for (int i = 2; i > 0; i--)
				{
					OldQuantum[i] = OldQuantum[i - 1];
					OldQuantum[0] = ProPlayer.quantum;
				}
			}
			if (ProPlayer.quantum >= OldQuantum[0])
				Timer--;
		}
		private void UpdateQuantumUseRect()
		{
			float quotient = ProPlayer.quantum / ProPlayer.quantumMax;
			QuantumBarRect.Width = (int)(192f * quotient);

			QuantumUseRect.Width = QuantumUseRect.Width < QuantumBarRect.Width ? QuantumBarRect.Width : QuantumUseRect.Width;

			if (Timer <= 0 && QuantumUseRect.Width >= QuantumBarRect.Width)
			{
				if ((QuantumUseRect.Width - QuantumBarRect.Width) * 0.05f < 1)
					QuantumUseRect.Width--;
				else
					QuantumUseRect.Width -= (int)((QuantumUseRect.Width - QuantumBarRect.Width) * 0.05f);
			}
		}
		private void ResetValues()
		{
			QuantumBarRect = Rectangle.Empty;
			QuantumUseRect = Rectangle.Empty;
			Position = Vector2.Zero;
			Array.Clear(OldQuantum, 0, 3);
			Timer = 30;
			OldSet = false;
		}
	}
}