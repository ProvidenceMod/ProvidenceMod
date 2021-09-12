using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Dusts;
using Terraria.DataStructures;
using ProvidenceMod.TexturePack;
using Terraria.Graphics.Effects;
using ProvidenceMod.Buffs.StatDebuffs;
using ProvidenceMod.Items.Weapons.Melee;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod
{
	public class ProvidencePlayer : ModPlayer
	{

		public bool cerberus;
		public bool dashing;
		public bool intimidated;
		public bool texturePackEnabled;

		public int dashMod;
		public int dashTimeMod;
		public int dashModDelay = 60;

		// -- Cleric --
		public bool cleric;
		public float parityMaxStacks = 100;
		public float parityStackGen;
		public float parityCyclePen = 40;
		public bool paritySigil;
		public bool paritySigilSet;

		// Cleric Radiant
		public bool radiant;
		public float radiantStacks;

		// Cleric Shadow
		public bool shadow;
		public float shadowStacks;
		// ------------

		public override void ResetEffects()
		{
			dashMod = 0;
			dashTimeMod = 0;
			cerberus = false;
			intimidated = false;
			// -- Cleric --

			// Cleric Shadow
			parityMaxStacks = 0;
			parityStackGen = 0;
			// shadowStacks = shadow ? shadowStacks : 0;
			// radiantStacks = radiant ? radiantStacks : 0;
			// ------------
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (cleric && ProvidenceMod.CycleParity.JustPressed)
			{
				if (shadow && shadowStacks >= parityCyclePen)
				{
					shadowStacks -= parityCyclePen;
					radiant = true;
					shadow = false;
					Main.PlaySound(SoundID.Item112, player.position);
				}
				if (radiant && radiantStacks >= parityCyclePen)
				{
					radiantStacks -= parityCyclePen;
					radiant = false;
					shadow = true;
					Main.PlaySound(SoundID.Item112, player.position);
				}
			}
		}
		public override void Load(TagCompound tag)
		{
		}
		public override TagCompound Save()
		{
			return new TagCompound
			{
			};
		}
		public override void PreUpdate()
		{
			Utils.Clamp(radiantStacks, 0, parityMaxStacks);
			Utils.Clamp(shadowStacks, 0, parityMaxStacks);
			if (radiant && (radiantStacks + parityStackGen) <= parityMaxStacks)
				radiantStacks += parityStackGen;
			if (shadow && (shadowStacks + parityStackGen) <= parityMaxStacks)
				shadowStacks += parityStackGen;
			if (!paritySigilSet && paritySigil)
			{
				radiant = true;
				paritySigilSet = true;
			}
			if (!paritySigil)
			{
				radiant = false;
				shadow = false;
				radiantStacks = 0;
				shadowStacks = 0;
				parityMaxStacks = 0;
				parityStackGen = 0;
				paritySigilSet = false;
			}
			if (IsThereABoss().Item1)
			{
				player.AddBuff(BuffType<Intimidated>(), 2);
				intimidated = true;
			}
			else
			{
				player.ClearBuff(BuffType<Intimidated>());
				intimidated = false;
			}
			// if(radiant)
			// {
			//   RadiantCleric();
			// }
			// else if(shadow)
			// {
			//   ShadowCleric();
			// }
			base.PreUpdate();
		}
		public override void PostUpdate()
		{
			if (!texturePackEnabled)
			{
				PlayerManager.InitializePlayerGlowMasks();
				texturePackEnabled = true;
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
				ModDashMovement();
			else
				dashModDelay--;
		}
		public void ModDashMovement()
		{
			string dashDir = string.Empty;
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
				//else if (player.controlUp && player.releaseUp)
				//{
				//  if (player.dashTime > 0 && dashDir == "up")
				//  {
				//    player.dashTime = 0;
				//    dashModDelay = delayTime;
				//    dashing = true;
				//  }
				//  else
				//  {
				//    dashDir = "up";
				//    player.dashTime = 15;
				//  }
				//}
				//else if (player.controlDown && player.releaseDown)
				//{
				//  if (player.dashTime > 0 && dashDir == "down")
				//  {
				//    player.dashTime = 0;
				//    dashModDelay = delayTime;
				//    dashing = true;
				//  }
				//  else
				//  {
				//    dashDir = "down";
				//    player.dashTime = 15;
				//  }
				//}
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
							//case "up":
							//  player.velocity.Y = -dashStrength;
							//  break;
							//case "down":
							//  player.velocity.Y = dashStrength;
							//  break;
					}
					player.dashDelay = 60;
				}
				else
				{
					return;
				}
				int dashDirInt = dashDir == "left" || dashDir == "right" ? 1 : 0;
				Point tileCoordinates1 = (player.Center + new Vector2((dashDirInt * player.width / 2) + 2, (float)(((double)player.gravDir * (double)-player.height / 2.0) + ((double)player.gravDir * 2.0)))).ToTileCoordinates();
				Point tileCoordinates2 = (player.Center + new Vector2((dashDirInt * player.width / 2) + 2, 0.0f)).ToTileCoordinates();
				if (WorldGen.SolidOrSlopedTile(tileCoordinates1.X, tileCoordinates1.Y) || WorldGen.SolidOrSlopedTile(tileCoordinates2.X, tileCoordinates2.Y))
					player.velocity.X /= 2f;
			}
		}
		public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
		{
		}
		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
		}
		public override void NaturalLifeRegen(ref float regen)
		{
		}
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			// Item item = player.inventory[player.selectedItem];
		}
		public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
		{
		}
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Providence().glowMask)
			{
				void layerTarget(PlayerDrawInfo s) => DrawGlowMask(s);
				PlayerLayer layer = new PlayerLayer("ProvidenceMod", "Weapon GlowMask", layerTarget);
				layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer);
			}
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Providence().animated)
			{
				void layerTarget2(PlayerDrawInfo s) => DrawAnimation(s);
				PlayerLayer layer2 = new PlayerLayer("ProvidenceMod", "Weapon Animation", layerTarget2);
				layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer2);
			}
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir && player.HeldItem.Providence().animatedGlowMask)
			{
				void layerTarget3(PlayerDrawInfo s) => DrawGlowMaskAnimation(s);
				PlayerLayer layer3 = new PlayerLayer("ProvidenceMod", "Weapon GlowMask Animation", layerTarget3);
				layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer3);
			}
		}
		//public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		//{
		//  for (int combatIndex2 = 99; combatIndex2 >= 0; --combatIndex2)
		//  {
		//    CombatText combatText = Main.combatText[combatIndex2];
		//    if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
		//    {
		//      if (combatText.color == CombatText.DamagedHostile || combatText.color == CombatText.DamagedHostileCrit)
		//      {
		//        switch (player.HeldItem.Providence().element)
		//        {
		//          case 0:
		//            Main.combatText[combatIndex2].color = new Color(238, 74, 89);
		//            break;
		//          case 1:
		//            Main.combatText[combatIndex2].color = new Color(238, 74, 204);
		//            break;
		//          case 2:
		//            Main.combatText[combatIndex2].color = new Color(238, 226, 74);
		//            break;
		//          case 3:
		//            Main.combatText[combatIndex2].color = new Color(74, 95, 238);
		//            break;
		//          case 4:
		//            Main.combatText[combatIndex2].color = new Color(74, 238, 137);
		//            break;
		//          case 5:
		//            Main.combatText[combatIndex2].color = new Color(145, 74, 238);
		//            break;
		//          case 6:
		//            Main.combatText[combatIndex2].color = new Color(255, 216, 117);
		//            break;
		//          case 7:
		//            Main.combatText[combatIndex2].color = new Color(96, 0, 188);
		//            break;
		//        }
		//      }
		//    }
		//  }
		//}
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)player.whoAmI);
			packet.Send(toWho, fromWho);
		}
	}
}