using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.Lighting;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Dusts
{
	public class SparkleDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale = 1f;
			dust.noLight = false;
			dust.noGravity = true;
			dust.velocity.X = Main.rand.NextFloat(-0.25f, 0.5f);
			dust.velocity.Y = Main.rand.NextFloat(-0.25f, 0.5f);
		}

		public override bool Update(Dust dust)
		{
			if (dust.scale <= 0)
				dust.active = false;
			AddLight(dust.position, ColorShift(new Color(144, 65, 74), new Color(232, 162, 114), 3f).ToVector3().ColorRGBIntToFloat());
			return true;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor) => Color.White;
	}
}