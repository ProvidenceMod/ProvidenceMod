using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using ProvidenceMod.Projectiles.Boss;
using ProvidenceMod.Items.Placeables.Ores;
using ProvidenceMod.Items.TreasureBags;
using ProvidenceMod.NPCs.PrimordialCaelus;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using static ProvidenceMod.Projectiles.ProvidenceGlobalProjectileAI;
using ProvidenceMod.Projectiles;
using ProvidenceMod.World;

namespace ProvidenceMod.NPCs.PrimordialCaelus
{
	public class PrimordialCaelus : ModNPC
	{
		public Vector4 color = new Vector4(1f, 1f, 1f, 1f);
		public bool preSpawnText;
		private bool spawnText;
		public int Phase()
		{
			double quotient = (double)npc.life / npc.lifeMax;
			if (quotient > 0.6d)
				return 1;
			else if (quotient > 0.3d && quotient <= 0.6d)
				return 2;
			else
				return 3;
		}
		public float rain = 1f;
		public float speedCap = 6f;
		public byte sentinelsSpawned, spiritsSpawned;

		public Player player;

		// Bullet hell
		public int preBulletHellTimer = 300;
		public int bulletHellTimer;

		// Stun
		public bool stunned;
		public int stunCounter;
		public int stunTimer;

		// Cooldowns
		public int dartCooldown;
		public int sentinelCooldown;

		// Dash
		public bool isDashing;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Caelus");
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
			NPCID.Sets.NeedsExpertScaling[npc.type] = true;
		}

		public override void SetDefaults()
		{
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/HighInTheSky");
			musicPriority = MusicPriority.BossMedium; // By default, musicPriority is BossLow
			npc.scale = 1f;
			npc.damage = 25;
			npc.width = 146;
			npc.height = 230;
			npc.aiStyle = -1;
			npc.lifeMax = 3400;
			npc.knockBackResist = 0f;
			npc.boss = true;
			npc.townNPC = false;
			npc.noGravity = true;
			npc.chaseable = true;
			npc.lavaImmune = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit30;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[mod.BuffType("Freezing")] = true;
			npc.buffImmune[mod.BuffType("Frozen")] = true;
		}
		public override void AI()
		{
			player = Main.player[npc.target];
			npc.UpdatePositionCache();
			npc.UpdateCenterCache();
			npc.UpdateRotationCache();
			DustEffects();
			npc.rotation = Utils.Clamp(npc.velocity.X * 0.05f, -0.5f, 0.5f);
			stunned = npc.ai[2] >= (Phase() == 1 ? 5 : 10);

			if (npc.ai[3] > 0) npc.ai[3]--;
			speedCap = player.mount.Type == MountID.MineCart || player.mount.Type == MountID.MineCartMech || player.mount.Type == MountID.MineCartWood ? (6f + Phase()) * 2 : 6f + Phase();

			if (--preBulletHellTimer > 0 && npc.life == npc.lifeMax)
				return;
			if ((preBulletHellTimer <= 0 || npc.life < npc.lifeMax) && !spawnText)
			{
				Talk("Primordial Caelus seeks your destruction.", new Color(71, 74, 145), npc.type);
				spawnText = true;
			}

			if (stunned)
			{
				if (stunTimer == 0)
					stunTimer = 180;
				stunTimer--;
				if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y + (npc.height / 2)), 1, 1))
					npc.velocity.Y = 0;
				else
					npc.velocity.Y = 6f;
				npc.velocity.X = 0;
				if (stunTimer == 0)
				{
					stunTimer = 180;
					npc.ai[2] = 0;
					stunCounter = 0;
					stunned = false;
				}
				else
				{
					return;
				}
			}
			else
			{
				bulletHellTimer++;
				npc.TargetClosest(true);
			}

			//Phase 1 gate
			if (Phase() == 1)
			{
				npc.ai[0]++;
				if (npc.ai[0] % 270 == 0)
				{
					SummonTridents();
				}
				Movement();

				if (bulletHellTimer % 600 == 0)
				{
					for (float i = 0; i >= -MathHelper.Pi; i -= MathHelper.PiOver4)
					{
						Vector2 pos = new Vector2(640f, 0f).RotatedBy(i);
						NewNPCExtraAI(new Vector2(npc.Center.X, npc.Center.Y) + pos, NPCType<ZephyrSpirit>(), default, npc.whoAmI);
					}
					npc.ai[3] = 600;
					//NPC.NewNPC((int)npc.Center.X - 400, (int)npc.Center.Y - 400, NPCType<ZephyrSpirit>(), default, npc.whoAmI);
					//NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 800, NPCType<ZephyrSpirit>(), default, npc.whoAmI);
					//NPC.NewNPC((int)npc.Center.X + 400, (int)npc.Center.Y - 400, NPCType<ZephyrSpirit>(), default, npc.whoAmI);
					//NPC.NewNPC((int)npc.Center.X + 800, (int)npc.Center.Y, NPCType<ZephyrSpirit>(), default, npc.whoAmI);
				}
				if (bulletHellTimer >= 1200)
					bulletHellTimer = 0;
			}
			if (Phase() == 2)
			{
				if (dartCooldown <= 0)
				{
					for (float i = 0; i <= MathHelper.TwoPi; i += MathHelper.PiOver4)
					{
						NewProjectileExtraAI(npc.Center, new Vector2(10f, 0f).RotatedBy(-i), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Spiral, i, 1);
						NewProjectileExtraAI(npc.Center, new Vector2(10f, 0f).RotatedBy(i), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Spiral, i, -1);
					}
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal);
					dartCooldown = 180;
					NewProjectileExtraAI(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Helix, npc.AngleTo(player.position), 1);
					NewProjectileExtraAI(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Helix, npc.AngleTo(player.position), -1);
				}
				if (sentinelCooldown <= 0)
				{
					NPC.NewNPC((int)npc.Center.X + 200, (int)npc.Center.Y - 200, NPCType<ZephyrSentinel>());
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 400, NPCType<ZephyrSentinel>());
					NPC.NewNPC((int)npc.Center.X - 200, (int)npc.Center.Y - 200, NPCType<ZephyrSentinel>());
					sentinelCooldown = 1200;
				}
				sentinelCooldown--;
				dartCooldown--;
				Movement();
			}
			if (Phase() == 3)
			{
				if (dartCooldown <= 0)
				{
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal);
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(player.position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.AcceleratedHoming, 1);
					dartCooldown = 60;
					for (float i = 0; i <= 10; i++)
					{
						float xDist = player.Center.X - npc.Center.X;
						float spread = 3f / npc.Distance(player.Center);
						Projectile.NewProjectile(npc.Center, new Vector2(xDist * (-5 + i) * spread, -16), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Gravity, i);
					}
				}
				dartCooldown--;
				Movement();
			}
		}
		public void SummonTridents()
		{
			Projectile.NewProjectile(npc.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.PiOver4), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 90);
			Projectile.NewProjectile(npc.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.PiOver2), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 180);
			Projectile.NewProjectile(npc.Center + new Vector2(300f, 0f).RotatedBy(-MathHelper.Pi + MathHelper.PiOver4), new Vector2(0f, 0f), ProjectileType<ZephyrTrident>(), 25, 2f, default, 0, 270);
		}
		public void SetAIVariables()
		{
		}
		public void RefreshRain()
		{
			Main.raining = true;
			Main.cloudBGActive = rain / 2f;
			Main.numCloudsTemp = Main.cloudLimit;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedTemp = rain / 2f;
			Main.windSpeedSet = Main.windSpeedTemp;
			Main.weatherCounter = 36000;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = rain;
		}
		public void Movement()
		{
			npc.spriteDirection = npc.direction;
			double wiggle = Math.Sin(Main.GlobalTime * 3f) * 0.1f;
			npc.velocity.Y += (float)wiggle;
			if (Phase() == 1)
			{
				Vector2 leftOfPlayer = player.Center + new Vector2(-300f, 0),
					rightOfPlayer = player.Center + new Vector2(300f, 0);
				float distLeft = Vector2.Distance(npc.Center, leftOfPlayer),
					distRight = Vector2.Distance(npc.Center, rightOfPlayer),
					shortestOfTwo = Math.Min(distLeft, distRight);
				Vector2 placeToFocus = shortestOfTwo == distRight ? rightOfPlayer : leftOfPlayer;
				Vector2 unitY = npc.DirectionTo(placeToFocus);
				npc.velocity = ((npc.velocity * 15f) + (unitY * speedCap)) / (15f + 1f);
			}
			if (Phase() == 2)
			{

				Vector2 position = player.Center + new Vector2(300f, 0f).RotatedBy((Math.Cos(Main.GlobalTime * 0.5f) * MathHelper.PiOver2) - MathHelper.PiOver2);
				Vector2 unitY = npc.DirectionTo(position);
				npc.velocity = ((npc.velocity * 45f) + (unitY * 4f)) / (45f + 1f);
			}
			if (Phase() == 3)
			{
				Vector2 unitY = npc.DirectionTo(player.Center);
				npc.velocity = ((npc.velocity * 15f) + (unitY * speedCap)) / (15f + 1f);
			}
		}
		public override void FindFrame(int frameheight)
		{
			//	Texture2D tex = GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus");
			//	if (npc.frameCounter + 0.125f >= 12f)
			//		npc.frameCounter = 0f;
			//	npc.frameCounter += 0.125f;
			//	npc.frame.Y = (int)npc.frameCounter * (tex.Height / 12);
		}
		public void DustEffects()
		{
			npc.ai[1]++; // Dust AI
			if (npc.ai[1] == 3)
			{
				npc.ai[1] = 0;
				Dust.NewDust(new Vector2(npc.Hitbox.X + Main.rand.NextFloat(0, npc.Hitbox.Width + 1), npc.Hitbox.Y + Main.rand.NextFloat(0, npc.Hitbox.Height + 1)), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			Lighting.AddLight(npc.Center, ColorShift(new Color(174, 197, 231), new Color(83, 46, 99), 3f).ToVector3());
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			DrawAfterImage(Main.spriteBatch);
			return false;
		}
		public void DrawAfterImage(SpriteBatch spriteBatch)
		{
			SpriteBatch spriteBatch1 = new SpriteBatch(Main.graphics.GraphicsDevice);
			spriteBatch1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			if (!isDashing && !stunned)
			{
				//for (int i = 1; i <= 3; i++)
				//{
				//	float alpha = 1f - (i * 0.25f);

				//	Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 0), new Vector4(83, 46, 99, 0), i / 3f).ColorRGBAIntToFloat();

				//	colorV.X *= alpha;
				//	colorV.Y *= alpha;
				//	colorV.Z *= alpha;
				//	colorV.W *= alpha;

				//	Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);

				//	spriteBatch1.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus"), npc.Providence().oldCen[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				//}
				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus"), npc.Center - Main.screenPosition, npc.frame, new Color(color.X, color.Y, color.Z, color.W), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			if (isDashing)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = 1f - (i * 0.1f);

					Color color = new Color(1f, 1f, 1f, alpha);

					spriteBatch1.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus"), npc.Providence().oldCen[i] - Main.screenPosition, npc.frame, color, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
				spriteBatch1.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus"), npc.Center - Main.screenPosition, npc.frame, new Color(color.X, color.Y, color.Z, color.W), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			if (stunned)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = (1f - (i * 0.1f)) * (float)((float)(0.375d * Math.Sin(Main.GlobalTime * 20d)) + 0.625d);
					float scale = (1f - (i * 0.025f)) * (float)((float)(0.025d * Math.Sin(Main.GlobalTime * 20d)) + 1.025d);

					Vector4 colorV = Vector4.Lerp(new Vector4(174, 197, 231, 128), new Vector4(83, 46, 99, 128), i / 9f).ColorRGBAIntToFloat();

					colorV.X *= alpha;
					colorV.Y *= alpha;
					colorV.Z *= alpha;
					colorV.W *= alpha;

					Color color = new Color(colorV.X, colorV.Y, colorV.Z, colorV.W);

					spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus"), npc.Providence().oldCen[i] - Main.screenPosition, npc.frame, color, npc.rotation, npc.frame.Size() / 2, scale, npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
			}
			spriteBatch1.End();
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					float angle = Main.rand.NextFloat(0, 360);
					Vector2 speed = new Vector2(Main.rand.NextFloat(2f, 12f), Main.rand.NextFloat(2f, 12f)).RotatedBy(angle.InRadians());
					float scale = Main.rand.NextFloat(1f, 2f);
					int dust = Dust.NewDust(npc.Hitbox.RandomPointInHitbox(), 5, 5, DustType<CloudDust>(), speed.X, speed.Y, 255, default, scale);
					Main.dust[dust].scale = scale;
					Main.dust[dust].noGravity = false;
				}
			}
		}
		public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
		{
			if (!ProvidenceWorld.downedCaelus || !BrinewastesWorld.downedCaelus)
			{
				ProvidenceWorld.downedCaelus = true;
				BrinewastesWorld.downedCaelus = true;
				ProvidenceWorld.zephyrGenned = true;
				WorldBuilding.BuildOre(TileType<Tiles.Ores.ZephyrOre>(), 0.00005f, 1, 10, 13, 0.35f, 0.6f);
			}
			Main.raining = false;
			Main.cloudBGActive = 0f;
			Main.numCloudsTemp = 4;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedTemp = 0.25f;
			Main.windSpeedSet = Main.windSpeedTemp;
			Main.weatherCounter = 0;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = 0f;
			if (Main.expertMode)
			{
				Main.item[Item.NewItem(npc.Center, ItemType<CaelusBag>(), 1)].Providence().highlight = true;
				if (ProvidenceWorld.lament && !ProvidenceWorld.wrath)
				{
					Item.NewItem(npc.Center, ItemID.GoldCoin, 10);
					return;
				}
				if (ProvidenceWorld.wrath)
				{
					Item.NewItem(npc.Center, ItemID.GoldCoin, 12);
					Item.NewItem(npc.Center, ItemID.SilverCoin, 50);
					return;
				}
				Item.NewItem(npc.Center, ItemID.GoldCoin, 7);
				Item.NewItem(npc.Center, ItemID.SilverCoin, 50);
			}
			else
			{
				Item.NewItem(npc.Center, ItemID.GoldCoin, 5);
				Item.NewItem(npc.Center, ItemType<ZephyrOre>(), Main.rand.Next(16, 51));
			}
		}
	}
}