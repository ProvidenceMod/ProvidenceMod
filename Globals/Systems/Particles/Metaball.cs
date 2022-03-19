
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.RenderTargets;
using Terraria.ModLoader;
using static Providence.RenderTargets.EmberLayer;
using static Providence.RenderTargets.FlameLayer;

namespace Providence.Globals.Systems.Particles
{
	public class Metaball : Particle, IEmberSprite, IFlameSprite
	{
		public bool Active => active;
		float maxScale;
		public override string Texture => "Providence/Globals/Systems/Particles/Metaball";
		public override void SetDefaults()
		{
			width = 128;
			height = 128;
			timeLeft = 120;
			tileCollide = false;
			SpawnAction = Spawn;
			DeathAction = Death;
		}
		public override void AI()
		{
			if (ai[0] == 0)
				maxScale = scale;
			ai[0]++;
			scale = MathHelper.Lerp(0f, maxScale, timeLeft / 120f);
			velocity *= 0.96f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			return false;
		}
		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			if (sender == RenderTargetManager.FlameLayer)
				spriteBatch.Draw(ModContent.Request<Texture2D>("Providence/Globals/Systems/Particles/GlowParticle").Value, VisualPosition, new Rectangle(0, 0, 128, 128), Color.Multiply(new Color(1f, 1f, 1f, 0f), 0.5f), rotation, new Vector2(64, 64), scale, SpriteEffects.None, 0f);
			if (sender == RenderTargetManager.EmberLayer)
				spriteBatch.Draw(ModContent.Request<Texture2D>("Providence/Assets/Textures/Masks/CircleMask").Value, VisualPosition, new Rectangle(0, 0, 512, 512), Color.White, rotation, new Vector2(256, 256), scale * 0.1f, SpriteEffects.None, 0f);
		}
		public void PostDraw(object sender, SpriteBatch spriteBatch)
		{

		}
		public void Spawn()
		{
			RenderTargetManager.FlameLayer.Sprites.Add(this);
			RenderTargetManager.EmberLayer.Sprites.Add(this);
		}
		public void Death()
		{
			RenderTargetManager.FlameLayer.Sprites.Remove(this);
			RenderTargetManager.EmberLayer.Sprites.Remove(this);
		}
	}
}
