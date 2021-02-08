using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Projectiles.Environment
{
  public class AbyssalPortal : ModProjectile
  {
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
      projectile.Providence().deflectableOverride = true;
      projectile.damage = 9;
      projectile.friendly = false;
      projectile.hostile = false;
    }
    public override void AI()
    {
    }
  }
}