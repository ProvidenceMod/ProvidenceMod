using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvidenceMod
{
	public static partial class ProvidenceUtils
	{
		/// <summary>Full - ( ( Cap * DR ) / ( DR * Harshness ) )</summary>
		/// <param name="x">Input DR.</param>
		public static float DiminishingDRFormula(float x) => 1f - (float)((0.75d * x) / (x * 45d));
	}
}
