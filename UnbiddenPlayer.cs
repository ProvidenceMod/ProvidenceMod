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

    public string[] elements = new string[8] { "fire", "ice", "lightning", "water", "earth", "air", "holy", "unholy" };
    public float[] resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };

    // Elemental variables also contained within GlobalItem, GlobalNPC, and GlobalProjectile
    public bool angelTear;
    public int tearCount;
    public bool brimHeart = false;
    public float cleric = 1f;
    public bool boosterShot = false;
    public float focus = 0f;
    public bool deflectable = false;
    public bool micitBangle = false;
    public bool hasClericSet = false;
    public float clericAuraRadius = 300f;
    public bool regenAura = false;
    public bool burnAura = false;
    public bool cFlameAura = false;
    public bool ampCapacitor = false;
    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", this.angelTear},
        {"tearCount", this.tearCount}
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
      resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
    }

    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
    }

    public override void PostUpdate()
    {
      ////////// DELETE THIS LATER //////////
      if (hasClericSet)
      {
        if (burnAura)
        {
          float burnRadiusBoost = -100f;
          GenerateAuraField(player, ModContent.DustType<AuraDust>(), burnRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && Vector2.Distance(npc.position, player.MountedCenter) <= (clericAuraRadius + burnRadiusBoost))
            {
              npc.AddBuff(BuffID.OnFire, 1);
            }
          }
        }
        if (cFlameAura)
        {
          float cFRadiusBoost = -150f;
          GenerateAuraField(player, ModContent.DustType<AuraDust>(), cFRadiusBoost);
          foreach (NPC npc in Main.npc)
          {
            if (!npc.townNPC && Vector2.Distance(npc.position, player.MountedCenter) <= (clericAuraRadius + cFRadiusBoost))
            {
              npc.AddBuff(BuffID.CursedInferno, 180);
            }
          }
        }
      }
      if(ampCapacitor)
      {
        float ampRadiusBoost = 0;
        GenerateAuraField(player, ModContent.DustType<MoonBlastDust>(), ampRadiusBoost);
        foreach(Projectile projectile in Main.projectile)
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

    public static List<Keys> GetPressedKeys()
    {
      List<Keys> list = ((IEnumerable<Keys>)Main.keyState.GetPressedKeys()).ToList<Keys>();
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (list[index] == Keys.None)
          list.RemoveAt(index);
      }
      return list;
    }

    public bool DoubleTapVertical()
    {
      bool w1 = false;
      bool w2 = false;
      List<Keys> pressedKeys = UnbiddenPlayer.GetPressedKeys();
      if (pressedKeys.Count == 0)
        return false;
      for (int k = 1; k < pressedKeys.Count; k++)
      {
        if (pressedKeys[k] == Keys.W && k == 1)
          w1 = true;
        if (pressedKeys[k] == Keys.W && k == 2)
          w2 = true;
      }
      if (w1 == true && w2 == true)
      {
        player.velocity.Y = -24;
        return true;
      }
      else
        return false;
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
    private float DetermineFocusGain(NPC boss, ref int useTime, ref int damage, ref bool crit)
    {
      // Default values to compare with
      const float defGain = 0.005f, defBossHPLoss = 0.5f;
      const int defUseTime = 20;

      // Determining approximate % HP lost
      int trueDamage = crit ? damage * 2 : damage;
      float perOffHP = ((boss.life - trueDamage) / boss.life) * 100;

      // Determining difference between actual and default
      float useDiff = (float)((useTime / defUseTime) * 100),
            bossHPLoss = (float)((perOffHP / defBossHPLoss) * 100);

      // Putting everything together and returning
      return defGain + useDiff + bossHPLoss;
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
      if (target.boss && target.active)
      {
        // Should add to the focus bar
        focus += DetermineFocusGain(target, ref item.useTime, ref damage, ref crit);
        if (focus > 1f) focus = 1f;
      }
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
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      Player player = Main.player[proj.owner];
      Item item = player.inventory[player.selectedItem];
      if (target.boss && target.active)
      {
        focus += DetermineFocusGain(target, ref item.useTime, ref damage, ref crit);
        if (focus > 1f) focus = 1f;
      }
    }

    // If the Player is hit by an NPC's contact damage...
    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
      damage = player.CalcEleDamageFromNPC(npc, ref damage);


      focus = 0f;
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
      damage = player.CalcEleDamageFromProj(proj, ref damage);



      focus = 0f;
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

      Action<PlayerDrawInfo> layerTarget = s => DrawSwordGlowmask(s); //the Action<T> of our layer. This is the delegate which will actually do the drawing of the layer.
      PlayerLayer layer = new PlayerLayer("UnbiddenMod", "Sword Glowmask", layerTarget); //Instantiate a new instance of PlayerLayer to insert into the list
      layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")), layer); //Insert the layer at the appropriate index. 
    }
    private void DrawSwordGlowmask(PlayerDrawInfo info)
    {
      Player player = info.drawPlayer; //the player!

      if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.MoonCleaver>() && player.itemAnimation != 0)
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
      }
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
      for (int combatIndex2 = 99 ; combatIndex2 >= 0 ; --combatIndex2)
      {
        CombatText combatText = Main.combatText[combatIndex2];
        if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
        {
          if (combatText.color == CombatText.DamagedHostile || combatText.color == CombatText.DamagedHostileCrit)
          {
            if (player.HeldItem.Unbidden().element == 0)
              Main.combatText[combatIndex2].color = new Color(235, 90, 33);
            else if (player.HeldItem.Unbidden().element == 1)
              Main.combatText[combatIndex2].color = new Color(0, 255, 255);
            else if (player.HeldItem.Unbidden().element == 2)
              Main.combatText[combatIndex2].color = new Color(235, 255, 0);
            else if (player.HeldItem.Unbidden().element == 3)
              Main.combatText[combatIndex2].color = new Color(0, 0, 255);
            else if (player.HeldItem.Unbidden().element == 4)
              Main.combatText[combatIndex2].color = new Color(0, 255, 0);
            else if (player.HeldItem.Unbidden().element == 5)
              Main.combatText[combatIndex2].color = new Color(128, 0, 255);
            else if (player.HeldItem.Unbidden().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 228, 153);
            else if (player.HeldItem.Unbidden().element == 7)
              Main.combatText[combatIndex2].color = new Color(74, 18, 179);
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