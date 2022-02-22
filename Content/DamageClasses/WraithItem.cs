//using Terraria.ModLoader;
//using Terraria;
//using ProvidenceMod.Dusts;
//using static ProvidenceMod.ProvidenceUtils;
//using System.Linq;
//using System.Collections.Generic;

//namespace ProvidenceMod.Items
//{
//	public abstract class WraithItem : ModItem
//	{
//		public override void SetDefaults()
//		{
//			Item.melee = false;
//			Item.ranged = false;
//			Item.magic = false;
//			Item.summon = false;
//			Item.thrown = true;
//		}
//		// 99% of the time you should use base.UpdateArmorSet on inherited classes
//		public override ModItem Clone(Item itemClone) => (WraithItem)base.Clone(itemClone);
//		public override void ModifyTooltips(List<TooltipLine> tooltips)
//		{
//			TooltipLine damagetip = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
//			if (damagetip != null)
//			{
//				string[] array = damagetip.text.Split(' ');
//				damagetip.text = array[0] + " wraith " + array.Last();
//			}
//		}
//		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
//		{
//			mult = player.thrownDamage;
//			float globalDmg = player.thrownDamage - 1;
//			if (player.GetDamage(DamageClass.Melee) - 1 < globalDmg)
//				globalDmg = player.GetDamage(DamageClass.Melee) - 1;
//			if (player.magicDamage - 1 < globalDmg)
//				globalDmg = player.magicDamage - 1;
//			if (player.rangedDamage - 1 < globalDmg)
//				globalDmg = player.rangedDamage - 1;
//			if (player.minionDamage - 1 < globalDmg)
//				globalDmg = player.minionDamage - 1;
//			if (player.Providence().clericDamage - 1 < globalDmg)
//				globalDmg = player.Providence().clericDamage - 1;
//			if (globalDmg > 1)
//				mult += globalDmg;
//		}
//	}
//}