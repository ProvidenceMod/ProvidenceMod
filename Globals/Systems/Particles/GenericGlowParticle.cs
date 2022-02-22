using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Particles
{
	public class GenericGlowParticle : Particle
	{
		public override void SetDefaults()
		{
			width = 128;
			height = 128;
			timeLeft = 120;
			tileCollide = false;
		}
		public override void AI()
		{
			scale = (120 - ai[0]) / 120;
			ai[0]++;
			velocity *= 0.96f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 drawPos, Color lightColor)
		{
			spriteBatch.Draw(ModContent.Request<Texture2D>("ProvidenceMod/Particles/GenericGlowParticle").Value, position - Main.screenPosition, new Rectangle(0, 0, 128, 128), color, rotation, new Vector2(64, 64), 0.125f * scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
