using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod
{
  public class UnbiddenProjectile : GlobalProjectile
  {
    public static int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place
  }
}