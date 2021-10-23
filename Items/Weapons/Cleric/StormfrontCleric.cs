using Terraria;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles.Cleric;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ProvidenceMod.Items.Weapons.Cleric
{
	public class StormfrontCleric : ClericDualblade
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormfront");
		}
		public override void SetDefaults()
		{
			base.SetDefaults();
			projs = new Projectile[2];
			projFadeoutTime = 60;
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
