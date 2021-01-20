using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Projectiles.Melee
{
  public class ConfluenceBeam : ModProjectile
  {
    private int el = -1;
    private bool elDetermined;

    private Color? DetermineColor(int e)
    {
      switch (e)
      {
        case 0: // Fire
          return new Color(255, 0, 0);
        case 1: // Ice
          return new Color(3, 248, 252);
        case 2: // Lightning
          return new Color(252, 240, 3);
        case 3: // Water
          return new Color(3, 20, 252);
        case 4: // Earth
          return new Color(101, 67, 33);
        case 5: // Air
          return new Color(162, 44, 209);
        case 6: // Radiant
          return new Color(232, 232, 132);
        case 7: // Necrotic
          return new Color(43, 30, 61);
        default: // Typeless
          return null;
      }
    }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Confluence Beam");
    }

    public override void SetDefaults()
    {
      projectile.CloneDefaults(ProjectileID.TerraBeam);
      projectile.damage = 42;
      projectile.penetrate = 1;
      projectile.tileCollide = true;
    }

    public override void AI()
    {
      if (!elDetermined)
      {
        el = Main.rand.Next(-1, 8);
        projectile.Unbidden().element = el;

        elDetermined = true;
      }
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return DetermineColor(el);
    }
  }
}