using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.Lighting;

namespace Providence.Content.Dusts
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
			AddLight(dust.position, ProvidenceColor.ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3().RGBIntToFloat());
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
