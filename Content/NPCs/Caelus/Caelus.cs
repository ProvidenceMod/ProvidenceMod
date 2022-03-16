using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.Content.Buffs.DamageOverTime;
using Providence.Content.Items.Placeables.Ores;
using Providence.Content.Items.TreasureBags;
using Providence.Globals.Systems.Particles;
using Providence.RenderTargets;
using Providence.Systems;
using Providence.Verlet;
using Providence.World;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Providence.RenderTargets.ZephyrLayer;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.NPCs.Caelus
{
	public class Caelus : ModNPC, IZephyrSprite
	{
		public bool Active => NPC.active;

		public Player player;
		public AIState state;
		public VerletChain leftTethers;
		public VerletChain rightTethers;
		public NPC tether1;
		public NPC tether2;
		public NPC tether3;
		public NPC tether4;
		public enum AIState
		{
			Moving
		}

		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(0, "Sentinel Caelus");
			NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
			NPCID.Sets.NeedsExpertScaling[NPC.type] = false;
		}
		public override void SetDefaults()
		{
			NPC.scale = 1f;
			NPC.damage = 25;
			NPC.width = 278;
			NPC.height = 134;
			NPC.lifeMax = Main.expertMode ? 9600 : 6200;
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			NPC.aiStyle = -1;
			NPC.townNPC = false;
			NPC.noGravity = true;
			NPC.chaseable = true;
			NPC.lavaImmune = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit30;
			NPC.buffImmune[BuffID.OnFire] = true;
			NPC.buffImmune[BuffID.Confused] = true;
			NPC.buffImmune[BuffType<PressureSpike>()] = true;
		}

		public int preTimer = 180;
		public bool spawn;
		public float maxSpeed;
		public override void AI()
		{
			NPC.TargetClosest(true);
			maxSpeed = 3f;

			if (!spawn)
			{
				spawn = true;
				Spawn();
			}

			if (--preTimer > 0 && NPC.life == NPC.lifeMax)
				return;

			state = AIState.Moving;

			NPC.UpdatePositionCache();
			NPC.UpdateCenterCache();
			NPC.UpdateRotationCache();

			Particles();
			UpdateTethers();
			leftTethers.Update();
			rightTethers.Update();

			player = Main.player[NPC.target];

			if (state == AIState.Moving)
				Moving();
		}

		public void Moving()
		{
			NPC.velocity = NPC.DirectionTo(player.Center) * 3f;
		}

		public void Spawn()
		{
			RenderTargetManager.ZephyrLayer.Sprites.Add(this);

			Vector2 p1 = new((int)((NPC.Center.X - 200)), (int)((NPC.Center.Y - 100)));
			Vector2 p2 = new((int)((NPC.Center.X - 100)), (int)((NPC.Center.Y - 75)));
			Vector2 p3 = new((int)((NPC.Center.X + 100)), (int)((NPC.Center.Y - 75)));
			Vector2 p4 = new((int)((NPC.Center.X + 200)), (int)((NPC.Center.Y - 100)));

			tether1 = Main.npc[NPC.NewNPC(new EntitySource_SpawnNPC(), (int)((NPC.Center.X - 200)), (int)((NPC.Center.Y - 100)), NPCType<CaelusTether>(), 0, p1.X, p1.Y, maxSpeed, NPC.whoAmI)];
			tether2 = Main.npc[NPC.NewNPC(new EntitySource_SpawnNPC(), (int)((NPC.Center.X - 100)), (int)((NPC.Center.Y - 75)), NPCType<CaelusTether>(), 0, p2.X, p2.Y, maxSpeed, NPC.whoAmI)];
			tether3 = Main.npc[NPC.NewNPC(new EntitySource_SpawnNPC(), (int)((NPC.Center.X + 100)), (int)((NPC.Center.Y - 75)), NPCType<CaelusTether>(), 0, p3.X, p3.Y, maxSpeed, NPC.whoAmI)];
			tether4 = Main.npc[NPC.NewNPC(new EntitySource_SpawnNPC(), (int)((NPC.Center.X + 200)), (int)((NPC.Center.Y - 100)), NPCType<CaelusTether>(), 0, p4.X, p4.Y, maxSpeed, NPC.whoAmI)];

			VerletNode leftOrigin = new(null, Vector2.Zero, maxSpeed * 9f, 0.25f, 7f, 100f, 200f, 0f, 0f);
			leftTethers = new(NPC.position + new Vector2(60f, 28f), leftOrigin);
			VerletNode t1 = new(leftOrigin, Vector2.Zero, maxSpeed, 0.25f, 7f, 100f, 200f, 0f, 45f.InRadians());
			VerletNode t2 = new(t1, Vector2.Zero, maxSpeed, 0.25f, 7f, 100f, 200f, 0f, -35f.InRadians());
			t1.position = p2;
			t2.position = p1;
			t1.branch = true;
			t2.branch = true;
			leftTethers.Add(t1, leftOrigin);
			leftTethers.Add(t2, t1);

			VerletNode rightOrigin = new(null, Vector2.Zero, maxSpeed * 9f, 0.25f, 7f, 100f, 200f, 0f, 0f);
			rightTethers = new(NPC.position + new Vector2(218f, 28f), rightOrigin);
			VerletNode t3 = new(rightOrigin, Vector2.Zero, maxSpeed, 0.25f, 7f, 100f, 200f, 0f, 135f.InRadians());
			VerletNode t4 = new(t3, Vector2.Zero, maxSpeed, 0.25f, 7f, 100f, 200f, 0f, 35f.InRadians());
			t3.position = p3;
			t4.position = p4;
			t3.branch = true;
			t4.branch = true;
			rightTethers.Add(t3, rightOrigin);
			rightTethers.Add(t4, t3);
		}
		public override void OnKill()
		{
			if (!WorldFlags.downedCaelus)
			{
				WorldFlags.downedCaelus = true;
				WorldFlags.zephyrGenned = true;
				Talk("Powerful winds suffuse into the ground...", new Color(158, 186, 226));
				WorldBuilding.BuildOre(TileType<Tiles.Ores.ZephyrOre>(), 0.00005f, 1, 10, 13, 0.35f, 0.6f);
			}

			if (Main.expertMode)
			{
				Main.item[Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, new Vector2(NPC.width, NPC.height), ItemType<CaelusBag>(), 1)].Providence().highlight = true;
				// No Lament, No Wrath.
				if (!WorldFlags.lament && !WorldFlags.wrath)
				{
					Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.GoldCoin, 7);
					Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.SilverCoin, 50);
				}
				// Only Lament.
				if (WorldFlags.lament && !WorldFlags.wrath)
				{
					Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.GoldCoin, 10);
				}
				// Lament and Wrath.
				if (WorldFlags.wrath)
				{
					Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.GoldCoin, 12);
					Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.SilverCoin, 50);
				}
			}
			else
			{
				Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemID.GoldCoin, 5);
				Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemType<ZephyrOre>(), Main.rand.Next(16, 51));
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			spriteBatch.Draw(Request<Texture2D>("Providence/Content/NPCs/Caelus/Caelus").Value, NPC.position - screenPos, NPC.frame, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}
		float wingPulse = 0f;
		float wingProgress = 0f;
		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			if (leftTethers == null || rightTethers == null)
				return;

			Texture2D glow = ModContent.Request<Texture2D>("Providence/Assets/Textures/SoftGlow").Value;
			Texture2D circle = ModContent.Request<Texture2D>("Providence/Assets/Textures/Masks/CircleMaskBlack").Value;
			Texture2D flare = ModContent.Request<Texture2D>("Providence/Globals/Systems/Particles/AetherFlare").Value;

			Color bright = new(0.5f, 0.5f, 0.5f, 0f);
			Color mid = new(0.3f, 0.3f, 0.3f, 0f);
			Color dark = new(0.1f, 0.1f, 0.1f, 0f);

			wingPulse += 1f / 60f;
			wingProgress++;

			for (int i = 0; i < rightTethers.points.Count; i++)
			{
				VerletNode point = leftTethers.points[i];
				spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 100f), SpriteEffects.None, 0f);
				if (point.branch)
				{
					spriteBatch.Draw(glow, point.lead.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.lead.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
				}
				if (point.lead != null)
				{
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, 0f, new Vector2(32f, 32f), 1f, SpriteEffects.None, 0f);

					for (int j = 0; j < 100; j++)
					{
						Vector2 particlePos = Vector2.Lerp(point.position, point.lead.position, j / 100f);
						float opacity = ((float)Math.Sin((wingProgress * 0.1f) + (j * 0.1f)) / 3f) + 1f;
						float scale = ((200f - ((i - 1) * 100f) + j) / 400f);
						if (j % 20 == 0)
						{
							spriteBatch.Draw(circle, particlePos - Main.screenPosition, new Rectangle(0, 0, 256, 512), Color.Multiply(dark, opacity), point.rotation - MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(0.25f * scale, (1f / 512f) * 100f), SpriteEffects.None, 0f);
							spriteBatch.Draw(circle, particlePos - Main.screenPosition, new Rectangle(0, 0, 256, 512), Color.Multiply(mid, opacity), point.rotation - MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(1f * scale, (1f / 512f) * 100f), SpriteEffects.None, 0f);
						}
						spriteBatch.Draw(glow, particlePos - Main.screenPosition, new Rectangle(0, 0, 64, 64), mid, 0f, new Vector2(32f, 32f), 1f * 0.125f, SpriteEffects.None, 0f);
					}
				}
			}
			for (int i = 0; i < rightTethers.points.Count; i++)
			{
				VerletNode point = rightTethers.points[i];
				spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 100f), SpriteEffects.None, 0f);
				if (point.branch)
				{
					spriteBatch.Draw(glow, point.lead.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.lead.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, point.rotation, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 4f), SpriteEffects.None, 0f);
				}
				if (point.lead != null)
				{
					spriteBatch.Draw(glow, point.position - Main.screenPosition, new Rectangle(0, 0, 64, 64), dark, 0f, new Vector2(32f, 32f), 1f, SpriteEffects.None, 0f);
					for (int j = 0; j < 100; j++)
					{
						Vector2 particlePos = Vector2.Lerp(point.position, point.lead.position, j / 100f);
						float opacity = ((float)Math.Sin((wingProgress * 0.1f) + (j * 0.1f)) / 3f) + 1f;
						float scale = ((200f - ((i - 1) * 100f) + j) / 400f);
						if (j % 20 == 0)
						{
							spriteBatch.Draw(circle, particlePos - Main.screenPosition, new Rectangle(0, 0, 256, 512), Color.Multiply(dark, opacity), point.rotation + MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(0.25f * scale, (1f / 512f) * 100f), SpriteEffects.None, 0f);
							spriteBatch.Draw(circle, particlePos - Main.screenPosition, new Rectangle(0, 0, 256, 512), Color.Multiply(mid, opacity), point.rotation + MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(1f * scale, (1f / 512f) * 100f), SpriteEffects.None, 0f);
						}
						spriteBatch.Draw(glow, particlePos - Main.screenPosition, new Rectangle(0, 0, 64, 64), mid, 0f, new Vector2(32f, 32f), 1f * 0.125f, SpriteEffects.None, 0f);
					}
				}
			}

			//for (int i = 0; i < 100; i++)
			//{
			//	Vector2 point = leftTethers.Evaluate(i / 100f);
			//	float angle = leftTethers.AngleEvaluate(i / 100f);
			//	if (i % 10 == 0)
			//		spriteBatch.Draw(circle, point - Main.screenPosition, new Rectangle(0, 0, 256, 512), bright, angle - MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(0.25f, (1f / 512f) * 100f), SpriteEffects.None, 0f);
			//}
			//for (int i = 0; i < 100; i++)
			//{
			//	Vector2 point = rightTethers.Evaluate(i / 100f);
			//	float angle = rightTethers.AngleEvaluate(i / 100f);
			//	Console.WriteLine(angle);
			//	if (i % 10 == 0)
			//		spriteBatch.Draw(circle, point - Main.screenPosition, new Rectangle(0, 0, 256, 512), bright, angle + MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(1f, (1f / 512f) * 100f), SpriteEffects.None, 0f);
			//}

			float pulseOpacity = 1f - Math.Abs(((wingPulse * 1.5f) - 0.75f));
			float auraOpacity = 0.75f + (float)((Math.Sin(Main.GlobalTimeWrappedHourly) + 1f) / 8f);

			spriteBatch.Draw(circle, NPC.Center - Main.screenPosition, new Rectangle(0, 0, 256, 512), Color.Multiply(bright, auraOpacity), MathHelper.PiOver2, new Vector2(256f, 256f), new Vector2(1f, (1f / 512f) * 100f), SpriteEffects.None, 0f);

			spriteBatch.Draw(glow, leftTethers.Evaluate(wingPulse) - Main.screenPosition, new Rectangle(0, 0, 64, 64), Color.Multiply(bright, pulseOpacity), 0f, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 100f), SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, rightTethers.Evaluate(wingPulse) - Main.screenPosition, new Rectangle(0, 0, 64, 64), Color.Multiply(bright, pulseOpacity), 0f, new Vector2(32f, 32f), new Vector2(10f, (1f / 64f) * 100f), SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, leftTethers.Evaluate(wingPulse) - Main.screenPosition, new Rectangle(0, 0, 64, 64), Color.Multiply(bright, pulseOpacity), 0f, new Vector2(32f, 32f), new Vector2(1f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, rightTethers.Evaluate(wingPulse) - Main.screenPosition, new Rectangle(0, 0, 64, 64), Color.Multiply(bright, pulseOpacity), 0f, new Vector2(32f, 32f), new Vector2(1f, 1f), SpriteEffects.None, 0f);

			if (wingPulse >= 1f)
				wingPulse = 0f;

			spriteBatch.Draw(circle, NPC.Center - Main.screenPosition, new Rectangle(0, 0, 512, 512), Color.Multiply(bright, auraOpacity), 0f, new Vector2(256f, 256f), new Vector2(0.6f, 0.6f), SpriteEffects.None, 0f);
			spriteBatch.Draw(flare, NPC.position + new Vector2(139f, 71f) - Main.screenPosition, flare.Bounds, Color.Multiply(bright, auraOpacity), 0f, flare.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
		}
		public int Phase()
		{
			double quotient = (double)NPC.life / NPC.lifeMax;
			if (quotient > 0.6d)
				return 1;
			else if (quotient > 0.3d && quotient <= 0.6d)
				return 2;
			else
				return 3;
		}

		public int particleCounter;
		public void Particles() // Spawns the dust around the boss.
		{
			particleCounter++;
			if (particleCounter % 3 == 0)
			{
				ParticleManager.NewParticle(NPC.position + NPC.frame.RandomPointInHitbox(), NPC.velocity, new ZephyrParticle(), Color.White, 1f);
				ParticleManager.NewParticle(NPC.position + new Vector2(10f, 70f), new Vector2(-5f, 2.5f), new ZephyrParticle(), Color.White, 1f);
				ParticleManager.NewParticle(NPC.position + new Vector2(267f, 70f), new Vector2(5f, 2.5f), new ZephyrParticle(), Color.White, 1f);
			}

		}
		public void UpdateTethers()
		{
			tether1.ai[0] = leftTethers.points[1].position.X;
			tether1.ai[1] = leftTethers.points[1].position.Y;
			tether1.ai[2] = maxSpeed * 9f;
			tether2.ai[0] = leftTethers.points[2].position.X;
			tether2.ai[1] = leftTethers.points[2].position.Y;
			tether2.ai[2] = maxSpeed * 9f;
			tether3.ai[0] = rightTethers.points[1].position.X;
			tether3.ai[1] = rightTethers.points[1].position.Y;
			tether3.ai[2] = maxSpeed * 9f;
			tether4.ai[0] = rightTethers.points[2].position.X;
			tether4.ai[1] = rightTethers.points[2].position.Y;
			tether4.ai[2] = maxSpeed * 9f;

			leftTethers.points[0].position = NPC.position + new Vector2(60f, 28f);
			rightTethers.points[0].position = NPC.position + new Vector2(218f, 28f);
		}
	}
}
