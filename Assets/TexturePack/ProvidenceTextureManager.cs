using Terraria.ModLoader;

namespace Providence.TexturePack
{
	public class ProvidenceTextureManager : ModSystem
	{
		public override void Load()
		{
			if (ProvidenceMod.TexturePack)
			{
				BigStyleManager.Load();
				BuffManager.Load();
				PlayerManager.Load();
				ItemManager.Load();
				NPCManager.Load();
				ProjectileManager.Load();
				TileManager.Load();
				UIManager.Load();
				WallManager.Load();
			}
		}
		public override void Unload()
		{
			if (ProvidenceMod.TexturePack)
			{
				BigStyleManager.Unload();
				BuffManager.Unload();
				PlayerManager.Unload();
				ItemManager.Unload();
				NPCManager.Unload();
				ProjectileManager.Unload();
				TileManager.Unload();
				UIManager.Unload();
				WallManager.Unload();
			}
		}
	}
}