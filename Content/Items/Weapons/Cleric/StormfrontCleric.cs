//using Terraria;
//using Terraria.ModLoader;
//using ProvidenceMod.Projectiles.Cleric;
//using Microsoft.Xna.Framework;
//using static Terraria.ModLoader.ModContent;
//using Terraria.ID;

//namespace ProvidenceMod.Items.Weapons.Cleric
//{
//	public class StormfrontCleric : BaseDualblade
//	{
//		public override float Speed => 24f;
//		public override int Fadeout => 60;
//		public override void SetStaticDefaults()
//		{
//			DisplayName.SetDefault("Stormfront");
//		}
//		public override void SetExtraDefaults()
//		{
//			Item.useTime = 15;
//			Item.useAnimation = 15;
//			Item.damage = 50;
//			Item.width = 190;
//			Item.height = 124;
//			Item.material = true;
//			Item.rare = (int)ProvidenceRarity.Orange;
//			Item.shoot = ProjectileType<StormfrontTempest>();
//		}
//	}
//}
