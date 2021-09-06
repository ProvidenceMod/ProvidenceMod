using Microsoft.Xna.Framework;
using Terraria;

namespace ProvidenceMod.Metaballs
{
	public class EnemyMetaball : MetaballParticle
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			Attach(ProvidenceMod.Metaballs.EnemyLayer);
		}
		public override bool Update(Dust dust)
		{
			dust.color = Lighting.GetColor((int)(dust.position.X / 16), (int)(dust.position.Y / 16)).MultiplyRGB(new Color(93, 201, 60)) * 0.11f;
			dust.position += dust.velocity * 0.5f;
			dust.scale *= 0.97f;
			dust.velocity *= 0.98f;
			dust.rotation += 0.1f;

			return base.Update(dust);
		}
	}
}