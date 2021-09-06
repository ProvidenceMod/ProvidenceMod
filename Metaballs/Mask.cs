using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using static ProvidenceMod.Metaballs.MaskManager;

namespace ProvidenceMod.Metaballs
{
	public class Mask
	{
		public Color BorderColor { get; set; }
		public List<IMetaball> Metaballs { get; protected set; }
		public List<IGalaxySprite> Sprites { get; protected set; }
		public RenderTarget2D Target { get; protected set; }
		public Texture2D Galaxy0 { get; set; }
		public Texture2D Galaxy1 { get; set; }
		public Texture2D Galaxy2 { get; set; }


		public Mask(Color borderColor, Texture2D galaxy0, Texture2D galaxy1, Texture2D galaxy2)
		{
			Metaballs = new List<IMetaball>();
			Sprites = new List<IGalaxySprite>();

			BorderColor = borderColor;

			Galaxy0 = galaxy0;
			Galaxy1 = galaxy1;
			Galaxy2 = galaxy2;
		}

		public void UpdateWindowSize(GraphicsDevice graphicsDevice, int width, int height)
		{
			Target = new RenderTarget2D(graphicsDevice, width, height);
		}

		public void DrawMetaballTarget(SpriteBatch sB, GraphicsDevice graphicsDevice)
		{
			graphicsDevice.SetRenderTarget(Target);
			graphicsDevice.Clear(Color.Transparent);

			ProvidenceMod.Metaballs.borderNoise.Parameters["offset"].SetValue((float)Main.time / 10f);

			sB.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

			ProvidenceMod.Metaballs.borderNoise.CurrentTechnique.Passes[0].Apply();
			foreach (var m in Metaballs)
			{
				m.DrawOnMetaballLayer(sB);
			}

			sB.End();

			AddEffect(sB, graphicsDevice, Target, ProvidenceMod.Metaballs.metaballColorCode);

			sB.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);

			foreach (var s in Sprites)
			{
				s.DrawGalaxyMappedSprite(sB);
			}

			sB.End();

			ProvidenceMod.Metaballs.metaballEdgeDetection.Parameters["width"].SetValue((float)Main.screenWidth / 2);
			ProvidenceMod.Metaballs.metaballEdgeDetection.Parameters["height"].SetValue((float)Main.screenHeight / 2);
			ProvidenceMod.Metaballs.metaballEdgeDetection.Parameters["border"].SetValue(BorderColor.ToVector4());

			AddEffect(sB, graphicsDevice, Target, ProvidenceMod.Metaballs.metaballEdgeDetection);
		}

		public void DrawLayer(SpriteBatch sB)
		{
			Effect galaxyParallax = ProvidenceMod.Metaballs.galaxyParallax;

			galaxyParallax.Parameters["screenWidth"].SetValue((float)Main.screenWidth / 2);
			galaxyParallax.Parameters["screenHeight"].SetValue((float)Main.screenHeight / 2);
			galaxyParallax.Parameters["width"].SetValue((float)Galaxy0.Width);
			galaxyParallax.Parameters["height"].SetValue((float)Galaxy0.Height);
			galaxyParallax.Parameters["GalaxyTexture0"].SetValue(Galaxy0);
			galaxyParallax.Parameters["GalaxyTexture1"].SetValue(Galaxy1);
			galaxyParallax.Parameters["GalaxyTexture2"].SetValue(Galaxy2);
			galaxyParallax.Parameters["offset"].SetValue(Main.player[Main.myPlayer].position * 0.21f);
			galaxyParallax.Parameters["time"].SetValue((float)Main.time);

			sB.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			galaxyParallax.CurrentTechnique.Passes[0].Apply();

			sB.Draw(Target, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);

			sB.End();
		}

		private void AddEffect(SpriteBatch sB, GraphicsDevice graphicsDevice, RenderTarget2D target, Effect effect)
		{
			graphicsDevice.SetRenderTarget(ProvidenceMod.Metaballs.TmpTarget);
			graphicsDevice.Clear(Color.Transparent);

			sB.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

			effect.CurrentTechnique.Passes[0].Apply();

			sB.Draw(target, position: Vector2.Zero, color: Color.White);

			sB.End();

			graphicsDevice.SetRenderTarget(target);
			graphicsDevice.Clear(Color.Transparent);

			sB.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

			sB.Draw(ProvidenceMod.Metaballs.TmpTarget, position: Vector2.Zero, color: Color.White);

			sB.End();
		}
	}
}
