using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.Globals.Systems.Particles;
using Providence.RenderTargets;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.RenderTargets.ZephyrLayer;

namespace Providence.Content.NPCs.Caelus
{
	public class CaelusTether : ModNPC, IZephyrSprite
	{
		public bool Active => NPC.active;
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(0, "Zephyr Sentinel");
		}
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.width = 30;
			NPC.height = 30;
			NPC.Opacity = 1f;
			NPC.damage = 25;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 1000;
			NPC.townNPC = false;
			NPC.scale = 1f;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.chaseable = true;
			NPC.knockBackResist = 0f;
		}
		public int particleCounter;
		public override void AI()
		{
			// NPC.ai[0] = target X
			// NPC.ai[1] = target Y
			// NPC.ai[2] = max speed
			// NPC.ai[3] = owner npc

			Vector2 pos = new(NPC.ai[0], NPC.ai[1]);
			NPC.position = pos - new Vector2(NPC.width / 2, NPC.height / 2);
			//Vector2 dir = NPC.DirectionTo(pos);
			//NPC.velocity = ((NPC.velocity * 55f) + (dir * NPC.ai[2])) / (55f + 1f);

			if (particleCounter == 0)
				RenderTargetManager.ZephyrLayer.Sprites.Add(this);

			particleCounter++;
			if (particleCounter % 15 == 0)
				ParticleManager.NewParticle(NPC.position + NPC.frame.RandomPointInHitbox(), NPC.velocity, new ZephyrParticle(), Color.White, 1f);

			if (Main.npc[(int)NPC.ai[3]] == null || !Main.npc[(int)NPC.ai[3]].active)
			{
				NPC.life = 0;
				NPC.dontTakeDamage = false;
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				NPC.life = 1;
				NPC.dontTakeDamage = true;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			int y = NPC.life < 500 && !NPC.dontTakeDamage ? 30 : NPC.dontTakeDamage ? 60 : 0;
			spriteBatch.Draw(ModContent.Request<Texture2D>("Providence/Content/NPCs/Caelus/CaelusTether").Value, NPC.position - Main.screenPosition, new Rectangle(0, y, 30, 30), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}

		public void Draw(object sender, SpriteBatch spriteBatch)
		{
			//if (NPC.dontTakeDamage)
			//{
			//	Effect effect = ModContent.Request<Effect>("Providence/Assets/Effects/Circle", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			//	effect.Parameters["uColor"].SetValue(new Vector4(1f, 1f, 1f, 0f));
			//	effect.Parameters["uOpacity"].SetValue(1f);
			//	effect.Parameters["uProgress"].SetValue(5f);
			//	effect.CurrentTechnique.Passes[0].Apply();

			//	Texture2D circle = ModContent.Request<Texture2D>("Providence/Assets/Textures/EmptyPixel").Value;

			//	spriteBatch.Draw(circle, NPC.position - Main.screenPosition, new Rectangle(0, 0, 1, 1), Color.White, 0f, Vector2.Zero, 50f, SpriteEffects.None, 0f);
			//}
			//spriteBatch.End();
			//spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D glow = ModContent.Request<Texture2D>("Providence/Assets/Textures/SoftGlow").Value;
			float auraOpacity = 0.75f + (float)((Math.Sin(Main.GlobalTimeWrappedHourly) + 1f) / 8f);
			spriteBatch.Draw(glow, NPC.Center - Main.screenPosition, new Rectangle(0, 0, 64, 64), Color.Multiply(new Color(0.5f, 0.5f, 0.5f, 0f), auraOpacity), 0f, new Vector2(32f, 32f), new Vector2(1f, 1f), SpriteEffects.None, 0f);
		}
	}
}
