using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Items.Materials
{
  public class LuminousFragment : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Luminous Fragment");
      Tooltip.SetDefault("\"It shimmers with luminous energy.\"");
      item.maxStack = 999;
      ItemID.Sets.ItemNoGravity[item.type] = true;
      Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 5));
    }

    public override void SetDefaults()
    {
      item.width = 38;
      item.height = 58;
    }
    public override Color? GetAlpha(Color lightColor) => new Color?(new Color(255, 255, 255));
  }
}