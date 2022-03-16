using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Providence
{
	public static class ProvidenceMath
	{
		/// <summary>Full - ( ( Cap * DR ) / ( DR * Harshness ) )</summary>
		/// <param name="x">Input DR.</param>
		public static float DiminishingDRFormula(float x) => 1f - (float)((0.75d * x) / (x * 45d));
		public static float DiminishingReturnFormula(float input, double cap, double harshness) => 1f - (float)((cap * input) / (input * harshness));
		public static Vector2 BezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			float cx = 3 * (p1.X - p0.X);
			float cy = 3 * (p1.Y - p0.Y);

			float bx = (3 * (p2.X - p1.X)) - cx;
			float by = (3 * (p2.Y - p1.Y)) - cy;

			float ax = p3.X - p0.X - cx - bx;
			float ay = p3.Y - p0.Y - cy - by;

			float Cube = t * t * t;
			float Square = t * t;

			float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.X;
			float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.Y;

			return new Vector2(resX, resY);
		}
		/// <summary>Shorthand for converting degrees of rotation into a radians equivalent.</summary>
		public static float InRadians(this float degrees) => MathHelper.ToRadians(degrees);
		/// <summary>Shorthand for converting radians of rotation into a degrees equivalent.</summary>
		public static float InDegrees(this float radians) => MathHelper.ToDegrees(radians);
		/// <summary>Automatically converts seconds into game ticks. 1 second is 60 ticks.</summary>
		public static int InTicks(this float seconds) => (int)(seconds * 60);
		public static decimal Round(this decimal dec, int points) => decimal.Round(dec, points);
		public static float Round(this float f, int points) => (float)Math.Round(f, points);
		public static double Round(this double d, int points) => Math.Round(d, points);
	}
}
