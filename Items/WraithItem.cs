using Terraria.ModLoader;
using Terraria;
using ProvidenceMod.Dusts;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items
{
	public abstract class WraithItem : ModItem
	{
		public override void SetDefaults()
		{
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.summon = false;
			item.thrown = true;
		}
		// 99% of the time you should use base.UpdateArmorSet on inherited classes
		public override void UpdateArmorSet(Player player)
		{
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			//add += player.Providence().throwingDamage - 1f;
			//if (!player.Providence().StealthStrikeAvailable() || this.item.prefix <= 0)
			//	return;
			//mult += this.StealthStrikeDamage - 1f;
		}
	}
}