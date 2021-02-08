
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Entities
{
  public class Portal : Entity
  {
    public static int frame;
    public static int frameTick;
    public static int frameTime;
    public static int frameCount;
    public static Texture2D texture;
    public static ModItem activationItem;
    public static bool animated;

    public virtual void SetDefaults(){}
  }
}