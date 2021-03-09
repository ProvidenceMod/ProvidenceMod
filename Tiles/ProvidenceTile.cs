using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.TexturePack;
using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod
{
  public class ProvidenceTile : GlobalTile
  {
    public bool texturePackEnabled;

    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
      if(!texturePackEnabled)
      {
        TileManager.InitializeTileGlowMasks();
        texturePackEnabled = true;
      }
    }
  }
}