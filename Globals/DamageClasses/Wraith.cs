using Terraria.ModLoader;
using Terraria;
using static Providence.ProvidenceUtils;
using System.Linq;
using System.Collections.Generic;

namespace Providence.DamageClasses
{
	public abstract class Wraith : DamageClass
	{
		public override void SetStaticDefaults()
		{
			ClassName.AddTranslation(0, "wraith");
		}
		protected override float GetBenefitFrom(DamageClass damageClass)
		{
			if (damageClass == Generic)
				return 1f;
			return 0f;
		}
	}
}