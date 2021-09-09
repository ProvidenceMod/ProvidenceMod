﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Metaballs;
using System;
using System.Linq;
using Terraria;
using static ProvidenceMod.Metaballs.MaskManager;

namespace ProvidenceMod.Metaballs
{
	// ===================================================== //
	// Huge thank you to Spirit Mod for this implementation! //
	// ===================================================== //
	public class Metaball : IMetaball
	{
		public Metaball(Vector2 position, float scale)
		{
			Position = position;
			Scale = scale;
			rotationConst = (float)Main.rand.NextDouble() * 6.28f;
		}

		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public float Scale { get; set; }
		private float rotationConst;


		public void Update()
		{
			Velocity = new Vector2((float)Math.Cos(Main.time * 0.05) * (float)Math.Sin(rotationConst) * 0.2f + rotationConst * 0.1f, (float)Math.Sin(Main.time * 0.05) * (float)Math.Cos(rotationConst) * 0.2f - rotationConst * 0.1f).RotatedBy(rotationConst);
			Scale *= 0.995f;
			Scale -= 0.015f;
			if (Scale < 0.25f)
			{
				if (ProvidenceMod.Metaballs.EnemyLayer.Metaballs.Contains(this))
					ProvidenceMod.Metaballs.EnemyLayer.Metaballs.Remove(this);
				if (ProvidenceMod.Metaballs.FriendlyLayer.Metaballs.Contains(this))
					ProvidenceMod.Metaballs.FriendlyLayer.Metaballs.Remove(this);
			}
			Position += Velocity;
		}

		public void DrawOnMetaballLayer(SpriteBatch sB)
		{
			ProvidenceMod.Metaballs.borderNoise.Parameters["offset"].SetValue((float)Main.time / 1000f + rotationConst);

			sB.Draw(ProvidenceMod.Metaballs.Mask, (Position - Main.screenPosition) / 2, null, Color.White, 0f, Vector2.One * 256f, Scale / 32f, SpriteEffects.None, 0);
		}
	}
}
