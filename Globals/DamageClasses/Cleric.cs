using Terraria.ModLoader;

namespace Providence.DamageClasses
{
	public abstract class Cleric : DamageClass
	{
		public override void SetStaticDefaults()
		{
			ClassName.AddTranslation(0, "cleric");
		}
		//protected override float GetBenefitFrom(DamageClass damageClass)
		//{
		//	if (damageClass == Generic)
		//		return 1f;
		//	return 0f;
		//}
	}
}