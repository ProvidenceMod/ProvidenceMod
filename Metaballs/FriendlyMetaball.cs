using Terraria;
namespace ProvidenceMod.Metaballs
{
	public class FriendlyMetaball : MetaballParticle
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			Attach(ProvidenceMod.Metaballs.FriendlyLayer);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.scale *= 0.95f;
			dust.velocity *= 0.9f;
			//dust.rotation += 0.1f;

			return base.Update(dust);
		}
	}
}