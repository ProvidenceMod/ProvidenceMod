using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ProvidenceMod.Metaballs
{
	public class MaskManager
	{
		private readonly Color EnemyBorderColor = new Color(255, 171, 51);
		private readonly Color FriendlyBorderColor = new Color(242, 240, 134);
		public Mask FriendlyLayer { get; set; }
		public Mask EnemyLayer { get; set; }
		public Mask NebulaLayer { get; set; }
		public RenderTarget2D TmpTarget { get; protected set; }
		public Texture2D Galaxy0 { get; set; }
		public Texture2D Galaxy1 { get; set; }
		public Texture2D Galaxy2 { get; set; }
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

			Galaxy0 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Galaxy0");
			Galaxy1 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Galaxy1");
			Galaxy2 = ProvidenceMod.Instance.GetTexture("ExtraTextures/Masks/Galaxy2");

			metaballColorCode = ProvidenceMod.Instance.GetEffect("Effects/MetaballColorCode");
			metaballEdgeDetection = ProvidenceMod.Instance.GetEffect("Effects/MetaballEdgeDetection");
			borderNoise = ProvidenceMod.Instance.GetEffect("Effects/BorderNoise");
			galaxyParallax = ProvidenceMod.Instance.GetEffect("Effects/GalaxyParallax");

			FriendlyLayer = new Mask(FriendlyBorderColor, Galaxy0, Galaxy1, Galaxy2);
			EnemyLayer = new Mask(EnemyBorderColor, Galaxy0, Galaxy1, Galaxy2);
		}

		public void UpdateWindowSize(GraphicsDevice graphicsDevice, int width, int height)
		{
			FriendlyLayer.UpdateWindowSize(graphicsDevice, width, height);
			EnemyLayer.UpdateWindowSize(graphicsDevice, width, height);
			TmpTarget = new RenderTarget2D(graphicsDevice, width, height);
		}

		public void DrawToTarget(SpriteBatch sB, GraphicsDevice graphicsDevice)
		{
			var prevTarget = graphicsDevice.GetRenderTargets();

			if (FriendlyLayer.Metaballs.Count > 0 || FriendlyLayer.Sprites.Count > 0)
				FriendlyLayer.DrawMetaballTarget(sB, graphicsDevice);

			if (EnemyLayer.Metaballs.Count > 0 || EnemyLayer.Sprites.Count > 0)
				EnemyLayer.DrawMetaballTarget(sB, graphicsDevice);

			graphicsDevice.SetRenderTargets(prevTarget);
		}

		public void DrawEnemyLayer(SpriteBatch sB)
		{
			if (EnemyLayer.Metaballs.Count > 0 || EnemyLayer.Sprites.Count > 0)
			{
				sB.End();
				EnemyLayer.DrawLayer(sB);
				sB.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			}
		}
		public void DrawFriendlyLayer(SpriteBatch sB)
		{
			if (FriendlyLayer.Metaballs.Count > 0 || FriendlyLayer.Sprites.Count > 0)
				FriendlyLayer.DrawLayer(sB);
		}

		public interface IGalaxySprite
		{
			/// <summary>
			/// Draw parts of sprite that are color coded, and should be drawn with the metaball layer. Galaxy Parallax shader is active.
			/// </summary>
			/// <param name="sB"></param>
			void DrawGalaxyMappedSprite(SpriteBatch sB);
		}

		public interface IMetaball
		{
			/// <summary>
			/// Draws metaball masks on the metaball target. The borded noise shader is active.
			/// </summary>
			/// <param name="sB">SpriteBatch to draw to</param>
			void DrawOnMetaballLayer(SpriteBatch sB);
		}
	}
}
