using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Particles
{
	public class ParticleManager
	{
		public List<Particle> particles;
		public SpriteBatch spriteBatch;

		public void Load()
		{
			particles = new List<Particle>();
			spriteBatch = new SpriteBatch(Main.graphics.GraphicsDevice);
		}
		public void Unload()
		{
			particles.Clear();
			particles = null;
			spriteBatch.Dispose();
			spriteBatch = null;
		}
		public void Dispose()
		{
			particles.Clear();
			spriteBatch.Dispose();
		}
		public void PreUpdate()
		{
			for (int i = 0; i < particles.Count; i++)
			{
				particles[i].PreAI();
			}
		}
		public void Update()
		{
			for (int i = 0; i < particles.Count; i++)
			{
				// Apply gravity.
				particles[i].Velocity.Y += particles[i].Gravity;
				// Apply velocity to position.
				particles[i].Position += particles[i].Velocity;
				// Run AI.
				particles[i].AI();
				// Draw particle.
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				bool draw = particles[i].PreDraw(Main.spriteBatch, Lighting.GetColor((int)(particles[i].Position.X / 16), (int)(particles[i].Position.Y / 16)));
				spriteBatch.End();
				if (draw)
				{
					spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
					particles[i].Draw(Main.spriteBatch, Lighting.GetColor((int)(particles[i].Position.X / 16), (int)(particles[i].Position.Y / 16)));
					spriteBatch.End();
				}
				// Time left check.
				particles[i].TimeLeft--;
				if (particles[i].TimeLeft == 0 || !particles[i].Active)
				{
					// Do death action.
					particles[i].DeathAction?.Invoke();
					// Deactivate particle.
					particles.RemoveAt(i);
					particles.TrimExcess();
				}
			}
		}
		public void PostUpdate()
		{
			for (int i = 0; i < particles.Count; i++)
			{
				particles[i].PostAI();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				particles[i].PostDraw(Main.spriteBatch, Lighting.GetColor((int)(particles[i].Position.X / 16), (int)(particles[i].Position.Y / 16)));
				spriteBatch.End();
			}
		}
		public void NewParticle(Vector2 Position, Vector2 Velocity, Particle Type, Color Color, float AI0 = 0, float AI1 = 0, float AI2 = 0, float AI3 = 0, float AI4 = 0, float AI5 = 0, float AI6 = 0, float AI7 = 0)
		{
			if (Type.Texture == null)
				throw new NullReferenceException();
			Type.Position = Position;
			Type.Velocity = Velocity;
			Type.Color = Color;
			Type.Active = true;
			Type.ai = new float[] { AI0, AI1, AI2, AI3, AI4, AI5, AI6, AI7 };
			particles.Add(Type);
			if (particles.Count > 500)
			{
				particles.RemoveAt(0);
				particles.TrimExcess();
			}
		}
	}
}
