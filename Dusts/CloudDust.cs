using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Dusts
{
  public class CloudDust : ModDust
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
			AddLight(dust.position, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3().ColorRGBIntToFloat());
      return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor) => Color.White;
  }
}