using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;

namespace UnbiddenMod.Dusts
{
  public class AuraDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.velocity.Y = 0f;
      dust.velocity.X = 0f;
      dust.scale = 1f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.alpha = 96;
    }

    public override bool Update(Dust dust)
    {
			dust.alpha -= 16;
			if (dust.alpha <= 32)
			{
				dust.active = false;
			}
			return false;
    }

    public override bool MidUpdate(Dust dust)
    {
      if (!dust.noGravity)
      {
        dust.velocity.Y += 0.05f;
      }

      if (dust.noLight)
      {
        return false;
      }

      float strength = dust.scale * 1.4f;
      if (strength > 1f)
      {
        strength = 1f;
      }
      float r = Main.DiscoR * (float)0.003921568627450980 / 3;
      float g = Main.DiscoG * (float)0.003921568627450980 / 3;
      float b = Main.DiscoB * (float)0.003921568627450980 / 3;
      Lighting.AddLight(dust.position, r, g, b);
      return false;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
      => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 25);
  }
}