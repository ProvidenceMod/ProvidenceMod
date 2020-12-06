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
			dust.alpha = 64;
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
    public override Color? GetAlpha(Dust dust, Color lightColor)
      => new Color(255, 255, 255);
  }
}