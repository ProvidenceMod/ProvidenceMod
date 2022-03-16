using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Providence.Particles;
using Providence.Structures;
using Providence.UI.Developer;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using ParticleLibrary;
using Providence.Rarities;
using Providence.RenderTargets;
using System.Collections.Generic;
using static Providence.RenderTargets.BasicLayer;
using static Providence.RenderTargets.EmberLayer;
using static Providence.RenderTargets.FlameLayer;

namespace Providence.Content
{
	public class ProvidenceSpriteSpawn : ModItem, IBasicSprite, IEmberSprite, IFlameSprite
	{
		public int x;
		public int y;
		public int divisions;
		public Vector2 offset = new(0f, 0f);
		float progress;
		public bool Active { get => Item.active; set => Item.active = value; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sprite Spawner");
			Tooltip.SetDefault("Spawns sprites at the cursor and continuously draws them at that location.\n" +
							   "Can be used to test shaders. You should use Edit and Continue to do this.");
		}
		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 44;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Purple;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (x != 0)
				{
					RenderTargetManager.BasicLayer.Sprites.Remove(this);
					RenderTargetManager.EmberLayer.Sprites.Remove(this);
					RenderTargetManager.FlameLayer.Sprites.Remove(this);
				}
				x = 0;
				y = 0;
				Talk("Coordinates cleared.", new Color(218, 70, 70));
				return true;
			}
			if (x == 0)
			{
				RenderTargetManager.BasicLayer.Sprites.Add(this);
				RenderTargetManager.EmberLayer.Sprites.Add(this);
				RenderTargetManager.FlameLayer.Sprites.Add(this);
			}
			x = (int)(Main.MouseWorld.X / 16);
			y = (int)(Main.MouseWorld.Y / 16);

			Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, new Color(218, 70, 70), null);
			Talk($"Drawing sprites at [{x}, {y}]. Right-click to discard.", new Color(218, 70, 70));
			return true;
		}
		public void Talk(string message, Color color) => Main.NewText(message, color.R, color.G, color.B);

		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			Vector2 pos = new(x * 16, y * 16);
			float sin = ((float)Math.Sin(Main.GlobalTimeWrappedHourly));
			if (sender == RenderTargetManager.BasicLayer)
			{
				Texture2D glow = Request<Texture2D>("Providence/Assets/Textures/SoftGlow").Value;
				Vector2 pos1 = pos + new Vector2(-(sin * 2f - 1f) * 50f - 50f, 0);
				Vector2 pos2 = pos + new Vector2((sin * 2f - 1f) * 10f + 50f, -150f);
				Vector2 pos3 = pos + new Vector2(-(sin * 2f - 1f) * 10f - 50f, -300f);
				Vector2 pos4 = pos + new Vector2((sin * 2f - 1f) * 50f + 50f, -450f);

				spriteBatch.Draw(glow, pos1 - Main.screenPosition, glow.Bounds, new Color(1f, 1f, 1f, 0f), 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(glow, pos2 - Main.screenPosition, glow.Bounds, new Color(1f, 1f, 1f, 0f), 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(glow, pos3 - Main.screenPosition, glow.Bounds, new Color(1f, 1f, 1f, 0f), 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(glow, pos4 - Main.screenPosition, glow.Bounds, new Color(1f, 1f, 1f, 0f), 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);

				Vector2 bezier = ProvidenceMath.BezierPoint(sin, pos1, pos2, pos3, pos4);

				Console.WriteLine(bezier);
				progress += 1f;

				if (progress % 10 == 0)
				{
					ParticleManager.NewParticle(bezier, Vector2.Zero, new EmberParticle(), Color.White, 1f);
				}

				DrawLine(spriteBatch, pos1 - Main.screenPosition, pos4 - Main.screenPosition, new Color(1f, 0.8f, 0.3f, 0f), 5f);
				// Draw code here.
				//Effect effect = ModContent.Request<Effect>("Redemption/Effects/Circle").Value;
				//effect.Parameters["uColor"].SetValue(new Vector4(1f, 1f, 1f, 1f));
				//effect.Parameters["uOpacity"].SetValue(1f);
				//effect.Parameters["uProgress"].SetValue(progress / 100f);
				//effect.CurrentTechnique.Passes[0].Apply();

				//Texture2D circle = ModContent.Request<Texture2D>("Redemption/Textures/EmptyPixel").Value;

				//spriteBatch.Draw(circle, new Vector2(x * 16, y * 16) - Main.screenPosition, Color.White);
				//spriteBatch.Draw(circle, new Vector2(x * 16, y * 16) - Main.screenPosition - new Vector2(progress * 0.5f * sin, progress * 0.5f * sin), new Rectangle(0, 0, 1, 1), Color.White, 0f, Vector2.Zero, progress * sin, SpriteEffects.None, 0f);
			}
		}
	}
}
