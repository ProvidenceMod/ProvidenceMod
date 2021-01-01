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
    private UIImage background;
    private UIImageFramed mainBar;
    private UIImageFramed barAfterImage;
    private UIText currFocus;
    private UIText currFocus2;
    private UIText currFocus3;
    private Rectangle mainBarRect;
    private Rectangle barAfterImageRect;
    private int cooldown = 90;
    private int[] lifeArray = new int[3] {0, 0, 0};
    private float quotient;
    private bool boss = false;
    private bool arraySet = false;
    private NPC bossNPC;

    public override void OnInitialize()
    {
      area = new UIElement();
      area.Left.Set(175f, 0f);
      area.Top.Set(0f, 0.85f);
      area.Width.Set(641f, 0f);
      area.Height.Set(41f, 0f);

      frame = new UIImage(GetTexture("UnbiddenMod/UI/BossHealthFrameUI"));
      frame.Top.Set(0, 0f);
      frame.Left.Set(0, 0f);
      frame.Width.Set(641f, 0f);
      frame.Height.Set(41f, 0f);

      background = new UIImage(GetTexture("UnbiddenMod/UI/BossHealthBackgroundUI"));
      background.Top.Set(8f, 0f);
      background.Left.Set(18f, 0f);
      background.Width.Set(605f, 0f);
      background.Height.Set(25f, 0f);

      mainBarRect = new Rectangle(0, 0, 605, 25);
      mainBar = new UIImageFramed(GetTexture("UnbiddenMod/UI/BossHealthBarUI"), mainBarRect);
      mainBar.Top.Set(8f, 0f);
      mainBar.Left.Set(18f, 0f);
      mainBar.Width.Set(605f, 0f);
      mainBar.Height.Set(25f, 0f);

      barAfterImageRect = new Rectangle(0, 0, 605, 25);
      barAfterImage = new UIImageFramed(GetTexture("UnbiddenMod/UI/BossHealthHitUI"), barAfterImageRect);
      barAfterImage.Top.Set(8f, 0f);
      barAfterImage.Left.Set(18f, 0f);
      barAfterImage.Width.Set(605f, 0f);
      barAfterImage.Height.Set(25f, 0f);
      Append(area);
    }
    public override void Update(GameTime gameTime)
    {
      if (IsThereABoss().Item1)
      {
        area.Append(background);
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
          barAfterImageRect.Width--;
          barAfterImage.SetFrame(barAfterImageRect);
        }
        if (bossNPC.life <= 0)
        {
          boss = false;
          bossNPC = null;
          barAfterImageRect.Width = 605;
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
      mainBarRect.Width = (int)(605 * quotient);
      mainBar.SetFrame(mainBarRect);
    }
  }
}