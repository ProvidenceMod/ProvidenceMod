//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace ProvidenceMod.Items.Accessories
//{
//	public class HeartOfReality : ClericItem
//	{
//		public override void SetStaticDefaults()
//		{
//			DisplayName.SetDefault("Heart of Reality");
//			Tooltip.SetDefault("+10 life regen\n+500 HP\nEnables the generation of Parity stacks\n+0.5 Parity stack generation\n+100 maximum Parity stacks\nYou're not a mortal...");
//		}
//		public override void SetDefaults()
//		{
//			Item.rare = ItemRarityID.Purple;
//			Item.Providence().customRarity = ProvidenceRarity.Developer;
//			Item.accessory = true;
//			Item.width = 78;
//			Item.height = 74;
//			Item.defense = 100;
//			Item.lifeRegen = 10;
//		}
//		public override void UpdateAccessory(Player player, bool hideVisual)
//		{
//			player.Providence().cleric = true;
//			player.Providence().heartOfReality = true;
//			player.Providence().parityStackGen += 0.5f;
//			player.Providence().parityMaxStacks += 100f;
//			player.statLifeMax2 += 500;

//			player.thrownDamage += 0.05f;
//			player.thrownCrit += 5;
//			//player.thrownDamageMult;
//			player.thrownVelocity += 0.05f;
//			player.Providence().wraith = true;
//			player.Providence().wraithDodgeCost = 1000f;
//			player.Providence().quantumGen = 10f;
//			player.Providence().quantumDrain = 20f;
//			player.Providence().quantumMax = 2000f;
//			player.Providence().wraithDodge = 0.10f;
//		}
//	}
//}
