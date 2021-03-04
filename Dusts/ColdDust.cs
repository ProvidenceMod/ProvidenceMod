using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Dusts
{
  public class ColdDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.scale = 1.5f;
      dust.noGravity = true;
      dust.velocity.X = Main.rand.NextFloat(-1f, 2f);
      dust.velocity.Y = Main.rand.NextFloat(-1f, 2f);
    }

    public override bool Update(Dust dust)
    {
      if (dust.scale <= 0)
        dust.active = false;
      AddLight(dust.position, new Vector3(152 / 2, 255 / 2, 241 / 2).ColorRGBIntToFloat());
      return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor) => ColorShift(new Color(0, 255, 255), new Color(0, 192, 255), 3f);
  }
}