﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Providence.RenderTargets
{
	public class FlameLayer
	{
		public RenderTarget2D EffectTarget;
		public RenderTarget2D Target;
		public List<IFlameSprite> Sprites;
		public Effect GradientEffect;
		public Texture2D FireGradient;
		public Texture2D FireGradient2;
		public Texture2D FireGradient3;
		public Texture2D FireGradient4;
		public Texture2D FireGradient5;
		public Texture2D FireGradient6;
		public Texture2D FireGradient7;
		public Texture2D FireGradient8;
		public Texture2D FireGradient9;
		public Texture2D FireGradient10;
		public Texture2D FireGradient11;
		public FlameLayer()
		{
			Sprites = new List<IFlameSprite>();
			GradientEffect = ModContent.Request<Effect>("Providence/Assets/Effects/Gradient", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient2 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient3 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient4 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient4", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient5 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient5", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient6 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient6", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient7 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient7", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient8 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient8", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient9 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient9", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient10 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient10", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			FireGradient11 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient11", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		}
		public interface IFlameSprite
		{
			bool Active { get; }
			void Draw(object sender, SpriteBatch spriteBatch);
		}
		// Prepare the layer to be drawn.
		public void PreDrawLayer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			// Set the RenderTarget to the main target.
			graphicsDevice.SetRenderTarget(Target);
			graphicsDevice.Clear(Color.Transparent);

			// Draw our sprites.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			for (int i = 0; i < Sprites.Count; i++)
			{
				IFlameSprite sprite = Sprites[i];
				if (!sprite.Active)
					Sprites.RemoveAt(i);
				sprite.Draw(this, spriteBatch);
			}
			spriteBatch.End();
		}
		// Draw the layer.
		public void DrawLayer(SpriteBatch spriteBatch)
		{
			// Setup shader params.
			GradientEffect.Parameters["sampleTexture2"].SetValue(FireGradient);

			// Draw the main RenderTarget.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			GradientEffect.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(Target, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
			spriteBatch.End();
		}
		public void Push(IFlameSprite item) => Sprites.Insert(0, item);
	}
}
