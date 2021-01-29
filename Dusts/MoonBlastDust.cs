using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;

namespace UnbiddenMod.Dusts
{
  public class MoonBlastDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.scale = 1f;
      dust.noGravity = true;
    }

    public override bool Update(Dust dust)
    {
      if (dust.scale <= 0)
        dust.active = false;
      float rLight = Main.DiscoR * (float)0.003921568627450980 / 3;
      float gLight = Main.DiscoG * (float)0.003921568627450980 / 3;
      float bLight = Main.DiscoB * (float)0.003921568627450980 / 3;
      AddLight(dust.position, rLight, gLight, bLight);
      return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
      => new Color (Main.DiscoR, Main.DiscoG, Main.DiscoB, 128);
  }
}