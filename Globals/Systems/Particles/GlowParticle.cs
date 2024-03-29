﻿using ParticleLibrary;

namespace Providence.Globals.Systems.Particles
{
	public class GlowParticle : Particle
	{
		public override string Texture => "Providence/Globals/Systems/Particles/GlowParticle";

		public override void SetDefaults()
		{
			width = 128;
			height = 128;
			timeLeft = 120;
			tileCollide = false;
			SpawnAction = Spawn;
		}
		public override void AI()
		{
			scale = (120 - ai[0]) / 120;
			ai[0]++;
			velocity *= 0.96f;
		}
		public void Spawn()
		{
			scale *= 0.125f;
		}
	}
}
