//using Terraria.ModLoader;
//using Terraria;
//using ProvidenceMod.Dusts;
//using static ProvidenceMod.ProvidenceUtils;
//using System.Collections.Generic;
//using System.Linq;

//namespace ProvidenceMod.Items
//{
//	public abstract class ClericItem : ModItem
//	{
//		public override void SetDefaults()
//		{
//			Item.melee = false;
//			Item.ranged = false;
//			Item.magic = false;
//			Item.summon = false;
//			Item.thrown = false;
//		}
//		public override ModItem Clone(Item itemClone) => (ClericItem)base.Clone(itemClone);
//		public override void ModifyTooltips(List<TooltipLine> tooltips)
//		{
//			TooltipLine damagetip = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
//			if (damagetip != null)
//			{
//				string[] array = damagetip.text.Split(' ');
//				damagetip.text = array[0] + " parity " + array.Last();
//			}
//		}
//		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
//		{
//			mult = player.Providence().clericDamage;
//			float globalDmg = player.Providence().clericDamage - 1;
//			if (player.GetDamage(DamageClass.Melee) - 1 < globalDmg)
//				globalDmg = player.GetDamage(DamageClass.Melee) - 1;
//			if (player.GetDamage(DamageClass.Magic) - 1 < globalDmg)
//				globalDmg = player.magicDamage - 1;
//			if (player.GetDamage(DamageClass.Ranged) - 1 < globalDmg)
//				globalDmg = player.rangedDamage - 1;
//			if (player.GetDamage(DamageClass.Summon) - 1 < globalDmg)
//				globalDmg = player.minionDamage - 1;
//			if (player.GetDamage(DamageClass.Throwing) - 1 < globalDmg)
//				globalDmg = player. - 1;
//			if (globalDmg > 1)
//				mult += globalDmg;
//		}
//	}
//}