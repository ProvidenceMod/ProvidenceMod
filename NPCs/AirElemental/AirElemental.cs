using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Items.Placeable;
using ProvidenceMod.Items.TreasureBags;
using ProvidenceMod.Projectiles.Boss;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;

namespace ProvidenceMod.NPCs.AirElemental
{
	public class AirElemental : ModNPC
	{
		public Vector4 color = new Vector4(1f, 1f, 1f, 1f);
		public Vector2[] oldPos = new Vector2[10] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };
		public bool preSpawnText;
		private bool spawnText;

		public int phase = 0;
		double quotient;

		// Movement
		int direction;

		// Bullet hell
		public int preBulletHellTimer = 300;
		public int bulletHellTimer;

		// Pierce
		public int sprayTimer = 30;
		public int sprayDelay = 18;

		// Zephyr Spirit
		public int stunTimer = 180;
		public int spiritTimer = 30;
		public int[] spiritArray = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		public bool stunned;
		public int stunCounter;

		// Dash
		public bool preppingDash;
		public bool isDashing;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Air Elemental");
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
			NPCID.Sets.NeedsExpertScaling[npc.type] = true;
			Main.npcFrameCount[npc.type] = 12;
		}

		public override void SetDefaults()
		{
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/HighInTheSky");
			musicPriority = MusicPriority.BossMedium; // By default, musicPriority is BossLow
			npc.damage = 25;
			npc.aiStyle = -1;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 3400;
			npc.townNPC = false;
			npc.boss = true;
			npc.scale = 2f;
			npc.HitSound = SoundID.NPCHit1;
			npc.chaseable = true;
			npc.width = 162;
			npc.height = 122;
			npc.knockBackResist = 0f;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[mod.BuffType("Freezing")] = true;
			npc.buffImmune[mod.BuffType("Frozen")] = true;
			npc.Providence().resists = new float[8] { 0.25f, 1f, 1f, 1.5f, 0.25f, 1.5f, 1f, 1f };
		}
		public override void AI()
		{
			// Old position array
			oldPos[9] = oldPos[8];
			oldPos[8] = oldPos[7];
			oldPos[7] = oldPos[6];
			oldPos[6] = oldPos[5];
			oldPos[5] = oldPos[4];
			oldPos[4] = oldPos[3];
			oldPos[3] = oldPos[2];
			oldPos[2] = oldPos[1];
			oldPos[1] = oldPos[0];
			oldPos[0] = npc.Center;

			// Phase gate quotient
			quotient = npc.life / 6800d;

			npc.ai[0]++; // Dust AI
			if (npc.ai[0] == 3)
			{
				npc.ai[0] = 0;
				Dust.NewDust(new Vector2(npc.Hitbox.X + Main.rand.NextFloat(0, npc.Hitbox.Width + 1), npc.Hitbox.Y + Main.rand.NextFloat(0, npc.Hitbox.Height + 1)), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			Lighting.AddLight(npc.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());

			// Spawn text
			if (!preSpawnText)
			{
				Talk("The wind begins to stir...", new Color(71, 74, 145), npc);
				preSpawnText = true;
			}

			// Pre-phase pause 
			if (--preBulletHellTimer > 0 && npc.life == npc.lifeMax)
			{
				return;
			}

			// Phase 1 text
			if (!spawnText)
			{
				Talk("The Air Elemental has awoken!", new Color(71, 74, 145), npc);
				RefreshRain();
				spawnText = true;
			}

			for (int i = 0; i < 10; i++)
			{
				if (spiritArray[i] != 0)
				{
					NPC npc = Main.npc[spiritArray[i]];
					if (npc == null || npc.life <= 0)
					{
						stunCounter++;
						spiritArray[i] = 0;
					}
				}
			}
			if (stunCounter == (quotient > 0.6d ? 5 : 10))
			{
				stunned = true;
			}
			if (stunned)
			{
				float alpha = (float)((float)(0.25d * Math.Sin(Main.GlobalTime * 20d)) + 0.75d);
				npc.Opacity = alpha;
				color.X = alpha;
				color.Y = alpha;
				color.Z = alpha;
				color.W = alpha;
				stunTimer--;
				if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y + (npc.height / 2)), 1, 1))
				{
					npc.velocity.Y = 0;
				}
				else
				{
					npc.velocity.Y = 5f;
				}
				npc.velocity.X = 0;
				if (stunTimer == 0)
				{
					npc.Opacity = 1f;
					color.X = 1f;
					color.Y = 1f;
					color.Z = 1f;
					color.W = 1f;
					stunCounter = 0;
					stunned = false;
					stunTimer = 180;
				}
			}
			else
			{
				bulletHellTimer++;
				npc.TargetClosest(false);
			}

			// Phase 1 gate
			if (quotient > 0.6d && !stunned)
			{
				RefreshRain();
				if (bulletHellTimer == 1)
				{
					direction = Main.rand.Next(0, 2) == 0 ? -1 : 1;
				}
				if (bulletHellTimer == 576)
				{
					direction *= -1;
					Movement();
				}
				if (bulletHellTimer < 960)
				{
					Movement();
					if (sprayTimer != 0)
						sprayTimer--;
					if (sprayTimer == 0)
					{
						sprayDelay--;
						if (sprayDelay == 12)
						{
							int proj1 = Projectile.NewProjectile(npc.Center, new Vector2(8f, 0f).RotateTo(Main.player[npc.target].AngleFrom(npc.Center)).RotatedBy(Main.player[npc.target].position.X > npc.position.X ? 10f.InRadians() : -10f.InRadians()), ProjectileType<ZephyrPierce>(), 0, 1f);
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj1);
						}
						if (sprayDelay == 6)
						{
							int proj2 = Projectile.NewProjectile(npc.Center, new Vector2(8f, 0f).RotateTo(Main.player[npc.target].AngleFrom(npc.Center)), ProjectileType<ZephyrPierce>(), 0, 1f);
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj2);
						}
						if (sprayDelay == 0)
						{
							int proj3 = Projectile.NewProjectile(npc.Center, new Vector2(8f, 0f).RotateTo(Main.player[npc.target].AngleFrom(npc.Center)).RotatedBy(Main.player[npc.target].position.X > npc.position.X ? -10f.InRadians() : 10f.InRadians()), ProjectileType<ZephyrPierce>(), 0, 1f);
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj3);
							sprayDelay = 18;
							sprayTimer = 30;
						}
					}
				}
				if (bulletHellTimer == 192 || bulletHellTimer == 384 || bulletHellTimer == 576 || bulletHellTimer == 768 || bulletHellTimer == 960)
				{
					Movement();
					int proj4 = Projectile.NewProjectile(npc.Center, new Vector2(6f, 0f).RotateTo(Main.player[npc.target].AngleFrom(npc.Center)).RotatedBy(Main.player[npc.target].position.X > npc.position.X ? -10f.InRadians() : 10f.InRadians()), ProjectileType<ZephyrWhirlwind>(), 0, 1f);
					NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj4);
				}
				if (bulletHellTimer > 960 && bulletHellTimer < 1020)
				{
					preppingDash = true;
				}
				if (bulletHellTimer == 1020)
				{
					preppingDash = false;
					isDashing = true;
					npc.Opacity = 1f;
					color.X = 1f;
					color.Y = 1f;
					color.Z = 1f;
					color.W = 1f;
					npc.velocity = new Vector2(16f, 0f).RotatedBy(npc.AngleTo(Main.player[npc.target].position));
				}
				if (bulletHellTimer >= 1080 && bulletHellTimer < 1110)
				{
					npc.velocity.X = 0;
					npc.velocity.Y = 0;
					isDashing = false;
					spiritTimer--;
					Vector2 position = new Vector2(npc.Center.X + 300f, npc.Center.Y + 100f);
					if (spiritArray[0] != 0)
					{
						Main.npc[spiritArray[0]].life = 0;
						spiritArray[0] = 0;
						stunCounter = 0;
					}
					if (spiritTimer == 0)
					{
						spiritArray[0] = NPC.NewNPC((int)position.X, (int)position.Y, NPCType<ZephyrSpirit>());
						spiritTimer = 30;
					}
				}
				if (bulletHellTimer >= 1110 && bulletHellTimer < 1140)
				{
					if (spiritArray[1] != 0)
					{
						Main.npc[spiritArray[1]].life = 0;
						spiritArray[1] = 0;
						stunCounter = 0;
					}
					spiritTimer--;
					Vector2 position = new Vector2(npc.Center.X + 150f, npc.Center.Y - 150f);
					if (spiritTimer == 0)
					{
						spiritArray[1] = NPC.NewNPC((int)position.X, (int)position.Y, NPCType<ZephyrSpirit>());
						spiritTimer = 30;
					}
				}
				if (bulletHellTimer >= 1140 && bulletHellTimer < 1170)
				{
					if (spiritArray[2] != 0)
					{
						Main.npc[spiritArray[2]].life = 0;
						spiritArray[2] = 0;
						stunCounter = 0;
					}
					spiritTimer--;
					Vector2 position = new Vector2(npc.Center.X - 150f, npc.Center.Y - 150f);
					if (spiritTimer == 0)
					{
						spiritArray[2] = NPC.NewNPC((int)position.X, (int)position.Y, NPCType<ZephyrSpirit>());
						spiritTimer = 30;
					}
				}
				if (bulletHellTimer >= 1170 && bulletHellTimer < 1200)
				{
					if (spiritArray[3] != 0)
					{
						Main.npc[spiritArray[3]].life = 0;
						spiritArray[3] = 0;
						stunCounter = 0;
					}
					spiritTimer--;
					Vector2 position = new Vector2(npc.Center.X - 300f, npc.Center.Y + 100f);
					if (spiritTimer == 0)
					{
						spiritArray[3] = NPC.NewNPC((int)position.X, (int)position.Y, NPCType<ZephyrSpirit>());
						spiritTimer = 30;
					}
				}
				if (bulletHellTimer >= 1200 && bulletHellTimer < 1230)
				{
					if (spiritArray[4] != 0)
					{
						Main.npc[spiritArray[4]].life = 0;
						spiritArray[4] = 0;
						stunCounter = 0;
					}
					spiritTimer--;
					Vector2 position = new Vector2(npc.Center.X, npc.Center.Y + 300);
					if (spiritTimer == 0)
					{
						spiritArray[4] = NPC.NewNPC((int)position.X, (int)position.Y, NPCType<ZephyrSpirit>());
						spiritTimer = 30;
					}
				}
				if (bulletHellTimer > 1230)
				{
					bulletHellTimer = 0;
				}
			}
			if (quotient <= 0.6d && quotient > 0.3d)
			{
				Movement();
			}
			if(quotient <= 0.3d)
			{
				Movement();
			}

		}
		public void SprayAttack()
		{

		}
		public void RefreshRain()
		{
			Main.raining = true;
			Main.cloudBGActive = ;
			Main.numCloudsTemp = Main.cloudLimit;
			Main.numClouds = Main.numCloudsTemp;
			Main.windSpeedTemp = rainMult[0];
			Main.windSpeedSet = Main.windSpeedTemp;
			Main.weatherCounter = 36000;
			Main.rainTime = Main.weatherCounter;
			Main.maxRaining = rainMult[1];
		}
		public void Movement()
		{
			Player player = Main.player[npc.target];
			const float speedCap = 6f;
			npc.spriteDirection = npc.direction;

			Vector2 unitY = npc.DirectionTo(new Vector2(player.Center.X + (direction == -1 ? -400 : 400), player.Center.Y - 200f));
			npc.velocity = ((npc.velocity * 15f) + (unitY * speedCap)) / (15f + 1f);
		}
		public override void FindFrame(int frameheight)
		{
			Texture2D tex = GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental");
			if (npc.frameCounter + 0.125f >= 12f)
				npc.frameCounter = 0f;
			npc.frameCounter += 0.125f;
			npc.frame.Y = (int)npc.frameCounter * (tex.Height / 12);
		}
		public void DrawAfterImage(SpriteBatch spriteBatch)
		{
			if (!isDashing && !stunned && !preppingDash)
			{
				for (int i = 1; i <= 3; i++)
				{
					float alpha = 1f - (i * 0.25f);
					spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), oldPos[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), npc.Center - Main.screenPosition, npc.frame, new Color(color.X, color.Y, color.Z, color.W), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
			}
			if (preppingDash)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = (float)((float)(0.25d * Math.Sin(Main.GlobalTime * 20d)) + 0.75d) - (1f - (i * 0.1f));
					npc.Opacity = alpha;
					color.X = alpha;
					color.Y = alpha;
					color.Z = alpha;
					color.W = alpha;
					spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), oldPos[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
				}
			}
			if (isDashing)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = 1f - (i * 0.1f);
					spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), oldPos[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), npc.Center - Main.screenPosition, npc.frame, new Color(color.X, color.Y, color.Z, color.W), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);

			}
			if (stunned)
			{
				for (int i = 0; i < 10; i++)
				{
					float alpha = (1f - (i * 0.1f)) * (float)((float)(0.375d * Math.Sin(Main.GlobalTime * 20d)) + 0.625d);
					float scale = (2f - (i * 0.025f)) * (float)((float)(0.025d * Math.Sin(Main.GlobalTime * 20d)) + 1.025d);
					spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/AirElemental/AirElemental"), oldPos[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, scale, SpriteEffects.None, 0f);
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			DrawAfterImage(Main.spriteBatch);
			return false;
		}

		public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
		{
			ProvidenceWorld world = GetInstance<ProvidenceWorld>();
			if (!ProvidenceWorld.downedAirElemental)
			{
				ProvidenceWorld.downedAirElemental = true;
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
			preSpawnText = true;
			if (Main.expertMode)
			{
				int item1 = Item.NewItem(npc.Center, ItemID.GoldCoin, 7);
				world.itemList.Add(Main.item[item1]);
				int item2 = Item.NewItem(npc.Center, ItemID.SilverCoin, 50);
				world.itemList.Add(Main.item[item2]);
				int item3 = Item.NewItem(npc.Center, ItemType<AirElementalBag>(), 1);
				world.itemList.Add(Main.item[item3]);
			}
			else
			{
				int item4 = Item.NewItem(npc.Center, ItemID.GoldCoin, 5);
				world.itemList.Add(Main.item[item4]);
				int item5 = Item.NewItem(npc.Center, ItemType<ZephyrOre>(), Main.rand.Next(16, 51));
				world.itemList.Add(Main.item[item5]);
			}
		}
		public override Color? GetAlpha(Color drawColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}