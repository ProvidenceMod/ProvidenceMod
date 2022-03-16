using Providence.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Items.Accessories
{
	public class HeartOfReality : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heart of Reality");
			Tooltip.SetDefault("+10 life regen\n+500 HP\nEnables the generation of Parity stacks\n+0.5 Parity stack generation\n+100 maximum Parity stacks\nYou're not a mortal...");
		}
		public override void SetDefaults()
		{
			Item.rare = ModContent.RarityType<Developer>();
			Item.accessory = true;
			Item.width = 78;
			Item.height = 74;
			Item.defense = 100;
			Item.lifeRegen = 10;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.Cleric().cleric = true;
			player.Cleric().heartOfReality = true;
			player.Cleric().parityStackGen += 0.5f;
			player.Cleric().parityMaxStacks += 100f;
			player.statLifeMax2 += 500;

			player.GetDamage(DamageClass.Throwing) += 0.05f;
			player.GetCritChance(DamageClass.Throwing) += 5;
			//player.thrownDamageMult;
			//player.thrownVelocity += 0.05f;

			player.Wraith().wraith = true;
			player.Wraith().wraithDodgeCost = 1000f;
			player.Wraith().quantumGen = 10f;
			player.Wraith().quantumDrain = 20f;
			player.Wraith().quantumMax = 2000f;
			player.Wraith().wraithDodge = 0.10f;
		}
	}
}
