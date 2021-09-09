using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ProvidenceMod.Metaballs
{
	public static class MaskHandles
	{

		// Initialize subscribed methods
		// These are ran every time the method they're subscribed to is run
		public static void Initialize()
		{
			On.Terraria.Main.DrawNPCs += Main_DrawNPCs;
			On.Terraria.Main.DrawPlayers += Main_DrawPlayers;
		}
		// Unload subscribed methods, aka unsubscribe methods
		public static void Unload()
		{
			On.Terraria.Main.DrawNPCs -= Main_DrawNPCs;
			On.Terraria.Main.DrawPlayers -= Main_DrawPlayers;
		}
		private static void Main_DrawNPCs(On.Terraria.Main.orig_DrawNPCs orig, Main self, bool behindTiles)
		{
			ProvidenceMod.Metaballs.DrawEnemyLayer(Main.spriteBatch);
			orig(self, behindTiles);
		}
		private static void Main_DrawPlayers(On.Terraria.Main.orig_DrawPlayers orig, Main self)
		{
			ProvidenceMod.Metaballs.DrawFriendlyLayer(Main.spriteBatch);
			orig(self);
		}
	}
}
