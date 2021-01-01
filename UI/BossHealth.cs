using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod.UI
{
  internal class BossHealth : UIState
  {
    public float oldScale = Main.inventoryScale;
    public static bool visible = true;
    private UIElement area;
    private UIImage frame;
    private UIImageFramed mainBar;
    private UIImageFramed barAfterImage;
    private Rectangle mainBarRect;
    private Rectangle barAfterImageRect;
    private int afterImageCooldown = 120;
    private int oldBarLength;
    private int barLength;
    private int amount;
    private bool justHit = false;
    private bool decreasing = false;
    private bool boss = false;
    private NPC bossNPC;
    private float quotient;

    public override void OnInitialize()
    {
      area = new UIElement();
      area.Left.Set(755f, 0f);
      area.Top.Set(25f, 0f);
      area.Width.Set(240, 0f);
      area.Height.Set(50, 0f);
      area.PaddingTop = 5f;
      area.PaddingBottom = 5f;

      frame = new UIImage(GetTexture("UnbiddenMod/UI/FocusFrameUI"));
      frame.Top.Set(0, 0f);
      frame.Left.Set(0, 0f);
      frame.Width.Set(120f, 0f);
      frame.Height.Set(25f, 0f);

      mainBarRect = new Rectangle(0, 0, 200, 34);
      mainBar = new UIImageFramed(GetTexture("UnbiddenMod/UI/FocusBarUI"), mainBarRect);
      mainBar.Top.Set(8f, 0f);
      mainBar.Left.Set(10f, 0f);
      mainBar.Width.Set(200f, 0f);
      mainBar.Height.Set(34f, 0f);

      barAfterImageRect = new Rectangle(0, 0, 200, 34);
      barAfterImage = new UIImageFramed(GetTexture("UnbiddenMod/UI/BossNextBarUI"), barAfterImageRect);
      barAfterImage.Top.Set(8f, 0f);
      barAfterImage.Left.Set(10f, 0f);
      barAfterImage.Width.Set(200f, 0f);
      barAfterImage.Height.Set(34f, 0f);

      Append(area);
    }
    public override void Update(GameTime gameTime)
    {
      if (IsThereABoss().Item1)
      {
        area.Append(barAfterImage);
        area.Append(mainBar);
        area.Append(frame);
      }
      else if (!IsThereABoss().Item1)
      {
        area.RemoveAllChildren();
      }
      if (oldScale != Main.inventoryScale)
      {
        oldScale = Main.inventoryScale;
        Recalculate();
      }
      foreach (NPC npc in Main.npc)
      {
        if (npc.active && npc.boss)
        {
          bossNPC = npc;
          boss = true;
        }
      }
      if(boss)
        quotient = ((float) bossNPC.life) / ((float)bossNPC.lifeMax);
      // Main Bar
      quotient = Utils.Clamp(quotient, 0f, 1f);
      int measure = (int)(quotient * 100);
      mainBarRect.Width = measure * 2;
      mainBar.SetFrame(mainBarRect);

      // After Image

      if (boss && !bossNPC.justHit)
      {
        oldBarLength = mainBarRect.Width;
      }
      else if (boss && bossNPC.justHit)
      {
        barLength = mainBarRect.Width;
        justHit = true;
      }
      if(justHit)
      {
        afterImageCooldown = 120;
        afterImageCooldown--;
        if(afterImageCooldown == 0)
        {
          afterImageCooldown = 120;
          justHit = false;
          decreasing = true;
        }
        amount = oldBarLength - barLength;
      }
      if(decreasing && amount > 0)
      {
        barAfterImageRect.Width--;
        amount--;
        mainBar.SetFrame(barAfterImageRect);
      }
    }
  }
}