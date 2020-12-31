using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.UI
{
  internal class Focus : UIState
  {
    public static bool visible = true;
    public float oldScale = Main.inventoryScale;
    private UIPanel area;
    private UIText currFocus, breakSlash, maxFocus;

    public override void OnInitialize()
    {
      area = new UIPanel();
      area.Left.Set(0f, 0.5f);
      area.Top.Set(50f, 0f);
      area.Width.Set(100, 0f);
      area.Height.Set(25, 0f);
      area.PaddingTop = 5f;
      area.PaddingBottom = 5f;

      currFocus = new UIText("0", 1f); breakSlash = new UIText("/", 1f); maxFocus = new UIText("100", 1f);

      currFocus.Top.Set(0f, 0f);
      breakSlash.Top.Set(0f, 0f);
      maxFocus.Top.Set(0f, 0f);

      currFocus.Left.Set(10, 0f);
      breakSlash.Left.Set(0, 0.5f);
      maxFocus.Left.Set(-25, 1f);

      area.Append(currFocus);
      area.Append(breakSlash);
      area.Append(maxFocus);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      UnbiddenPlayer unPlayer = Main.player[0].Unbidden();
      currFocus.SetText(((int)(unPlayer.focus * 100)).ToString());

      // Minor optimization so it doesn't have to run as much.
      // ONLY RECOMMENDED FOR SMALLER CHANGING ITEMS LIKE MAX VALUES.
      if (maxFocus.Text != unPlayer.focusMax.ToString())
        maxFocus.SetText((unPlayer.focusMax * 100).ToString());
      base.Update(gameTime);
      if (oldScale != Main.inventoryScale)
      {
        oldScale = Main.inventoryScale;
        Recalculate();
      }
    }
  }
}