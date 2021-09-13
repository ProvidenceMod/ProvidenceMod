using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using System;

namespace ProvidenceMod.NPCs.FireAncient
{
	public class FireAncient : ModNPC
	{
		private bool spawnText = false;
		public int frame;
		public int frameTick;
		Player player;
		public override bool Autoload(ref string name)
		{
			name = "FireAncient";
			return mod.Properties.Autoload;
		}
		public int timer = 10;
		public int radialAttack = -1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Ancient");
			Main.npcFrameCount[npc.type] = 5;
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
		}

		public override void SetDefaults()
		{
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FromTheDepths");
			musicPriority = MusicPriority.BossMedium; // By default, musicPriority is BossLow
			npc.damage = 75;
			npc.aiStyle = -1;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 100000;
			npc.townNPC = false;
			npc.boss = true;
			npc.HitSound = SoundID.NPCHit41;
			npc.chaseable = true;
			npc.width = 760;
			npc.height = 484;
			npc.knockBackResist = 0f;
			//npc.buffImmune[BuffID.OnFire] = true;
		}

		public override void AI() //this is where you program your AI
		{
			player = Main.player[npc.target];
			npc.ai[0]++;
			Vector2 pos = npc.getRect().RandomPointInHitbox();
			if (!spawnText)
			{
				Talk("A Fiery Ancient has awoken!");
				spawnText = true;
			}
			FindPlayers();
			npc.TargetClosest(false);
			Movement();
		}
		public void Movement()
		{
			npc.spriteDirection = npc.direction;
			double wiggle = (Math.Sin(Main.GlobalTime * 3f) * 0.1f);
			npc.velocity.Y += (float)wiggle;
			Vector2 unitY = npc.DirectionTo(new Vector2(player.Center.X, player.Center.Y));
			npc.velocity = ((npc.velocity * 15f) + (unitY * 8f)) / (15f + 1f);
		}
		//private void AbyssalHellblast()
		//{
		//	int type = mod.ProjectileType("AbyssalHellblast");
		//	const float speedX = 0f;
		//	const float speedY = 10f;
		//	Vector2 speed = new Vector2(speedX, speedY);
		//	int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, type, 50, 0f, Main.myPlayer, npc.whoAmI);
		//	NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
		//}
		private void Talk(string message)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				string text = Language.GetTextValue(message, Lang.GetNPCNameValue(npc.type), message);
				Main.NewText(text, 241, 127, 82);
			}
			else
			{
				NetworkText text = NetworkText.FromKey(message, Lang.GetNPCNameValue(npc.type), message);
				NetMessage.BroadcastChatMessage(text, new Color(241, 127, 82));
			}
		}
		public override void FindFrame(int frameHeight)
		{
			Texture2D tex = mod.GetTexture("NPCs/FireAncient/FireAncient");
			NPC npc = this.npc;
			if (npc.frameCounter + 0.5f > 5f)
			{
				npc.frameCounter = 0f;
			}
			npc.frameCounter += 0.125f;
			npc.frame.Y = (int)npc.frameCounter * (tex.Height / 5);
		}
		public override bool PreDraw(SpriteBatch sb, Color color) => false;
		public void DrawGalaxyMappedSprite(SpriteBatch sB)
		{
			if (npc.type == NPCType<FireAncient>() && npc.active)
			{
				Texture2D tex = GetTexture("ProvidenceMod/NPCs/FireAncient/FireAncient");
				sB.Draw(tex, (npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY)) / 2, npc.AnimationFrame(ref frame, ref frameTick, 6, 5, true), Color.White, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), npc.scale / 2f, SpriteEffects.None, 0);
			}
		}
		public void FindPlayers()
		{
		}

		public override void NPCLoot()
		{
			if (!ProvidenceWorld.downedFireAncient || !BrinewastesWorld.downedFireAncient)
			{
				ProvidenceWorld.downedFireAncient = true;
				BrinewastesWorld.downedFireAncient = true;
			}
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}