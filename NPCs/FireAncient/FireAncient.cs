using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles;

namespace UnbiddenMod.NPCs.FireAncient
{
    public class FireAncient : ModNPC
    {
        private bool spawnText = false;
        public readonly IList<int> targets = new List<int>();
        public override bool Autoload(ref string name)
        {
            name = "FireAncient";
            return mod.Properties.Autoload;
        }

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
            npc.buffImmune[BuffID.OnFire] = true;
            npc.GetGlobalNPC<UnbiddenNPC>().resists = new int[8] {0, 75, 100, 250, 100, 100, 100, 100};
            npc.GetGlobalNPC<UnbiddenNPC>().contactDamageEl = 0;
        }

        private int stage {
			get => (int)npc.ai[0];
			set => npc.ai[0] = value;
		}

        public override void AI() //this is where you program your AI
        {
            int timer = 180;
            npc.ai[0] += 1;
            if(spawnText == false)
            {
                Talk("A Fiery Ancient has awoken!");
                spawnText = true;
            }
            FindPlayers();
            if (timer == 0)
            {
                AbyssalHellblast();
                timer = Main.rand.Next(180, 240);
            }
            timer--;
        }

        private void AbyssalHellblast() 
        {
            int numAttacks = 20;
            int type = mod.ProjectileType("AbyssalHellblast");
            Player player = Main.player[0];
            for (int k = 0 ; k < numAttacks ; k++)
            {
                /*Vector2 offset = player.Center - npc.Center;
                float speedX = (float) offset.X;
                float speedY = (float) offset.Y;
                if(speedX > 5f) {speedX = 5f;}
                if(speedY > 5f) {speedY = 5f;}*/
                int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 1f, 1f, type, 50, 0f, Main.myPlayer, npc.whoAmI);
				NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
            }
		}

        private void Talk(string message) 
        {
			if (Main.netMode != NetmodeID.Server) {
				string text = Language.GetTextValue(message, Lang.GetNPCNameValue(npc.type), message);
				Main.NewText(text, 241, 127, 82);
			}
			else {
				NetworkText text = NetworkText.FromKey(message, Lang.GetNPCNameValue(npc.type), message);
				NetMessage.BroadcastChatMessage(text, new Color(241, 127, 82));
			}
		}

        /*public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = mod.GetTexture("NPCs/FireAncient/FireAncient");
            Vector2 drawPos = npc.position - Main.screenPosition;
            Vector2 drawCenter = new Vector2 (221f, 29f);
            int animationFrame = (int)npc.frameCounter;
            Rectangle frame = new Rectangle(0, 0, tex.Width, animationFrame * (tex.Height / Main.npcFrameCount[npc.type]));
            spriteBatch.Draw(tex, drawPos, frame, Color.White, 0f, drawCenter, 1f, SpriteEffects.None, 0f);   
        }*/

        public override void FindFrame(int frameHeight)
        {   
            Texture2D tex = mod.GetTexture("NPCs/FireAncient/FireAncient");
            NPC npc = this.npc;
            if(npc.frameCounter + 0.5f > 5f)
            {
                npc.frameCounter = 0f;
            }
            npc.frameCounter += 0.125f;
            npc.frame.Y = (int) npc.frameCounter * (tex.Height / 5);
        }

        /*public override void HitEffect(int hitDirection, double damage, bool isDead) //This is for whenever your boss gets hit by an attack. Create dust or gore.
        {

        }

        public override void SelectFrame(int frameSize) //This is for animating your boss, such as how Queen Bee changes between charging or hovering images, or how Pumpking rotates its face.
        {

        }*/
        public void FindPlayers() 
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				int originalCount = targets.Count;
				targets.Clear();
				for (int k = 0; k < 255; k++) 
				{
					if (Main.player[k].active) 
					{
						targets.Add(k);
					}
				}
				if (Main.netMode == NetmodeID.Server && targets.Count != originalCount) 
				{
					ModPacket netMessage = GetPacket(FireAncientMessageType.TargetList);
					netMessage.Write(targets.Count);
					foreach (int target in targets) 
					{
						netMessage.Write(target);
					}
					netMessage.Send();
				}
			}
		}

        public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
        {
            if(UnbiddenWorld.downedFireAncient)
            {
            UnbiddenWorld.downedFireAncient = true;
            }
        }

        private ModPacket GetPacket(FireAncientMessageType type) {
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)UnbiddenModMessageType.FireAncient);
			packet.Write(npc.whoAmI);
			packet.Write((byte)type);
			return packet;
		}

        internal enum FireAncientMessageType : byte
	    {
		    HeroPlayer,
		    TargetList,
		    DontTakeDamage,
		    PlaySound,
		    Damage
	    }

        public void HandlePacket(BinaryReader reader) 
		{
			FireAncientMessageType type = (FireAncientMessageType)reader.ReadByte();
			if (type == FireAncientMessageType.TargetList) 
			{
				int numTargets = reader.ReadInt32();
				targets.Clear();
				for (int k = 0; k < numTargets; k++) 
				{
					targets.Add(reader.ReadInt32());
				}
			}
		}

        public override Color? GetAlpha(Color lightColor)
        {
        Color color = Color.White;
        return(color);
        }
    }
}