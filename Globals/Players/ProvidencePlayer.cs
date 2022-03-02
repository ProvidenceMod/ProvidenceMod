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
using Terraria.DataStructures;
using Providence.TexturePack;
using Terraria.Graphics.Effects;
using static Providence.ProvidenceUtils;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using Providence.Content.Buffs.StatDebuffs;

namespace Providence
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

		// -- Wraith -- //
		public bool wraith;
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
			if (intimidated = IsThereABoss().bossExists)
				Player.AddBuff(BuffType<Intimidated>(), 2);
			else
				Player.ClearBuff(BuffType<Intimidated>());
		}
		public override void PostUpdate()
		{
			Player.UpdatePositionCache();
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
					SoundEngine.PlaySound(ProvidenceSound.GetSoundSlot(Mod, "Sounds/Custom/AbilityPing"), Player.Center);
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
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegen -= 3;
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
			if (dashMod == 1 && Player.dash == 0)
			{
				Player.eocDash = Player.dashTime;
				Player.armorEffectDrawShadowEOCShield = true;
				const float dashStrength = 12f;
				const int delayTime = 60;
				if (!dashing && Player.dashTime == 0)
					dashDir = "";
				if (Player.dashTime > 0)
					Player.dashTime--;
				if (Player.controlRight && Player.releaseRight)
				{
					if (Player.dashTime > 0 && dashDir == "right")
					{
						Player.dashTime = 0;
						dashModDelay = delayTime;
						dashing = true;
					}
					else
					{
						dashDir = "right";
						Player.dashTime = 15;
					}
				}
				else if (Player.controlLeft && Player.releaseLeft)
				{
					if (Player.dashTime > 0 && dashDir == "left")
					{
						Player.dashTime = 0;
						dashModDelay = delayTime;
						dashing = true;
					}
					else
					{
						dashDir = "left";
						Player.dashTime = 15;
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
							Player.velocity.X = -dashStrength;
							break;
						case "right":
							Player.velocity.X = dashStrength;
							break;
							//case "up":
							//  player.velocity.Y = -dashStrength;
							//  break;
							//case "down":
							//  player.velocity.Y = dashStrength;
							//  break;
					}
					Player.dashDelay = 60;
				}
				else
				{
					return;
				}
				int dashDirInt = dashDir == "left" || dashDir == "right" ? 1 : 0;
				Point tileCoordinates1 = (Player.Center + new Vector2((dashDirInt * Player.width / 2) + 2, (float)(((double)Player.gravDir * (double)-Player.height / 2.0) + ((double)Player.gravDir * 2.0)))).ToTileCoordinates();
				Point tileCoordinates2 = (Player.Center + new Vector2((dashDirInt * Player.width / 2) + 2, 0.0f)).ToTileCoordinates();
				if (WorldGen.SolidOrSlopedTile(tileCoordinates1.X, tileCoordinates1.Y) || WorldGen.SolidOrSlopedTile(tileCoordinates2.X, tileCoordinates2.Y))
					Player.velocity.X /= 2f;
			}
		}
		public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
		{
			//createItem(ItemType<StarterBag>())

			Item createItem(int type)
			{
				Item obj = new Item();
				obj.SetDefaults(type, false);
				return obj;
			}
		}
		//public override void ModifyDrawLayers(List<PlayerLayer> layers)
		//{
		//	if (Player != null && Player.itemAnimation != 0 && !Player.HeldItem.IsAir && Player.HeldItem.Providence().glowMask)
		//	{
		//		void layerTarget(PlayerDrawInfo s) => DrawGlowMask(s);
		//		PlayerLayer layer = new PlayerLayer("Providence", "Weapon GlowMask", layerTarget);
		//		layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer);
		//	}
		//	if (Player != null && Player.itemAnimation != 0 && !Player.HeldItem.IsAir && Player.HeldItem.Providence().animated)
		//	{
		//		void layerTarget2(PlayerDrawInfo s) => DrawAnimation(s);
		//		PlayerLayer layer2 = new PlayerLayer("Providence", "Weapon Animation", layerTarget2);
		//		layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer2);
		//	}
		//	if (Player != null && Player.itemAnimation != 0 && !Player.HeldItem.IsAir && Player.HeldItem.Providence().animatedGlowMask)
		//	{
		//		void layerTarget3(PlayerDrawInfo s) => DrawGlowMaskAnimation(s);
		//		PlayerLayer layer3 = new PlayerLayer("Providence", "Weapon GlowMask Animation", layerTarget3);
		//		layers.Insert(layers.IndexOf(layers.Find(n => n.Name == "Arms")), layer3);
		//	}
		//}
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
				SoundEngine.PlaySound(SoundID.Item112, Player.Center);
			}
			if (wraith && ProvidenceMod.UseQuantum.JustPressed && quantum > 0.75f * quantumMax && !quantumFlux)
			{
				SoundEngine.PlaySound(SoundID.Item119, Player.Center);
				quantumFlux = true;
			}
		}
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)Player.whoAmI);
			packet.Send(toWho, fromWho);
		}
	}
}