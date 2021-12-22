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
		private List<Particle> particles;

		public void Load()
		{
			particles = new List<Particle>();
		}
		public void Unload()
		{
			particles.Clear();
			particles = null;
		}
		public void Dispose()
		{
			particles.Clear();
		}
		public void PreUpdate(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < particles?.Count; i++)
			{
				particles[i].PreAI();
			}
		}
		public void Update(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < particles?.Count; i++)
			{
				if(particles[i].tileCollide)
				{
					if (!Collision.SolidCollision(particles[i].position + (new Vector2(particles[i].width / 2f, particles[i].height / 2f) * particles[i].scale), 1, 1))
					{
						// Apply gravity.
						particles[i].velocity.Y += particles[i].gravity;
						// Apply velocity to position.
						particles[i].position += particles[i].velocity;
					}
				}
				else
				{
					// Apply gravity.
					particles[i].velocity.Y += particles[i].gravity;
					// Apply velocity to position.
					particles[i].position += particles[i].velocity;
				}

				// Run AI.
				particles[i].AI();
				// Draw particle.
				bool draw = particles[i].PreDraw(spriteBatch, Lighting.GetColor((int)(particles[i].position.X / 16), (int)(particles[i].position.Y / 16)));
				if (draw)
				{
					particles[i].Draw(spriteBatch, Lighting.GetColor((int)(particles[i].position.X / 16), (int)(particles[i].position.Y / 16)));
				}
				// Time left check.
				if (particles[i].timeLeft-- == 0 || !particles[i].active)
				{
					// Do death action.
					particles[i].DeathAction?.Invoke();
					// Deactivate particle.
					particles.RemoveAt(i);
					particles.TrimExcess();
				}
			}
		}
		public void PostUpdate(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < particles?.Count; i++)
			{
				particles[i].PostAI();
				particles[i].PostDraw(spriteBatch, Lighting.GetColor((int)(particles[i].position.X / 16), (int)(particles[i].position.Y / 16)));
			}
		}
		public void NewParticle(Vector2 Position, Vector2 Velocity, Particle Type, Color Color, float scale, float AI0 = 0, float AI1 = 0, float AI2 = 0, float AI3 = 0, float AI4 = 0, float AI5 = 0, float AI6 = 0, float AI7 = 0)
		{
			if (Type.texture == null)
				throw new NullReferenceException();
			Type.position = Position;
			Type.velocity = Velocity;
			Type.color = Color;
			Type.scale = scale;
			Type.active = true;
			Type.ai = new float[] { AI0, AI1, AI2, AI3, AI4, AI5, AI6, AI7 };
			if (particles?.Count < 6000)
			{
				Type.SpawnAction?.Invoke();
				particles?.Add(Type);
			}
			if (particles?.Count > 6000)
				particles.TrimExcess();
		}
	}
}
