using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Providence.RenderTargets
{
	public class EmberLayer
	{
		public RenderTarget2D EffectTarget;
		public RenderTarget2D Target;
		public RenderTarget2D PostTarget;
		// For sprites that should have the Threshold shader applied
		public List<IEmberSprite> Sprites;
		// For sprites that are already masked
		public List<IEmberSprite> MaskedSprites;
		public Effect Parallax;
		public Effect Threshold;
		public Texture2D EmbersTexture1;
		public Texture2D EmbersTexture2;
		public Texture2D EmbersTexture3;

		public float threshold;
		public EmberLayer()
		{
			Sprites = new List<IEmberSprite>();
			MaskedSprites = new List<IEmberSprite>();
			Parallax = ModContent.Request<Effect>("Providence/Assets/Effects/Parallax", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			Threshold = ModContent.Request<Effect>("Providence/Assets/Effects/Threshold", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			EmbersTexture1 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Embers/Embers1", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			EmbersTexture2 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Embers/Embers2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			EmbersTexture3 = ModContent.Request<Texture2D>("Providence/Assets/Textures/RenderTargets/Embers/Embers3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		}
		public interface IEmberSprite
		{
			bool Active { get; }
			void Draw(object sender, SpriteBatch spriteBatch);
			void PostDraw(object sender, SpriteBatch spriteBatch);
		}
		// Prepare the layer to be drawn.
		public void PreDrawLayer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			// Set the RenderTarget to the main target.
			graphicsDevice.SetRenderTarget(Target);
			graphicsDevice.Clear(Color.Transparent);

			// Draw our sprites.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			for (int i = 0; i < Sprites.Count; i++)
			{
				IEmberSprite sprite = Sprites[i];
				if (!sprite.Active)
					Sprites.RemoveAt(i);
				sprite.Draw(this, spriteBatch);
			}
			spriteBatch.End();

			Threshold.Parameters["mask"].SetValue(new Vector4(0f, 1f, 0f, 1f));
			Threshold.Parameters["threshold"].SetValue(0.25f);

			AddEffect(spriteBatch, graphicsDevice, Threshold);

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			for (int i = 0; i < MaskedSprites.Count; i++)
			{
				IEmberSprite sprite = MaskedSprites[i];
				if (!sprite.Active)
					MaskedSprites.RemoveAt(i);
				sprite.Draw(this, spriteBatch);
			}
			spriteBatch.End();

			// Draw our sprites.

			//Set the RenderTarget to the main target.
			graphicsDevice.SetRenderTarget(PostTarget);
			graphicsDevice.Clear(Color.Transparent);

			// Draw our sprites.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			for (int i = 0; i < MaskedSprites.Count; i++)
			{
				IEmberSprite sprite = MaskedSprites[i];
				if (!sprite.Active)
					MaskedSprites.RemoveAt(i);
				sprite.PostDraw(this, spriteBatch);
			}
			spriteBatch.End();
		}
		// Draw the layer.
		public void DrawLayer(SpriteBatch spriteBatch)
		{
			// Setup shader params.
			Parallax.Parameters["border"].SetValue(Main.hslToRgb(1f / 360f * 14, 0.76f, 0.41f, 0).ToVector4());
			Parallax.Parameters["mask"].SetValue(new Vector4(0f, 1f, 0f, 1f));
			Parallax.Parameters["offset"].SetValue(Main.player[Main.myPlayer].position * 0.21f / new Vector2(EmbersTexture1.Width, EmbersTexture1.Height));
			Parallax.Parameters["spriteRatio"].SetValue(new Vector2(Main.screenWidth / 2 / EmbersTexture1.Width, Main.screenHeight / 2 / EmbersTexture1.Height));
			Parallax.Parameters["conversion"].SetValue(new Vector2(1f / (Main.screenWidth / 2), 1f / (Main.screenHeight / 2)));
			Parallax.Parameters["sampleTexture"].SetValue(EmbersTexture1);
			Parallax.Parameters["sampleTexture2"].SetValue(EmbersTexture2);
			Parallax.Parameters["sampleTexture3"].SetValue(EmbersTexture3);

			// Draw the main RenderTarget.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			Parallax.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(Target, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
			spriteBatch.End();
		}
		public void PostDrawLayer(SpriteBatch spriteBatch)
		{
			// Setup shader params.
			Parallax.Parameters["border"].SetValue(Main.hslToRgb(1f / 360f * 14, 0.76f, 0.41f, 0).ToVector4());
			Parallax.Parameters["mask"].SetValue(new Vector4(0f, 1f, 0f, 1f));
			Parallax.Parameters["offset"].SetValue(Main.player[Main.myPlayer].position * 0.21f / new Vector2(EmbersTexture1.Width, EmbersTexture1.Height));
			Parallax.Parameters["spriteRatio"].SetValue(new Vector2(Main.screenWidth / 2 / EmbersTexture1.Width, Main.screenHeight / 2 / EmbersTexture1.Height));
			Parallax.Parameters["conversion"].SetValue(new Vector2(1f / (Main.screenWidth / 2), 1f / (Main.screenHeight / 2)));
			Parallax.Parameters["sampleTexture"].SetValue(EmbersTexture1);
			Parallax.Parameters["sampleTexture2"].SetValue(EmbersTexture2);
			Parallax.Parameters["sampleTexture3"].SetValue(EmbersTexture3);

			// Draw the main RenderTarget.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			Parallax.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(PostTarget, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
			spriteBatch.End();
		}
		// Adds an effect to the RenderTarget.
		private void AddEffect(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Effect effect)
		{
			// Set our RenderTarget to the temporary EffectTarget.
			// This is so we dont overlap textures in our main RenderTarget.
			graphicsDevice.SetRenderTarget(EffectTarget);
			graphicsDevice.Clear(Color.Transparent);

			// We draw what we have so far, a.k.a our main target, to this RenderTarget, and apply our Effect.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
			effect.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(Target, Vector2.Zero, Color.White);
			spriteBatch.End();

			// Now we switch back to our main RenderTarget...
			graphicsDevice.SetRenderTarget(Target);
			graphicsDevice.Clear(Color.Transparent);

			// And we apply the result.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
			spriteBatch.Draw(EffectTarget, Vector2.Zero, Color.White);
			spriteBatch.End();
		}
		public void Push(IEmberSprite item) => Sprites.Insert(0, item);
	}
}
