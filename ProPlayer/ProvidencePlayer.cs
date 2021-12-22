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
using ProvidenceMod.Items.TreasureBags;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Audio;
using ProvidenceMod.Items.Armor;
using ProvidenceMod.Items;

namespace ProvidenceMod
{
	public class ProvidencePlayer : ModPlayer
	{
		public Vector2[] oldPos = new Vector2[10];

		public bool dashing;
		public int dashMod;
		public int dashTimeMod;
		public int dashModDelay = 60;

		public bool abilityPing;

		public float DR;

		public bool starreaverArmor;

		// Buffs
		public bool pressureSpike;
		public bool intimidated;
		public bool miasma;

		// -- Wraith -- //
		public bool wraith;
		//public float wraithDamage; => player.thrownDamage;
		//public float wraithDamageMult; => player.thrownDamageMult;
		//public int wraithCrit; => player.thrownCrit;
		public float wraithCritMult;
		public Action wraithCritEffect;
		public float wraithDodge;
		public float wraithDodgeCost;
		public Action wraithDodgeEffect;
		public float wraithHitPenalty;
		public float quantum;
		public bool quantumFlux;
		public float quantumGen;
		public float quantumMax;
		public float quantumDrain;
		public int wraithPierceMod;
		public int wraithArmorPen;
		public int wraithProjectileCountMod;
		public float wraithAttackSpeedMult;
		//public float wraithProjectileVelocityMult; => player.thrownVelocity;
		// ------------ //

		// -- Cleric -- //
		public bool cleric;
		public float clericDamage;
		public int clericCrit;
		public float parityMaxStacks = 100;
		public float parityStackGen;
		public bool heartOfReality;
		public bool radiant;
		public float radiantStacks;
		public bool shadow;
		public float shadowStacks;
		// ------------ //

		public override void ResetEffects()
		{
			DR = 0f;
			dashMod = 0;
			dashTimeMod = 0;

			// Debuffs.
			intimidated = false;

			// Armor
			starreaverArmor = false;

			// Accessories.
			heartOfReality = false;

			// Wraith.
			wraith = false;
			wraithCritMult = 0f;
			wraithCritEffect = null;
			wraithDodge = 0f;
			wraithDodgeCost = 0f;
			wraithDodgeEffect = null;
			wraithHitPenalty = 0f;
			quantumGen = 0f;
			quantumMax = 0f;
			quantumDrain = 0f;
			wraithPierceMod = 0;
			wraithArmorPen = 0;
			wraithProjectileCountMod = 0;
			wraithAttackSpeedMult = 0f;

			// Cleric.
			cleric = false;
			clericDamage = 1f;
			clericCrit = 4;
			parityMaxStacks = 0;
			parityStackGen = 0;
		}
		public override void PreUpdate()
		{
			player.UpdatePositionCache();
			if (cleric) CLeric();
			else quantum = 0f;
			if (wraith) Wraith();
			else
			{
				radiant = false;
				radiantStacks = 0;
				shadow = false;
				shadowStacks = 0;
			}
			if(intimidated = IsThereABoss().bossExists)
				player.AddBuff(BuffType<Intimidated>(), 2);
			else
				player.ClearBuff(BuffType<Intimidated>());
		}
		public override void PostUpdate()
		{
		}
		public void CLeric()
		{
			Utils.Clamp(radiantStacks, 0, parityMaxStacks);
			Utils.Clamp(shadowStacks, 0, parityMaxStacks);
			if (radiant)
			{
				if (shadowStacks + parityStackGen > parityMaxStacks)
					shadowStacks = parityMaxStacks;
				else
					shadowStacks += parityStackGen;
			}
			if (shadow)
			{
				if (radiantStacks + parityStackGen > parityMaxStacks)
					radiantStacks = parityMaxStacks;
				else
					radiantStacks += parityStackGen;
			}
		}
		public void Wraith()
		{
			if (!quantumFlux)
			{
				quantum = quantum + quantumGen > quantumMax ? quantumMax : quantum + quantumGen;
				if (quantum >= (quantumMax * 0.75f) && !abilityPing)
				{
					abilityPing = true;
					Main.PlaySound(ProvidenceSound.GetSoundSlot(SoundType.Custom, "Sounds/Custom/AbilityPing"), player.Center);
				}
			}
			if (quantumFlux)
			{
				abilityPing = false;
				quantum = quantum - quantumDrain < 0f ? 0f : quantum - quantumDrain;
				quantumFlux = quantum > 0f;
			}
		}
		public override void UpdateLifeRegen()
		{
			if (pressureSpike)
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}
				player.lifeRegen -= 3;
			}
		}
		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			damage = (int)(damage * DiminishingDRFormula(DR));
		}
		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
		{
			damage = (int)(damage * DiminishingDRFormula(DR));
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (wraith)
				return (Main.rand.NextFloat(1f, 101f) / 10f) <= wraithDodge;
			return true;
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
		public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			items.Add(createItem(ItemType<StarterBag>()));

			Item createItem(int type)
			{
				Item obj = new Item();
				obj.SetDefaults(type, false);
				return obj;
			}
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
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (cleric && ProvidenceMod.CycleParity.JustPressed)
			{
				if (!radiant && !shadow)
				{
					radiant = true;
				}
				else
				{
					radiant = !radiant;
					shadow = !shadow;
				}
				Main.PlaySound(SoundID.Item112, player.Center);
			}
			if (wraith && ProvidenceMod.UseQuantum.JustPressed && quantum > 0.75f * quantumMax && !quantumFlux)
			{
				Main.PlaySound(SoundID.Item119, player.Center);
				quantumFlux = true;
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
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)player.whoAmI);
			packet.Send(toWho, fromWho);
		}
	}
}