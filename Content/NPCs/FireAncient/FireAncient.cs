using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.Systems;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.NPCs.FireAncient
{
	public class FireAncient : ModNPC
	{
		private bool spawnText = false;
		public int frame;
		public int frameTick;
		Player player;
		public int timer = 10;
		public int radialAttack = -1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Ancient");
			Main.npcFrameCount[NPC.type] = 5;
			NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
		}

		public override void SetDefaults()
		{
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/FromTheDepths");
			NPC.damage = 75;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 100000;
			NPC.townNPC = false;
			NPC.boss = true;
			NPC.HitSound = SoundID.NPCHit41;
			NPC.chaseable = true;
			NPC.width = 760;
			NPC.height = 484;
			NPC.knockBackResist = 0f;
			//npc.buffImmune[BuffID.OnFire] = true;
		}

		public override void AI()
		{
			player = Main.player[NPC.target];
			NPC.ai[0]++;
			Vector2 pos = NPC.getRect().RandomPointInHitbox();
			if (!spawnText)
			{
				Talk("A Fiery Ancient has awoken!");
				spawnText = true;
			}
			FindPlayers();
			NPC.TargetClosest(false);
			Movement();
		}
		public void Movement()
		{
			NPC.spriteDirection = NPC.direction;
			double wiggle = (Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f);
			NPC.velocity.Y += (float)wiggle;
			Vector2 unitY = NPC.DirectionTo(new Vector2(player.Center.X, player.Center.Y));
			NPC.velocity = ((NPC.velocity * 15f) + (unitY * 8f)) / (15f + 1f);
		}
		private void Talk(string message)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				string text = Language.GetTextValue(message, Lang.GetNPCNameValue(NPC.type), message);
				Main.NewText(text, 241, 127, 82);
			}
			else
			{
				NetworkText text = NetworkText.FromKey(message, Lang.GetNPCNameValue(NPC.type), message);
				ChatHelper.BroadcastChatMessage(text, new Color(241, 127, 82));
			}
		}
		public override void FindFrame(int frameHeight)
		{
			Texture2D tex = Request<Texture2D>("Providence/Content/NPCs/FireAncient/FireAncient").Value;
			NPC npc = NPC;
			if (NPC.frameCounter + 0.5f > 5f)
			{
				NPC.frameCounter = 0f;
			}
			NPC.frameCounter += 0.125f;
			NPC.frame.Y = (int)NPC.frameCounter * (tex.Height / 5);
		}
		//public override bool PreDraw(SpriteBatch sb, Color color) => false;
		public void FindPlayers()
		{
		}

		public override void OnKill()
		{
			if (!WorldFlags.downedFireAncient)
				WorldFlags.downedFireAncient = true;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
