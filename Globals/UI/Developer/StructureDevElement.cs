using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Providence.ProvidenceUtils;

namespace Providence.UI.Developer
{
	internal class StructureDevElement : UIElement
	{
		private Vector2 offset;
		public bool dragging;
		public override void MouseDown(UIMouseEvent evt)
		{
			if (!StructureDev.visible)
				return;
			base.MouseDown(evt);
			DragStart(evt);
		}
		public override void MouseUp(UIMouseEvent evt)
		{
			if (!StructureDev.visible)
				return;
			base.MouseUp(evt);
			DragEnd(evt);
		}
		private void DragStart(UIMouseEvent evt)
		{
			if (!StructureDev.visible)
				return;
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}
		private void DragEnd(UIMouseEvent evt)
		{
			if (!StructureDev.visible)
				return;

			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}
		public override void Update(GameTime gameTime)
		{
			if (!StructureDev.visible)
				return;

			base.Update(gameTime);
			if (ContainsPoint(Main.MouseScreen))
				Main.LocalPlayer.mouseInterface = true;

			if (dragging)
			{
				Left.Set(Main.mouseX - offset.X, 0f);
				Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}

			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Terraria.Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Terraria.Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				Recalculate();
			}
		}
	}
}
