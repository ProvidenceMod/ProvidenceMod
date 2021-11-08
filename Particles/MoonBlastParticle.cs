using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Particles
{
	public class MoonBlastParticle : Particle
	{
		public override void SetDefaults()
		{
			particle.Width = 10;
			particle.Height = 10;
			particle.Texture = GetTexture("ProvidenceMod/Particles/MoonBlastParticle");
			particle.Scale = 3f;
			particle.TimeLeft = 360;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Color color = Color == default ? new Color(lightColor.R, lightColor.G, lightColor.B, Alpha) : new Color(Color.R, Color.G, Color.B, Alpha);
			//spriteBatch.Draw(Texture, Position - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, Rotation, Texture.Size() * 0.5f, Scale * 10f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/Particles/MoonBlastParticle"), particle.Position - Main.screenPosition, new Rectangle(0, 0, particle.Width, particle.Height), lightColor, particle.Rotation, new Vector2(particle.Width / 2, particle.Height / 2), particle.Scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
