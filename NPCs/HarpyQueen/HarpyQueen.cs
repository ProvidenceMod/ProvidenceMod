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
using UnbiddenMod.Projectiles.Boss;

namespace UnbiddenMod.NPCs.HarpyQueen
{
  public class HarpyQueen : ModNPC
  {
    private bool spawnText = false;
    public readonly IList<int> targets = new List<int>();
    public override bool Autoload(ref string name)
    {
      name = "HarpyQueen";
      return mod.Properties.Autoload;
    }
    public int preBHTimer = 300;
    public bool preSpawnText = false;
    public int shootTimer = 20;
    public int royalFeatherTimer = 150;
    public int plusTimer = 25;
    public int xTimer = 25;
    public int radialTimer = 45;
    public int flurryTimer = 10;
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
      npc.Hitbox = new Rectangle((int)npc.Center.X, (int)npc.Center.Y, 140, 72);
      npc.knockBackResist = 0f;
      npc.buffImmune[BuffID.OnFire] = true;
      npc.buffImmune[mod.BuffType("Freezing")] = true;
      npc.buffImmune[mod.BuffType("Frozen")] = true;
      npc.Unbidden().resists = new float[8] { 0.25f, 1f, 1f, 1.5f, 0.25f, 1.5f, 1f, 1f };
    }
    public override void AI()
    {
      if (!preSpawnText)
      {
        Talk("The Harpy Queen has spotted you...");
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
            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 25, 0f, Main.myPlayer, npc.whoAmI);
            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
            shootTimer = 30;
          }
        }
        else if (npc.life <= npc.lifeMax / 2)
        {
          flurryTimer--;
          royalFeatherTimer--;
          Movement();
          if (flurryTimer == 0)
          {
            FlurryAttack();
            flurryTimer = 15;
          }
          if (royalFeatherTimer == 0)
          {
            const float speedX = 0f;
            const float speedY = 4f;
            Vector2 speed = new Vector2(speedX, speedY).RotateTo(player.AngleFrom(npc.Center));
            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<RoyalFeather>(), 35, 0f, Main.myPlayer, npc.whoAmI);
            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
            royalFeatherTimer = 150;
          }
        }
      }
      else if (bulletHellTimer < 700f)
      {
        phase = 1;
        plusTimer--;
        Movement();
        if (plusTimer == 0)
        {
          PlusAttack();
          plusTimer = 25;
        }
      }
      else if (bulletHellTimer < 800f)
      {
        xTimer--;
        Movement();
        if (xTimer == 0)
        {
          XAttack();
          xTimer = 25;
        }
      }
      else if (bulletHellTimer < 900f)
      {
        plusTimer--;
        Movement();
        if (plusTimer == 0)
        {
          PlusAttack();
          plusTimer = 25;
        }
      }
      else if (bulletHellTimer < 1000f)
      {
        xTimer--;
        Movement();
        if (xTimer == 0)
        {
          XAttack();
          xTimer = 25;
        }
      }
      else if (bulletHellTimer < 1300f)
      {
        radialTimer--;
        if (radialTimer == 0)
        {
          RadialAttack();
          radialTimer = 45;
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
    public void PlusAttack()
    {
      // Creates a projectile, whose speed is based on the current rotation (in degrees for simplicity)
      // Same happens with XAttack and RadialAttack.
      for (float rotation = 0f; rotation < 360f; rotation += 90f)
      {
        Vector2 speed = new Vector2(0f, -10f).RotatedBy(rotation.InRadians());
        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 25, 0f, Main.myPlayer, npc.whoAmI);
        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
      }
    }
    public void XAttack()
    {
      for (float rotation = 0f; rotation < 360f; rotation += 90f)
      {
        Vector2 speed = new Vector2(10f, -10f).RotatedBy(rotation.InRadians());
        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 25, 0f, Main.myPlayer, npc.whoAmI);
        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
      }
    }
    public void RadialAttack()
    {
      npc.velocity.X = 0f;
      npc.velocity.Y = 0f;
      for (float rotation = 0f; rotation < 360f; rotation += 45f)
      {
        Vector2 speed = new Vector2(0f, -10f).RotateTo(rotation.InRadians());
        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 25, 0f, Main.myPlayer, npc.whoAmI);
        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
      }
    }
    public void FlurryAttack()
    {
      Player player = Main.player[npc.target];
      Vector2 speed = new Vector2(0f, -10f).RotateTo(npc.AngleTo(player.Center));
      speed = speed.RotatedByRandom(5f.InRadians());
      //Vector2 directionTo = DirectionTo(target.Center);
      int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 25, 0f, Main.myPlayer, npc.whoAmI);
      NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
    }
    public void Movement()
    {
      Player player = Main.player[npc.target];
      Vector2 offset = npc.position - player.position;
      const float speedCap = 6f;
      const float gainStrength = 0.1f;
      const float slowStrength = 1.1f;
      if(npc.velocity.X > 0)
      {npc.spriteDirection = 1;}
      else if (npc.velocity.X < 0)
      {npc.spriteDirection = -1;}
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
        if (npc.position.Y < player.position.Y - 200f)
        {
          if (npc.velocity.Y < 0)
            npc.velocity.Y /= slowStrength;
          if (npc.velocity.Y < speedCap)
            npc.velocity.Y += gainStrength;
        }
        if (npc.position.Y > player.position.Y - 200f)
        {
          if (npc.velocity.Y > 0)
            npc.velocity.Y /= slowStrength;
          if (npc.velocity.Y > -speedCap)
            npc.velocity.Y -= gainStrength;
        }
        if (npc.position.Y == player.position.Y - 200f)
          npc.velocity.Y = 0f;
      }
    }
    /*    public override void FindFrame(int frameHeight)
    {
      Texture2D tex = mod.GetTexture("NPCs/FireAncient/FireAncient");
      NPC npc = this.npc;
      if (npc.frameCounter + 0.5f > 5f)
      {
        npc.frameCounter = 0f;
      }
      npc.frameCounter += 0.125f;
      npc.frame.Y = (int)npc.frameCounter * (tex.Height / 5);
    }*/

    public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
    {
      if (UnbiddenWorld.downedFireAncient)
      {
        UnbiddenWorld.downedFireAncient = true;
      }
    }

    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.White;
      return color;
    }
  }
}