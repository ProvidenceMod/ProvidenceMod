using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using UnbiddenMod.Dusts;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod
{
  public class UnbiddenPlayer : ModPlayer
  {
    // Elemental variables for Player
    public string[] elements = new string[8] { "fire", "ice", "lightning", "water", "earth", "air", "radiant", "necrotic" };
    public int[] resists = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 },
                 affinities = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    //  affExp = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    // public int affExpCooldown = 0;

    // Elemental variables also contained within GlobalItem, GlobalNPC, and GlobalProjectile
    public bool angelTear;
    public int tearCount;
    public bool brimHeart = false;

    public bool boosterShot = false;

    public bool allowFocus = false;
    public float focus = 0f;
    public float focusMax = 1f;
    public float tankingItemCount;
    public const float defaultFocusGain = 0.005f;
    public float bonusFocusGain = 0f;
    public float focusLoss = 0.15f;
    public float defaultFocusLoss = 0.25f;
    public int focusLossCooldown = 0;
    public int focusLossCooldownMax = 20;
    public bool deflectable = false;
    public bool micitBangle = false;
    public float cleric = 1f;
    public bool hasClericSet = false;
    public float clericAuraRadius = 300f;
    public bool regenAura = false;
    public bool burnAura = false;
    public bool cFlameAura = false;
    public bool ampCapacitor = false;
    public bool bastionsAegis = false;
    public int dashTimeMod;
    public int dashMod;
    public int dashModDelay = 60;
    public string dashDir = "";
    public bool thereIsABoss;
    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", this.angelTear},
        {"tearCount", this.tearCount},
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

      focusMax = 1f;
      allowFocus = IsThereABoss().Item1;
      bonusFocusGain = 0f;
      player.moveSpeed += focus / 2;
    }

    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
      // affExp = tag.GetIntArray("affExp");
    }
    // private void DetermineAffinityLevel()
    // {
    //   for (int e = 0; e < 8; e++)
    //   {
    //     affinities[e] = DetermineElAffinityLevel(e);
    //   }
    // }
    // private int DetermineElAffinityLevel(int e)
    // {
    //   // Spoofed numbers for actual experience caps, just to proof of concept.
    //   int elExp = affExp[e];
    //   if (elExp >= 15)
    //     return 15;
    //   else if (elExp >= 14)
    //     return 14;
    //   else if (elExp >= 13)
    //     return 13;
    //   else if (elExp >= 12)
    //     return 12;
    //   else if (elExp >= 11)
    //     return 11;
    //   else if (elExp >= 10)
    //     return 10;
    //   else if (elExp >= 9)
    //     return 9;
    //   else if (elExp >= 8)
    //     return 8;
    //   else if (elExp >= 7)
    //     return 7;
    //   else if (elExp >= 6)
    //     return 6;
    //   else if (elExp >= 5)
    //     return 5;
    //   else if (elExp >= 4)
    //     return 4;
    //   else if (elExp >= 3)
    //     return 3;
    //   else if (elExp >= 2)
    //     return 2;
    //   else if (elExp >= 1)
    //     return 1;
    //   else
    //     return 0;
    // }

    public override void PreUpdate()
    {
      if (IsThereABoss().Item1)
      {
        allowFocus = true;
        thereIsABoss = true;
      }
      else if (!IsThereABoss().Item1)
      {
        thereIsABoss = false;
      }

      if (focusLossCooldown > 0)
        focusLossCooldown--;
      if (!allowFocus)
        focus = 0;

      focus = Utils.Clamp(focus, 0, focusMax);
      base.PreUpdate();
    }
    public override void PostUpdate()
    {
      tankingItemCount = (int)Math.Floor((decimal)(player.statDefense / 15));
      // Max 15%, min 5%
      focusLoss = 0.25f - (tankingItemCount / 100) < 0.05f ? 0.05f : 0.25f - (tankingItemCount / 100);
      // // Provide a cooldown so it's actually a challenge to level up naturally
      // if (affExpCooldown > 0)
      // {
      //   affExpCooldown--;
      // }
      // Safeguard against weird ass number overflowing
      player.CalcElemDefense();
      ////////// DELETE THIS LATER //////////
      if (hasClericSet)
      {
        if (burnAura)
        {
          const float burnRadiusBoost = -100f;
          GenerateAuraField(player, ModContent.DustType<AuraDust>(), burnRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && !npc.friendly && Vector2.Distance(npc.position, player.MountedCenter) <= (clericAuraRadius + burnRadiusBoost))
            {
              npc.AddBuff(BuffID.OnFire, 1);
            }
          }
        }
        if (cFlameAura)
        {
          const float cFRadiusBoost = -150f;
          GenerateAuraField(player, ModContent.DustType<AuraDust>(), cFRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && !npc.friendly && Vector2.Distance(npc.position, player.MountedCenter) <= (clericAuraRadius + cFRadiusBoost))
            {
              npc.AddBuff(BuffID.CursedInferno, 180);
            }
          }
        }
      }
      if (ampCapacitor)
      {
        const float ampRadiusBoost = 0;
        GenerateAuraField(player, ModContent.DustType<MoonBlastDust>(), ampRadiusBoost);
        foreach (Projectile projectile in Main.projectile)
        {
          if (Vector2.Distance(projectile.position, player.MountedCenter) <= (clericAuraRadius + ampRadiusBoost) && !projectile.Unbidden().amped)
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
          float distance = Vector2.Distance(targetPlayer.MountedCenter, player.MountedCenter);
          if (distance <= clericAuraRadius)
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
      Item item = player.inventory[player.selectedItem];
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
    }

    public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
    {
      if (boosterShot && item.potion)
      {
        healValue = (int)(healValue * 2); // Doubles potion power
        player.DelBuff(player.FindBuffIndex(mod.BuffType("BoosterShot"))); // Immediately deletes it from buff bar
      }
    }

    public override void ModifyDrawLayers(List<PlayerLayer> layers)
    {
      // Note that if you want to give your held item an animation, you should set that item's NoUseGraphic field to true so that the entire spritesheet for that item wont draw.

      //Animated Sword Code
      /*Action<PlayerDrawInfo> layerTarget2 = s => DrawSwordAnimation(s);
			PlayerLayer layer2 = new PlayerLayer("UnbiddenMod", "Sword Animation", layerTarget2);
			layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")), layer2);*/

      void layerTarget(PlayerDrawInfo s) => DrawSwordGlowmask(s); //the Action<T> of our layer. This is the delegate which will actually do the drawing of the layer.
      PlayerLayer layer = new PlayerLayer("UnbiddenMod", "Sword Glowmask", layerTarget); //Instantiate a new instance of PlayerLayer to insert into the list
      layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer); //Insert the layer at the appropriate index. 
    }
    private void DrawSwordGlowmask(PlayerDrawInfo info)
    {
      Player player = info.drawPlayer; //the player!

      /*if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.MoonCleaver>() && player.itemAnimation != 0)
      {
        Texture2D tex = mod.GetTexture("Items/Weapons/Melee/MoonCleaverGlow");
        Main.playerDrawData.Add(
          new DrawData(
            tex,
            info.itemLocation - Main.screenPosition,
            tex.Frame(),
            Color.White,
            player.itemRotation,
            new Vector2(player.direction == 1 ? 0 : tex.Width, tex.Height),
            player.HeldItem.scale,
            info.spriteEffects,
            0
          ));
      }*/
    }

    /*private void DrawSwordAnimation(PlayerDrawInfo info)
    {
      Player player = info.drawPlayer; //the player!

      if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.MoonCleaver.MoonCleaver>() && player.itemAnimation != 0) //We want to make sure that our layer only draws when the player is swinging our specific item.
      {
        Texture2D tex = ModContent.GetTexture("Items/Weapons/MoonCleaver/MoonCleaverGlow"); //The texture of our animated sword.
        Rectangle frame = Main.itemAnimations[ModContent.ItemType<Items.Weapons.MoonCleaver.MoonCleaver>()].GetFrame(tex);//the animation frame that we want should be passed as the source rectangle. this is the region if your sprite the game will read to draw.
        //special note that this requires your item's animation to be set up correctly in the inventory. If you want your item to be animated ONLY when you swing you will have to find the frame another way.
        //Draws via adding to Main.playerDrawData. Always do this and not Main.spriteBatch.Draw().
        Main.playerDrawData.Add(
          new DrawData(
            tex, //pass our item's spritesheet
            info.itemLocation - Main.screenPosition, //pass the position we should be drawing at from the PlayerDrawInfo we pass into this method. Always use this and not player.itemLocation.
            frame, //the animation frame we got earlier
            Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16), //since our sword shouldn't glow, we want the color of the light on our player to be the color our sword draws with. 
            //We divide by 16 and cast to int to get TILE coordinates rather than WORLD coordinates, as thats what Lighting.GetColor takes.
            player.itemRotation, //the rotation of the player's item based on how they used it. This allows our glowmask to rotate with swingng swords or guns pointing in a direction.
            new Vector2(player.direction == 1 ? 0 : frame.Width, frame.Height), //the origin that our mask rotates about. This needs to be adjusted based on the player's direction, thus the ternary expression.
            player.HeldItem.scale, //scales our mask to match the item's scale
            info.spriteEffects, //the PlayerDrawInfo that was passed to this will tell us if we need to flip the sprite or not.
            0 //we dont need to worry about the layer depth here
          )
        );
      }
    }*/
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