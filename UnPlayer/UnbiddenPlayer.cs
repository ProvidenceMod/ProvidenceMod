using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UnbiddenMod.Dusts;
using static UnbiddenMod.UnbiddenUtils;
using UnbiddenMod.Buffs.StatDebuffs;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Buffs.Cooldowns;
using UnbiddenMod.Projectiles.Ability;
using UnbiddenMod.Buffs.StatBuffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace UnbiddenMod
{
  public class UnbiddenPlayer : ModPlayer
  {
    // Elemental variables for Player
    // Testing commit
    public string[] elements = new string[8] { "fire", "ice", "lightning", "water", "earth", "air", "radiant", "necrotic" };
    public int[] resists = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 },
                 affinities = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    //  affExp = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    // public int affExpCooldown = 0;

    // Elemental variables also contained within GlobalItem, GlobalNPC, and GlobalProjectile
    public Projectile parryProj;

    public bool allowFocus;
    public bool ampCapacitor;
    public bool angelTear;
    public bool bastionsAegis;
    public bool brimHeart;
    public bool boosterShot;
    public bool burnAura;
    public bool deflectable;
    public bool cFlameAura;
    public bool hasClericSet;
    public bool micitBangle;
    public bool intimidated;
    public bool parryActive;
    public bool parryActiveCooldown;
    public bool parryCapable;
    public bool parryWasActive;
    public bool regenAura;
    public bool spawnReset = true;
    public bool tankParryOn;
    public bool zephyriumAglet;

    public const float defaultFocusGain = 0.005f;
    public float bonusFocusGain;
    public float cleric = 1f;
    public float clericAuraRadius = 300f;
    public float defaultFocusLoss = 0.25f;
    public float focus;
    public float focusLoss = 0.15f;
    public float focusMax = 1f;
    public float tankingItemCount;

    // This should NEVER be changed.
    public const int maxParryActiveTime = 90;
    public int dashMod;
    public int dashTimeMod;
    public int dashModDelay = 60;
    public int focusLossCooldown;
    public int focusLossCooldownMax = 20;
    public int parriedProjs;
    public int parryActiveTime;
    public int parryProjID;
    public int parryType;
    public int tankParryPWR;
    public int tearCount;

    // TODO: Make this have use (see tooltip in the item of same name)
    public string dashDir = "";

    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", angelTear},
        {"tearCount", tearCount},
        // {"affExp", this.affExp}
      };
    }

    public override void ResetEffects()
    {
      brimHeart = false;
      boosterShot = false;
      cleric = 1f;
      player.statLifeMax2 += tearCount * 20;
      hasClericSet = false;
      clericAuraRadius = 300f;
      regenAura = false;
      burnAura = false;
      cFlameAura = false;
      ampCapacitor = false;
      resists = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
      dashMod = 0;
      dashTimeMod = 0;
      affinities = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
      zephyriumAglet = false;
      parryCapable = false;
      parryActive = parryActiveTime > 0;
      parryActiveCooldown = parryActiveTime > 0 && parryActiveTime <= maxParryActiveTime;
      parryType = ParryTypeID.Support;
      tankParryPWR = player.HasBuff(BuffType<TankParryBoost>()) || parryActive ? tankParryPWR : 0;
      tankParryOn = false;
      tankParryPWR = tankParryOn ? tankParryPWR : 0;
      intimidated = false;
      focusMax = 1f;
      allowFocus = IsThereABoss().Item1;
      bonusFocusGain = 0f;
      player.moveSpeed += focus / 2;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
      if (UnbiddenMod.ParryHotkey.JustPressed && !player.HasBuff(BuffType<CantDeflect>()) && !parryActiveCooldown)
      {
        parryActiveTime = maxParryActiveTime;
        int p = Projectile.NewProjectile(player.position, new Vector2(0, 0), ProjectileType<ParryShield>(), 0, 0, player.whoAmI);
        parryProj = Main.projectile[p];
        parryProjID = p;
      }
    }
    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
    }

    public void ActivateParry()
    {
      switch (parryType)
      {
        case ParryTypeID.Universal:
          StandardParry(player, parryProj.Hitbox, ref parryProjID);
          break;
        case ParryTypeID.Tank:
          tankParryPWR += TankParry(player, parryProj.Hitbox, ref parryProjID);
          break;
        case ParryTypeID.DPS:
          DPSParry(player, parryProj.Hitbox, ref parryProjID);
          break;
        case ParryTypeID.Support:
          SupportParry(player, parryProj.Hitbox, ref parryProjID);
          break;
      }
    }
    public override void PreUpdate()
    {
      if (IsThereABoss().Item1)
        allowFocus = true;
      player.AddBuff(BuffType<Intimidated>(), 2);
      intimidated = true;

      if (!IsThereABoss().Item1)
        player.ClearBuff(BuffType<Intimidated>());
      intimidated = false;

      if (focusLossCooldown > 0)
        focusLossCooldown--;
      if (!allowFocus)
        focus = 0;

      if (parryCapable && parryActive)
      {
        if (parryActiveTime > 0)
          parryActiveTime--;
        // Setting an "oldX" bool for PostUpdate
        parryWasActive = parryActive;
        ActivateParry();

        if (parryType == ParryTypeID.Tank && tankParryPWR != 0 && !player.HasBuff(BuffType<TankParryBoost>()))
        {
          // 5 secs of time, -1 second for each hit tanked with this active
          player.AddBuff(BuffType<TankParryBoost>(), 300);
        }
      }
      focus = Utils.Clamp(focus, 0, focusMax);
      base.PreUpdate();
    }
    public override void PostUpdate()
    {
      tankingItemCount = (int)Math.Floor((decimal)(player.statDefense / 15));
      // Max 15%, min 5%
      focusLoss = 0.25f - (tankingItemCount / 100) < 0.05f ? 0.05f : 0.25f - (tankingItemCount / 100);

      // Safeguard against weird ass number overflowing
      player.CalcElemDefense();

      if (parryCapable)
      {
        // This shouldn't just be when unequal, it specifically needs to be when it's not active anymore, but was within this tick
        if (parryWasActive && !parryActive)
        {
          player.AddBuff(BuffType<CantDeflect>(), 180 + (parriedProjs * 60), true);
          parryWasActive = false;
          parriedProjs = 0;
          parryProj = null;
          parryProjID = 255;
        }
      }

      if (hasClericSet)
      {
        if (burnAura)
        {
          const float burnRadiusBoost = -100f;
          GenerateAuraField(player, DustType<AuraDust>(), burnRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && !npc.friendly && npc.position.IsInRadiusOf(player.MountedCenter, clericAuraRadius + burnRadiusBoost))
            {
              npc.AddBuff(BuffID.OnFire, 1);
            }
          }
        }
        if (cFlameAura)
        {
          const float cFRadiusBoost = -150f;
          GenerateAuraField(player, DustType<AuraDust>(), cFRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && !npc.friendly && npc.position.IsInRadiusOf(player.MountedCenter, clericAuraRadius + cFRadiusBoost))
            {
              npc.AddBuff(BuffID.CursedInferno, 180);
            }
          }
        }
      }
      if (ampCapacitor)
      {
        const float ampRadiusBoost = 0;
        GenerateAuraField(player, DustType<ParryShieldDust>(), ampRadiusBoost);
        foreach (Projectile projectile in Main.projectile)
        {
          if (projectile.position.IsInRadiusOf(player.MountedCenter, clericAuraRadius + ampRadiusBoost) && !projectile.Unbidden().amped)
          {
            if (projectile.friendly)
            {
              projectile.damage = (int)(projectile.damage * 1.15);
              projectile.velocity *= 1.15f;
              projectile.Unbidden().amped = true;
            }
            else if (projectile.hostile)
            {
              projectile.damage = (int)(projectile.damage * 0.85);
              projectile.velocity *= 0.85f;
              projectile.Unbidden().amped = true;
            }
          }
        }
      }
    }

    public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
    {
      items.Add(createItem(mod.ItemType("StarterBag")));

      Item createItem(int type)
      {
        Item obj = new Item();
        obj.SetDefaults(type, false);
        return obj;
      }
    }
    public override void PostUpdateRunSpeeds()
    {
      if (dashMod > 0 && dashModDelay <= 0)
      {
        ModDashMovement();
      }
      else
      {
        player.releaseLeft = false;
        dashModDelay--;
      }
    }

    //TODO: #4 Fix dash movement: Dashing left is unreliable and sometimes nonfunctional with dash accessories and armor abilities. Double press time is buggy for right dashes
    public void ModDashMovement()
    {
      if (dashMod == 1)
      {
        const float dashStrength = 12f;
        const int delayTime = 60;
        bool dashing = false;
        if (!dashing && player.dashTime == 0)
          dashDir = "";
        if (player.dashTime > 0)
          player.dashTime--;
        if (player.controlRight && player.releaseRight)
        {
          if (player.dashTime > 0 && dashDir == "right")
          {
            player.dashTime = 0;
            dashModDelay = delayTime;
            dashing = true;
          }
          else
          {
            dashDir = "right";
            player.dashTime = 15;
          }
        }
        else if (player.controlLeft && player.releaseLeft)
        {
          if (player.dashTime > 0 && dashDir == "left")
          {
            player.dashTime = 0;
            dashModDelay = delayTime;
            dashing = true;
          }
          else
          {
            dashDir = "left";
            player.dashTime = 15;
          }
        }
        else if (player.controlUp && player.releaseUp)
        {
          if (player.dashTime > 0 && dashDir == "up")
          {
            player.dashTime = 0;
            dashModDelay = delayTime;
            dashing = true;
          }
          else
          {
            dashDir = "up";
            player.dashTime = 15;
          }
        }
        else if (player.controlDown && player.releaseDown)
        {
          if (player.dashTime > 0 && dashDir == "down")
          {
            player.dashTime = 0;
            dashModDelay = delayTime;
            dashing = true;
          }
          else
          {
            dashDir = "down";
            player.dashTime = 15;
          }
        }
        if (dashing)
        {
          switch (dashDir)
          {
            case "left":
              player.velocity.X = -dashStrength;
              break;
            case "right":
              player.velocity.X = dashStrength;
              break;
            case "up":
              player.velocity.Y = -dashStrength;
              break;
            case "down":
              player.velocity.Y = dashStrength;
              break;
          }
        }
        else
        {
          return;
        }
        int dashDirInt = dashDir == "left" || dashDir == "up" ? -1 : dashDir == "right" || dashDir == "down" ? 1 : 0;
        Point tileCoordinates1 = (player.Center + new Vector2((float)((dashDirInt * player.width / 2) + 2), (float)(((double)player.gravDir * (double)-player.height / 2.0) + ((double)player.gravDir * 2.0)))).ToTileCoordinates();
        Point tileCoordinates2 = (player.Center + new Vector2((float)((dashDirInt * player.width / 2) + 2), 0.0f)).ToTileCoordinates();
        if (WorldGen.SolidOrSlopedTile(tileCoordinates1.X, tileCoordinates1.Y) || WorldGen.SolidOrSlopedTile(tileCoordinates2.X, tileCoordinates2.Y))
          player.velocity.X /= 2f;
        player.dashDelay = 60;
      }
    }
    public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
    {
      if (brimHeart)
      {
        player.buffImmune[BuffID.OnFire] = true;
        if (item.GetGlobalItem<UnbiddenGlobalItem>().element == 0) // If the weapon is fire-based
        {
          // Reduce cost by 15%
          reduce -= 0.15f;
        }
      }
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
      if (target.boss && target.active)
      {
        // Should add to the focus bar
        focus += defaultFocusGain + bonusFocusGain;
        if (focus > focusMax) focus = focusMax;
      }
      // Determined at the end of everything so any focus gained within a tick is retroactive
      damage += (int)(damage * (focus / 5));
    }
    public override void NaturalLifeRegen(ref float regen)
    {
      if (regenAura)
      {
        foreach (Player targetPlayer in Main.player)
        {
          if (targetPlayer.MountedCenter.IsInRadiusOf(player.MountedCenter, clericAuraRadius))
          {
            targetPlayer.lifeRegen += (int)(cleric * 2);
          }
        }
      }

      // Focus regain
      regen += regen * focus;
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      // Item item = player.inventory[player.selectedItem];
      if (target.boss && target.active)
      {
        focus += defaultFocusGain + bonusFocusGain;
        if (focus > focusMax) focus = focusMax;
      }
      damage += (int)(damage * (focus / 5));
    }

    // If the Player is hit by an NPC's contact damage...
    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
      damage = player.CalcEleDamageFromNPC(npc, ref damage);

      if (allowFocus)
      {
        float focusDR = focus / 4 >= 0.25f ? 0.25f : focus / 4;
        damage -= (int)(damage * focusDR);

        if (focusLossCooldown == 0)
        {
          focus -= focusLoss;
          if (focus < 0f) focus = 0f;
          focusLossCooldown = focusLossCooldownMax;
        }
      }

      damage -= damage * (tankParryPWR / 100);
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
      damage = player.CalcEleDamageFromProj(proj, ref damage);

      if (allowFocus)
      {
        float focusDR = focus / 4 >= 0.25f ? 0.25f : focus / 4;
        damage -= (int)(damage * focusDR);

        if (focusLossCooldown == 0)
        {
          focus -= focusLoss;
          if (focus < 0f) focus = 0f;
          focusLossCooldown = focusLossCooldownMax;
        }
      }

      damage -= damage * (tankParryPWR / 100);
    }

    public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
    {
      if (boosterShot && item.potion)
      {
        healValue *= 2; // Doubles potion power
        player.ClearBuff(BuffType<BoosterShot>()); // Immediately deletes it from buff bar
      }
    }

    public override void ModifyDrawLayers(List<PlayerLayer> layers)
    {
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Unbidden().glowmask)
      {
      void layerTarget(PlayerDrawInfo s) => DrawSwordGlowmask(s);
      PlayerLayer layer = new PlayerLayer("UnbiddenMod", "Sword Glowmask", layerTarget);
      layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer);
      }
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Unbidden().glowmask && player.HeldItem.Unbidden().animated)
      {
      //Animated Sword Code
      void layerTarget2(PlayerDrawInfo s) => DrawGlowmaskAnimation(s);
      PlayerLayer layer2 = new PlayerLayer("UnbiddenMod", "Sword Animation", layerTarget2);
      layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer2);
      } 
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
      for (int combatIndex2 = 99; combatIndex2 >= 0; --combatIndex2)
      {
        CombatText combatText = Main.combatText[combatIndex2];
        if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
        {
          if (combatText.color == CombatText.DamagedHostile || combatText.color == CombatText.DamagedHostileCrit)
          {
            if (player.HeldItem.Unbidden().element == 0)
              Main.combatText[combatIndex2].color = new Color(238, 74, 89);
            else if (player.HeldItem.Unbidden().element == 1)
              Main.combatText[combatIndex2].color = new Color(238, 74, 204);
            else if (player.HeldItem.Unbidden().element == 2)
              Main.combatText[combatIndex2].color = new Color(238, 226, 74);
            else if (player.HeldItem.Unbidden().element == 3)
              Main.combatText[combatIndex2].color = new Color(74, 95, 238);
            else if (player.HeldItem.Unbidden().element == 4)
              Main.combatText[combatIndex2].color = new Color(74, 238, 137);
            else if (player.HeldItem.Unbidden().element == 5)
              Main.combatText[combatIndex2].color = new Color(145, 74, 238);
            else if (player.HeldItem.Unbidden().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 216, 117);
            else if (player.HeldItem.Unbidden().element == 7)
              Main.combatText[combatIndex2].color = new Color(96, 0, 188);
          }
        }
      }
    }
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      ModPacket packet = mod.GetPacket();
      packet.Write((byte)UnbiddenModMessageType.UnbiddenPlayerSyncPlayer);
      packet.Write((byte)player.whoAmI);
      packet.Write(tearCount); // While we sync nonStopParty in SendClientChanges, we still need to send it here as well so newly joining players will receive the correct value.
      packet.Send(toWho, fromWho);
    }
  }
}
/*
currFocus = new UIText("0", 1f);
      currFocus.Top.Set(0f, 0f);
      currFocus.Left.Set(0, 0f);

      currFocus2 = new UIText("0", 1f);
      currFocus2.Top.Set(0f, 0f);
      currFocus2.Left.Set(90, 0f);

      currFocus3 = new UIText("0", 1f);
      currFocus3.Top.Set(0f, 0f);
      currFocus3.Left.Set(180, 0f);
      currFocus.SetText(lifeArray[0].ToString());
        currFocus2.SetText(lifeArray[1].ToString());
        currFocus3.SetText(lifeArray[2].ToString());
      */