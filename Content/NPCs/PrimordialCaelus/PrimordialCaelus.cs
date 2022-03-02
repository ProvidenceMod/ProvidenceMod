using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.World;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using static Providence.Projectiles.ProvidenceGlobalProjectileAI;
using Terraria.DataStructures;
using Providence.Rarities.Systems;
using Providence.Content.Buffs.DamageOverTime;
using Providence.Content.Dusts;
using Providence.Content.Items.Placeables.Ores;
using Providence.Content.Items.TreasureBags;
using Providence.Content.NPCs.PrimordialCaelus;
using Providence.Content.Projectiles.Boss;

namespace Providence.Content.NPCs.PrimordialCaelus
{
	public class PrimordialCaelus : ModNPC
	{
		public Vector4 color = new Vector4(1f, 1f, 1f, 1f);
		private bool spawnText;
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

		public Player player;

		public float rain = 1f;

		// Bullet hell
		public int preBulletHellTimer = 300;
		public int bulletHellTimer;

		// Stun
		public bool stunned;
		public int stunCounter;
		public int stunTimer;

		// Timers & Cooldowns
		public int tridentTimer;
		public int dartCooldown;
		public int sentinelCooldown;

		// Movement
		public float speedCap = 6f;
		public int dashCounter = 300;
		public bool preDashing;
		public bool dashing;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Caelus");
			NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
			NPCID.Sets.NeedsExpertScaling[NPC.type] = false;
		}
		public override void SetDefaults()
		{
			NPC.scale = 1f;
			NPC.damage = 25;
			NPC.width = 146;
			NPC.height = 230;
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
		public override void AI()
		{
			// npc.ai[0] = None?
			// npc.ai[1] = Dust Counter
			// npc.ai[2] = Stun Counter
			// npc.ai[3] = ?
			NPC.TargetClosest(true);

			if (--preBulletHellTimer > 0 && NPC.life == NPC.lifeMax)
				return;
			if ((preBulletHellTimer <= 0 || NPC.life < NPC.lifeMax) && !spawnText)
			{
				Talk("Primordial Caelus seeks your destruction.", new Color(71, 74, 145), NPC.type);
				spawnText = true;
			}

			NPC.UpdatePositionCache();
			NPC.UpdateCenterCache();
			NPC.UpdateRotationCache();

			DustEffects();

			NPC.rotation = Utils.Clamp(NPC.velocity.X * 0.05f, -0.5f, 0.5f);

			stunTimer = NPC.ai[2] >= (Phase() == 1 ? 3 : 6) ? 180 : 0;
			stunned = NPC.ai[2] >= (Phase() == 1 ? 3 : 6);

			if (stunned && Stunned())
				return;

			if (dashCounter <= 61 && dashCounter > 0)
			{
				dashCounter--;
				return;
			}
			if (dashCounter <= 0)
			{
				dashCounter = 300;
				dashing = false;
			}

			player = Main.player[NPC.target];

			bulletHellTimer++;

			if (Phase() == 1)
				PhaseOne();
			if (Phase() == 2)
				PhaseTwo();
			if (Phase() == 3)
				PhaseThree();
		}
		public void PhaseOne()
		{
			Movement();

			if (tridentTimer++ % 270 == 0)
				SummonTridents();

			if (bulletHellTimer % 600 == 0)
			{
				for (float i = 0; i < 3; i++)
				{
					Vector2 pos = new Vector2(640f, 0f).RotatedBy((-i * MathHelper.PiOver4) + MathHelper.PiOver4);
					NewNPCExtraAI(new Vector2(NPC.Center.X, NPC.Center.Y) + pos, NPCType<ZephyrSpirit>(), default, NPC.whoAmI);
				}
			}

			if (bulletHellTimer >= 1200)
				bulletHellTimer = 0;
		}
		public void PhaseTwo()
		{
			if (preDashing)
				speedCap = 4f;
			else
				speedCap = 8f;
			Movement();

			sentinelCooldown--;
			dartCooldown--;

			if (dartCooldown <= 0)
			{
				// Zephyr Blossom
				for (float i = 0; i <= MathHelper.TwoPi; i += MathHelper.PiOver4)
				{
					NewProjectileExtraAI(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(-i), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Spiral, i, 1);
					NewProjectileExtraAI(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(i), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Spiral, i, -1);
				}

				// Normal Shot
				Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal);

				// Helix Shot
				NewProjectileExtraAI(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Helix, NPC.AngleTo(player.position), 1);
				NewProjectileExtraAI(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Helix, NPC.AngleTo(player.position), -1);

				dartCooldown = 180;
			}

			if (sentinelCooldown <= 0)
			{
				NPC.NewNPC((int)NPC.Center.X + 200, (int)NPC.Center.Y - 200, NPCType<ZephyrSentinel>());
				NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y - 400, NPCType<ZephyrSentinel>());
				NPC.NewNPC((int)NPC.Center.X - 200, (int)NPC.Center.Y - 200, NPCType<ZephyrSentinel>());
				sentinelCooldown = 1800;
			}

			dashCounter--;

			if (dashCounter > 61 && dashCounter <= 161)
				preDashing = true;
			else
				preDashing = false;

			if (dashCounter == 61)
			{
				Dash();
				dashing = true;
			}
		}
		public void PhaseThree()
		{
			speedCap = 8f;

			Movement();

			dartCooldown--;

			if (dartCooldown <= 0)
			{
				Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal);
				Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.AcceleratedHoming, 1);

				for (float i = 0; i <= 1; i++)
				{
					float xDist = player.Center.X - NPC.Center.X;
					float spread = 3f / NPC.Distance(player.Center);
					Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(xDist * (5 + i) * spread, -6), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Gravity, i);
				}

				dartCooldown = 60;
			}
		}
		public bool Stunned() // Check if it's stunned and apply effects if it is.
		{
			if (stunTimer == 0)
				stunTimer = 180;
			if (Collision.SolidCollision(new Vector2(NPC.Center.X, NPC.Center.Y + (NPC.height / 2)), 1, 1))
				NPC.velocity.Y = 0;
			else
				NPC.velocity.Y = 6f;
			NPC.velocity.X /= 1.05f;
			if (--stunTimer == 0)
			{
				stunTimer = 180;
				NPC.ai[2] = 0;
				stunCounter = 0;
				stunned = false;
				return false;
			}
			else
				return true;
		}
		// TODO: expand list to encompass all minecarts
		public bool Enraged() // Check if it's enraged and apply effects if it is.
		{
			speedCap = player.mount.Type == MountID.Minecart ||
					   player.mount.Type == MountID.MinecartMech ||
					   player.mount.Type == MountID.MinecartWood ? (6f + Phase()) * 2 : 6f + Phase();
			return false;
		}
		public void Dash() // Perform a dash.
		{
			if (NPC.Distance(player.Center) > 500f)
			{
				Vector2 unitY = NPC.DirectionTo(player.Center);
				NPC.velocity = ((NPC.velocity * 15f) + (unitY * 10f)) / (15f + 1f);
				return;
			}
			NPC.velocity = NPC.DirectionTo(player.Center) * 15f;
		}
		public void SummonTridents() // Summons the trident attack.
		{
			Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.PiOver4), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 90);
			Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.PiOver2), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 180);
			Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.Pi + MathHelper.PiOver4), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 270);
		}
		public void RefreshRain() // Refreshes the rain timers.
		{
			Main.raining = true;
			Main.cloudBGActive = rain / 2f;
			Main.numCloudsTemp = Main.maxClouds;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedCurrent = rain / 2f;
			Main.windSpeedTarget = Main.windSpeedCurrent;
			Main.weatherCounter = 36000;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = rain;
		}
		public void Movement() // Movement method.
		{
			NPC.spriteDirection = NPC.direction;
			double wiggle = Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f;
			NPC.velocity.Y += (float)wiggle;
			if (Phase() == 1)
			{
				//Vector2 leftOfPlayer = player.Center + new Vector2(-300f, 0),
				//	rightOfPlayer = player.Center + new Vector2(300f, 0);
				//float distLeft = Vector2.Distance(npc.Center, leftOfPlayer),
				//	distRight = Vector2.Distance(npc.Center, rightOfPlayer),
				//	shortestOfTwo = Math.Min(distLeft, distRight);
				//Vector2 placeToFocus = shortestOfTwo == distRight ? rightOfPlayer : leftOfPlayer;
				//Vector2 unitY = npc.DirectionTo(placeToFocus);
				//npc.velocity = ((npc.velocity * 15f) + (unitY * speedCap)) / (15f + 1f);

				Vector2 position = player.Center + new Vector2(300f, 0f).RotatedBy((Math.Cos(Main.GlobalTimeWrappedHourly * 0.5f) * MathHelper.PiOver2) - MathHelper.PiOver2);
				Vector2 unitY = NPC.DirectionTo(position);
				NPC.velocity = ((NPC.velocity * 45f) + (unitY * 4f)) / (45f + 1f);
			}
			if (Phase() == 2)
			{
				Vector2 position = player.Center + new Vector2(300f, 0f).RotatedBy((Math.Cos(Main.GlobalTimeWrappedHourly * 0.5f) * MathHelper.PiOver2) - MathHelper.PiOver2);
				Vector2 unitY = NPC.DirectionTo(position);
				NPC.velocity = ((NPC.velocity * 45f) + (unitY * speedCap)) / (45f + 1f);
			}
			if (Phase() == 3)
			{
				Vector2 unitY = NPC.DirectionTo(player.Center);
				NPC.velocity = ((NPC.velocity * 15f) + (unitY * speedCap)) / (15f + 1f);
			}
		}
		public void DustEffects() // Spawns the dust around the boss.
		{
			NPC.ai[1]++; // Dust AI
			if (NPC.ai[1] == 3)
			{
				NPC.ai[1] = 0;
				Dust.NewDust(new Vector2(NPC.Hitbox.X + Main.rand.NextFloat(0, NPC.Hitbox.Width + 1), NPC.Hitbox.Y + Main.rand.NextFloat(0, NPC.Hitbox.Height + 1)), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			Lighting.AddLight(NPC.Center, ColorShift(new Color(174, 197, 231), new Color(83, 46, 99), 3f).ToVector3());
		}
		public override void FindFrame(int frameheight) // Animates the sprite.
		{
			//	Texture2D tex = Request<Texture2D>("Providence/NPCs/PrimordialCaelus/PrimordialCaelus");
			//	if (npc.frameCounter + 0.125f >= 12f)
			//		npc.frameCounter = 0f;
			//	npc.frameCounter += 0.125f;
			//	npc.frame.Y = (int)npc.frameCounter * (tex.Height / 12);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteBatch sb = new SpriteBatch(Main.graphics.GraphicsDevice);
			sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			float sin = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1f) * 12f;
			float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 12f) * 12f;
			Texture2D tex = Request<Texture2D>("Providence/NPCs/PrimordialCaelus/PrimordialCaelus").Value;
			sb.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(8f, -8f) + new Vector2(cos, -sin), NPC.frame, new Color(color.X, color.Y, color.Z, 0.25f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			sb.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(8f, 8f) + new Vector2(cos, sin), NPC.frame, new Color(color.X, color.Y, color.Z, 0.25f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			sb.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(-8f, 8f) + new Vector2(-cos, sin), NPC.frame, new Color(color.X, color.Y, color.Z, 0.25f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			sb.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(-8f, -8f) + new Vector2(-cos, -sin), NPC.frame, new Color(color.X, color.Y, color.Z, 0.25f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			if (!dashing && !stunned)
				spriteBatch.Draw(tex, NPC.Center - Main.screenPosition, NPC.frame, new Color(color.X, color.Y, color.Z, 1f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			if (dashing)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = 0.5f - (i * 0.05f);

					Color color = new Color(1f, 1f, 1f, alpha);

					sb.Draw(tex, NPC.Providence().oldCen[i] - Main.screenPosition, NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
				sb.Draw(tex, NPC.Center - Main.screenPosition, NPC.frame, new Color(color.X, color.Y, color.Z, color.W), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			if (stunned)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = (1f - (i * 0.1f)) * (float)((float)(0.375d * Math.Sin(Main.GlobalTimeWrappedHourly * 20d)) + 0.625d);
					float scale = (1f - (i * 0.025f)) * (float)((float)(0.025d * Math.Sin(Main.GlobalTimeWrappedHourly * 20d)) + 1.025d);

					Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 128), new Vector4(83, 46, 99, 128), i / 9f).RGBAIntToFloat();

					colorV.X *= alpha;
					colorV.Y *= alpha;
					colorV.Z *= alpha;
					colorV.W *= alpha;

					Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);

					spriteBatch.Draw(tex, NPC.Providence().oldCen[i] - Main.screenPosition, NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2, scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
			}
			sb.End();
			return false;
		} // Draws the afterimage.
		public override void HitEffect(int hitDirection, double damage) // Spawns dust when boss dies.
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					float angle = Main.rand.NextFloat(0, 360);
					Vector2 speed = new Vector2(Main.rand.NextFloat(2f, 12f), Main.rand.NextFloat(2f, 12f)).RotatedBy(angle.InRadians());
					float scale = Main.rand.NextFloat(1f, 2f);
					int dust = Dust.NewDust(NPC.Hitbox.RandomPointInHitbox(), 5, 5, DustType<CloudDust>(), speed.X, speed.Y, 255, default, scale);
					Main.dust[dust].scale = scale;
					Main.dust[dust].noGravity = false;
				}
			}
		}
		public override void OnKill() // Drops loot.
		{
			if (!WorldFlags.downedCaelus)
			{
				WorldFlags.downedCaelus = true;
				WorldFlags.zephyrGenned = true;
				Talk("Powerful air suffuses into the ground...", new Color(158, 186, 226));
				WorldBuilding.BuildOre(TileType<Tiles.Ores.ZephyrOre>(), 0.00005f, 1, 10, 13, 0.35f, 0.6f);
			}
			Main.raining = false;
			Main.cloudBGActive = 0f;
			Main.numCloudsTemp = 4;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedCurrent = 0.25f;
			Main.windSpeedTarget = Main.windSpeedCurrent;
			Main.weatherCounter = 0;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = 0f;
			if (Main.expertMode)
			{
				Main.item[Item.NewItem(NPC.Center, ItemType<CaelusBag>(), 1)].Providence().highlight = true;
				// No Lament, No Wrath.
				if (!WorldFlags.lament && !WorldFlags.wrath)
				{
					Item.NewItem(NPC.Center, ItemID.GoldCoin, 7);
					Item.NewItem(NPC.Center, ItemID.SilverCoin, 50);
				}
				// Only Lament.
				if (WorldFlags.lament && !WorldFlags.wrath)
				{
					Item.NewItem(NPC.Center, ItemID.GoldCoin, 10);
				}
				// Lament and Wrath.
				if (WorldFlags.wrath)
				{
					Item.NewItem(NPC.Center, ItemID.GoldCoin, 12);
					Item.NewItem(NPC.Center, ItemID.SilverCoin, 50);
				}
			}
			else
			{
				Item.NewItem(NPC.Center, ItemID.GoldCoin, 5);
				Item.NewItem(NPC.Center, ItemType<ZephyrOre>(), Main.rand.Next(16, 51));
			}
		}
	}
}
