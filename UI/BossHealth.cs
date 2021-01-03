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
    private int cooldown = 90;
    private readonly int[] lifeArray = new int[3] {0, 0, 0};
    private float quotient;
    private bool boss = false;
    private bool arraySet = false;
    private NPC bossNPC;

    public override void OnInitialize()
    {
      area = new UIElement();
      area.Left.Set(250f, 0f);
      area.Top.Set(700f, 0f);
      area.Width.Set(1120f, 0f);
      area.Height.Set(60f, 0f);

      frame = new UIImage(GetTexture("UnbiddenMod/UI/BossHealthUIFrame"));
      frame.Top.Set(0, 0f);
      frame.Left.Set(0, 0f);
      frame.Width.Set(1120f, 0f);
      frame.Height.Set(60f, 0f);

      mainBarRect = new Rectangle(0, 0, 1000, 40);
      mainBar = new UIImageFramed(GetTexture("UnbiddenMod/UI/BossHealthUIBar"), mainBarRect);
      mainBar.Top.Set(10f, 0f);
      mainBar.Left.Set(60f, 0f);
      mainBar.Width.Set(1000f, 0f);
      mainBar.Height.Set(40f, 0f);

      barAfterImageRect = new Rectangle(0, 0, 1000, 40);
      barAfterImage = new UIImageFramed(GetTexture("UnbiddenMod/UI/BossHealthUIHit"), barAfterImageRect);
      barAfterImage.Top.Set(10f, 0f);
      barAfterImage.Left.Set(60f, 0f);
      barAfterImage.Width.Set(1000f, 0f);
      barAfterImage.Height.Set(40f, 0f);
      Append(area);
    }
    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
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
      {
        // Don't change this please, it works, Roslynator is wack
        quotient = ((float) bossNPC.life) / ((float)bossNPC.lifeMax);
        if(!arraySet)
        {
          lifeArray[2] = lifeArray[1];
          lifeArray[1] = lifeArray[0];
          lifeArray[0] = bossNPC.life;
          arraySet = true;
        }
        if(bossNPC.life < lifeArray[0])
        {
          lifeArray[2] = lifeArray[1];
          lifeArray[1] = lifeArray[0];
          lifeArray[0] = bossNPC.life;
          cooldown = 30;
        }
        else if(bossNPC.life == lifeArray[0])
        {
          if(cooldown > 0) cooldown--;
        }
        if(cooldown == 0 && barAfterImageRect.Width != mainBarRect.Width)
        {
          if((barAfterImageRect.Width - mainBarRect.Width) * 0.05f < 1)
          {
            barAfterImageRect.Width--;
          }
          else
          {
            barAfterImageRect.Width -= (int) ((barAfterImageRect.Width - mainBarRect.Width) * 0.05f);
          }
          barAfterImage.SetFrame(barAfterImageRect);
        }
        if (bossNPC.life <= 0)
        {
          boss = false;
          bossNPC = null;
          barAfterImageRect.Width = 1000;
          barAfterImage.SetFrame(barAfterImageRect);
          cooldown = 30;
          lifeArray[0] = 0;
          lifeArray[1] = 0;
          lifeArray[2] = 0;
          arraySet = false;
        }
      }
      // Main Bar
      quotient = Utils.Clamp(quotient, 0f, 1f);
      mainBarRect.Width = (int)(1000 * quotient);
      mainBar.SetFrame(mainBarRect);
    }
  }
}