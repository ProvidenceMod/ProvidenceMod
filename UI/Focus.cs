using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.UI
{
  internal class Focus : UIState
  {
    public static bool visible = true;
    public float oldScale = Main.inventoryScale;
    private FocusElement area;
    private UIText currFocus, breakSlash, maxFocus;
    private UIImageFramed focusFrame;
    private Rectangle frameRect;
    private UIImage focusBackground;
    private Rectangle focusBarRect;
    private Rectangle focusUseRect;
    private UIImageFramed focusBar;
    private UIImageFramed focusUse;
    private int cooldown = 30;
    private readonly int[] focusArray = new int[3] { 0, 0, 0 };
    private bool arraySet;
    private bool boss;
    private bool barSet = false;
    private NPC bossNPC;

    // private int frame = 0;
    // private int frameCounter = 10;
    // private readonly int frameTime = 10;
    // private readonly int frameAmount = 2;
    // private readonly bool frameCounterUp = true;

    public override void OnInitialize()
    {
      area = new FocusElement();
      area.Left.Set(500f, 0f);
      area.Top.Set(25f, 0f);
      area.Width.Set(110, 0f);
      area.Height.Set(52, 0f);

      currFocus = new UIText("0", 1f); breakSlash = new UIText("/", 1f); maxFocus = new UIText("100", 1f);

      currFocus.Top.Set(0f, 0f);
      breakSlash.Top.Set(0f, 0f);
      maxFocus.Top.Set(0f, 0f);

      currFocus.Left.Set(20, 0f);
      breakSlash.Left.Set(0, 0.5f);
      maxFocus.Left.Set(-50, 1f);

      frameRect = new Rectangle(0, 0, 130, 30);
      focusFrame = new UIImageFramed(GetTexture("UnbiddenMod/UI/FocusUIFrame"), frameRect);
      focusFrame.Top.Set(0, 0f);
      focusFrame.Left.Set(0, 0f);
      focusFrame.Width.Set(130f, 0f);
      focusFrame.Height.Set(30f, 0f);

      focusBackground = new UIImage(GetTexture("UnbiddenMod/UI/FocusUIBackground"));
      focusBackground.Top.Set(8f, 0f);
      focusBackground.Left.Set(15f, 0f);
      focusBackground.Width.Set(100f, 0f);
      focusBackground.Height.Set(20f, 0f);

      focusBarRect = new Rectangle(0, 0, 100, 20);
      focusBar = new UIImageFramed(GetTexture("UnbiddenMod/UI/FocusUIBar"), focusBarRect);
      focusBar.Top.Set(8f, 0f);
      focusBar.Left.Set(15f, 0f);
      focusBar.Width.Set(100f, 0f);
      focusBar.Height.Set(20f, 0f);

      focusUseRect = new Rectangle(0, 0, 100, 20);
      focusUse = new UIImageFramed(GetTexture("UnbiddenMod/UI/FocusUIUse"), focusBarRect);
      focusUse.Top.Set(8f, 0f);
      focusUse.Left.Set(15f, 0f);
      focusUse.Width.Set(100f, 0f);
      focusUse.Height.Set(20f, 0f);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      UnbiddenPlayer unPlayer = Main.player[0].Unbidden();
      currFocus.SetText(((int)(unPlayer.focus * 100)).ToString());
      float quotient = unPlayer.focus / unPlayer.focusMax;
      quotient = Utils.Clamp(quotient, 0f, 1f);
      focusBarRect.Width = (int)(100 * quotient);
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
      foreach (NPC npc in Main.npc)
      {
        if (npc.boss)
        {
          bossNPC = npc;
          boss = true;
        }
      }
      if (boss)
      {
        if (!barSet)
        {
          focusUseRect.Width = 0;
          focusUse.SetFrame(focusBarRect);
          barSet = true;
        }
        // if (frameCounter >= frameTime)
        // {
        //   frameCounter = -1;
        //   frame = frame == frameAmount - 1 ? 0 : frame + 1;
        // }
        // if (frameCounterUp)
        //   frameCounter++;
        // frameRect.Y = 34 * frame;
        // focusFrame.SetFrame(frameRect);

        area.Append(focusBackground);
        area.Append(focusUse);
        area.Append(focusBar);
        area.Append(focusFrame);
        area.Append(currFocus);
        area.Append(breakSlash);
        area.Append(maxFocus);
        if (!arraySet)
        {
          focusArray[2] = focusArray[1];
          focusArray[1] = focusArray[0];
          focusArray[0] = (int)(unPlayer.focus * 100);
          arraySet = true;
        }
        if (unPlayer.focus < focusArray[0])
        {
          focusArray[2] = focusArray[1];
          focusArray[1] = focusArray[0];
          focusArray[0] = (int)(unPlayer.focus * 100);
          cooldown = 30;
        }
        else if (unPlayer.focus == focusArray[0])
        {
          if (cooldown > 0) cooldown--;
        }
        if (cooldown == 0 && focusUseRect.Width != focusBarRect.Width)
        {
          if ((focusUseRect.Width - focusBarRect.Width) * 0.05f < 1)
          {
            focusUseRect.Width--;
          }
          else
          {
            focusUseRect.Width -= (int)((focusUseRect.Width - focusBarRect.Width) * 0.05f);
          }
          focusUse.SetFrame(focusUseRect);
        }
        if (focusBarRect.Width > focusUseRect.Width)
        {
          focusUseRect.Width = focusBarRect.Width;
          focusUse.SetFrame(focusUseRect);
        }
        if (bossNPC.life <= 0)
        {
          area.RemoveAllChildren();
          boss = false;
          bossNPC = null;
          barSet = false;
          focusUseRect.Width = 0;
          focusUse.SetFrame(focusBarRect);
          cooldown = 30;
          focusArray[0] = 0;
          focusArray[1] = 0;
          focusArray[2] = 0;
          arraySet = false;
        }
      }
    }
  }
}