using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod
{
    public class UnbiddenGlobalProjectile : GlobalProjectile
  {
    public override bool InstancePerEntity => true;
    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place
  }
}