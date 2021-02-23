using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using System;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Dusts;

namespace ProvidenceMod.Projectiles.Aura
{
  public class BurnProjectile3 : ModProjectile
  {
    public Vector2[] oldPos = new Vector2[] { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), };
    public bool velocitySet;
    public double AngularVelocity;
    public float degrees = 240;
    public int cooldown = 4;
    public override void SetDefaults()
    {
      projectile.hostile = false;
      projectile.friendly = false;
      projectile.Providence().deflectableOverride = true;
      projectile.timeLeft = 2147483647;
      projectile.tileCollide = false;
      projectile.width = 6;
      projectile.height = 6;
      projectile.extraUpdates = 1;
    }

    public override void AI()
    {
      Player player = LocalPlayer();
      ProvidencePlayer proPlayer = player.Providence();
      double rad = degrees.InRadians();
      double dist = proPlayer.clericAuraRadius;
      projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - (projectile.width / 2);
      projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - (projectile.height / 2);
      degrees += 2f;
      projectile.rotation = new Vector2(projectile.position.X - player.position.X, projectile.position.Y - player.position.Y).ToRotation();
      if (cooldown > 0)
      {
        cooldown--;
      }
      if (cooldown == 0)
      {
        Dust.NewDustPerfect(projectile.position, DustType<BurnDust>());
        cooldown = 4;
      }
      if(!proPlayer.cerberus)
      {
        for(int i = 0 ; i < 8 ; i++)
        {
          Dust.NewDustPerfect(projectile.position, DustType<BurnDust>(), new Vector2(0f, 5f).RotatedBy((i * 45f).InRadians()));
        }
        projectile.Kill();
      }
      projectile.ai[1]++;

      // Player player = LocalPlayer();
      // ProvidencePlayer proPlayer = player.Providence();
      // double deg = projectile.ai[1];
      // double rad = deg * (MathHelper.Pi / 180);
      // double dist = proPlayer.clericAuraRadius;
      // projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - (projectile.width / 2);
      // projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - (projectile.height / 2);
      // projectile.rotation += (flot) AngularVelocity;
      // projectile.ai[1]++;
      // AngularVelocity = MathHelper.TwoPi * projectile.velocity.Length();
      // projectile.velocity.X *= proPlayer.clericAuraRadius * (float) AngularVelocity;
      // projectile.velocity.Y *= proPlayer.clericAuraRadius * (float) AngularVelocity;a

      // projectile.ai[1]++;
      // double rad = degrees * (MathHelper.Pi / 180);
      // degrees++;
      // double dist = proPlayer.clericAuraRadius;
      // projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - (projectile.width / 2);
      // projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - (projectile.height / 2);
      // projectile.velocity = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(degrees.InDegrees());
      // projectile.ai[1]++;
      // degrees += 2f;
      // projectile.rotation = degrees;
      // double rad = degrees.InRadians();
      // double dist = proPlayer.clericAuraRadius;
      // projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - (projectile.width / 2);
      // projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - (projectile.height / 2);
    }
    public override Color? GetAlpha(Color lightColor) => Color.White;
  }
}