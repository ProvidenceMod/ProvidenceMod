using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.UI
{
  internal class ShadowUI : UIState
  {
    public static bool visible = true;
    private bool set;
    private bool arraySet;
    public float oldScale = Main.inventoryScale;
    private int cooldown = 30;
    private ShadowElement area;
    private UIImage shadowBackground, shadowFrame;
    private UIImageFramed shadowUse, shadowBar;
    private Rectangle shadowUseRect, shadowBarRect;
    private readonly float[] shadowArray = new float[3] { 0, 0, 0 };
    public override void OnInitialize()
    {
      area = new ShadowElement();
      area.Left.Set(1350f, 0f);
      area.Top.Set(30f, 0f);
      area.Width.Set(128, 0f);
      area.Height.Set(30, 0f);

      shadowBackground = new UIImage(GetTexture("ProvidenceMod/UI/ShadowUIBackground"));
      shadowBackground.Width.Set(128, 0f);
      shadowBackground.Height.Set(30, 0f);

      shadowFrame = new UIImage(GetTexture("ProvidenceMod/UI/ShadowUIFrame"));
      shadowFrame.Width.Set(128, 0f);
      shadowFrame.Height.Set(30, 0f);

      shadowUseRect = new Rectangle(0, 0, 124, 26);
      shadowUse = new UIImageFramed(GetTexture("ProvidenceMod/UI/ShadowUIUse"), shadowUseRect);
      shadowUse.Top.Set(2, 0f);
      shadowUse.Left.Set(1, 0f);
      shadowUse.Width.Set(124, 0f);
      shadowUse.Height.Set(26, 0f);

      shadowBarRect = new Rectangle(0, 0, 124, 26);
      shadowBar = new UIImageFramed(GetTexture("ProvidenceMod/UI/ShadowUIBar"), shadowBarRect);
      shadowBar.Top.Set(2, 0f);
      shadowBar.Left.Set(1, 0f);
      shadowBar.Width.Set(124, 0f);
      shadowBar.Height.Set(26, 0f);

      area.Append(shadowBackground);
      area.Append(shadowFrame);
      area.Append(shadowUse);
      area.Append(shadowBar);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      area.visible = visible;
      if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
      ProvidencePlayer proPlayer = LocalPlayer().Providence();
      visible = proPlayer.shadow;
      if (visible)
      {
        float quotient = (float)LocalPlayer().Providence().ShadowStacks / (float)LocalPlayer().Providence().maxShadowStacks;
        quotient = Utils.Clamp(quotient, 0f, 1f);
        shadowBarRect.Width = (int)(124 * quotient);
        shadowBar.SetFrame(shadowBarRect);
        if (!set)
        {
          shadowUseRect.Width = 0;
          shadowUse.SetFrame(shadowBarRect);
          set = true;
        }
        if (!arraySet)
        {
          shadowArray[2] = shadowArray[1];
          shadowArray[1] = shadowArray[0];
          shadowArray[0] = proPlayer.ShadowStacks;
          arraySet = true;
        }
        if (proPlayer.ShadowStacks < shadowArray[0])
        {
          shadowArray[2] = shadowArray[1];
          shadowArray[1] = shadowArray[0];
          shadowArray[0] = proPlayer.ShadowStacks;
          cooldown = 30;
        }
        else if (proPlayer.ShadowStacks == shadowArray[0])
        {
          if (cooldown > 0) cooldown--;
        }
        if (cooldown == 0 && shadowUseRect.Width != shadowBarRect.Width)
        {
          if ((shadowUseRect.Width - shadowBarRect.Width) * 0.05f < 1)
          {
            shadowUseRect.Width--;
          }
          else
          {
            shadowUseRect.Width -= (int)((shadowUseRect.Width - shadowBarRect.Width) * 0.05f);
          }
          shadowUse.SetFrame(shadowUseRect);
        }
        if (shadowBarRect.Width > shadowUseRect.Width)
        {
          shadowUseRect.Width = shadowBarRect.Width;
          shadowUse.SetFrame(shadowUseRect);
        }

        // if (prov.shadowAmp) bLFrame.SetImage(GetTexture("ProvidenceMod/UI/ShadowUIFrameAmp"));
        // else bLFrame.SetImage(GetTexture("ProvidenceMod/UI/ShadowUIFrame"));
      }
    }
  }
}