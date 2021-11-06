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
		public const double conversion = 1f / 255f;
		public static Vector3 RGBIntToFloat(this Vector3 v) => new Vector3((float)(v.X * conversion), (float)(v.Y * conversion), (float)(v.Z * conversion));
		public static Color RGBIntToFloat(this Color color) => new Color((float) (color.R * conversion), (float)(color.G * conversion), (float)(color.B * conversion));
		public static Vector4 RGBAIntToFloat(this Vector4 v) => new Vector4((float)(v.X * conversion), (float)(v.Y * conversion), (float)(v.Z * conversion), (float)(v.W * conversion));
		public static Color RGBAIntToFloat(this Color color) => new Color((float)(color.R * conversion), (float)(color.G * conversion), (float)(color.B * conversion), (float)(color.A * conversion));
		public static Vector3 RGBFloatToInt(this Vector3 v) => new Vector3((float)(v.X / conversion), (float) (v.Y / conversion), (float) (v.Z / conversion));
		public static Color RGBFloatToInt(this Color color) => new Color((float)(color.R / conversion), (float)(color.G / conversion), (float)(color.B / conversion));
		public static Vector4 RGBAFloatToInt(this Vector4 v) => new Vector4((float)(v.X / conversion), (float)(v.Y / conversion), (float)(v.Z / conversion), (float)(v.W / conversion));
		public static Color RGBAFloatToInt(this Color color) => new Color((float)(color.R / conversion), (float)(color.G / conversion), (float)(color.B / conversion), (float)(color.A / conversion));
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
