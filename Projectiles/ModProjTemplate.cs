using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Projectiles
{
  public abstract class UnbiddenProjectileNameHere : ModProjectile
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