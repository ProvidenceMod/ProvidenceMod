using System;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod
{
	public static partial class ProvidenceUtils
	{
		public static Vector3 ColorRGBIntToFloat(this Vector3 vector3)
		{
			const double conversion = 1f / 255f;
			vector3.X = (float)(vector3.X * conversion);
			vector3.Y = (float)(vector3.Y * conversion);
			vector3.Z = (float)(vector3.Z * conversion);
			return vector3;
		}
		public static Color ColorRGBIntToFloat(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R * conversion);
			color.G = (byte)(color.G * conversion);
			color.B = (byte)(color.B * conversion);
			return color;
		}
		public static Vector4 ColorRGBAIntToFloat(this Vector4 vector4)
		{
			const double conversion = 1f / 255f;
			vector4.X = (float)(vector4.X * conversion);
			vector4.Y = (float)(vector4.Y * conversion);
			vector4.Z = (float)(vector4.Z * conversion);
			vector4.W = (float)(vector4.W * conversion);
			return vector4;
		}
		public static Color ColorRGBAIntToFloat(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R * conversion);
			color.G = (byte)(color.G * conversion);
			color.B = (byte)(color.B * conversion);
			color.A = (byte)(color.A * conversion);
			return color;
		}
		public static Vector3 ColorRGBFloatToInt(this Vector3 vector3)
		{
			const double conversion = 1f / 255f;
			vector3.X = (float)(vector3.X / conversion);
			vector3.Y = (float)(vector3.Y / conversion);
			vector3.Z = (float)(vector3.Z / conversion);
			return vector3;
		}
		public static Color ColorRGBFloatToInt(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R / conversion);
			color.G = (byte)(color.G / conversion);
			color.B = (byte)(color.B / conversion);
			return color;
		}
		public static Vector4 ColorRGBAFloatToInt(this Vector4 vector4)
		{
			const double conversion = 1f / 255f;
			vector4.X = (float)(vector4.X / conversion);
			vector4.Y = (float)(vector4.Y / conversion);
			vector4.Z = (float)(vector4.Z / conversion);
			vector4.W = (float)(vector4.W / conversion);
			return vector4;
		}
		public static Color ColorRGBAFloatToInt(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R / conversion);
			color.G = (byte)(color.G / conversion);
			color.B = (byte)(color.B / conversion);
			color.A = (byte)(color.A / conversion);
			return color;
		}
		/// <summary>Gradually shifts between two colors over time.</summary>
		public static Color ColorShift(Color firstColor, Color secondColor, float seconds)
		{
			float amount = (float)((Math.Sin(Math.PI * Math.PI / seconds * Main.GlobalTime) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, amount);
		}
		/// <summary>
		/// <para>Gradually shifts between multiple colors over time.</para>
		/// <para>Remember to provide the middle colors in reverse order for correct shifting.</para>
		/// <param name="colors">The array of colors to shift between</param> 
		/// <param name="seconds">The time to shift colors color</param> 
		/// </summary>
		public static Color ColorShiftMultiple(Color[] colors, float seconds)
		{
			float fade = Main.GameUpdateCount % (int)(seconds * 60) / (seconds * 60f);
			int index = (int)(Main.GameUpdateCount / (seconds * 60f) % colors.Length);
			return Color.Lerp(colors[index], colors[(index + 1) % colors.Length], fade);
		}
	}
}
