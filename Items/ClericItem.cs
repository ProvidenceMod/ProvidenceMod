using Terraria.ModLoader;
using Terraria;
using ProvidenceMod.Dusts;
using static ProvidenceMod.ProvidenceUtils;
using System.Collections.Generic;
using System.Linq;

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
		}
		public override ModItem Clone(Item itemClone) => (ClericItem)base.Clone(itemClone);
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine damagetip = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
			if (damagetip != null)
			{
				string[] array = damagetip.text.Split(' ');
				damagetip.text = array[0] + " parity " + array.Last();
			}
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