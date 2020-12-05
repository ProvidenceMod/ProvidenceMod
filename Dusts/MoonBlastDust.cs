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
            dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
            dust.velocity.X *= 0.3f;
            dust.scale *= 0.7f;
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