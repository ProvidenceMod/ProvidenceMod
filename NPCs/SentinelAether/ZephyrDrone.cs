using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.NPCs.SentinelAether
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
			npc.width = 58;
			npc.height = 50;
			npc.friendly = false;
			npc.damage = 25;
			npc.aiStyle = -1;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 50;
			npc.townNPC = false;
			npc.scale = 1f;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.chaseable = true;
			npc.knockBackResist = 0.5f;
		}

		public override void AI()
		{
			Player player = npc.ClosestPlayer();

			npc.UpdateCenterCache();
			npc.UpdateRotationCache();
			npc.rotation = npc.velocity.ToRotation();

			npc.ai[0]++;

			if (npc.ai[0] % 2.5 == 0)
			{
				Vector2 v = npc.velocity;
				v.Normalize();
				NewParticle(npc.getRect().RandomPointInHitbox(), v.RotatedBy(MathHelper.Pi), new GenericGlowParticle(), new Color(158, 186, 226, 0), Main.rand.NextFloat(10f, 21f) / 10f);
			}

			float cos = (float)Math.Cos(Main.GlobalTime * 6f);
			float sin = (float)Math.Sin(Main.GlobalTime * 6f);

			if (state == AIState.Strafing)
			{
				if (npc.Distance(player.Center) > 2048f)
					ResetStrafe();
				if (npc.position.Y - player.position.Y > 256f)
					ResetStrafe();
				if (npc.WithinRange(strafeVector, 64f))
					ResetStrafe();
				if (strafeTimer == 0)
					ResetStrafe();
				maxSpeed = 20f;
				strafeTimer--;
				if (strafeTarget > 0)
				{
					Vector2 v = npc.DirectionTo(player.Center);
					Vector2 v2 = player.Center + new Vector2(384f, 0f).RotatedBy(v.ToRotation());
					strafeVector = npc.DirectionTo(v2);
					strafeTarget--;
				}
				npc.velocity = ((npc.velocity * 5f) + (strafeVector * maxSpeed)) / (5f + 1f);
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
				if (npc.WithinRange(v, 256f))
					maxSpeed = 15f;
				else
					maxSpeed = 15f;
				Vector2 d = npc.DirectionTo(v);
				npc.velocity = ((npc.velocity * 15f) + (d * maxSpeed)) / (15f + 1f);
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
		public override void NPCLoot()
		{
			Item.NewItem(npc.Center, ItemType<AetherShard>(), Main.rand.Next(1, 3));
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Rectangle rect = npc.AnimationFrame(ref frame, ref frameTick, 4, 5, true);
			if (state == AIState.Strafing)
			{
				Texture2D tex = GetTexture("ProvidenceMod/ExtraTextures/Flare");
				spriteBatch.Draw(tex, npc.Center - Main.screenPosition + new Vector2(71f * 3.25f, 0f).RotatedBy(npc.velocity.ToRotation()), new Rectangle(71, 0, 71, 42), new Color(100, 83, 156, 0), npc.velocity.ToRotation(), new Vector2(npc.width, npc.height) * 0.5f, new Vector2(7f, 1f), SpriteEffects.None, 0f);
			}
			for (int i = 0; i < 5; i++)
			{
				float alpha = 1f - (i * 0.2f);
				float scale = 1f; // + (i * 0.1f)
				SpriteEffects effect = npc.oldRot[i + i] > MathHelper.PiOver2 || npc.oldRot[i + i] < -MathHelper.PiOver2 ? SpriteEffects.FlipVertically : SpriteEffects.None;

				Color color = new Color(alpha * npc.Opacity, alpha * npc.Opacity, alpha * npc.Opacity, alpha * npc.Opacity);

				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/SentinelAether/ZephyrDroneSheet"), npc.Providence().oldCen[i + i] - Main.screenPosition, rect, color, npc.oldRot[i + i], new Vector2(npc.width, npc.height) * 0.5f, scale, effect, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/SentinelAether/ZephyrDroneSheet"), npc.Center - Main.screenPosition, rect, new Color(npc.Opacity, npc.Opacity, npc.Opacity, npc.Opacity), npc.rotation, new Vector2(npc.width, npc.height) * 0.5f, npc.scale, npc.velocity.X < 0 ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
			return false;
		}
	}
}
