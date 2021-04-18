using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Projectiles.Melee
{
  public class Hellblade : ModProjectile
  {
    public bool animationDone;
    public Vector4 color = new Vector4 (0, 0, 0, 0);
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Hellblade");
    }
    public override void SetDefaults()
    {
      projectile.width = 76;
      projectile.height = 30;
      projectile.ignoreWater = true;
      projectile.timeLeft = 360;
      projectile.penetrate = 1;
      projectile.scale = 1.5f;
      projectile.damage = 100;
      projectile.friendly = true;
      projectile.Providence().element = 0; // Fire
      projectile.tileCollide = false;
      projectile.Opacity = 0f;
    }
    public override void AI()
    {
      projectile.ai[0]++;
      projectile.localAI[0]++;
      projectile.rotation = projectile.velocity.ToRotation();
      projectile.Opacity += 0.05f;
    }
    public override Color? GetAlpha(Color lightColor) => new Color(projectile.Opacity, projectile.Opacity, projectile.Opacity, projectile.Opacity);
  }
}