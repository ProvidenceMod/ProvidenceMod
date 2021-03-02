using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Dusts
{
  public class AirDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.scale = 1f;
      dust.noGravity = true;
      dust.velocity.X = 0f;
      dust.velocity.Y = 0f;
    }

    public override bool Update(Dust dust)
    {
      if (dust.scale <= 0)
        dust.active = false;
      AddLight(dust.position, new Vector3(152, 255, 241).ColorRGBIntToFloat());
      return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor) => ColorShift(new Color(0, 255, 255), new Color(0, 128, 255), 3f);
  }
}