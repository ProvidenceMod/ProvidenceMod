using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Providence.RenderTargets
{
	public class ZephyrLayer
	{
		public RenderTarget2D EffectTarget;
		public RenderTarget2D Target;
		public List<IZephyrSprite> Sprites;
		public Effect GradientEffect;
		public Texture2D ZephyrGradient;
		public ZephyrLayer()
		{
			Sprites = new List<IZephyrSprite>();
			GradientEffect = ModContent.Request<Effect>("Providence/Assets/Effects/Gradient", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			ZephyrGradient = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Flame/FireGradient9", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		}
		public interface IZephyrSprite
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
				IZephyrSprite sprite = Sprites[i];
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
			GradientEffect.Parameters["sampleTexture2"].SetValue(ZephyrGradient);

			// Draw the main RenderTarget.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			GradientEffect.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(Target, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
			spriteBatch.End();
		}
		public void Push(IZephyrSprite item) => Sprites.Insert(0, item);
	}
}
