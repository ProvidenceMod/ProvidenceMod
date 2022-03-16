using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Providence.RenderTargets
{
	public class RenderTargetManager : ModSystem
	{
		public static RenderTargetManager Instance;
		public static Vector2 lastViewSize;
		public static BasicLayer BasicLayer;
		public static EmberLayer EmberLayer;
		public static FlameLayer FlameLayer;
		public static ZephyrLayer ZephyrLayer;
		public override void OnModLoad()
		{
			Instance = this;
			ProvidenceMod.Targets = this;
			BasicLayer = new BasicLayer();
			EmberLayer = new EmberLayer();
			FlameLayer = new FlameLayer();
			ZephyrLayer = new ZephyrLayer();
			On.Terraria.Main.DrawNPCs += (orig, self, behindTiles) =>
			{
				DrawLayers(Main.spriteBatch);
				orig(self, behindTiles);
			};
			Main.OnResolutionChanged += CheckScreenSize;
			Main.OnPreDraw += (gameTime) => { PreDrawLayers(Main.spriteBatch, Main.graphics.GraphicsDevice); };
			UpdateRenderTargets();
		}
		public override void Unload()
		{
			Instance = null;
			ProvidenceMod.Targets = null;
			BasicLayer = null;
			EmberLayer = null;
			FlameLayer = null;
			ZephyrLayer = null;
			On.Terraria.Main.DrawNPCs -= (orig, self, behindTiles) =>
			{
				DrawLayers(Main.spriteBatch);
				orig(self, behindTiles);
			};
			Main.OnResolutionChanged -= CheckScreenSize;
			Main.OnPreDraw -= (gameTime) => { PreDrawLayers(Main.spriteBatch, Main.graphics.GraphicsDevice); };
		}
		public override void OnWorldLoad()
		{
			Main.QueueMainThreadAction(() =>
			{
				ResetLayerTargets();
			});
			DisposeLayers();
		}
		public override void OnWorldUnload()
		{
			Main.QueueMainThreadAction(() =>
			{
				ResetLayerTargets();
			});
			DisposeLayers();
		}
		public void UpdateRenderTargets()
		{
			Main.QueueMainThreadAction(() =>
			{
				ResetLayerTargets();
			});
		}
		public void DisposeLayers()
		{
			BasicLayer.Sprites.Clear();
			EmberLayer.Sprites.Clear();
			FlameLayer.Sprites.Clear();
			ZephyrLayer.Sprites.Clear();
		}
		public void ResetLayerTargets()
		{
			BasicLayer.Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			BasicLayer.EffectTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			FlameLayer.Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			FlameLayer.EffectTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			EmberLayer.Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			EmberLayer.EffectTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			ZephyrLayer.Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
			ZephyrLayer.EffectTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
		}
		public void CheckScreenSize(Vector2 obj)
		{
			if (!Main.dedServ && lastViewSize != Main.ViewSize)
				UpdateRenderTargets();
		}
		public void PreDrawLayers(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			RenderTargetBinding[] previousTargets = graphicsDevice.GetRenderTargets();

			if (BasicLayer?.Sprites.Count > 0)
				BasicLayer.PreDrawLayer(spriteBatch, graphicsDevice);
			if (FlameLayer?.Sprites.Count > 0)
				FlameLayer?.PreDrawLayer(spriteBatch, graphicsDevice);
			if (EmberLayer?.Sprites.Count > 0)
				EmberLayer.PreDrawLayer(spriteBatch, graphicsDevice);
			if (ZephyrLayer?.Sprites.Count > 0)
				ZephyrLayer.PreDrawLayer(spriteBatch, graphicsDevice);

			graphicsDevice.SetRenderTargets(previousTargets);
		}
		private void DrawLayers(SpriteBatch spriteBatch)
		{
			spriteBatch.End();

			if (BasicLayer?.Sprites.Count > 0)
				BasicLayer.DrawLayer(spriteBatch);
			if (FlameLayer?.Sprites.Count > 0)
				FlameLayer.DrawLayer(spriteBatch);
			if (EmberLayer?.Sprites.Count > 0)
				EmberLayer.DrawLayer(spriteBatch);
			if (ZephyrLayer?.Sprites.Count > 0)
				ZephyrLayer.DrawLayer(spriteBatch);

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}
	}
}
