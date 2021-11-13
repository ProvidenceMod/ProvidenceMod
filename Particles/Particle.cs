using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ProvidenceMod.Particles
{
	public class Particle
	{
		public Particle particle;
		/// <summary>
		/// Initializes a new particle instance.
		/// </summary>
		public Particle()
		{
			particle = this;
			SetDefaults();
			SetPrivateDefaults();
		}
		/// <summary>
		/// The position of the particle.
		/// </summary>
		public Vector2 Position = new Vector2(0f, 0f);
		/// <summary>
		/// The velocity of the particle.
		/// </summary>
		public Vector2 Velocity = new Vector2(0f, 0f);
		/// <summary>
		/// The old positions of the particle.
		/// </summary>
		public Vector2[] OldPosition;
		/// <summary>
		/// The old centers of the particle.
		/// </summary>
		public Vector2[] OldCenter;
		/// <summary>
		/// The old rotations of the particle.
		/// </summary>
		public Vector2[] OldRotation;
		/// <summary>
		/// The old velocities of the particle.
		/// </summary>
		public Vector2[] OldVelocity;
		/// <summary>
		/// Method to run on the spawning of the particle.
		/// </summary>
		public Action SpawnAction;
		/// <summary>
		/// Method to run on the death of the particle.
		/// </summary>
		public Action DeathAction;
		/// <summary>
		/// Color to apply or colors to shift between.
		/// </summary>
		public Color Color;
		/// <summary>
		/// The hitbox for this particle.
		/// </summary>
		public Rectangle Frame;
		/// <summary>
		/// Shaders to apply to this particle.
		/// </summary>
		public Effect[] Shader;
		/// <summary>
		/// The texture of this particle.
		/// </summary>
		public Texture2D Texture;
		/// <summary>
		/// The ID of this particle instance.
		/// </summary>
		public int WhoAmI;
		/// <summary>
		/// Whether this particle is active or not.
		/// </summary>
		public bool Active;
		/// <summary>
		/// The direction of this particle on the previous tick
		/// </summary>
		public int OldDirection;
		/// <summary>
		/// The current direction of this particle.
		/// </summary>
		public int Direction;
		/// <summary>
		/// The width of this particle.
		/// </summary>
		public int Width;
		/// <summary>
		/// The height of this particle.
		/// </summary>
		public int Height;
		/// <summary>
		/// The size of this particle.
		/// </summary>
		public float Scale;
		/// <summary>
		/// The rotation of this particle.
		/// </summary>
		public float Rotation;
		/// <summary>
		/// The type of this particle.
		/// </summary>
		public int Type;
		/// <summary>
		/// The alpha of this particle.
		/// </summary>
		public float Alpha = 1f;
		/// <summary>
		/// The strength of the gravity applied to this projectile.
		/// </summary>
		public float Gravity;
		/// <summary>
		/// The duration of the lifetime of this particle, in frames.
		/// </summary>
		public int TimeLeft;
		/// <summary>
		/// Length of the old position cache;
		/// </summary>
		public int OldPosLength;
		/// <summary>
		/// Length of the old rotation cache;
		/// </summary>
		public int OldRotLength;
		/// <summary>
		/// Length of the old center cache;
		/// </summary>
		public int OldCenLength;
		/// <summary>
		/// Length of the old velocity cache;
		/// </summary>
		public int OldVelLength;
		/// <summary>
		/// AI array, useful for passing data into the particle on spawn.
		/// </summary>
		public float[] ai;

		public virtual void SetDefaults()
		{
		}
		private void SetPrivateDefaults()
		{
			if (OldPosLength > 0)
				OldPosition = new Vector2[OldPosLength];
			if (OldRotLength > 0)
				OldRotation = new Vector2[OldRotLength];
			if (OldCenLength > 0)
				OldCenter = new Vector2[OldCenLength];
			if (OldVelLength > 0)
				OldVelocity = new Vector2[OldVelLength];
		}
		/// <summary>
		/// Code to run pre-update.
		/// </summary>
		public virtual void PreAI()
		{
		}
		/// <summary>
		/// Code to run mid-update.
		/// </summary>
		public virtual void AI()
		{
		}
		/// <summary>
		/// Code to run post-update.
		/// </summary>
		public virtual void PostAI()
		{
		}
		/// <summary>
		/// Drawing code to run before the particle manager draw call. Return false to keep the particle manager from drawing your particle.
		/// </summary>
		/// <returns>bool</returns>
		public virtual bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return true;
		}
		/// <summary>
		/// This drawing method runs if PreDraw returns true.
		/// </summary>
		public void Draw(SpriteBatch spriteBatch, Color lightColor)
		{
			Color color = Color == default ? new Color(lightColor.R, lightColor.G, lightColor.B, Alpha) : new Color(Color.R, Color.G, Color.B, Alpha);
			spriteBatch.Draw(Texture, Position - Main.screenPosition, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, Rotation, Texture.Size() * 0.5f, Scale * 10f, SpriteEffects.None, 0f);
		}
		/// <summary>
		/// Drawing code to run after the particle manager draw call.
		/// </summary>
		public virtual void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
		}
	}
}
