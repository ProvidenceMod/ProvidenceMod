using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Providence.Verlet
{
	public class VerletNode : Entity
	{
		public VerletChain chain;
		public VerletNode lead;
		public Vector2 modifier;
		public Vector2 targetMod;
		public float maxSpeed;
		public float slowMult;
		public float snapStrength;
		public float minDistance;
		public float maxDistance;
		public float rotation;
		public float angle;
		public bool branch;

		public VerletNode(VerletNode lead, Vector2 modifier, float maxSpeed, float slowMult, float snapStrength, float minDistance, float maxDistance, float rotation, float angle)
		{
			this.lead = lead;
			this.maxSpeed = maxSpeed;
			this.slowMult = slowMult;
			this.snapStrength = snapStrength;
			this.minDistance = minDistance;
			this.maxDistance = maxDistance;
			this.modifier = modifier;
			this.rotation = rotation;
			this.angle = angle;
		}
		public void Update()
		{
			if (lead != null)
			{
				if (position.Distance(lead.position) > maxDistance)
				{
					Vector2 dir = DirectionTo(lead.position);
					velocity = ((velocity * 1f) + (dir * maxSpeed)) / (1f + 1f);
					Vector2.Clamp(velocity, new Vector2(-maxSpeed, maxSpeed), new Vector2(-maxSpeed, maxSpeed));
				}
				else if (position.Distance(lead.position) < minDistance)
				{
					Vector2 dir = lead.position.DirectionTo(position);
					velocity = ((velocity * 1f) + (dir * maxSpeed)) / (1f + 1f);
					Vector2.Clamp(velocity, new Vector2(-maxSpeed, maxSpeed), new Vector2(-maxSpeed, maxSpeed));
				}
				else if (branch && angle != 0f && (rotation != lead.rotation + angle))
				{
					rotation = Center.AngleTo(lead.Center);
					//float minRot = lead.rotation + minAngle;
					//float maxRot = lead.rotation + maxAngle;


					Angle reference = new(lead.rotation);
					Angle rot = new(angle + reference.Value);
					Vector2 offset = position - lead.position;
					Vector2 target = lead.position - offset.RotateTo(rot.Value) + targetMod;
					float distance = Distance(target);
					if (distance > 2f)
					{
						Vector2 dir = lead.position.DirectionTo(target);
						velocity = ((velocity * 1f) + (dir * snapStrength)) / (1f + 1f);
					}
					else
						velocity *= slowMult;
				}
				else
				{
					velocity *= slowMult;
				}
				position += velocity;
				rotation = position.AngleTo(lead.position);
			}
		}
		public void NaNCheck()
		{
			if (float.IsNaN(position.X) || float.IsNaN(position.Y))
				position = lead.position - modifier;
		}
		public void DebugDraw(SpriteBatch spriteBatch)
		{
			//Texture2D glow = ModContent.Request<Texture2D>("Embers/Textures/SoftGlow").Value;
			//if (!branch)
			//{
			//	spriteBatch.Draw(glow, position - Main.screenPosition, new Rectangle(0, 0, 64, 64), new Color(0f, 0f, 1f, 0f), 0f, new Vector2(32f, 32f), 0.25f, SpriteEffects.None, 0f);
			//}
			//if (branch)
			//{
			//	spriteBatch.Draw(glow, position - Main.screenPosition, new Rectangle(0, 0, 64, 64), new Color(0f, 0f, 1f, 0f), 0f, new Vector2(32f, 32f), 0.25f, SpriteEffects.None, 0f);
			//	spriteBatch.Draw(glow, position - Main.screenPosition, new Rectangle(0, 0, 64, 64), new Color(0f, 0f, 1f, 0f), rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
			//}
		}
		public void CorrectAngularPosition()
		{

		}
	}
}
