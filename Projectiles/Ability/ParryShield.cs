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
    public bool alphaLowering = true;
    public bool alphaRising;
    public bool setLower;
    public bool setRise;
    public float decrement = 0.05f;
    public float increment = 0.05f;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Parry Projectile");
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 44;
      projectile.height = 76;
      projectile.friendly = true;
      projectile.ignoreWater = true;
      projectile.timeLeft = 90;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.hide = false;
      projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Acid
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      // Don't want it colliding with anything not on our terms.
      return false;
    }

    public override void AI()
    {
      if (projectile.timeLeft == 30)
      {
        decrement = 0.1f;
        increment = 0.1f;
      }
      if (projectile.Opacity >= 1f && !setRise)
      {
        alphaLowering = true;
        alphaRising = false;
        setRise = true;
        setLower = false;
      }
      if (projectile.Opacity > 0.5f && alphaLowering)
      {
        projectile.Opacity -= decrement;
      }
      if (projectile.Opacity <= 0.5f && !setLower)
      {
        alphaLowering = false;
        alphaRising = true;
        setRise = false;
        setLower = true;
      }
      if (projectile.Opacity < 1f && alphaRising)
      {
        projectile.Opacity += increment;
      }
      Player owner = projectile.ProjectileOwnerPlayer();
      projectile.position = Vector2.Add(new Vector2(owner.position.X - (owner.width / 2), owner.position.Y - (owner.height / 2)), new Vector2(35, 0).RotateTo(owner.AngleTo(Main.MouseWorld)));
      projectile.rotation = owner.AngleTo(Main.MouseWorld);
      int dustSpawn = Main.rand.Next(0, 2);
      Lighting.AddLight(projectile.Center, new Vector3(1.0f, 0.43137254901960784313725490196078f, 0.62745098039215686274509803921569f));
      if (dustSpawn == 1)
      {
        Dust.NewDust(new Vector2(projectile.getRect().X + Main.rand.Next(0, 44), projectile.getRect().Y + Main.rand.Next(0, 76)), 5, 5, DustType<ParryShieldDust>(), 0, 0, 0, Color.White, 1f);
      }
    }
    // public override Color? GetAlpha(Color lightColor) => new Color?(new Color(255, 255, 255, 255));
  }
}