using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.TexturePack;

namespace ProvidenceMod
{
  public class ProvidenceWall : GlobalWall
  {
    public bool texturePackEnabled;
    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
      if (!texturePackEnabled)
      {
        WallManager.InitializeWallGlowMasks();
        texturePackEnabled = true;
      }
    }
  }
}