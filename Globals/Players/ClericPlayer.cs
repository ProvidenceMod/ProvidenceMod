using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Globals.Players
{
	public class ClericPlayer : ModPlayer
	{
		public bool abilityPing;

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

		public override void ResetEffects()
		{
			cleric = false;
			clericDamage = 1f;
			clericCrit = 4;
			parityMaxStacks = 0;
			parityStackGen = 0;
		}
		public override void PreUpdate()
		{
			if (cleric) 
				CLeric();
			if (!Player.Wraith().wraith)
			{
				radiant = false;
				radiantStacks = 0;
				shadow = false;
				shadowStacks = 0;
			}
		}
		public void CLeric()
		{
			Terraria.Utils.Clamp(radiantStacks, 0, parityMaxStacks);
			Terraria.Utils.Clamp(shadowStacks, 0, parityMaxStacks);
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
		}
	}
}
