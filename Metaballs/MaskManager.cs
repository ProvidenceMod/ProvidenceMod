using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ProvidenceMod.Metaballs
{
	// ===================================================== //
	// Huge thank you to Spirit Mod for this implementation! //
	// ===================================================== //
	public class MaskManager
	{
		private readonly Color EnemyBorderColor = new Color(255, 171, 51);
		private readonly Color FriendlyBorderColor = new Color(242, 240, 134);
		public Mask FriendlyLayer { get; set; }
		public Mask EnemyLayer { get; set; }
		public RenderTarget2D TmpTarget { get; protected set; }
		public Texture2D Nebula0 { get; set; }
		public Texture2D Nebula1 { get; set; }
		public Texture2D Nebula2 { get; set; }
		public Texture2D Mask { get; set; }

		public Effect metaballColorCode;
		public Effect metaballEdgeDetection;
		public Effect borderNoise;
		public Effect galaxyParallax;

		public void Initialize(GraphicsDevice graphicsDevice)
		{
			UpdateWindowSize(graphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2);
		}
		public void LoadContent()
		{
			Mask = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Mask");

			Nebula0 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Nebula0");
			Nebula1 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Nebula1");
			Nebula2 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Nebula2");

			metaballColorCode = ProvidenceMod.Instance.GetEffect("Effects/MetaballColorCode");
			metaballEdgeDetection = ProvidenceMod.Instance.GetEffect("Effects/MetaballEdgeDetection");
			borderNoise = ProvidenceMod.Instance.GetEffect("Effects/BorderNoise");
			galaxyParallax = ProvidenceMod.Instance.GetEffect("Effects/GalaxyParallax");

			FriendlyLayer = new Mask(FriendlyBorderColor, Nebula0, Nebula1, Nebula2);
			EnemyLayer = new Mask(EnemyBorderColor, Nebula0, Nebula1, Nebula2);
		}

		public void UpdateWindowSize(GraphicsDevice graphicsDevice, int width, int height)
		{
			FriendlyLayer.UpdateWindowSize(graphicsDevice, width, height);
			EnemyLayer.UpdateWindowSize(graphicsDevice, width, height);
			TmpTarget = new RenderTarget2D(graphicsDevice, width, height);
		}

		public void DrawToTarget(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			var prevTarget = graphicsDevice.GetRenderTargets();

			if (FriendlyLayer.Metaballs.Count > 0 || FriendlyLayer.Sprites.Count > 0)
				FriendlyLayer.DrawMetaballTarget(spriteBatch, graphicsDevice);

			if (EnemyLayer.Metaballs.Count > 0 || EnemyLayer.Sprites.Count > 0)
				EnemyLayer.DrawMetaballTarget(spriteBatch, graphicsDevice);

			graphicsDevice.SetRenderTargets(prevTarget);
		}

		public void DrawEnemyLayer(SpriteBatch spriteBatch)
		{
			if (EnemyLayer.Metaballs.Count > 0 || EnemyLayer.Sprites.Count > 0)
			{
				spriteBatch.End();
				EnemyLayer.DrawLayer(spriteBatch);
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			}
		}
		public void DrawFriendlyLayer(SpriteBatch spriteBatch)
		{
			if (FriendlyLayer.Metaballs.Count > 0 || FriendlyLayer.Sprites.Count > 0)
				FriendlyLayer.DrawLayer(spriteBatch);
		}

		public interface IGalaxySprite
		{
			/// <summary>
			/// Draw parts of sprite that are color coded, and should be drawn with the metaball layer. Galaxy Parallax shader is active.
			/// </summary>
			/// <param name="spriteBatch"></param>
			void DrawGalaxyMappedSprite(SpriteBatch spriteBatch);
		}

		public interface IMetaball
		{
			/// <summary>
			/// Draws metaball masks on the metaball target. The borded noise shader is active.
			/// </summary>
			/// <param name="spriteBatch">SpriteBatch to draw to</param>
			void DrawOnMetaballLayer(SpriteBatch spriteBatch);
		}
	}
}
