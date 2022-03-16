using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;
using static Providence.ProvidenceUtils;

namespace Providence.UI.Developer
{
	public class StructureDevTextBox : UIElement
	{
		private bool hasFocus;
		private string defaultText;
		public string text = string.Empty;
		private float textScale;
		private bool large;
		private int cursorPosition;
		private int cursorTimer;
		private int backTimer = 4;
		private float scale;
		private Color color;
		private Color emptyColor;
		private MouseState oldMouse;
		private MouseState curMouse;
		public bool MouseClicked { get => curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released; }
		public StructureDevTextBox(string _text, float _scale, Color _color)
		{
			defaultText = _text;
			scale = _scale;
			color = _color;
			emptyColor = _color * 0.75f;
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(SoundID.MenuTick);
		}
		public override void Update(GameTime gameTime)
		{
			oldMouse = curMouse;
			curMouse = Mouse.GetState();

			cursorTimer++;
			cursorTimer %= 60;

			bool mouseOver = curMouse.X > Parent.Left.Pixels + Left.Pixels && curMouse.X < Parent.Left.Pixels + Left.Pixels + Width.Pixels && curMouse.Y > Parent.Top.Pixels + Top.Pixels && curMouse.Y < Parent.Top.Pixels + Top.Pixels + Height.Pixels;

			if (MouseClicked && Parent != null)
			{
				if (!hasFocus && mouseOver)
				{
					hasFocus = true;
					CheckBlockInput();
				}
				else if (hasFocus && !mouseOver)
				{
					hasFocus = false;
					CheckBlockInput();
					cursorPosition = text.Length;
				}
			}
			else if (curMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released && Parent != null && hasFocus && !mouseOver)
			{
				hasFocus = false;
				cursorPosition = text.Length;
				CheckBlockInput();
			}
			else if (curMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released && mouseOver)
			{
				if (text.Length > 0)
				{
					text = string.Empty;
					cursorPosition = 0;
				}
			}

			if (hasFocus)
			{
				string newString = HandleInput();
				text += newString;
				cursorPosition += newString.Length;


				if (KeyTyped(Keys.Delete) && text.Length > 0 && cursorPosition <= text.Length - 1)
					text = text.Remove(cursorPosition, 1);
				if (Main.keyState.IsKeyDown(Keys.Back))
					backTimer = backTimer-- <= 0 ? 4 : backTimer--;
				else
					backTimer = 0;
				if (Main.keyState.IsKeyDown(Keys.Back) && text.Length > 0 && cursorPosition <= text.Length && cursorPosition > 0 && backTimer <= 0)
				{
					text = text.Remove(cursorPosition - 1, 1);
					cursorPosition--;
				}
				if (KeyTyped(Keys.Left) && cursorPosition > 0)
					cursorPosition--;
				if (KeyTyped(Keys.Right) && cursorPosition < text.Length)
					cursorPosition++;
				if (KeyTyped(Keys.Home))
					cursorPosition = 0;
				if (KeyTyped(Keys.End))
					cursorPosition = text.Length;
				if ((Main.keyState.IsKeyDown(Keys.LeftControl) || Main.keyState.IsKeyDown(Keys.RightControl)) && KeyTyped(Keys.Back))
				{
					text = string.Empty;
					cursorPosition = 0;
				}
				if ((Main.keyState.IsKeyDown(Keys.LeftControl) || Main.keyState.IsKeyDown(Keys.RightControl)) && KeyTyped(Keys.V))
				{
				}
				if (KeyTyped(Keys.Enter) || KeyTyped(Keys.Tab) || KeyTyped(Keys.Escape))
				{
					hasFocus = false;
					CheckBlockInput();
				}
			}

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			DrawPanel(spriteBatch, new Vector2(Parent.Left.Pixels + Left.Pixels, Parent.Top.Pixels + Top.Pixels), (int)Width.Pixels, (int)Height.Pixels, new Color(1f, 1f, 1f, 0.8f));
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

			bool isEmpty = text.Length == 0;
			string drawText = isEmpty ? defaultText : text;
			DynamicSpriteFont font = Terraria.GameContent.FontAssets.ItemStack.Value;
			Vector2 size = font.MeasureString(drawText);

			if (isEmpty && hasFocus)
			{
				drawText = string.Empty;
				isEmpty = false;
			}

			spriteBatch.DrawString(font, drawText, new Vector2(Parent.Left.Pixels + Left.Pixels + 10f, Parent.Top.Pixels + Top.Pixels + 5f), isEmpty ? emptyColor : color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			if (!isEmpty && hasFocus && cursorTimer < 30)
			{
				float drawCursor = font.MeasureString(drawText.Substring(0, cursorPosition)).X * scale;
				spriteBatch.DrawString(font, "|", new Vector2(Parent.Left.Pixels + Left.Pixels + 10f + drawCursor, Parent.Top.Pixels + Top.Pixels + 5f), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			}
		}
		private void CheckBlockInput()
		{
			Main.blockInput = false;
			if (hasFocus)
				Main.blockInput = true;
		}
		public bool KeyTyped(Keys key) => Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		public string HandleInput()
		{
			bool shift = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);
			string newText = string.Empty;
			if (hasFocus)
			{
				if (Main.keyState.IsKeyDown(Keys.LeftControl) || Main.keyState.IsKeyDown(Keys.RightControl))
					return "";
				if (KeyTyped(Keys.Space))
					newText += " ";
				if (KeyTyped(Keys.A))
					newText += shift ? "A" : "a";
				if (KeyTyped(Keys.B))
					newText += shift ? "B" : "b";
				if (KeyTyped(Keys.C))
					newText += shift ? "C" : "c";
				if (KeyTyped(Keys.D))
					newText += shift ? "D" : "d";
				if (KeyTyped(Keys.E))
					newText += shift ? "E" : "e";
				if (KeyTyped(Keys.F))
					newText += shift ? "F" : "f";
				if (KeyTyped(Keys.G))
					newText += shift ? "G" : "g";
				if (KeyTyped(Keys.H))
					newText += shift ? "H" : "h";
				if (KeyTyped(Keys.I))
					newText += shift ? "I" : "i";
				if (KeyTyped(Keys.J))
					newText += shift ? "J" : "j";
				if (KeyTyped(Keys.K))
					newText += shift ? "K" : "k";
				if (KeyTyped(Keys.L))
					newText += shift ? "L" : "l";
				if (KeyTyped(Keys.M))
					newText += shift ? "M" : "m";
				if (KeyTyped(Keys.N))
					newText += shift ? "N" : "n";
				if (KeyTyped(Keys.O))
					newText += shift ? "O" : "o";
				if (KeyTyped(Keys.P))
					newText += shift ? "P" : "p";
				if (KeyTyped(Keys.Q))
					newText += shift ? "Q" : "q";
				if (KeyTyped(Keys.R))
					newText += shift ? "R" : "r";
				if (KeyTyped(Keys.S))
					newText += shift ? "S" : "s";
				if (KeyTyped(Keys.T))
					newText += shift ? "T" : "t";
				if (KeyTyped(Keys.U))
					newText += shift ? "U" : "u";
				if (KeyTyped(Keys.V))
					newText += shift ? "V" : "v";
				if (KeyTyped(Keys.W))
					newText += shift ? "W" : "w";
				if (KeyTyped(Keys.X))
					newText += shift ? "X" : "x";
				if (KeyTyped(Keys.Y))
					newText += shift ? "Y" : "y";
				if (KeyTyped(Keys.Z))
					newText += shift ? "Z" : "z";
				if (KeyTyped(Keys.OemSemicolon))
					newText += shift ? ":" : ";";
				if (KeyTyped(Keys.OemQuestion))
					newText += shift ? "?" : "/";
				if (KeyTyped(Keys.OemBackslash))
					newText += shift ? "|" : "\\";
			}
			return newText;
		}
	}
}
