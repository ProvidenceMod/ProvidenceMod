using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Providence.Projectiles;

namespace Providence.Content.Projectiles
{
	public abstract class ProvidenceProjectileNameHere : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
		}

		public override void SetDefaults()
		{
		}

		public override void AI()
		{
		}
	}
}
