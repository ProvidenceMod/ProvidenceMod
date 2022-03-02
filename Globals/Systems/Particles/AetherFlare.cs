using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ParticleLibrary;

namespace Providence.Particles
{
	public class AetherFlare : Particle
	{
		public Color[] colors = new Color[] { HexToColor("36103500"), HexToColor("47285500"), HexToColor("64539c00"), HexToColor("757ec700"), HexToColor("9ebae200"), HexToColor("dafaf400") };
		public float maxScale;
		public override string Texture => "Providence/Assets/Textures/Flare";
		public override void SetDefaults()
		{
			width = 128;
			height = 128;
			timeLeft = 120;
			tileCollide = false;
		}
		public override void AI()
		{
			if (ai[0] == 0)
			{
				maxScale = scale;
				ai[1] = 6;
				ai[3] = Main.rand.NextFloat(-1f, 2f) / 100f;
			}
			maxScale = MathHelper.Lerp(maxScale, 0, ai[0] / 120);
			ai[0]++;
			if (timeLeft % 20 == 0)
			{
				ai[1]--;
				ai[2] = 0;
			}
			velocity *= 0.96f;
			rotation += ai[3];
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 drawPos, Color lightColor)
		{
			int index = (int)ai[1];
			Color color = Color.Lerp(colors[index < 0 ? 5 : index], colors[index - 1 < 0 ? 5 : index - 1], ai[2] / 20);
			ai[2]++;
			spriteBatch.Draw(Request<Texture2D>("Providence/Assets/Textures/Flare").Value, position - Main.screenPosition, new Rectangle(0, 0, 128, 128), color, rotation, new Vector2(64, 64), 0.5f * maxScale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
