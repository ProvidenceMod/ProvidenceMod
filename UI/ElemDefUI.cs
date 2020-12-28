using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.UI
{
  internal class ElemDefUI : UIState
  {
    public static bool visible = true;
    public float oldScale = Main.inventoryScale;
    private UIText fireText;
    private UIText iceText;
    private UIText airText;
    private UIText earthText;
    private UIText lightningText;
    private UIText waterText;
    private UIText radiantText;
    private UIText necroticText;
    private UIElement area;
    private UIImage elemDefFire;
    private UIImage elemDefIce;
    private UIImage elemDefAir;
    private UIImage elemDefEarth;
    private UIImage elemDefLightning;
    private UIImage elemDefWater;
    private UIImage elemDefRadiant;
    private UIImage elemDefNecrotic;

    public override void OnInitialize()
    {
      // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
      // UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
      area = new UIElement();
      area.Left.Set(-228, 1f); // Halfway across the screen?
      area.Top.Set(176, 0f); // Placing it just a bit below the top of the screen.
      area.Width.Set(36, 0f); // 36 * 8 = 288 // 43 * 8 = 344
      area.Height.Set(344, 0f); // Our icons are all 36x
      float currentTop = 0;
      const float left = 0;
      // Fire
      elemDefFire = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefFireUI"));
      elemDefFire.Top.Set(currentTop, 0f);
      elemDefFire.Left.Set(left, 0f);
      elemDefFire.Width.Set(36, 0f);
      elemDefFire.Height.Set(36, 0f);
      currentTop += 43;

      fireText = new UIText("0", 1f); // text to show stat
      fireText.Top.Set(currentTop - 35, 0f);
      fireText.Left.Set(left + 8, 0f);
      fireText.Width.Set(18, 0f);
      fireText.Height.Set(18, 0f);
      // Ice
      elemDefIce = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefIceUI"));
      elemDefIce.Top.Set(currentTop, 0f);
      elemDefIce.Left.Set(left, 0f);
      elemDefIce.Width.Set(36, 0f);
      elemDefIce.Height.Set(36, 0f);
      currentTop += 43;

      iceText = new UIText("0", 1f); // text to show stat
      iceText.Top.Set(currentTop - 35, 0f);
      iceText.Left.Set(left + 8, 0f);
      iceText.Width.Set(18, 0f);
      iceText.Height.Set(18, 0f);
      // Lightning
      elemDefLightning = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefLightningUI"));
      elemDefLightning.Top.Set(currentTop, 0f);
      elemDefLightning.Left.Set(left, 0f);
      elemDefLightning.Width.Set(36, 0f);
      elemDefLightning.Height.Set(36, 0f);
      currentTop += 43;

      lightningText = new UIText("0", 1f); // text to show stat
      lightningText.Top.Set(currentTop - 35, 0f);
      lightningText.Left.Set(left + 8, 0f);
      lightningText.Width.Set(18, 0f);
      lightningText.Height.Set(18, 0f);
      // Water
      elemDefWater = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefWaterUI"));
      elemDefWater.Top.Set(currentTop, 0f);
      elemDefWater.Left.Set(left, 0f);
      elemDefWater.Width.Set(36, 0f);
      elemDefWater.Height.Set(36, 0f);
      currentTop += 43;

      waterText = new UIText("0", 1f); // text to show stat
      waterText.Top.Set(currentTop - 35, 0f);
      waterText.Left.Set(left + 8, 0f);
      waterText.Width.Set(18, 0f);
      waterText.Height.Set(18, 0f);
      // Earth
      elemDefEarth = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefEarthUI"));
      elemDefEarth.Top.Set(currentTop, 0f);
      elemDefEarth.Left.Set(left, 0f);
      elemDefEarth.Width.Set(36, 0f);
      elemDefEarth.Height.Set(36, 0f);
      currentTop += 43;

      earthText = new UIText("0", 1f); // text to show stat
      earthText.Top.Set(currentTop - 35, 0f);
      earthText.Left.Set(left + 8, 0f);
      earthText.Width.Set(18, 0f);
      earthText.Height.Set(18, 0f);
      // Air
      elemDefAir = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefAirUI"));
      elemDefAir.Top.Set(currentTop, 0f);
      elemDefAir.Left.Set(left, 0f);
      elemDefAir.Width.Set(36, 0f);
      elemDefAir.Height.Set(36, 0f);
      currentTop += 43;

      airText = new UIText("0", 1f); // text to show stat
      airText.Top.Set(currentTop - 35, 0f);
      airText.Left.Set(left + 8, 0f);
      airText.Width.Set(18, 0f);
      airText.Height.Set(18, 0f);
      // Radiant
      elemDefRadiant = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefRadiantUI"));
      elemDefRadiant.Top.Set(currentTop, 0f);
      elemDefRadiant.Left.Set(left, 0f);
      elemDefRadiant.Width.Set(36, 0f);
      elemDefRadiant.Height.Set(36, 0f);
      currentTop += 43;

      radiantText = new UIText("0", 1f); // text to show stat
      radiantText.Top.Set(currentTop - 35, 0f);
      radiantText.Left.Set(left + 8, 0f);
      radiantText.Width.Set(18, 0f);
      radiantText.Height.Set(18, 0f);
      // Necrotic
      elemDefNecrotic = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefNecroticUI"));
      elemDefNecrotic.Top.Set(currentTop, 0f);
      elemDefNecrotic.Left.Set(left, 0f);
      elemDefNecrotic.Width.Set(36, 0f);
      elemDefNecrotic.Height.Set(36, 0f);
      currentTop += 43;

      necroticText = new UIText("0", 1f); // text to show stat
      necroticText.Top.Set(currentTop - 35, 0f);
      necroticText.Left.Set(left + 8, 0f);
      necroticText.Width.Set(18, 0f);
      necroticText.Height.Set(18, 0f);
      /*
      area.Append(elemDefFire);
      area.Append(elemDefIce);
      area.Append(elemDefLightning);
      area.Append(elemDefWater);
      area.Append(elemDefEarth);
      area.Append(elemDefAir);
      area.Append(elemDefRadiant);
      area.Append(elemDefNecrotic);

      area.Append(fireText);
      area.Append(iceText);
      area.Append(lightningText);
      area.Append(waterText);
      area.Append(earthText);
      area.Append(airText);
      area.Append(radiantText);
      area.Append(necroticText);
      */
      Append(area);
    }
    public override void Update(GameTime gameTime)
    {
      UnbiddenPlayer unPlayer = Main.player[0].Unbidden();
      fireText.SetText(unPlayer.resists[0].ToString());
      iceText.SetText(unPlayer.resists[1].ToString());
      lightningText.SetText(unPlayer.resists[2].ToString());
      waterText.SetText(unPlayer.resists[3].ToString());
      earthText.SetText(unPlayer.resists[4].ToString());
      airText.SetText(unPlayer.resists[5].ToString());
      radiantText.SetText(unPlayer.resists[6].ToString());
      necroticText.SetText(unPlayer.resists[7].ToString());
      base.Update(gameTime);
      if (oldScale != Main.inventoryScale)
      {
        oldScale = Main.inventoryScale;
        Recalculate();
      }
      // Map handling
      if (Main.mapStyle == 1)
      {
        area.Top.Set(427, 0f);
      }
      else if (Main.mapStyle == 0 || Main.mapStyle == 2)
      {
        area.Top.Set(176, 0f);
      }
      // Fire
      if (unPlayer.resists[0] > 0)
      {
        area.Append(elemDefFire);
      }
      else
      {
        area.RemoveChild(elemDefFire);
      }
      if (unPlayer.resists[0] > 0)
      {
        area.Append(fireText);
      }
      else
      {
        area.RemoveChild(fireText);
      }
      // Ice
      if (unPlayer.resists[1] > 0)
      {
        area.Append(elemDefIce);
      }
      else
      {
        area.RemoveChild(elemDefIce);
      }
      if (unPlayer.resists[1] > 0)
      {
        area.Append(iceText);
      }
      else
      {
        area.RemoveChild(iceText);
      }
      // Lightning
      if (unPlayer.resists[2] > 0)
      {
        area.Append(elemDefLightning);
      }
      else
      {
        area.RemoveChild(elemDefLightning);
      }
      if (unPlayer.resists[2] > 0)
      {
        area.Append(lightningText);
      }
      else
      {
        area.RemoveChild(lightningText);
      }
      // Water
      if (unPlayer.resists[3] > 0)
      {
        area.Append(elemDefWater);
      }
      else
      {
        area.RemoveChild(elemDefWater);
      }
      if (unPlayer.resists[3] > 0)
      {
        area.Append(waterText);
      }
      else
      {
        area.RemoveChild(waterText);
      }
      // Earth
      if (unPlayer.resists[4] > 0)
      {
        area.Append(elemDefEarth);
      }
      else
      {
        area.RemoveChild(elemDefEarth);
      }
      if (unPlayer.resists[4] > 0)
      {
        area.Append(earthText);
      }
      else
      {
        area.RemoveChild(earthText);
      }
      // Air
      if (unPlayer.resists[5] > 0)
      {
        area.Append(elemDefAir);
      }
      else
      {
        area.RemoveChild(elemDefAir);
      }
      if (unPlayer.resists[5] > 0)
      {
        area.Append(airText);
      }
      else
      {
        area.RemoveChild(airText);
      }
      // Radiant
      if (unPlayer.resists[6] > 0)
      {
        area.Append(elemDefRadiant);
      }
      else
      {
        area.RemoveChild(elemDefRadiant);
      }
      if (unPlayer.resists[6] > 0)
      {
        area.Append(radiantText);
      }
      else
      {
        area.RemoveChild(radiantText);
      }
      // Necrotic
      if (unPlayer.resists[7] > 0)
      {
        area.Append(elemDefNecrotic);
      }
      else
      {
        area.RemoveChild(elemDefNecrotic);
      }
      if (unPlayer.resists[7] > 0)
      {
        area.Append(necroticText);
      }
      else
      {
        area.RemoveChild(necroticText);
      }
    }
  }
}