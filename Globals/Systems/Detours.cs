using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence
{
	public class Detours : ModSystem
	{
		public override void OnModLoad()
		{
			On.Terraria.Main.DrawHealthBar += Main_DrawHealthBar;
			On.Terraria.WorldGen.Hooks.WorldLoaded += WorldGen_Hooks_WorldLoaded;
			On.Terraria.CombatText.NewText_Rectangle_Color_int_bool_bool += ElementalText;
		}
		public override void Unload()
		{
			On.Terraria.Main.DrawHealthBar -= Main_DrawHealthBar;
			On.Terraria.WorldGen.Hooks.WorldLoaded -= WorldGen_Hooks_WorldLoaded;
		}

		private int ElementalText(On.Terraria.CombatText.orig_NewText_Rectangle_Color_int_bool_bool orig, Microsoft.Xna.Framework.Rectangle location, Microsoft.Xna.Framework.Color color, int amount, bool dramatic, bool dot)
		{
			if (color == CombatText.DamagedHostile || color == CombatText.DamagedHostileCrit)
			{
				// change color depending on element here
			}
			return orig(location, color, amount, dramatic, dot);
		}
		public virtual void Main_DrawHealthBar(On.Terraria.Main.orig_DrawHealthBar orig, Main self, float X, float Y, int Health, int MaxHealth, float alpha, float scale, bool noFlip)
		{
			if (!ProvidenceMod.TexturePack)
				orig(self, X, Y, Health, MaxHealth, alpha, scale, noFlip);
		}
		public virtual void WorldGen_Hooks_WorldLoaded(On.Terraria.WorldGen.Hooks.orig_WorldLoaded orig)
		{
			// Want things to happen when you load your subworld?
			// Put them here with conditional logic to trigger when the active subworld is loaded.
			// Example:
			// if (SubworldManager.IsActive<BrinewastesSubworld>())
			// {
			//		Run this code!
			// }
			orig();
		}
	}
}
