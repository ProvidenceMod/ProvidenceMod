using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ProvidenceMod.Dusts;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Buffs.StatDebuffs;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Buffs.Cooldowns;
using ProvidenceMod.Projectiles.Ability;
using ProvidenceMod.Buffs.StatBuffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using ProvidenceMod.TexturePack;

namespace ProvidenceMod
{
  public class ProvidencePlayer : ModPlayer
  {
    //  affExp = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    // public int affExpCooldown = 0;
    public Projectile parryProj;

    public bool allowFocus;
    public bool ampCapacitor;
    public bool angelTear;
    public bool shadowAmp;
    public bool brimHeart;
    public bool boosterShot;
    public bool burnAura;
    public bool cantdeflect;
    public bool cerberus;
    public bool cerberusAura;
    public bool cerberusAuraSpawned;
    public bool cFlameAura;
    public bool dashing;
    public bool hasClericSet;
    public bool shadow;
    public bool micitBangle;
    public bool intimidated;
    public bool parryActive;
    public bool parryActiveCooldown;
    public bool parryCapable;
    public bool parryWasActive;
    public bool radiance;
    public bool regenAura;
    public bool spawnReset = true;
    public bool tankParryOn;
    public bool texturePackEnabled;
    public bool ZephyrAglet;
    public bool vampFang;

    public const float defaultFocusGain = 0.005f;
    public float bonusFocusGain;
    public float cleric = 1f;
    public float clericAuraRadius = 300f;
    public float defaultFocusLoss = 0.25f;
    public float focus;
    public float focusLoss = 0.15f;
    public float focusMax = 1f;
    public float shadowDamage;
    public float shadowGained;
    public float tankingItemCount;

    public const int maxParryActiveTime = 90;
    public readonly int maxGainPerSecond = 10;
    public int[] affinities = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] resists = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int auraStyle = -1;
    public int auraType = -1;
    public int shadowCollectionCooldown;
    public int shadowConsumedOnUse = 25;
    public int ShadowStacks;
    public int dashMod;
    public int dashTimeMod;
    public int dashModDelay = 60;
    public int focusLossCooldown;
    public int focusLossCooldownMax = 20;
    public int shadowCrit;
    public int maxShadowStacks = 100;
    public int parriedProjs;
    public int parryActiveTime;
    public int parryProjID;
    public int parryType;
    public int radianceStacks;
    public int tankParryPWR;
    public int tearCount;

    // TODO: Make this have use (see tooltip in the item of same name)
    public string[] elements = new string[8] { "fire", "ice", "lightning", "water", "earth", "air", "radiant", "necrotic" };
    public string dashDir = "";
    public override void ResetEffects()
    {
      affinities = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
      allowFocus = IsThereABoss().Item1;
      auraStyle = -1;
      auraType = -1;
      ampCapacitor = false;
      shadowConsumedOnUse = 25;
      ShadowStacks = shadow ? ShadowStacks : 0;
      brimHeart = false;
      bonusFocusGain = 0f;
      boosterShot = false;
      burnAura = false;
      cerberus = false;
      cerberusAura = false;
      cFlameAura = false;
      cleric = 1f;
      clericAuraRadius = 300f;
      dashMod = 0;
      dashTimeMod = 0;
      focusMax = 1f;
      hasClericSet = false;
      shadowCrit = 0;
      shadowDamage = 1f;
      shadow = false;
      intimidated = false;
      maxShadowStacks = 100;
      parryActive = parryActiveTime > 0;
      parryActiveCooldown = parryActiveTime > 0 && parryActiveTime <= maxParryActiveTime;
      parryCapable = false;
      parryType = ParryTypeID.Support;
      player.moveSpeed += focus / 2;
      player.statLifeMax2 += tearCount * 20;
      regenAura = false;
      resists = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
      tankParryPWR = player.HasBuff(BuffType<TankParryBoost>()) || parryActive ? tankParryPWR : 0;
      tankParryOn = false;
      tankParryPWR = tankParryOn ? tankParryPWR : 0;
      vampFang = false;
      ZephyrAglet = false;
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
      if (player.Providence().parryCapable && ProvidenceMod.ParryHotkey.JustPressed && !player.HasBuff(BuffType<CantDeflect>()))
      {
        parryActiveTime = maxParryActiveTime;
        int p = Projectile.NewProjectile(player.position, new Vector2(0, 0), ProjectileType<ParryShield>(), 0, 0, player.whoAmI);
        parryProj = Main.projectile[p];
        parryProjID = p;
      }
      if (shadow && !shadowAmp && ProvidenceMod.UseShadowStacks.JustPressed && ShadowStacks >= shadowConsumedOnUse)
      {
        ShadowStacks -= shadowConsumedOnUse;
        shadowAmp = true;
        Main.PlaySound(SoundID.Item112, player.position);
        return;
      }
      if (shadow && shadowAmp && ProvidenceMod.UseShadowStacks.JustPressed && vampFang && !player.HasBuff(BuffID.PotionSickness))
      {
        shadowAmp = false;
        int healing = (int)(player.statLifeMax2 * 0.35f);
        player.statLife += healing;
        player.HealEffect(healing);
        player.AddBuff(BuffID.PotionSickness, 30.InTicks());
        Main.PlaySound(SoundID.Item3);
      }
    }
    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
    }
    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", angelTear},
        {"tearCount", tearCount},
        // {"affExp", this.affExp}
      };
    }
    public override void PreUpdate()
    {
      BuffHelper();
      FocusHelper(false);
      base.PreUpdate();
    }
    public override void PostUpdate()
    {
      if (!texturePackEnabled)
      {
        PlayerManager.InitializePlayerGlowMasks();
        texturePackEnabled = true;
      }
      FocusHelper(true);
      AuraHelper();
      ParryHelper(true);
      player.CalcElemDefense();
      ShadowHelper();
      // Mod mod = ModLoader.GetMod("ProvidenceMod");
      // Item item = player.HeldItem;
      // mod.Logger.InfoFormat($"{item.Providence().customRarity}", "ProvidenceMod");
    }
    public void RadiantCleric()
    { }
    public void ShadowCleric()
    { }
    public void BuffHelper()
    {
      if (IsThereABoss().Item1)
        allowFocus = true;
      player.AddBuff(BuffType<Intimidated>(), 2);
      intimidated = true;
      if (!IsThereABoss().Item1)
        player.ClearBuff(BuffType<Intimidated>());
      intimidated = false;
    }
    public void FocusHelper(bool postUpdate = false)
    {
      if (!postUpdate)
      {
        if (focusLossCooldown > 0)
          focusLossCooldown--;
        if (!allowFocus)
          focus = 0;
        focus = Utils.Clamp(focus, 0, focusMax);
      }
      else
      {
        tankingItemCount = (int)Math.Floor((decimal)(player.statDefense / 15));
        // Max 15%, min 5%
        focusLoss = 0.25f - (tankingItemCount / 100) < 0.05f ? 0.05f : 0.25f - (tankingItemCount / 100);
      }
    }
    public void AuraHelper()
    {
      if (!cerberus)
      {
        cerberusAura = false;
        cerberusAuraSpawned = false;
      }
      if (cerberus)
      {
        cerberusAura = true;
      }
      switch (auraStyle)
      {
        case 0:
          GenerateAuraField(player, AuraStyle.CerberusStyle, 0f);
          break;
        case 1:
          GenerateAuraField(player, AuraStyle.BurnStyle, -100f);
          break;
        case 2:
          GenerateAuraField(player, AuraStyle.CFlameStyle, -150f);
          break;
        case 3:
          GenerateAuraField(player, AuraStyle.AmpStyle, 0f);
          break;
      }
    }
    public void ParryHelper(bool postUpdate = false)
    {
      if (!postUpdate)
      {
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
      }
      else
      {
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
      }
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
    public void ShadowHelper()
    {
      if (shadowCollectionCooldown > 0) shadowCollectionCooldown--;
      if (shadowCollectionCooldown is 0) shadowGained = 0;
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
        ModDashMovement();
      else
        dashModDelay--;
    }
    public void ModDashMovement()
    {
      if (dashMod == 1 && player.dash == 0)
      {
        player.eocDash = player.dashTime;
        player.armorEffectDrawShadowEOCShield = true;
        const float dashStrength = 12f;
        const int delayTime = 60;
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
          player.dashDelay = 60;
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
      }
    }
    public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
    {
      if (brimHeart)
      {
        player.buffImmune[BuffID.OnFire] = true;
        if (item.Providence().element == 0) // If the weapon is fire-based
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
      if (shadowAmp)
      {
        shadowAmp = false;
        damage = (int)(damage * shadowDamage);
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

      if (player.Providence().shadowAmp)
      {
        player.Providence().shadowAmp = false;
        damage = (int)(damage * shadowDamage);
      }
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
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Providence().glowmask)
      {
        void layerTarget(PlayerDrawInfo s) => DrawGlowmask(s);
        PlayerLayer layer = new PlayerLayer("ProvidenceMod", "Sword Glowmask", layerTarget);
        layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer);
      }
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Providence().glowmask && player.HeldItem.Providence().animated)
      {
        void layerTarget2(PlayerDrawInfo s) => DrawGlowmaskAnimation(s);
        PlayerLayer layer2 = new PlayerLayer("ProvidenceMod", "Sword Animation", layerTarget2);
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
            switch (player.HeldItem.Providence().element)
            {
              case 0:
                Main.combatText[combatIndex2].color = new Color(238, 74, 89);
                break;
              case 1:
                Main.combatText[combatIndex2].color = new Color(238, 74, 204);
                break;
              case 2:
                Main.combatText[combatIndex2].color = new Color(238, 226, 74);
                break;
              case 3:
                Main.combatText[combatIndex2].color = new Color(74, 95, 238);
                break;
              case 4:
                Main.combatText[combatIndex2].color = new Color(74, 238, 137);
                break;
              case 5:
                Main.combatText[combatIndex2].color = new Color(145, 74, 238);
                break;
              case 6:
                Main.combatText[combatIndex2].color = new Color(255, 216, 117);
                break;
              case 7:
                Main.combatText[combatIndex2].color = new Color(96, 0, 188);
                break;
            }
          }
        }
      }
      if (target.active && !target.townNPC && !target.friendly && shadow && shadowGained < maxGainPerSecond)
      {
        ShadowStacks++;
        shadowGained++;
        if (shadowCollectionCooldown is 0) shadowCollectionCooldown = 60;

        ShadowStacks = Utils.Clamp(ShadowStacks, 0, maxShadowStacks);
      }
    }
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      ModPacket packet = mod.GetPacket();
      packet.Write((byte)ProvidenceModMessageType.ProvidencePlayerSyncPlayer);
      packet.Write((byte)player.whoAmI);
      packet.Write(tearCount); // While we sync nonStopParty in SendClientChanges, we still need to send it here as well so newly joining players will receive the correct value.
      packet.Send(toWho, fromWho);
    }
  }
}