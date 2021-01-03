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
    private UIElement area;
    private UIText currFocus, breakSlash, maxFocus;
    private UIImage focusFrame;
    private Rectangle focusBarRect;
    private UIImageFramed focusBar;

    public override void OnInitialize()
    {
      area = new UIElement();
      area.Left.Set(500f, 0f);
      area.Top.Set(25f, 0f);
      area.Width.Set(220, 0f);
      area.Height.Set(50, 0f);

      currFocus = new UIText("0", 1f); breakSlash = new UIText("/", 1f); maxFocus = new UIText("100", 1f);

      currFocus.Top.Set(0f, 0f);
      breakSlash.Top.Set(0f, 0f);
      maxFocus.Top.Set(0f, 0f);

      currFocus.Left.Set(20, 0f);
      breakSlash.Left.Set(0, 0.5f);
      maxFocus.Left.Set(-50, 1f);

      focusFrame = new UIImage(GetTexture("UnbiddenMod/UI/FocusUIFrame"));
      focusFrame.Top.Set(0, 0f);
      focusFrame.Left.Set(0, 0f);
      focusFrame.Width.Set(220f, 1f);
      focusFrame.Height.Set(50f, 1f);

      focusBarRect = new Rectangle(0, 0, 0, 34);
      focusBar = new UIImageFramed(GetTexture("UnbiddenMod/UI/FocusUIBar"), focusBarRect);
      focusBar.Top.Set(8f, 0f);
      focusBar.Left.Set(5f, 0f);
      focusBar.Width.Set(200f, 0f);
      focusBar.Height.Set(34f, 0f);

      area.Append(focusBar);
      area.Append(focusFrame);
      area.Append(currFocus);
      area.Append(breakSlash);
      area.Append(maxFocus);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      UnbiddenPlayer unPlayer = Main.player[0].Unbidden();
      currFocus.SetText(((int)(unPlayer.focus * 100)).ToString());
      float quotient = unPlayer.focus / unPlayer.focusMax;
      quotient = Utils.Clamp(quotient, 0f, 1f);
      focusBarRect.Width = (int) (200 * quotient);
      focusBar.SetFrame(focusBarRect);
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