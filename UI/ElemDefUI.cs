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
			area.Left.Set(-area.Width.Pixels - 314, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(300, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(36, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(408, 0f);

			elemDefFire = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefFireUI"));
			elemDefFire.Left.Set(0, 0f);
			elemDefFire.Top.Set(0, 0f);
			elemDefFire.Width.Set(36, 0f);
			elemDefFire.Height.Set(36, 0f);

			fireText = new UIText("1", 0.8f); // text to show stat
			fireText.Width.Set(24, 0f);
			fireText.Height.Set(24, 0f);
			fireText.Top.Set(18, 0f);
			fireText.Left.Set(18, 0f);

			elemDefIce = new UIImage(GetTexture("UnbiddenMod/UI/ElemDefIceUI"));
			elemDefIce.Left.Set(0, 0f);
			elemDefIce.Top.Set(51, 0f);
			elemDefIce.Width.Set(36, 0f);
			elemDefIce.Height.Set(36, 0f);

			iceText = new UIText("1", 0.8f); // text to show stat
			iceText.Width.Set(24, 0f);
			iceText.Height.Set(24, 0f);
			iceText.Top.Set(69, 0f);
			iceText.Left.Set(69, 0f);
			
			area.Append(elemDefFire);
			area.Append(fireText);
			area.Append(elemDefIce);
			area.Append(iceText);
			Append(area);
        }
        public override void Draw(SpriteBatch spriteBatch) 
        {
			base.Draw(spriteBatch);
		}
        protected override void DrawSelf(SpriteBatch spriteBatch) 
		{
			base.DrawSelf(spriteBatch);

			UnbiddenPlayer unPlayer = Main.LocalPlayer.GetModPlayer<UnbiddenPlayer>();
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
			UnbiddenPlayer unPlayer = Main.LocalPlayer.GetModPlayer<UnbiddenPlayer>();
			fireText.SetText(unPlayer.resists[0].ToString());
			iceText.SetText(unPlayer.resists[1].ToString());
			base.Update(gameTime);
		}
    }
}