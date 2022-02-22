using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ProvidenceMod.Particles
{
	public class AetherFlare : Particle
	{
		public Color[] colors = new Color[] { HexToColor("36103500"), HexToColor("47285500"), HexToColor("64539c00"), HexToColor("757ec700"), HexToColor("9ebae200"), HexToColor("dafaf400") };
		public float scale;
		public override void SetDefaults()
		{
			particle.width = 128;
			particle.height = 128;
			particle.timeLeft = 120;
			particle.tileCollide = false;
			particle.texture = GetTexture("ProvidenceMod/ExtraTextures/Flare");
		}
		public override void AI()
		{
			if(particle.ai[0] == 0)
			{
				scale = particle.scale;
				particle.ai[1] = 6;
				particle.ai[3] = Main.rand.NextFloat(-1f, 2f) / 100f;
			}
			particle.scale = MathHelper.Lerp(particle.scale, 0, particle.ai[0] / 120);
			particle.ai[0]++;
			if(particle.timeLeft % 20 == 0)
			{
				particle.ai[1]--;
				particle.ai[2] = 0;
			}
			particle.velocity *= 0.96f;
			particle.rotation += particle.ai[3];
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			int index = (int)particle.ai[1];
			Color color = Color.Lerp(colors[index < 0 ? 5 : index], colors[index - 1 < 0 ? 5 : index - 1], particle.ai[2] / 20);
			particle.ai[2]++;
			spriteBatch.Draw(GetTexture("ProvidenceMod/ExtraTextures/Flare"), particle.position - Main.screenPosition, new Rectangle(0, 0, 128, 128), color, particle.rotation, new Vector2(64, 64), 0.5f * particle.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
