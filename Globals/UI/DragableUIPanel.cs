using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Providence.UI
{
	internal class DragableUIPanel : UIPanel
	{
		private Vector2 offset;
		public bool dragging;
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
		}
		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);
			DragStart(evt);
		}
		public override void MouseUp(UIMouseEvent evt)
		{
			base.MouseUp(evt);
			DragEnd(evt);
		}
		private void DragStart(UIMouseEvent evt)
		{
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}
		private void DragEnd(UIMouseEvent evt)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging)
			{
				Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
				Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}

			// Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
			// (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes their window or otherwise changes resolution.
			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Terraria.Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Terraria.Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
		}
	}
}
