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
  internal class ParityUI : UIState
  {
    public static bool visible = true;
    private bool set;
    private bool arraySet;
    public float oldScale = Main.inventoryScale;
    private int orderCooldown = 30;
    private int chaosCooldown = 30;
    // private float oldOrder;
    // private float oldChaos;
    private ParityElement area;
    private UIImage ParityFrame;
    private UIImageFramed OrderUse, ChaosUse, OrderBar, ChaosBar;
    private Rectangle OrderUseRect, ChaosUseRect, OrderBarRect, ChaosBarRect;
    private readonly float[] ChaosArray = new float[3] { 0, 0, 0 };
    private readonly float[] OrderArray = new float[3] { 0, 0, 0 };
    public override void OnInitialize()
    {
      area = new ParityElement();
      area.Left.Set(1350f, 0f);
      area.Top.Set(30f, 0f);
      area.Width.Set(220, 0f);
      area.Height.Set(38, 0f);

      ParityFrame = new UIImage(GetTexture("ProvidenceMod/UI/ParityUIFrame"));
      ParityFrame.Width.Set(220, 0f);
      ParityFrame.Height.Set(38, 0f);

      OrderUseRect = new Rectangle(0, 0, 0, 6);
      OrderUse = new UIImageFramed(GetTexture("ProvidenceMod/UI/ParityUIOrderUse"), OrderUseRect);
      OrderUse.Top.Set(16, 0f);
      OrderUse.Left.Set(32, 0f);
      OrderUse.Width.Set(70, 0f);
      OrderUse.Height.Set(6, 0f);

      OrderBarRect = new Rectangle(0, 0, 0, 6);
      OrderBar = new UIImageFramed(GetTexture("ProvidenceMod/UI/ParityUIOrderBar"), OrderBarRect);
      OrderBar.Top.Set(16, 0f);
      OrderBar.Left.Set(32, 0f);
      OrderBar.Width.Set(70, 0f);
      OrderBar.Height.Set(6, 0f);

      ChaosUseRect = new Rectangle(70, 0, 70, 6);
      ChaosUse = new UIImageFramed(GetTexture("ProvidenceMod/UI/ParityUIChaosUse"), ChaosUseRect);
      ChaosUse.Top.Set(16, 0f);
      ChaosUse.Left.Set(120, 0f);
      ChaosUse.Width.Set(70, 0f);
      ChaosUse.Height.Set(6, 0f);

      ChaosBarRect = new Rectangle(70, 0, 70, 6);
      ChaosBar = new UIImageFramed(GetTexture("ProvidenceMod/UI/ParityUIChaosBar"), ChaosBarRect);
      ChaosBar.Top.Set(16, 0f);
      ChaosBar.Left.Set(120, 0f);
      ChaosBar.Width.Set(70, 0f);
      ChaosBar.Height.Set(6, 0f);

      area.Append(ParityFrame);
      area.Append(OrderUse);
      area.Append(OrderBar);
      area.Append(ChaosUse);
      area.Append(ChaosBar);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      area.visible = visible;
      if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
      ProvidencePlayer proPlayer = LocalPlayer().Providence();
      visible = proPlayer.cleric;
      if (visible)
      {
        float orderQuotient = LocalPlayer().Providence().orderStacks / LocalPlayer().Providence().maxParityStacks;
        float chaosQuotient = LocalPlayer().Providence().chaosStacks / LocalPlayer().Providence().maxParityStacks;
        orderQuotient = Utils.Clamp(orderQuotient, 0f, 1f);
        chaosQuotient = Utils.Clamp(chaosQuotient, 0f, 1f);
        OrderBarRect.Width = (int)(68 * orderQuotient);
        ChaosBarRect.X = (int)(68 * chaosQuotient);
        ChaosBarRect.Width = (int)(68 * chaosQuotient);
        ChaosBar.Left.Pixels = 120 + (70 - ChaosBarRect.Width);
        OrderBar.SetFrame(OrderBarRect);
        ChaosBar.SetFrame(ChaosBarRect);

        if (!set)
        {
          OrderUseRect.Width = 0;
          ChaosUseRect.Width = 0;
          OrderUse.SetFrame(OrderBarRect);
          ChaosUse.SetFrame(ChaosBarRect);
          set = true;
        }

        if (!arraySet)
        {
          ChaosArray[2] = ChaosArray[1];
          ChaosArray[1] = ChaosArray[0];
          ChaosArray[0] = proPlayer.chaosStacks;
          OrderArray[2] = OrderArray[1];
          OrderArray[1] = OrderArray[0];
          OrderArray[0] = proPlayer.orderStacks;
          arraySet = true;
        }
        // Chaos
        if (proPlayer.chaosStacks < ChaosArray[0])
        {
          ChaosArray[2] = ChaosArray[1];
          ChaosArray[1] = ChaosArray[0];
          ChaosArray[0] = proPlayer.chaosStacks;
          // oldChaos = proPlayer.chaosStacks;
          chaosCooldown = 30;
        }
        if (chaosCooldown > 0)
        {
          chaosCooldown--;
        }
        if (chaosCooldown == 0 && ChaosUseRect.Width != ChaosBarRect.Width)
        {
          if ((ChaosUseRect.Width - ChaosBarRect.Width) * 0.05f < 1)
          {
            ChaosUseRect.Width--;
            ChaosUseRect.X--;
            ChaosUse.Left.Pixels++;
          }
          else
          {
            ChaosUseRect.Width -= (int)((ChaosUseRect.Width - ChaosBarRect.Width) * 0.05f);
            ChaosUseRect.X -= (int)((ChaosUseRect.X - ChaosBarRect.X) * 0.05f);
            ChaosUse.Left.Pixels += (int)((ChaosUse.Left.Pixels - 120 - (ChaosUse.Left.Pixels - 120)) * 0.05f);
          }
          ChaosUse.SetFrame(ChaosUseRect);
        }
        if (ChaosBarRect.X < ChaosUseRect.X)
        {
          ChaosUseRect.X = ChaosBarRect.X;
          ChaosUse.SetFrame(ChaosUseRect);
        }
        // Order
        if (proPlayer.orderStacks < OrderArray[0])
        {
          OrderArray[2] = OrderArray[1];
          OrderArray[1] = OrderArray[0];
          OrderArray[0] = proPlayer.orderStacks;
          if (OrderArray[1] < OrderArray[0])
          {
            orderCooldown = 120;
          }
        }
        if (orderCooldown > 0)
        {
          orderCooldown--;
        }
        if (orderCooldown == 0 && OrderUseRect.Width != OrderBarRect.Width)
        {
          if ((OrderUseRect.Width - OrderBarRect.Width) * 0.05f < 1)
          {
            OrderUseRect.Width--;
          }
          else
          {
            OrderUseRect.Width -= (int)((OrderUseRect.Width - OrderBarRect.Width) * 0.05f);
          }
          OrderUse.SetFrame(OrderUseRect);
        }
        if (OrderBarRect.Width > OrderUseRect.Width)
        {
          OrderUseRect.Width = OrderBarRect.Width;
          OrderUse.SetFrame(OrderUseRect);
        }

        // if (prov.ChaosAmp) bLFrame.SetImage(GetTexture("ProvidenceMod/UI/ChaosUIFrameAmp"));
        // else bLFrame.SetImage(GetTexture("ProvidenceMod/UI/ChaosUIFrame"));
      }
    }
  }
}