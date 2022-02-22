using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProvidenceMod.Particles;
using ProvidenceMod.Structures;
using ProvidenceMod.UI.Developer;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ParticleLibrary;

namespace ProvidenceMod.Items
{
	public class ProvidenceSpriteSpawn : ModItem
	{
		public int x;
		public int y;
		public int divisions;
		public Vector2 offset = new Vector2(0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Providence Sprite Spawn");
			Tooltip.SetDefault("Spawns sprites at the cursor and continuously draws them at that location.\n" +
												 "Can be used to test shaders. You should use Edit and Continue to do this.");
		}
		public override void SetDefaults()
		{
			Item.width = 68;
			Item.height = 68;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.rare = (int)ProvidenceRarity.Purple;
			Item.Providence().customRarity = ProvidenceRarity.Developer;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				x = 0;
				y = 0;
				for (int i = 0; i < 20; i++)
					ParticleManager.NewParticle(Main.MouseWorld, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-360, 361).InRadians()), new GenericGlowParticle(), new Color(218, 70, 70, 0), 0.25f);
				Talk("Coordinates cleared.", new Color(218, 70, 70));
				return true;
			}
			x = (int)(Main.MouseWorld.X / 16);
			y = (int)(Main.MouseWorld.Y / 16);
			Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, new Color(218, 70, 70), null);
			Talk($"Drawing sprites at [{x}, {y}]. Right-click to discard.", new Color(218, 70, 70));
			return true;
		}
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (x != 0 && y != 0)
			{
				spriteBatch.End();

				Effect quantum = Request<Effect>("Effects/DivinityShader").Value;
				Effect outline = Request<Effect>("Effects/Outline").Value;
				Effect chroma = Request<Effect>("Effects/ChromaAberration").Value;

				Texture2D mbTex = Request<Texture2D>("ProvidenceMod/NPCs/PrimordialCaelus/PrimordialCaelus").Value;
				Texture2D swirlTex = Request<Texture2D>("ProvidenceMod/ExtraTextures/Masks/SwirlTexture").Value;

				offset.X += 0.0075f;
				offset.Y += 0.0025f;

				quantum.Parameters["offset"].SetValue(offset);
				quantum.Parameters["SwirlTexture"].SetValue(swirlTex);
				outline.Parameters["conversion"].SetValue(new Vector2(1f / mbTex.Width, 1f / mbTex.Height) * 2f);
				chroma.Parameters["conversion"].SetValue((new Vector2(1f / mbTex.Width, 1f / mbTex.Height) * 2f) / 3f);

				SpriteBatch sb = new SpriteBatch(Main.graphics.GraphicsDevice);

				sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

				//quantum.CurrentTechnique.Passes[0].Apply();
				//outline.CurrentTechnique.Passes[0].Apply();
				//chroma.CurrentTechnique.Passes[0].Apply();

				sb.Draw(mbTex, new Vector2(x * 16, y * 16) - Main.screenPosition, mbTex.Bounds, Color.White, MathHelper.PiOver2, Vector2.Zero, 0f, SpriteEffects.None, 0f);
				sb.End();

				//var prevTarget = Main.graphics.GraphicsDevice.GetRenderTargets();

				//RenderTarget2D renderTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, mbTex.Width, mbTex.Height);

				//Main.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
				//Main.graphics.GraphicsDevice.Clear(Color.Transparent);

				//sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
				//sb.Draw(mbTex, new Vector2(x * 16, y * 16) - Main.screenPosition, mbTex.Bounds, Color.White, MathHelper.PiOver2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				//sb.End();

				//Main.graphics.GraphicsDevice.SetRenderTargets(prevTarget);

				//quantum.CurrentTechnique.Passes[0].Apply();
				//outline.CurrentTechnique.Passes[0].Apply();

				//sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
				//sb.Draw(renderTarget, new Vector2(x * 16, y * 16) - Main.screenPosition, mbTex.Bounds, Color.White, MathHelper.PiOver2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				//sb.End();

				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			}
		}
	}
}
