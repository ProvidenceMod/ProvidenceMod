using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Providence.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Items.Materials;
using Providence.Content.NPCs.SentinelAether;
using Terraria.DataStructures;

namespace Providence.Content.NPCs.SentinelAether
{
	public class ZephyrDrone : ModNPC
	{
		public int frame;
		public int frameTick;
		public float maxSpeed;
		public float strafeTimer = 100;
		public int strafeTarget = 10;
		public Vector2 strafeVector;
		public float circleTimer = 150;
		public float circleTimerMax = 150;
		public bool circleLeft = false;
		public AIState state = AIState.Circling;
		public enum AIState
		{
			Strafing,
			Circling
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Drone");
		}
		public override void SetDefaults()
		{
			NPC.width = 58;
			NPC.height = 50;
			NPC.friendly = false;
			NPC.damage = 25;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 50;
			NPC.townNPC = false;
			NPC.scale = 1f;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.chaseable = true;
			NPC.knockBackResist = 0.5f;
		}

		public override void AI()
		{
			Player player = NPC.ClosestPlayer();

			NPC.UpdateCenterCache();
			NPC.UpdateRotationCache();
			NPC.rotation = NPC.velocity.ToRotation();

			NPC.ai[0]++;

			if (NPC.ai[0] % 2.5 == 0)
			{
				Vector2 v = NPC.velocity;
				v.Normalize();
				ParticleManager.NewParticle(NPC.getRect().RandomPointInHitbox(), v.RotatedBy(MathHelper.Pi), new GlowParticle(), new Color(158, 186, 226, 0), Main.rand.NextFloat(10f, 21f) / 10f);
			}

			float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 6f);
			float sin = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f);

			if (state == AIState.Strafing)
			{
				if (NPC.Distance(player.Center) > 2048f)
					ResetStrafe();
				if (NPC.position.Y - player.position.Y > 256f)
					ResetStrafe();
				if (NPC.WithinRange(strafeVector, 64f))
					ResetStrafe();
				if (strafeTimer == 0)
					ResetStrafe();
				maxSpeed = 20f;
				strafeTimer--;
				if (strafeTarget > 0)
				{
					Vector2 v = NPC.DirectionTo(player.Center);
					Vector2 v2 = player.Center + new Vector2(384f, 0f).RotatedBy(v.ToRotation());
					strafeVector = NPC.DirectionTo(v2);
					strafeTarget--;
				}
				NPC.velocity = ((NPC.velocity * 5f) + (strafeVector * maxSpeed)) / (5f + 1f);
				void ResetStrafe()
				{
					state = AIState.Circling;
					strafeTimer = 100;
					maxSpeed = 15f;
					strafeTarget = 10;
				}
			}
			if (state == AIState.Circling)
			{
				circleTimer--;
				if (circleTimer / 2 == circleTimerMax)
					circleLeft = !circleLeft;
				Vector2 v = player.Center + new Vector2(circleLeft ? -512f : 512f, -256f) + new Vector2(cos * 256f, sin * 256f);
				if (NPC.WithinRange(v, 256f))
					maxSpeed = 15f;
				else
					maxSpeed = 15f;
				Vector2 d = NPC.DirectionTo(v);
				NPC.velocity = ((NPC.velocity * 15f) + (d * maxSpeed)) / (15f + 1f);
				if (circleTimer == 0)
				{
					state = AIState.Strafing;
					circleTimer = Main.rand.NextBool() ? 300 : 150;
					circleTimerMax = circleTimer;
					maxSpeed = 20f;
					circleLeft = !circleLeft;
				}
			}
		}
		public override void OnKill()
		{
			Item.NewItem(new EntitySource_Loot(NPC), NPC.Center, ItemType<AetherShard>(), Main.rand.Next(1, 3));
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Rectangle rect = NPC.AnimationFrame(ref frame, ref frameTick, 4, 5, true);
			if (state == AIState.Strafing)
			{
				Texture2D tex = Request<Texture2D>("Providence/Assets/Textures/Flare").Value;
				spriteBatch.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(71f * 3.25f, 0f).RotatedBy(NPC.velocity.ToRotation()), new Rectangle(71, 0, 71, 42), new Color(100, 83, 156, 0), NPC.velocity.ToRotation(), new Vector2(NPC.width, NPC.height) * 0.5f, new Vector2(7f, 1f), SpriteEffects.None, 0f);
			}
			for (int i = 0; i < 5; i++)
			{
				float alpha = 1f - (i * 0.2f);
				float scale = 1f; // + (i * 0.1f)
				SpriteEffects effect = NPC.oldRot[i + i] > MathHelper.PiOver2 || NPC.oldRot[i + i] < -MathHelper.PiOver2 ? SpriteEffects.FlipVertically : SpriteEffects.None;

				Color color = new Color(alpha * NPC.Opacity, alpha * NPC.Opacity, alpha * NPC.Opacity, alpha * NPC.Opacity);

				spriteBatch.Draw(Request<Texture2D>("Providence/NPCs/SentinelAether/ZephyrDroneSheet").Value, NPC.Providence().oldCen[i + i] - Main.screenPosition, rect, color, NPC.oldRot[i + i], new Vector2(NPC.width, NPC.height) * 0.5f, scale, effect, 0f);
			}
			spriteBatch.Draw(Request<Texture2D>("Providence/NPCs/SentinelAether/ZephyrDroneSheet").Value, NPC.Center - Main.screenPosition, rect, new Color(NPC.Opacity, NPC.Opacity, NPC.Opacity, NPC.Opacity), NPC.rotation, new Vector2(NPC.width, NPC.height) * 0.5f, NPC.scale, NPC.velocity.X < 0 ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
			return false;
		}
	}
}
