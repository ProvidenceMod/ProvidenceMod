using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ProvidenceMod.UI
{
  // This DragableUIPanel class inherits from UIPanel. 
  // Inheriting is a great tool for UI design. By inheriting, we get the background drawing for free from UIPanel
  // We've added some code to allow the panel to be dragged around. 
  // We've also added some code to ensure that the panel will bounce back into bounds if it is dragged outside or the screen resizes.
  // UIPanel does not prevent the player from using items when the mouse is clicked, so we've added that as well.
  internal class BLElement : UIElement
  {
    private string HoverText;
    private void SetHoverText()
    {
      Player player = Main.player[0];
      HoverText = $"{player.Providence().bloodLevel} / {player.Providence().maxBloodLevel}";
    }
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);

      if (IsMouseHovering) Main.hoverItemName = HoverText;
    }
    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      SetHoverText();
    }
  }
}