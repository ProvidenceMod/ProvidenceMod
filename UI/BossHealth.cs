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
    // This assigs the current scale value to a variable so that we can later reassign it and recalculate,
    // This allows us to properly scale our UI to fit with the inventory
    public float oldScale = Main.inventoryScale;
    //
    //
    // This allows us to turn our UI on and off as needed, as it is the variable that is checked before drawing is performed in UnbiddenMod.cs
    public static bool visible = true;
    //
    //
    // This is the UIElement that we will append our UI children to
    // You can alternatively use a UIPanel if you would like a panel background
    private UIElement area;
    //
    //
    // This is the image for the frame around the health bar
    private UIImage frame;
    //
    //
    // This is the health bar. It is a framed UIImage to allow us to crop it down to the correct size
    private UIImageFramed mainBar;
    //
    //
    // This is the hit bar. It is a framed UIImage for the same reason.
    private UIImageFramed barAfterImage;
    private Rectangle mainBarRect;
    private Rectangle barAfterImageRect;
    //
    //
    // This is the cooldown timer (in frames) for how long to wait after the health changes before beginning to decrease
    private int cooldown = 90;
    //
    //
    // This is a life array, you can see how the life values are written to it in the Update method
    private readonly int[] lifeArray = new int[3] {0, 0, 0};
    //
    //
    // This is the quotient, or, the value from 0.0f to 1.0f that we use to determine how much of the health bar to render
    private float quotient;
    //
    //
    // This is the variable that allows our boss health bar to run, but only if there is a boss
    private bool boss = false;
    //
    //
    // This lets us set the initial health value to the array only once, and then never again until the next boss
    private bool arraySet = false;
    //
    //
    // This is the NPC variable that contains the identity for the Boss
    private NPC bossNPC;

    public override void OnInitialize()
    {
      // Here we initialize the area and set our variables
      // Never use odd numbers for placement, or sprite dimensions
      // For some odd reason, Terraria does not like odd numbers when it comes to UI 
      // The first thing you'll notice when you use them is that your UI looks nowhere near as crisp as vanilla UI
      // My assumption is that this is because of how it renders the UI, it may blur the edges when the dimensions are uneven
      area = new UIElement();
      // The first valoe is the pixel count fron the left or top, the second value is the percentage in case that's easier to use
      area.Left.Set(250f, 0f);
      area.Top.Set(700f, 0f);
      area.Width.Set(1120f, 0f);
      area.Height.Set(60f, 0f);

      frame = new UIImage(GetTexture("UnbiddenMod/UI/BossHealthUIFrame"));
      frame.Top.Set(0, 0f);
      frame.Left.Set(0, 0f);
      frame.Width.Set(1120f, 0f);
      frame.Height.Set(60f, 0f);

      // We give the rectangle the same dimensions as our health bar so that it always draws all of it unless told otherwise
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

      // Don't forget to append the area, otherwise your UI wont draw
      // You should also append any type that starts with UI (Like UIImage or UIImageFramed)
      // You can hold off on this if you only want it to be displayed when a certain condition is fulfilled
      Append(area);
    }
    public override void Update(GameTime gameTime)
    {
      // Always add this to your UI
      base.Update(gameTime);
      // Here we call our utility method to check if there is a boss, and we ask for Item1 which is the bossExists boolean
      // This is a Tuple, which is basically like a normal method that can return a value, 
      // but you can ask for a certain value to be returned instead of being limited to one
      if (IsThereABoss().Item1)
      {
        // If there is a boss, we append our UI chilren so that it displays the boss health bar 
        area.Append(barAfterImage);
        area.Append(mainBar);
        area.Append(frame);
      }
      else if (!IsThereABoss().Item1)
      {
        // This lets us remove all of the UI children in the case that the boss is dead or that there is no boss
        area.RemoveAllChildren();
      }
      // This method lets us scale the boss health bar accordingly.
      // We set the initial scale value earlier, but if the current one doesnt match the one we set earlier, 
      // then we reset it and recalculate our scaling
      if (oldScale != Main.inventoryScale)
      {
        oldScale = Main.inventoryScale;
        Recalculate();
      }
      // Here we check the entire NPC array, this lets us set our boss variables from earlier
      foreach (NPC npc in Main.npc)
      {
        if (npc.active && npc.boss)
        {
          bossNPC = npc;
          boss = true;
        }
      }
      // This only runs if there is a boss
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
        // This checks if the current boss life is less than the previous recorded value
        // If it is, it resets the cooldown for the hit bar movement
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
        // Resetting everything after the boss is dead, make sure to do this if your UI is dependent on variables and changing things around
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