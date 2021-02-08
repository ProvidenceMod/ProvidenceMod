using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;

namespace ProvidenceMod.Dusts
{
  public class ParryShieldDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.velocity.Y = Main.rand.Next(-10, 10) * 0.25f;
      dust.velocity.X = Main.rand.Next(-5, 5) * 0.25f;
      dust.scale = 1.25f;
      dust.noGravity = true;
    }

    public override bool MidUpdate(Dust dust)
    {
      if (!dust.noGravity)
      {
      }
      if (dust.noLight)
      {
        return false;
      }
      dust.velocity.Y += 0.05f;
      // float r = Main.DiscoR * (float)0.003921568627450980 / 3;
      // float g = Main.DiscoG * (float)0.003921568627450980 / 3;
      // float b = Main.DiscoB * (float)0.003921568627450980 / 3;
      const float r = 240 * (float)0.003921568627450980 / 3;
      const float g = 45 * (float)0.003921568627450980 / 3;
      const float b = 207 * (float)0.003921568627450980 / 3;
      AddLight(dust.position, r, g, b);
      return false;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
      => Color.White;
  }
}