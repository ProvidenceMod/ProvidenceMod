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
    private Color gradientA;
    private Color gradientB;

    public override void OnInitialize()
    {
      // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
      // UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
      area = new UIElement();
      area.Left.Set(600, 0f); // Halfway across the screen?
      area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
      area.Width.Set(288, 0f); // 36 * 8 = 288
      area.Height.Set(36, 0f); // Our icons are all 36x
      float currentLeft = 0;
      float top = 0;
      // Fire
      elemDefFire = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefFireUI"));
      elemDefFire.Left.Set(currentLeft, 0f);
      elemDefFire.Top.Set(top, 0f);
      elemDefFire.Width.Set(36, 0f);
      elemDefFire.Height.Set(36, 0f);
      currentLeft += 36;

      fireText = new UIText("1", 1f); // text to show stat
      fireText.Left.Set(currentLeft - 18, 0f);
      fireText.Top.Set(top + 18, 0f);
      fireText.Width.Set(18, 0f);
      fireText.Height.Set(18, 0f);
      // Ice
      elemDefIce = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefIceUI"));
      elemDefIce.Left.Set(currentLeft, 0f);
      elemDefIce.Top.Set(top, 0f);
      elemDefIce.Width.Set(36, 0f);
      elemDefIce.Height.Set(36, 0f);
      currentLeft += 36;

      iceText = new UIText("1", 1f); // text to show stat
      iceText.Left.Set(currentLeft - 18, 0f);
      iceText.Top.Set(top + 18, 0f);
      iceText.Width.Set(18, 0f);
      iceText.Height.Set(18, 0f);
      // Lightning
      elemDefLightning = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefLightningUI"));
      elemDefLightning.Left.Set(currentLeft, 0f);
      elemDefLightning.Top.Set(top, 0f);
      elemDefLightning.Width.Set(36, 0f);
      elemDefLightning.Height.Set(36, 0f);
      currentLeft += 36;

      lightningText = new UIText("1", 1f); // text to show stat
      lightningText.Left.Set(currentLeft - 18, 0f);
      lightningText.Top.Set(top + 18, 0f);
      lightningText.Width.Set(18, 0f);
      lightningText.Height.Set(18, 0f);
      // Water
      elemDefWater = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefWaterUI"));
      elemDefWater.Left.Set(currentLeft, 0f);
      elemDefWater.Top.Set(top, 0f);
      elemDefWater.Width.Set(36, 0f);
      elemDefWater.Height.Set(36, 0f);
      currentLeft += 36;

      waterText = new UIText("1", 1f); // text to show stat
      waterText.Left.Set(currentLeft - 18, 0f);
      waterText.Top.Set(top + 18, 0f);
      waterText.Width.Set(18, 0f);
      waterText.Height.Set(18, 0f);

      area.Append(elemDefFire);
      area.Append(fireText);
      area.Append(elemDefIce);
      area.Append(iceText);
      Append(area);
    }
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);

      UnbiddenPlayer unPlayer = Main.LocalPlayer.Unbidden();
      int fireAff = unPlayer.affinities[0];
      int iceAff = unPlayer.affinities[1];
      int airAff = unPlayer.affinities[2];
      int earthAff = unPlayer.affinities[3];
      int lightningAff = unPlayer.affinities[4];
      int waterAff = unPlayer.affinities[5];
      int radiantAff = unPlayer.affinities[6];
      int necroticAff = unPlayer.affinities[7];
    }
    public override void Update(GameTime gameTime)
    {
      UnbiddenPlayer unPlayer = Main.player[0].Unbidden();
      fireText.SetText(unPlayer.resists[0].ToString());
      iceText.SetText(unPlayer.resists[1].ToString());
      base.Update(gameTime);
      if (oldScale != Main.inventoryScale)
      {
        oldScale = Main.inventoryScale;
        Recalculate();
      }
    }
  }
}