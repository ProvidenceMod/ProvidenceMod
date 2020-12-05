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
    public bool hasClericSet = true;
    public float clericAuraRadius = 50f;
    private int auraDustTimer = 5;
    private const int auraDustTimerCap = 5;
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
      hasClericSet = true;
      clericAuraRadius = 50f;
      resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
    }

    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
    }

    public override void PreUpdate()
    {
      if (player.Unbidden().hasClericSet)
      {
        for (float rotation = 0f; rotation < 360f; rotation += 2f)
        {
          Vector2 origin = new Vector2(player.Center.X, player.Center.Y).RotatedBy(MathHelper.ToRadians(rotation));
          Vector2 pos = new Vector2(clericAuraRadius, 0f).RotatedBy(MathHelper.ToRadians(rotation));
          float trueX = origin.X + pos.X,
                trueY = origin.Y + pos.Y;
          Vector2 truePos = new Vector2(trueX, trueY);
          // Vector2 pos = new Vector2(player.Center.X, player.Center.Y);
          Dust.NewDust(truePos, 5, 5, mod.DustType("MoonBlastDust"), 0f, 0f, 255, new Color(0, 255, 0), 1f);
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
      if (player.Unbidden().hasClericSet)
      {
        // Get the position of targets
        // Find the distance between the targets and aura center
        // if distance <= radius, they are in the circle


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

      /* This shows an example of how we can give the held sprite of an item a glowmask or animation. This example goes over drawing a mask for a specific item, but you may want to
			 * expand your layer to cover multiple items, rather than adding in a new layer for every single item you want to create a mask for.
			 * 
			 * PlayerLayers have many other uses, basically anything you would want to visually create on a player, such as a custom accessory layer, holding a weapon, or anything else you can think of
			 * that would involve drawing sprites on a player or their held items. these examples only serve to illustrate common requests and can be extrapolated into anything you want. DrawData's constructor has
			 * the same set of params as SpriteBatch.Draw(), so you can get as creative as you want here.
			 * 
			 * Note that if you want to give your held item an animation, you should set that item's NoUseGraphic field to true so that the entire spritesheet for that item wont draw.
			 */

      //this layer is for our animated sword example! this is pretty much the same as the above layer insertion.
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

      if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.MoonCleaver>() && player.itemAnimation != 0) //We want to make sure that our layer only draws when the player is swinging our specific item.
      {
        Texture2D tex = mod.GetTexture("Items/Weapons/MoonCleaverGlow"); //The texture of our glowmask.

        //Draws via adding to Main.playerDrawData. Always do this and not Main.spriteBatch.Draw().
        Main.playerDrawData.Add(
          new DrawData(
            tex, //pass our glowmask's texture
            info.itemLocation - Main.screenPosition, //pass the position we should be drawing at from the PlayerDrawInfo we pass into this method. Always use this and not player.itemLocation.
            tex.Frame(), //our source rectangle should be the entire frame of our texture. If our mask was animated it would be the current frame of the animation.
            Color.White, //since we want our glowmask to glow, we tell it to draw with Color.White. This will make it ignore all lighting
            player.itemRotation, //the rotation of the player's item based on how they used it. This allows our glowmask to rotate with swingng swords or guns pointing in a direction.
            new Vector2(player.direction == 1 ? 0 : tex.Width, tex.Height), //the origin that our mask rotates about. This needs to be adjusted based on the player's direction, thus the ternary expression.
            player.HeldItem.scale, //scales our mask to match the item's scale
            info.spriteEffects, //the PlayerDrawInfo that was passed to this will tell us if we need to flip the sprite or not.
            0 //we dont need to worry about the layer depth here
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
					));
			}
		}*/

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