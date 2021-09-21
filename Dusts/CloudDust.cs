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
    }

    public override bool Update(Dust dust)
    {
      if (dust.scale <= 0)
        dust.active = false;
			AddLight(dust.position, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3().ColorRGBIntToFloat());
			if (!Collision.EmptyTile((int)(dust.position.X / 16), (int)(dust.position.Y / 16)))
			{
				Main.dust[dust.type].velocity.X = 0;
				Main.dust[dust.type].velocity.Y = 0;
			}
			return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(1f, 1f, 1f, 0f);
  }
}