using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnbiddenMod
{
  public class UnbiddenPlayer : ModPlayer
  {
    public bool angelTear;
    public int tearCount;
    public string[] elements = new string[7] {"fire", "ice", "lightning", "poison", "acid", "holy", "unholy"};
    public int[] resists = new int[7] {100, 100, 100, 100, 100, 100, 100};
    public bool brimHeart = false;

    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", this.angelTear},
        {"tearCount", this.tearCount},
        {"resists", this.resists},
      };
    }

    public override void ResetEffects()
    {
      brimHeart = false;
      player.statLifeMax2 += tearCount * 20;
      resists = new int[7] {100, 100, 100, 100, 100, 100, 100};
    }

    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
      resists = tag.GetIntArray("resists");
    }
    public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
    {
      if (brimHeart)
      {
        player.buffImmune[BuffID.OnFire] = true;
        if (item.GetGlobalItem<UnbiddenItem>().element == 0) // If the weapon is fire-based
        {
          // Reduce cost by 15%
          reduce -= 0.15f;
        }
      }
    }
    // If the Player is hit by an NPC's contact damage...
    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
      int npcEl = npc.GetGlobalNPC<UnbiddenNPC>().contactDamageEl;
      if (npcEl != -1)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = (float)(resists[npcEl]) / 100f;
        if (resistMod > 0f) // If you don't have immunity (meaning your resist number is 0 or lower)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else // If not, you're damage is set to 1
        {
          damage = 1;
        }
      }
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
      int projEl = proj.GetGlobalProjectile<UnbiddenProjectile>().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = (float)(resists[projEl]) / 100f;
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
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

			if (player.HeldItem.type == ModContent.ItemType<Code.Items.Weapons.MoonCleaver.MoonCleaver>() && player.itemAnimation != 0) //We want to make sure that our layer only draws when the player is swinging our specific item.
			{
				Texture2D tex = mod.GetTexture("Code/Items/Weapons/MoonCleaver/MoonCleaverGlow"); //The texture of our glowmask.

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

			if (player.HeldItem.type == ModContent.ItemType<Code.Items.Weapons.MoonCleaver.MoonCleaver>() && player.itemAnimation != 0) //We want to make sure that our layer only draws when the player is swinging our specific item.
			{
				Texture2D tex = ModContent.GetTexture("Code/Items/Weapons/MoonCleaver/MoonCleaverGlow"); //The texture of our animated sword.
				Rectangle frame = Main.itemAnimations[ModContent.ItemType<Code.Items.Weapons.MoonCleaver.MoonCleaver>()].GetFrame(tex);//the animation frame that we want should be passed as the source rectangle. this is the region if your sprite the game will read to draw.
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