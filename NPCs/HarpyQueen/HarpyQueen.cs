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
    public int shootTimer = 20;
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
      Main.npcFrameCount[npc.type] = 5;
      NPCID.Sets.MustAlwaysDraw[npc.type] = true;
    }

    public override void SetDefaults()
    {
      music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/HighInTheSky");
      musicPriority = MusicPriority.BossMedium; // By default, musicPriority is BossLow
      npc.damage = 75;
      npc.aiStyle = -1;
      npc.lavaImmune = true;
      npc.noGravity = true;
      npc.noTileCollide = true;
      npc.lifeMax = 3400;
      npc.townNPC = false;
      npc.boss = true;
      npc.scale = 0.5f;
      npc.HitSound = SoundID.NPCHit1;
      npc.chaseable = true;
      npc.Hitbox = new Rectangle((int)npc.Center.X, (int)npc.Center.Y - 100, 178, 224);
      npc.knockBackResist = 0f;
      npc.buffImmune[BuffID.OnFire] = true;
      npc.buffImmune[mod.BuffType("Freezing")] = true;
      npc.buffImmune[mod.BuffType("Frozen")] = true;
      npc.Unbidden().resists = new float[8] { 0.25f, 1f, 1f, 1.5f, 0.25f, 1.5f, 1f, 1f };
      npc.Unbidden().contactDamageEl = 0;
    }
    public override void AI()
    {
      if (!spawnText)
      {
        Talk("The Harpy Queen has awoken!");
        spawnText = true;
      }
      // int? phase = PhaseChecker();
      bulletHellTimer++;
      npc.ai[0]++;
      Movement();
      FindPlayers();
      npc.TargetClosest(false);
      Player player = Main.player[npc.target];
      if (bulletHellTimer < 600)
      {
        if (npc.life > npc.lifeMax / 2)
        {
          phase = 0;
          shootTimer--;
          if (shootTimer == 0)
          {
            const float speedX = 0f;
            const float speedY = 10f;
            Vector2 speed = new Vector2(speedX, speedY).RotateTo(player.AngleFrom(npc.Center));
            //Vector2 directionTo = DirectionTo(target.Center);
            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 50, 0f, Main.myPlayer, npc.whoAmI);
            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
            shootTimer = 30;
          }
        }
        else if (npc.life <= npc.lifeMax / 2)
        {
          flurryTimer--;
          if (flurryTimer == 0)
          {
            FlurryAttack();
            flurryTimer = 15;
          }
        }
      }
      else if (bulletHellTimer < 700f)
      {
        phase = 1;
        plusTimer--;
        if (plusTimer == 0)
        {
          PlusAttack();
          plusTimer = 25;
        }
      }
      else if (bulletHellTimer < 800f)
      {
        xTimer--;
        if (xTimer == 0)
        {
          XAttack();
          xTimer = 25;
        }
      }
      else if (bulletHellTimer < 900f)
      {
        plusTimer--;
        if (plusTimer == 0)
        {
          PlusAttack();
          plusTimer = 25;
        }
      }
      else if (bulletHellTimer < 1000f)
      {
        xTimer--;
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
        Vector2 speed = new Vector2(0f, -10f).RotatedBy(MathHelper.ToRadians(rotation));
        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 50, 0f, Main.myPlayer, npc.whoAmI);
      }
    }
    public void XAttack()
    {
      for (float rotation = 0f; rotation < 360f; rotation += 90f)
      {
        Vector2 speed = new Vector2(10f, -10f).RotatedBy(MathHelper.ToRadians(rotation));
        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 50, 0f, Main.myPlayer, npc.whoAmI);
      }
    }
    public void RadialAttack()
    {
      for (float rotation = 0f; rotation < 360f; rotation += 45f)
      {
        Vector2 speed = new Vector2(0f, -10f).RotateTo(MathHelper.ToRadians(rotation));
        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 50, 0f, Main.myPlayer, npc.whoAmI);
      }
    }
    public void FlurryAttack()
    {
      Player player = Main.player[npc.target];
      Vector2 speed = new Vector2(0f, -10f).RotateTo(npc.AngleTo(player.Center));
      speed = speed.RotatedByRandom(MathHelper.ToRadians(5f));
      //Vector2 directionTo = DirectionTo(target.Center);
      _ = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X, speed.Y, ProjectileID.HarpyFeather, 50, 0f, Main.myPlayer, npc.whoAmI);
    }
    public void Movement()
    {
      Player player = Main.player[npc.target];
      Vector2 offset = npc.position - player.position;
      const float speedCap = 6f;
      const float gainStrength = 0.1f;
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
      if (UnbiddenWorld.downedFireAncient)
      {
        UnbiddenWorld.downedFireAncient = true;
      }
    }

    private ModPacket GetPacket(FireAncientMessageType type)
    {
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
      return color;
    }
  }
}