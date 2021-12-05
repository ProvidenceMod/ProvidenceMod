﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items;
namespace ProvidenceMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZephyrVeil : WraithItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Veil");
			Tooltip.SetDefault("+5% wraith damage\n+5% Crit Chance\n+5% Thrown Velocity\n-50% Max Health\n+10% Dodge Chance\nAllows generation of Quantum Stacks:\n+1 Quantum Flux generation\n+20 Quantum Drain\n+2000 Quantum Max");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.defense = 3;
			item.Providence().wraith = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.05f;
			player.thrownCrit += 5;
			//player.thrownDamageMult;
			player.thrownVelocity += 0.05f;
			player.Providence().wraith = true;
			player.Providence().wraithDodgeCost = 1000f;
			player.Providence().quantumGen = 10f;
			player.Providence().quantumDrain = 20f;
			player.Providence().quantumMax = 2000f;
			player.Providence().wraithDodge = 0.10f;
			player.statLifeMax2 = (int)(player.statLifeMax2 * 0.5f);
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) =>
			body.type == ItemType<ZephyrBreastplate>() &&
			legs.type == ItemType<ZephyrLeggings>() &&
			head.type == ItemType<ZephyrVeil>();
		public override void UpdateArmorSet(Player player)
		{
			player.runAcceleration *= 1.1f;
			player.maxRunSpeed *= 1.1f;
			player.setBonus = "+10% Movement Speed";
		}
		public override void AddRecipes()
		{
		}
	}
}