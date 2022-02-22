using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Dusts;
using ProvidenceMod.Projectiles.Boss;
using System;

namespace ProvidenceMod.NPCs.PrimordialCaelus
{
	public class ZephyrSpirit : ModNPC
	{
		public int frame;
		public int frameTick;
		public bool fadeOut;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Spirit");
		}
		public override void SetDefaults()
		{
			NPC.damage = 10;
			NPC.damage = 25;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 100;
			NPC.townNPC = false;
			NPC.scale = 1f;
			NPC.HitSound = SoundID.DD2_LightningBugHurt;
			NPC.DeathSound = SoundID.DD2_LightningBugDeath;
			NPC.chaseable = true;
			NPC.width = 94;
			NPC.height = 64;
			NPC.Opacity = 0f;
			NPC.knockBackResist = 1f;
		}

		public override void AI()
		{
			if (NPC.Opacity < 1f && !fadeOut)
			{
				NPC.Opacity += 0.05f;
			}
			if (Main.npc[(int)NPC.ai[0]].ai[3] < 120)
			{
				fadeOut = true;
				NPC.Opacity -= 1f / 60f;
			}
			if (Main.npc[(int)NPC.ai[0]].ai[3] <= 60)
			{
				NPC.active = false;
			}
			NPC.UpdateCenterCache();
			NPC.UpdateRotationCache();
			//npc.rotation = Utils.Clamp(npc.velocity.X * 0.05f, -0.5f, 0.5f);
			Player player = NPC.ClosestPlayer();

			float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 6f);
			float sin = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f);

			if (NPC.velocity.X > 4f && NPC.velocity.X < -4f)
				NPC.velocity.X += cos * NPC.direction * 0.3f;

			if (NPC.velocity.Y > 4f && NPC.velocity.Y < -4f)
				NPC.velocity.Y += sin * NPC.direction * 0.3f;

			Vector2 unitY = NPC.DirectionTo(new Vector2(player.Center.X + sin * 30f, player.Center.Y + sin * 30f));

			NPC.velocity = ((NPC.velocity * (60f - (60f / Math.Abs(NPC.Center.X - player.Center.X)))) + (unitY * 8f)) / ((60f - (60f / Math.Abs(NPC.Center.X - player.Center.X))) + 1f);

			Lighting.AddLight(NPC.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());

			if (NPC.ai[1] % 10 == 0)
				Dust.NewDustPerfect(NPC.Hitbox.RandomPointInHitbox(), DustType<CloudDust>(), -NPC.velocity);

			NPC.ai[1]++;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Rectangle rect = NPC.AnimationFrame(ref frame, ref frameTick, 4, 7, true);
			for (int i = 0; i < 5; i++)
			{
				float alpha = 1f - (i * 0.2f);
				float scale = 1f + (i * 0.1f);

				Color color = new Color(alpha * NPC.Opacity, alpha * NPC.Opacity, alpha * NPC.Opacity, alpha * NPC.Opacity);

				spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/NPCs/PrimordialCaelus/ZephyrSpiritSheet").Value, NPC.Center - Main.screenPosition, rect, color, NPC.oldRot[i + i], new Vector2(NPC.width, NPC.height) * 0.5f, scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(Request<Texture2D>("ProvidenceMod/NPCs/PrimordialCaelus/ZephyrSpiritSheet").Value, NPC.Center - Main.screenPosition, rect, new Color(NPC.Opacity, NPC.Opacity, NPC.Opacity, NPC.Opacity), NPC.rotation, new Vector2(NPC.width, NPC.height) * 0.5f, NPC.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
				{
					Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
					Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed);
					Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f));
					Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f));
				}
				if (Main.npc[(int)NPC.ai[0]] != null)
					Main.npc[(int)NPC.ai[0]].ai[2]++;
			}
		}
		public override Color? GetAlpha(Color drawColor) => Color.White;
	}
}
