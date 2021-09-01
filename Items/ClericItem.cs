using Terraria.ModLoader;
using Terraria;
using ProvidenceMod.Dusts;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items
{
	public abstract class ClericItem : ModItem
	{
		public override void SetDefaults()
		{
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.summon = false;
			item.thrown = false;
			item.Providence().cleric = true;
		}
		// 99% of the time you should use base.UpdateArmorSet on inherited classes
		public override void UpdateArmorSet(Player player)
		{
			player.Providence().hasClericSet = true;
		}
		public virtual void ModifyWeaponDamage(Player player, ref int damage)
		{
			int originalDmg = damage;
			//damage = (int)(damage * modPlayer.clericMultiplier);
			float globalDmg = player.meleeDamage - 1;
			if (player.magicDamage - 1 < globalDmg)
				globalDmg = player.magicDamage - 1;
			if (player.rangedDamage - 1 < globalDmg)
				globalDmg = player.rangedDamage - 1;
			if (player.minionDamage - 1 < globalDmg)
				globalDmg = player.minionDamage - 1;
			if (player.thrownDamage - 1 < globalDmg)
				globalDmg = player.thrownDamage - 1;
			if (globalDmg > 1)
				damage += (int)(originalDmg * globalDmg);
		}
	}
}