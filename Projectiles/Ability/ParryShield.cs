using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Dusts;

namespace UnbiddenMod.Projectiles.Ability
{
  public class ParryShield : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Parry Projectile");
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 44;
      projectile.height = 76  ;
      projectile.friendly = true;
      projectile.ignoreWater = true;
      projectile.timeLeft = 90;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Acid
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      // Don't want it colliding with anything not on our terms.
      return false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return Color.White;
    }
    public override void AI()
    {
      Player owner = Main.LocalPlayer;
      projectile.position = Vector2.Add(new Vector2(owner.position.X - (owner.width / 2), owner.position.Y - (owner.height / 2)), new Vector2(35, 0).RotateTo(owner.AngleTo(Main.MouseWorld)));
      projectile.rotation = owner.AngleTo(Main.MouseWorld);
    }
  }
}