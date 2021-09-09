using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Metaballs;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.Metaballs.MaskManager;

namespace ProvidenceMod.Metaballs
{
	// ===================================================== //
	// Huge thank you to Spirit Mod for this implementation! //
	// ===================================================== //
	public class MetaballParticle : ModDust, IMetaball
	{
		protected Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();
		protected Dictionary<int, float> scales = new Dictionary<int, float>();

		protected void Attach(Mask layer)
		{
			if (!layer.Metaballs.Contains(this))
				layer.Metaballs.Add(this);
		}

		public void Reset()
		{
			positions = new Dictionary<int, Vector2>();
			scales = new Dictionary<int, float>();
		}
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "ProvidenceMod/Metaballs/MetaballParticle";
			return base.Autoload(ref name, ref texture);
		}
		public override Color? GetAlpha(Dust dust, Color lightColor) => Color.Transparent;

		public override bool Update(Dust dust)
		{
			positions[dust.dustIndex] = dust.position;
			scales[dust.dustIndex] = dust.scale;

			if (dust.scale <= 0.25f)
			{
				positions.Remove(dust.dustIndex);
				scales.Remove(dust.dustIndex);
				dust.active = false;
			}
			return false;
		}

		public void DrawOnMetaballLayer(SpriteBatch sB)
		{
			foreach (var k in positions.Keys)
			{
				if (Main.dust[k].active)
					sB.Draw(ProvidenceMod.Metaballs.Mask, (positions[k] - Main.screenPosition) / 2, null, Color.White, 0f, Vector2.One * 256f, scales[k] / 64f, SpriteEffects.None, 0);
			}
		}
	}
}