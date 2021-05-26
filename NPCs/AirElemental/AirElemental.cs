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

namespace ProvidenceMod.NPCs.AirElemental
{
	public class AirELemental : ModNPC
	{
		public int frame;
		public int frameTick;
		private bool spawnText = false;
		public readonly IList<int> targets = new List<int>();
		public override bool Autoload(ref string name)
		{
			name = "AirElemental";
			return mod.Properties.Autoload;
		}
		public int preBHTimer = 300;
		public bool preSpawnText = false;
		public int shootTimer = 20;
		public int bulletHellTimer = 0;
		public int phase = 0;
		/*private int? PhaseChecker()
    {
      float pLifeLeft = npc.life / npc.lifeMax * 100;
      if (pLifeLeft >= 50)
        return 0;
      else if (pLifeLeft < 50)
        return 1;
      else return null;
    }*/

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harpy Queen");
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
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
			if (!preSpawnText)
			{
				Talk("The winds begin to stir...");
				Main.raining = true;
				Main.cloudBGActive = 0.5f;
				Main.numCloudsTemp = Main.cloudLimit;
				Main.numClouds = Main.numCloudsTemp;
				Main.windSpeedTemp = 0.75f;
				Main.windSpeedSet = Main.windSpeedTemp;
				Main.weatherCounter = 36000;
				Main.rainTime = Main.weatherCounter;
				Main.maxRaining = 1f;
				preSpawnText = true;
			}
			if (--preBHTimer > 0 && npc.life == npc.lifeMax)
			{
				return;
			}
			if (!spawnText)
			{
				Talk("The Harpy Queen has awoken!");
				spawnText = true;
			}
			// int? phase = PhaseChecker();
			bulletHellTimer++;
			npc.ai[0]++;
			npc.TargetClosest(false);
			Player player = Main.player[npc.target];
			if (bulletHellTimer < 600)
			{
				if (npc.life > npc.lifeMax / 2)
				{
					phase = 0;
					shootTimer--;
					Movement();
					if (shootTimer == 0)
					{
						const float speedX = 0f;
						const float speedY = 10f;
						Vector2 speed = new Vector2(speedX, speedY).RotateTo(player.AngleFrom(npc.Center));
						//Vector2 directionTo = DirectionTo(target.Center);
						SprayAttack();
						shootTimer = 30;
					}
				}
			}
			else
			{
				bulletHellTimer = 0;
			}
		}

		private void Talk(string message)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				string text = Language.GetTextValue(message, Lang.GetNPCNameValue(npc.type), message);
				Main.NewText(text, 4, 127, 82);
			}
			else
			{
				NetworkText text = NetworkText.FromKey(message, Lang.GetNPCNameValue(npc.type), message);
				NetMessage.BroadcastChatMessage(text, new Color(241, 127, 82));
			}
		}

		public void SprayAttack()
		{
			Vector2 speed = new Vector2(8f, 0f).RotateTo(Main.player[npc.target].AngleFrom(npc.Center));
			int proj1 = Projectile.NewProjectile(npc.Center, speed.RotatedBy(-5f.InRadians()), ProjectileType<ZephyrPierce>(), 0, 1f);
			NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj1);
			int proj2 = Projectile.NewProjectile(npc.Center, speed, ProjectileType<ZephyrPierce>(), 0, 1f);
			NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj2);
			int proj3 = Projectile.NewProjectile(npc.Center, speed.RotatedBy(5f.InRadians()), ProjectileType<ZephyrPierce>(), 0, 1f);
			NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj3);
		}
		public void Movement()
		{
			Player player = Main.player[npc.target];
			const float speedCap = 6f;
			npc.spriteDirection = npc.direction;
			Vector2 unitY = npc.DirectionTo(new Vector2(player.Center.X, player.Center.Y - 200f));
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

		public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
		{
			if (!ProvidenceWorld.downedAirElemental)
			{
				ProvidenceWorld.downedAirElemental = true;
			}
			Main.raining = false;
			if (Main.expertMode)
			{
				int item1 = Item.NewItem(npc.Center, ItemID.GoldCoin, 7);
				int item2 = Item.NewItem(npc.Center, ItemID.SilverCoin, 50);
				int item3 = Item.NewItem(npc.Center, ItemType<AirElementalBag>(), 1);
			}
			else
			{
				int item4 = Item.NewItem(npc.Center, ItemID.GoldCoin, 5);
				int item5 = Item.NewItem(npc.Center, ItemType<ZephyrOre>(), Main.rand.Next(16, 51));
				int item6 = Item.NewItem(npc.Center, ItemType<HarpyQueenTalon>(), Main.rand.Next(1, 6));
				int item7 = Item.NewItem(npc.Center, ItemType<HarpyQueenFeather>(), Main.rand.Next(2, 6));
			}
		}
	}
}