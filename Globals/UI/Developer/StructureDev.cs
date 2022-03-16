using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Providence.Content;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence.UI.Developer
{
	internal class StructureDev : UIState
	{
		public static bool visible;
		public float oldScale = Main.inventoryScale;
		private StructureDevElement area;
		private UIImageFramed areaGrabable;
		public static UIImageButton clearValues;
		public static UIImageButton generateStructure;
		public static StructureDevTextBox filePath;
		public static StructureDevTextBox fileName;
		private MouseState oldMouse;
		private MouseState curMouse;
		public static Action genAction;
		public static Action clearAction;
		private SpriteBatch sb;
		public bool MouseClicked { get => curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released; }
		public override void OnInitialize()
		{
			area = new StructureDevElement();
			area.Left.Set(600f, 0f);
			area.Top.Set(30f, 0f);
			area.Width.Set(400, 0f);
			area.Height.Set(200, 0f);

			areaGrabable = new UIImageFramed(Request<Texture2D>("Providence/Assets/Textures/UI/PanelCenter"), new Rectangle(0, 0, (int)area.Width.Pixels, (int)area.Height.Pixels));
			areaGrabable.Left.Set(0f, 0f);
			areaGrabable.Top.Set(0f, 0f);
			areaGrabable.Width.Set(400, 0f);
			areaGrabable.Height.Set(200, 0f);

			generateStructure = new UIImageButton(Request<Texture2D>("Providence/Assets/Textures/UI/GenerateStructureInactive"));
			generateStructure.Left.Set(340f, 0f);
			generateStructure.Top.Set(10f, 0f);
			generateStructure.Width.Set(24f, 0f);
			generateStructure.Height.Set(32f, 0f);
			generateStructure.SetVisibility(1f, 1f);

			clearValues = new UIImageButton(Request<Texture2D>("Providence/Assets/Textures/UI/ClearValuesInactive"));
			clearValues.Left.Set(370f, 0f);
			clearValues.Top.Set(10f, 0f);
			clearValues.Width.Set(22f, 0f);
			clearValues.Height.Set(32f, 0f);
			clearValues.SetVisibility(1f, 1f);

			filePath = new StructureDevTextBox("Filepath", 1f, new Color(104, 213, 237, 255));
			filePath.Left.Set(10f, 0f);
			filePath.Top.Set(10f, 0f);
			filePath.Width.Set(200f, 0f);
			filePath.Height.Set(32f, 0f);

			fileName = new StructureDevTextBox("Filename", 1f, new Color(104, 213, 237, 255));
			fileName.Left.Set(10f, 0f);
			fileName.Top.Set(60f, 0f);
			fileName.Width.Set(200f, 0f);
			fileName.Height.Set(32f, 0f);

			area.Append(areaGrabable);
			area.Append(clearValues);
			area.Append(generateStructure);
			area.Append(filePath);
			area.Append(fileName);
			Append(area);

			Main.QueueMainThreadAction(() =>
			{
				sb = new SpriteBatch(Main.graphics.GraphicsDevice);
			});
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!visible)
				return;
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			DrawPanel(sb, new Vector2(area.Left.Pixels, area.Top.Pixels), 400, 200);
			generateStructure.Draw(sb);
			clearValues.Draw(sb);
			filePath.Draw(sb);
			fileName.Draw(sb);
			sb.End();
		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (oldScale != Main.inventoryScale) { oldScale = Main.inventoryScale; Recalculate(); }
			visible = Main.player[LocalPlayer().whoAmI].HeldItem.type == ItemType<ProvidenceStructureCreator>();
			oldMouse = curMouse;
			curMouse = Mouse.GetState();
			bool mouseOverGen = curMouse.X > area.Left.Pixels + generateStructure.Left.Pixels && curMouse.X < area.Left.Pixels + generateStructure.Left.Pixels + generateStructure.Width.Pixels && curMouse.Y > area.Top.Pixels + generateStructure.Top.Pixels && curMouse.Y < area.Top.Pixels + generateStructure.Top.Pixels + generateStructure.Height.Pixels;
			bool mouseOverClear = curMouse.X > area.Left.Pixels + clearValues.Left.Pixels && curMouse.X < area.Left.Pixels + clearValues.Left.Pixels + clearValues.Width.Pixels && curMouse.Y > area.Top.Pixels + clearValues.Top.Pixels && curMouse.Y < area.Top.Pixels + clearValues.Top.Pixels + clearValues.Height.Pixels;
			if (mouseOverGen)
			{
				generateStructure.SetImage(Request<Texture2D>("Providence/Assets/Textures/UI/GenerateStructureActive"));
				if (MouseClicked)
					genAction.Invoke();
			}
			if (!mouseOverGen)
				generateStructure.SetImage(Request<Texture2D>("Providence/Assets/Textures/UI/GenerateStructureInactive"));
			if (mouseOverClear)
			{
				clearValues.SetImage(Request<Texture2D>("Providence/Assets/Textures/UI/ClearValuesActive"));
				if (MouseClicked)
					clearAction.Invoke();
			}
			if (!mouseOverClear)
				clearValues.SetImage(Request<Texture2D>("Providence/Assets/Textures/UI/ClearValuesInactive"));
		}
	}
}
