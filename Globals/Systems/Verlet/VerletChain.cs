using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;

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
		public Vector2 Evaluate(float progress)
		{
			float length = Length();
			float target = length * progress;
			float old = 0f;
			float current = 0f;
			if (points.Count == 1)
				return points.First().position;
			int index1 = 0;
			int index2 = 0;
			for (int i = 0; i < points.Count; i++)
			{
				if (i == points.Count - 1)
					break;
				old = current;
				current += Math.Abs(points[i].Distance(points[i + 1].position));
				if (current > target)
				{
					index1 = i;
					index2 = i + 1;
					break;
				}
			}
			float distance = target - old;
			return points[index1].position + (points[index1].position.DirectionTo(points[index2].position) * distance);
		}
		public Vector2 BezierEvaulate(float progress)
		{
			List<Vector2> p = new();
			for (int i = 0; i < points.Count; i++)
				p.Add(points[i].position);
			return Bezier(progress, p);
		}
		public float AngleEvaluate(float progress)
		{
			if(progress >= 1f)
				return points[^1].rotation;

			float divisor = 1f / points.Count;
			float remainder = progress % divisor;
			float quotient = remainder / divisor;
			int index = (int) Math.Floor(progress / divisor);

			if(index == points.Count - 1)
				return points[index].angle;

			return MathHelper.Lerp(points[index].rotation, points[index + 1].rotation, quotient);
		}
		public float Length()
		{
			float length = 0f;
			if (points.Count == 1)
				return length;
			for (int i = 0; i < points.Count; i++)
			{
				if (i == points.Count - 1)
					break;
				length += Math.Abs(points[i].Distance(points[i + 1].position));
			}
			return length;
		}
	}
}
