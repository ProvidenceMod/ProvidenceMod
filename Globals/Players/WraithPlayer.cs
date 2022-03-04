using Providence.Content.Buffs.DamageOverTime;
using Providence.Content.Buffs.StatDebuffs;
using Providence.Content.Items.Accessories;
using Providence.DamageClasses;
using Providence.UI;
using System.Collections.Generic;
using Terraria.ModLoader;
using System;
using Terraria.Audio;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.DataStructures;

namespace Providence.Globals.Players
{
	public class WraithPlayer : ModPlayer
	{
		public bool abilityPing;

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

		public override void ResetEffects()
		{
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
		}
		public override void PreUpdate()
		{
			if (wraith) 
				Wraith();
			if (!Player.Cleric().cleric)
				quantum = 0;
		}
		public void Wraith()
		{
			if (!quantumFlux)
			{
				ProvidencePlayer pro = Player.Providence();
				quantum = quantum + quantumGen > quantumMax ? quantumMax : quantum + quantumGen;
				if (quantum >= (quantumMax * 0.75f) && !abilityPing)
				{
					abilityPing = true;
					SoundEngine.PlaySound(BetterSoundID.GetSoundSlot(Mod, "Sounds/Custom/AbilityPing"), Player.Center);
				}
			}
			if (quantumFlux)
			{
				abilityPing = false;
				quantum = quantum - quantumDrain < 0f ? 0f : quantum - quantumDrain;
				quantumFlux = quantum > 0f;
			}
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (wraith)
				return (Main.rand.NextFloat(1f, 101f) / 10f) <= wraithDodge;
			return true;
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (wraith && ProvidenceMod.UseQuantum.JustPressed && quantum > 0.75f * quantumMax && !quantumFlux)
			{
				SoundEngine.PlaySound(SoundID.Item119, Player.Center);
				quantumFlux = true;
			}
		}
	}
}
