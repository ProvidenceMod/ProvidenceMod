using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.RenderTargets;
using Terraria;
using static Providence.RenderTargets.ZephyrLayer;
using static Terraria.ModLoader.ModContent;

namespace Providence.Globals.Systems.Particles
{
	public class AetherFlare : Particle, IZephyrSprite
	{
		public float maxScale;
		public bool Active => active;

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
			{
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
			return false;
		}
		public void Spawn()
		{
			maxScale = scale;
			ai[1] = 6;
			ai[3] = Main.rand.NextFloat(-1f, 2f) / 100f;
			RenderTargetManager.ZephyrLayer.Sprites.Add(this);
		}
		public void Death()
		{
			RenderTargetManager.ZephyrLayer.Sprites.Remove(this);
		}
		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			float num = timeLeft / 120f;
			spriteBatch.Draw(Request<Texture2D>("Providence/Globals/Systems/Particles/AetherFlare").Value, position - Main.screenPosition, new Rectangle(0, 0, 114, 62), new Color(num, num, num, 0f), rotation, texture.Size() * 0.5f, maxScale, SpriteEffects.None, 0f);
		}
	}
}
