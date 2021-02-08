
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.Entities
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