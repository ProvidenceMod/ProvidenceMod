using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.UI
{
  public class Petals : UIState
  {
    public float oldScale = Main.inventoryScale;
    private UIElement area;
    private UIImage center;
    private UIImageFramed petals;

    public override void OnInitialize()
    {
      area = new UIElement();
      area.Top.Set(100, 0f);
      area.Left.Set(-200, 1f);
      area.Width.Set(54, 0f);
      area.Height.Set(54, 0f);

      center = new UIImage(GetTexture("ProvidenceMod/UI/Petal_Inert"));
      center.Width.Set(54, 0f);
      center.Height.Set(54, 0f);

      petals = new UIImageFramed(GetTexture("ProvidenceMod/UI/Petal_Petals"), new Rectangle(0, 0, 54, 54));
      petals.Width.Set(54, 0f);
      petals.Height.Set(54, 0f);

      area.Append(center);
      Append(area);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
      // if (!Main.dedServ)
      // {
      ProvidencePlayer p = Main.LocalPlayer.Providence();
      if (p.petal)
      {
        center.SetImage(GetTexture("ProvidenceMod/UI/Petal_Center"));
        area.RemoveAllChildren();
        area.Append(center);
        if (p.petalCount > 0)
        {
          petals.SetFrame(new Rectangle(0, 54 * (p.petalCount - 1), 54, 54));
          area.Append(petals);
        }
      }
      else
      {
        center.SetImage(GetTexture("ProvidenceMod/UI/Petal_Inert"));
      }
      // }
    }
  }
}