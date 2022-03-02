using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Providence.RenderTargets
{
	public class ShieldLayer
	{
		public RenderTarget2D EffectTarget;
		public RenderTarget2D Target;
		public List<IShieldSprite> Sprites;
		public Effect ShieldEffect;
		public Texture2D HexagonTexture;
		public Texture2D NegativeHexagons;
		public ShieldLayer()
		{
			Sprites = new List<IShieldSprite>();
			ShieldEffect = ModContent.Request<Effect>("Providence/Assets/Effects/Shield", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			HexagonTexture = ModContent.Request<Texture2D>("Providence/Assets/Textures/Hexagons", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			NegativeHexagons = ModContent.Request<Texture2D>("Providence/Assets/Textures/NegativeHexagons", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		}
		public interface IShieldSprite
		{
			bool Active { get; set; }
			void Draw(object sender, SpriteBatch spriteBatch);
		}
		// Prepare the layer to be drawn.
		public void PreDrawLayer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			// Set the RenderTarget to the main target.
			graphicsDevice.SetRenderTarget(Target);
			graphicsDevice.Clear(Color.Transparent);


			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
			for (int i = 0; i < Sprites.Count; i++)
			{
				IShieldSprite sprite = Sprites[i];
				if (!sprite.Active)
					Sprites.RemoveAt(i);
				sprite.Draw(this, spriteBatch);
			}
			Effect outline = ModContent.Request<Effect>("Providence/Assets/Effects/Outline", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			outline.Parameters["border"].SetValue(new Vector4(1f, 1f, 1f, 0f));
			outline.Parameters["conversion"].SetValue(new Vector2(1f / (Main.screenWidth / 2), 1f / (Main.screenHeight / 2)));
			spriteBatch.End();

			AddEffect(spriteBatch, graphicsDevice, outline);
		}
		// Draw the layer.
		public void DrawLayer(SpriteBatch spriteBatch)
		{
			Effect outline = ModContent.Request<Effect>("Providence/Assets/Effects/Outline", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			outline.Parameters["border"].SetValue(new Vector4(0.75f, 0.75f, 0.75f, 0f));
			outline.Parameters["conversion"].SetValue(new Vector2(1f / (Main.screenWidth / 2), 1f / (Main.screenHeight / 2)));
			// Draw the main RenderTarget.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			outline.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(Target, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
			spriteBatch.End();
		}
		public void Push(IShieldSprite item) => Sprites.Insert(0, item);
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
	}
}



