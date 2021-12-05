using ProvidenceMod.Subworld;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProvidenceMod
{
	public class SentinelAetherWorld : ModWorld
	{
		public override void PostUpdate()
		{
			if (SubworldManager.IsActive<SentinelAetherSubworld>())
			{
				SubworldLibrary.SLWorld.drawUnderworldBackground = false;
				//SubworldLibrary.SLWorld.noReturn = false;
				//SubworldLibrary.SLWorld.drawMenu = true;
			}
		}
	}
}