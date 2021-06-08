using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.NPCs.FireAncient
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
      npc.Hitbox = new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 100, 178, 224);
      npc.knockBackResist = 0f;
      npc.buffImmune[BuffID.OnFire] = true;
    }

    public override void AI() //this is where you program your AI
    {
      npc.ai[0]++;
      if (!spawnText)
      {
        Talk("A Fiery Ancient has awoken!");
        spawnText = true;
      }
      FindPlayers();
      npc.TargetClosest(false);
      Player player = Main.player[npc.target];
      // ProvidenceGlobalNPC ProvidenceNPC = npc.Providence();
      Vector2 offset = npc.position - player.position;
      const float speedCap = 8f;
      const float gainStrength = 0.2f;
      const float slowStrength = 1.1f;
      if (player.active && !player.dead)
      {
        if (npc.Center.X < player.Center.X)
        {
          if (npc.velocity.X < 0)
            npc.velocity.X /= slowStrength;
          if (npc.velocity.X < speedCap)
            npc.velocity.X += gainStrength;
        }
        if (npc.Center.X > player.Center.X)
        {
          if (npc.velocity.X > 0)
            npc.velocity.X /= slowStrength;
          if (npc.velocity.X > -speedCap)
            npc.velocity.X -= gainStrength;
        }
        if (offset.X == 0)
          npc.velocity.X = 0f;
        /////
        if (npc.position.Y < player.position.Y - 500f)
        {
          if (npc.velocity.Y < 0)
            npc.velocity.Y /= slowStrength;
          if (npc.velocity.Y < speedCap)
            npc.velocity.Y += gainStrength;
        }
        if (npc.position.Y > player.position.Y - 500f)
        {
          if (npc.velocity.Y > 0)
            npc.velocity.Y /= slowStrength;
          if (npc.velocity.Y > -speedCap)
            npc.velocity.Y -= gainStrength;
        }
        if (npc.position.Y == player.position.Y - 500f)
          npc.velocity.Y = 0f;
      }
      if (timer == 0)
      {
        AbyssalHellblast();
        timer = 50;
      }
      timer--;
    }

    private void AbyssalHellblast()
    {
      /*radialAttack += 1;
      if(radialAttack == 21)
          {radialAttack = 1;}*/
      int type = mod.ProjectileType("AbyssalHellblast");
      const float speedX = 0f;
      const float speedY = 10f;
      Vector2 speed = new Vector2(speedX, speedY);
      //Vector2 directionTo = DirectionTo(target.Center);
      int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, type, 50, 0f, Main.myPlayer, npc.whoAmI);
      NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
    }

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
      if (npc.frameCounter + 0.5f > 5f)
      {
        npc.frameCounter = 0f;
      }
      npc.frameCounter += 0.125f;
      npc.frame.Y = (int)npc.frameCounter * (tex.Height / 5);
    }

    /*public override void HitEffect(int hitDirection, double damage, bool isDead) //This is for whenever your boss gets hit by an attack. Create dust or gore.
    {

    }

    public override void SelectFrame(int frameSize) //This is for animating your boss, such as how Queen Bee changes between charging or hovering images, or how Pumpking rotates its face.
    {

    }*/
    public void FindPlayers()
    {
    }

    public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
    {
      if (!ProvidenceWorld.downedFireAncient)
      {
        ProvidenceWorld.downedFireAncient = true;
      }
    }
    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.White;
      return color;
    }
  }
}