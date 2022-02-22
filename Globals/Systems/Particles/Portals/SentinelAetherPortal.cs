using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ParticleLibrary;
using Terraria;
using System;

namespace ProvidenceMod.Particles.Portals
{
	public class SentinelAetherPortal : Particle
	{
		public float radial = 1f;
		public Vector2 vector = new Vector2(24f, 0f);
		public AIState state = AIState.Resonating;
		public enum AIState
		{
			Resonating,
			Stable,
			Fading
		}
		public override void SetDefaults()
		{
			timeLeft = 1920;
			texture = Request<Texture2D>("ProvidenceMod/ExtraTextures/EmptyPixel").Value;
		}
		public override void AI()
		{
			//if (ai[0] == 600)
			//	state = AIState.Stable;
			//if (ai[0] == 1800)
			//	state = AIState.Fading;
			if (state == AIState.Resonating)
			{
				//ai[1] += radial;
				if (radial < 2560f)
					radial *= 1.01f;
				else
					radial += 10;
				float sin = ((float)Math.Sin(radial) * 1.5f);
				float cos = ((float)Math.Cos(radial));
				float angle = new Vector2(cos, sin).ToRotation();
				Vector2 c = new Vector2(24f * cos, 0f);
				Vector2 s = new Vector2(0f, 24f * sin);
				if (radial % 17.5f == 0)
					ParticleManager.NewParticle(position + c + s, new Vector2(Main.rand.NextFloat(-2f, 3f), Main.rand.NextFloat(-2f, 3f)), new AetherFlare(), new Color(1f, 1f, 1f, 0f), (Main.rand.NextFloat(4f, 6f) / 10f) * (cos * 1.5f));
				ParticleManager.NewParticle(position + c + s, Vector2.Zero, new GenericGlowParticle(), new Color(1f, 1f, 1f, 0f), (Main.rand.NextFloat(4f, 6f) / 10f) * (cos * 1.5f));
			}
			else if (state == AIState.Stable)
			{
				for (int i = 0; i < 360; i++)
				{
					float sin = ((float)Math.Sin(i) / 2f) + 1f;
					Vector2 v = new Vector2(24f * sin, 0f).RotatedBy(((float)i).InRadians());
					ParticleManager.NewParticle(position + new Vector2(24f, 32f) + v, Vector2.Zero, new AetherFlare(), Color.White, Main.rand.NextFloat(10f, 16f) / 10f);
				}
			}
			ai[0]++;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 drawPos, Color lightColor)
		{

			return false;
		}
	}
}
