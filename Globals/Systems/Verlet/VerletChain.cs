using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Providence.Verlet
{
	public class VerletChain : ModSystem
	{
		public VerletNode origin;
		public List<VerletNode> points = new();
		public VerletChain(Vector2 origin, VerletNode originPoint)
		{
			originPoint.position = origin;
			originPoint.chain = this;
			this.origin = originPoint;
			points.Add(originPoint);
		}

		public void Add(VerletNode point, VerletNode lead = null)
		{
			if (lead != null)
				point.lead = lead;
			point.chain = this;
			points.Add(point);
		}
		public void Update()
		{
			for (int i = 0; i < points.Count; i++)
			{
				points[i].Update();
			}
		}
	}
}
