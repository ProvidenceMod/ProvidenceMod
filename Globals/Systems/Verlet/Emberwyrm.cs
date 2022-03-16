using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.Globals.Systems.Particles;
using Providence.RenderTargets;
using Providence.Verlet;
using System;
using Terraria;
using Terraria.ModLoader;
using static Providence.RenderTargets.FlameLayer;

namespace Providence.Globals.Systems.Verlet
{
	public class Emberwyrm : ModNPC, IFlameSprite
	{
		public int timer = 240;
		public Vector2[] vectors = new Vector2[4];
		public Vector2 lastVector;
		public VerletChain chain;
		public bool Active => NPC.active;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Igniscyl");
		}
		public override void SetDefaults()
		{
			NPC.lifeMax = 10;
			NPC.aiStyle = -1;
			NPC.width = 10;
			NPC.height = 10;
			NPC.friendly = false;
			NPC.noGravity = true;
		}
		public override void AI()
		{
			if (NPC.ai[0] == 0)
				Spawn();
			NPC.ai[0]++;
			if (NPC.ai[0] % Main.rand.Next(10, 20) == 0)
			{
				int index = Main.rand.Next(0, chain.points.Count);
				ParticleManager.NewParticle(chain.points[index].position, Vector2.Zero, new EmberParticle(), Color.White, 1f);
			}
			//NPC.position = EmbersMath.BezierPoint((240f - timer) / 240f, vectors[0], vectors[1], vectors[2], vectors[3]);
			Vector2 dir = NPC.Center.DirectionTo(vectors[1]);
			float sin = ((float)Math.Sin(Main.GlobalTimeWrappedHourly) + 1.5f) * 0.5f;
			Console.WriteLine(sin);
			NPC.velocity = ((NPC.velocity * 1f) + (dir * 1f * sin)) / (1f + 1f);
			chain.points[0].position = NPC.Center;
			timer--;
			if (timer <= 0)
				Cycle();

			for (int i = 0; i < chain.points.Count; i++)
			{
				VerletNode point = chain.points[i];
				if (point.branch)
				{
					float sin2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly + (0.1f * ((i + 3) / 2))) * 20f;
					Vector2 v = new(sin2, 0f);
					point.targetMod = v;
				}
			}
		}
		public void Cycle()
		{
			vectors[0] = Vector2.Zero;
			vectors[1] = vectors[0] + RandomVector().RotatedBy(vectors[0].ToRotation()).RotatedBy((Main.rand.NextFloat(-360, 361) * 0.2f).InRadians());
			vectors[2] = vectors[1] + RandomVector().RotatedBy(vectors[1].ToRotation()).RotatedBy((Main.rand.NextFloat(-360, 361) * 0.2f).InRadians());
			vectors[3] = vectors[2] + RandomVector().RotatedBy(vectors[2].ToRotation()).RotatedBy((Main.rand.NextFloat(-360, 361) * 0.2f).InRadians());
			vectors[0] += NPC.position;
			vectors[1] += NPC.position;
			vectors[2] += NPC.position;
			vectors[3] += NPC.position;
			timer = 240;
		}
		public void Spawn()
		{
			Cycle();
			RenderTargetManager.FlameLayer.Sprites.Add(this);
			VerletNode origin = new(null, new Vector2(16f, 0f), 1f, 0.5f, 3f, 2f, 10f, 0f, 0f);
			chain = new(NPC.Center, origin);
			for (int i = 0; i < 10; i++)
			{
				VerletNode point = new(null, new Vector2(16f, 0f), 1f, 0.5f, 3f, 2f, 20f, 0f, 0f);
				point.position = origin.position + new Vector2(10f * (i + 1), 0f);
				chain.Add(point, chain.points[i]);
			}
			for (int i = 0; i < 10; i++)
			{
				VerletNode point = chain.points[i];
				if (i != 0)
				{
					float scale = 2f - (i * (1f / 9f));
					VerletNode branchL = new(point, new Vector2(-10f, 7f), 1f, 0.5f, 3f, 10f * scale, 20f * scale, 0f, 45f.InRadians());
					VerletNode branchR = new(point, new Vector2(-10f, -7f), 1f, 0.5f, 3f, 10f * scale, 20f * scale, 0f, -45f.InRadians());
					branchL.branch = true;
					branchR.branch = true;
					branchL.position = point.position + new Vector2(0f, 30f);
					branchR.position = point.position + new Vector2(0f, -30f);
					chain.Add(branchL);
					if (i == 1 || i == 9)
					{
						for (int j = 0; j < 10; j++)
						{
							VerletNode branchLPoint = new(chain.points[^1], new Vector2(16f, 0f), 1f, 0.5f, 3f, 2f * scale, 4f * scale, 0f, 0f);
							branchLPoint.position = point.position + new Vector2(0f, 30f);
							chain.Add(branchLPoint);
						}
					}
					chain.Add(branchR);
					if (i == 1 || i == 9)
					{
						for (int j = 0; j < 10; j++)
						{
							VerletNode branchRPoint = new(chain.points[^1], new Vector2(16f, 0f), 1f, 0.5f, 3f, 2f * scale, 4f * scale, 0f, 0f);
							branchRPoint.position = point.position + new Vector2(0f, -30f);
							chain.Add(branchRPoint);
						}
					}
				}
			}
		}
		public Vector2 RandomVector() => new Vector2(128f * Main.rand.NextFloat(3, 6), 0f).RotateTo(NPC.velocity.ToRotation()).RotatedBy(Main.rand.NextFloat(-30f, 31f).InRadians());
		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			chain.Update();
			Texture2D circle = ModContent.Request<Texture2D>("Providence/Assets/Textures/Circle").Value;
			Texture2D glow = ModContent.Request<Texture2D>("Providence/Assets/Textures/SoftGlow").Value;

			Color bright = new(0.5f, 0.5f, 0.5f, 0f);
			Color mid = new(0.3f, 0.3f, 0.3f, 0f);
			Color dark = new(0.1f, 0.1f, 0.1f, 0f);

			spriteBatch.Draw(glow, NPC.VisualPosition, new Rectangle(0, 0, 64, 64), mid, 0f, new Vector2(32f, 32f), 1f, SpriteEffects.None, 0f);
			for (int i = 0; i < chain.points.Count; i++)
			{
				VerletNode point = chain.points[i];
				if (point.branch)
				{
					spriteBatch.Draw(glow, point.lead.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.lead.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
				}
				if (point.lead != null)
				{
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, 0f, new Vector2(32f, 32f), 1f, SpriteEffects.None, 0f);
					for (int j = 0; j < 10; j++)
					{
						float iScale = 1f + (10f - i) / 10f;
						float jScale = (10f - j) / 100f;
						Vector2 particlePos = Vector2.Lerp(point.position, point.lead.position, j / 10f);
						spriteBatch.Draw(glow, particlePos - Main.screenPosition, new Rectangle(0, 0, 64, 64), mid, 0f, new Vector2(32f, 32f), 1f * 0.125f, SpriteEffects.None, 0f);
					}
				}
			}
		}
	}
}
