using Terraria;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Cleric;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Weapons.Cleric
{
	public class StormfrontCleric : BaseDualblade
	{
		public override float Speed => 24f;
		public override int Fadeout => 60;
		public override bool Cleric => true;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormfront");
		}
		public override void SetExtraDefaults()
		{
			projectiles = new Projectile[2];
			item.useTime = 15;
			item.useAnimation = 15;
			item.damage = 50;
			item.Providence().cleric = true;
			item.width = 190;
			item.height = 124;
			item.material = true;
			item.rare = (int)ProvidenceRarity.Orange;
			item.shoot = ProjectileType<StormfrontTempest>();
		}
	}
}
