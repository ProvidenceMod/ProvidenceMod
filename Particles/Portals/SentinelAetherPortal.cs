using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
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
			particle.timeLeft = 1920;
			particle.texture = GetTexture("ProvidenceMod/ExtraTextures/EmptyPixel");
		}
		public override void AI()
		{
			//if (particle.ai[0] == 600)
			//	state = AIState.Stable;
			//if (particle.ai[0] == 1800)
			//	state = AIState.Fading;
			if (state == AIState.Resonating)
			{
				//particle.ai[1] += radial;
				if (radial < 2560f)
					radial *= 1.01f;
				else
					radial += 10;
				float sin = ((float)Math.Sin(radial) * 1.5f);
				float cos = ((float)Math.Cos(radial));
				float angle = new Vector2(cos, sin).ToRotation();
				Vector2 c = new Vector2(24f * cos, 0f);
				Vector2 s = new Vector2(0f, 24f * sin);
				if(radial % 17.5f == 0)
					NewParticle(particle.position + c + s, new Vector2(Main.rand.NextFloat(-2f, 3f), Main.rand.NextFloat(-2f, 3f)), new AetherFlare(), new Color(1f, 1f, 1f, 0f), (Main.rand.NextFloat(4f, 6f) / 10f) * (cos * 1.5f));
				NewParticle(particle.position + c + s, Vector2.Zero, new GenericGlowParticle(), new Color(1f, 1f, 1f, 0f), (Main.rand.NextFloat(4f, 6f) / 10f) * (cos * 1.5f));
			}
			else if (state == AIState.Stable)
			{
				for (int i = 0; i < 360; i++)
				{
					float sin = ((float)Math.Sin(i) / 2f) + 1f;
					Vector2 v = new Vector2(24f * sin, 0f).RotatedBy(((float)i).InRadians());
					NewParticle(particle.position + new Vector2(24f, 32f) + v, Vector2.Zero, new AetherFlare(), Color.White, Main.rand.NextFloat(10f, 16f) / 10f);
				}
			}
			particle.ai[0]++;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			return false;
		}
	}
}
