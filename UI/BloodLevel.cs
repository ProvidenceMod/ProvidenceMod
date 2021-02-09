using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.UI
{
  internal class BloodLevel : UIState
  {
    public static bool visible = true;
    public float oldScale = Main.inventoryScale;
    private BLElement area;
    private UIImage bLFrame, bLBG;
    private UIImageFramed bLFill;
    private Rectangle frame;
    private byte aniFrameCounter = 12;
    private byte aniFrame = 1;

    public override void OnInitialize()
    {
      area = new BLElement();
      area.Left.Set(-228, 1f);
      area.Top.Set(-176 * 2, 1f);
      area.Width.Set(44, 0f);
      area.Height.Set(130, 0f);

      bLFrame = new UIImage(GetTexture("ProvidenceMod/UI/BloodLevelFrame"));
      bLFrame.Width.Set(44, 0f);
      bLFrame.Height.Set(130, 0f);

      bLBG = new UIImage(GetTexture("ProvidenceMod/UI/BloodLevelBG"));
      bLBG.Width.Set(44, 0f);
      bLBG.Height.Set(130, 0f);

      frame = new Rectangle(0, 0, 44, 130);
      bLFill = new UIImageFramed(GetTexture("ProvidenceMod/UI/BloodLevelEmpty1"), frame);
      bLFill.Height.Set(0f, 0f);
      bLFill.Width.Set(44, 0f);
      bLFill.Height.Set(130, 0f);

      area.Append(bLBG);
      area.Append(bLFill);
      area.Append(bLFrame);

      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }

      Player p = Main.player[0];
      ProvidencePlayer prov = p.Providence();
      visible = prov.hemomancy;
      if (Main.mapStyle == 1)
      {
        int mH = 256;
        if (mH + 600 > Main.screenHeight)
        {
          mH = Main.screenHeight - 600;
        }
        area.Top.Set(176 + mH, 0f);
      }
      else if (Main.mapStyle == 0 || Main.mapStyle == 2)
      {
        area.Top.Set(176, 0f);
      }
      if (visible)
      {
        if (aniFrameCounter > 0) { aniFrameCounter--; }
        else { aniFrameCounter = 12; aniFrame++; }
        if (aniFrame > 3) aniFrame = 1;

        int bL = prov.bloodLevel;
        if (bL <= 13) bLFill.SetImage(GetTexture($"ProvidenceMod/UI/BloodLevelEmpty{aniFrame}"), frame);
        else if (bL <= 38) bLFill.SetImage(GetTexture($"ProvidenceMod/UI/BloodLevel25_{aniFrame}"), frame);
        else if (bL <= 63) bLFill.SetImage(GetTexture($"ProvidenceMod/UI/BloodLevel50_{aniFrame}"), frame);
        else if (bL <= 88) bLFill.SetImage(GetTexture($"ProvidenceMod/UI/BloodLevel75_{aniFrame}"), frame);
        else bLFill.SetImage(GetTexture($"ProvidenceMod/UI/BloodLevelFull{aniFrame}"), frame);

        if (prov.bloodAmp) bLFrame.SetImage(GetTexture("ProvidenceMod/UI/BloodLevelFrameAmp"));
        else bLFrame.SetImage(GetTexture("ProvidenceMod/UI/BloodLevelFrame"));
      }
    }
  }
}