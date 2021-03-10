using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Projectiles.Magic
{
  public class ZephyrSpirit : ModProjectile
  {
    public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
    public Color lighting = new Color(0, 255, 255);
    public Color lighting2 = new Color(0, 192, 255);
    public Color lighting3;
    public int cooldown = 5;
    public int frame;
    public int frameTick;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Zephyr Spirit");
      Main.projFrames[projectile.type] = 5;
    }

    public override void SetDefaults()
    {
      projectile.width = 64;
      projectile.height = 34;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      projectile.timeLeft = 300;
      projectile.penetrate = 1;
      projectile.scale = 1f;
      projectile.damage = 25;
      projectile.friendly = true;
      projectile.Opacity = 0f;
      projectile.Providence().element = -1; // Typeless
      projectile.Providence().homingID = HomingID.Natural;
    }

    public override void AI()
    {
      projectile.ai[1]++;
      projectile.localAI[0]++;
      NPC npc = ClosestEnemyNPC(projectile);
      projectile.Homing(npc, 16f, default, default, 25f, 300f);

      if (projectile.Opacity < 1f)
      {
        projectile.Opacity += 0.05f;
        color.X += 0.05f;
        color.Y += 0.05f;
        color.Z += 0.05f;
        color.W += 0.05f;
      }
      projectile.rotation = projectile.velocity.ToRotation();
      if (++projectile.frameCounter >= 5) // Frame time
      {
        projectile.frameCounter = 0;
        if (++projectile.frame >= 5) //Frame number
        {
          projectile.frame = 0;
        }
      }
      Dust.NewDust(projectile.TrueCenter(), 6, 6, ModContent.DustType<ColdDust>());
      Dust.NewDust(projectile.TrueCenter(), 6, 6, ModContent.DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
      if (cooldown > 0)
        cooldown--;
      if (cooldown == 0)
      {
        cooldown = 5;
      }
      lighting3 = ColorShift(lighting, lighting2, 3f);
      Lighting.AddLight(projectile.Center, lighting3.ToVector3());
    }
    // public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    // {
    //   Texture2D texture = ModContent.GetTexture("ProvidenceMod/Projectiles/Magic/ZephyrSpirit");
    //   spriteBatch.Draw(texture, projectile.position - Main.screenPosition, projectile.AnimationFrame(ref frame, ref frameTick, 6, 5, true), Color.White, projectile.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
    //   return false;
    // }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      base.OnHitPlayer(target, damage, crit);
      projectile.Kill();
    }
    public override Color? GetAlpha(Color lightColor)
    {
      return new Color(color.X, color.Y, color.Z, color.W);
    }
  }
}