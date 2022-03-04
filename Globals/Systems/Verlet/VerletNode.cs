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
					if (distance > 32f)
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
	}
}
