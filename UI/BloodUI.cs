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
  internal class BloodUI : UIState
  {
    public static bool visible = true;
    private bool set;
    private bool arraySet;
    public float oldScale = Main.inventoryScale;
    private int cooldown = 30;
    private BloodElement area;
    private UIImage bloodBackground, bloodFrame;
    private UIImageFramed bloodUse, bloodBar;
    private Rectangle bloodUseRect, bloodBarRect;
    private readonly float[] bloodArray = new float[3] { 0, 0, 0 };
    public override void OnInitialize()
    {
      area = new BloodElement();
      area.Left.Set(1350f, 0f);
      area.Top.Set(30f, 0f);
      area.Width.Set(128, 0f);
      area.Height.Set(30, 0f);

      bloodBackground = new UIImage(GetTexture("ProvidenceMod/UI/BloodUIBackground"));
      bloodBackground.Width.Set(128, 0f);
      bloodBackground.Height.Set(30, 0f);

      bloodFrame = new UIImage(GetTexture("ProvidenceMod/UI/BloodUIFrame"));
      bloodFrame.Width.Set(128, 0f);
      bloodFrame.Height.Set(30, 0f);

      bloodUseRect = new Rectangle(0, 0, 124, 26);
      bloodUse = new UIImageFramed(GetTexture("ProvidenceMod/UI/BloodUIUse"), bloodUseRect);
      bloodUse.Top.Set(2, 0f);
      bloodUse.Left.Set(1, 0f);
      bloodUse.Width.Set(124, 0f);
      bloodUse.Height.Set(26, 0f);

      bloodBarRect = new Rectangle(0, 0, 124, 26);
      bloodBar = new UIImageFramed(GetTexture("ProvidenceMod/UI/BloodUIBar"), bloodBarRect);
      bloodBar.Top.Set(2, 0f);
      bloodBar.Left.Set(1, 0f);
      bloodBar.Width.Set(124, 0f);
      bloodBar.Height.Set(26, 0f);

      area.Append(bloodBackground);
      area.Append(bloodFrame);
      area.Append(bloodUse);
      area.Append(bloodBar);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      area.visible = visible;
      if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
      ProvidencePlayer proPlayer = LocalPlayer().Providence();
      visible = proPlayer.hemomancy;
      if (visible)
      {
        float quotient = (float)LocalPlayer().Providence().bloodLevel / (float)LocalPlayer().Providence().maxBloodLevel;
        quotient = Utils.Clamp(quotient, 0f, 1f);
        bloodBarRect.Width = (int)(124 * quotient);
        bloodBar.SetFrame(bloodBarRect);
        if (!set)
        {
          bloodUseRect.Width = 0;
          bloodUse.SetFrame(bloodBarRect);
          set = true;
        }
        if (!arraySet)
        {
          bloodArray[2] = bloodArray[1];
          bloodArray[1] = bloodArray[0];
          bloodArray[0] = proPlayer.bloodLevel;
          arraySet = true;
        }
        if (proPlayer.bloodLevel < bloodArray[0])
        {
          bloodArray[2] = bloodArray[1];
          bloodArray[1] = bloodArray[0];
          bloodArray[0] = proPlayer.bloodLevel;
          cooldown = 30;
        }
        else if (proPlayer.bloodLevel == bloodArray[0])
        {
          if (cooldown > 0) cooldown--;
        }
        if (cooldown == 0 && bloodUseRect.Width != bloodBarRect.Width)
        {
          if ((bloodUseRect.Width - bloodBarRect.Width) * 0.05f < 1)
          {
            bloodUseRect.Width--;
          }
          else
          {
            bloodUseRect.Width -= (int)((bloodUseRect.Width - bloodBarRect.Width) * 0.05f);
          }
          bloodUse.SetFrame(bloodUseRect);
        }
        if (bloodBarRect.Width > bloodUseRect.Width)
        {
          bloodUseRect.Width = bloodBarRect.Width;
          bloodUse.SetFrame(bloodUseRect);
        }

        // if (prov.bloodAmp) bLFrame.SetImage(GetTexture("ProvidenceMod/UI/BloodUIFrameAmp"));
        // else bLFrame.SetImage(GetTexture("ProvidenceMod/UI/BloodUIFrame"));
      }
    }
  }
}