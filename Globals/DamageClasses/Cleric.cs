using Terraria.ModLoader;
using Terraria;
using static Providence.ProvidenceUtils;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;

namespace Providence.DamageClasses
{
	public abstract class Cleric : DamageClass
	{
		public override void SetStaticDefaults()
		{
			ClassName.AddTranslation(0, "cleric");
		}
		protected override float GetBenefitFrom(DamageClass damageClass)
		{
			if (damageClass == Generic)
				return 1f;
			return 0f;
		}
	}
}