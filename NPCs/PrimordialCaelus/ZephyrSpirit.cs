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
			npc.damage = 10;
			npc.damage = 25;
			npc.aiStyle = -1;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 100;
			npc.townNPC = false;
			npc.scale = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.chaseable = true;
			npc.width = 94;
			npc.height = 64;
			npc.Opacity = 0f;
			npc.knockBackResist = 1f;
		}

		public override void AI()
		{
			if (npc.Opacity < 1f && !fadeOut)
			{
				npc.Opacity += 0.05f;
			}
			if (Main.npc[(int)npc.ai[0]].ai[3] < 120)
			{
				fadeOut = true;
				npc.Opacity -= 1f / 60f;
			}
			if (Main.npc[(int)npc.ai[0]].ai[3] <= 60)
			{
				npc.active = false;
			}
			npc.UpdateCenterCache();
			npc.UpdateRotationCache();
			npc.rotation = Utils.Clamp(npc.velocity.X * 0.05f, -0.5f, 0.5f);
			Player player = (Player)ClosestEntity(npc, false);

			float cos = (float) Math.Cos(Main.GlobalTime * 6f);
			float sin = (float) Math.Sin(Main.GlobalTime * 6f);

			if (npc.velocity.X > 4f && npc.velocity.X < -4f)
				npc.velocity.X += cos * npc.direction * 0.3f;

			if (npc.velocity.Y > 4f && npc.velocity.Y < -4f)
				npc.velocity.Y += sin * npc.direction * 0.3f;

			Vector2 unitY = npc.DirectionTo(new Vector2(player.Center.X + sin * 30f, player.Center.Y + sin * 30f));
			
			npc.velocity = ((npc.velocity * (60f - (60f / Math.Abs(npc.Center.X - player.Center.X)))) + (unitY * 8f)) / ((60f - (60f / Math.Abs(npc.Center.X - player.Center.X))) + 1f);

			Lighting.AddLight(npc.Center, ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Rectangle rect = npc.AnimationFrame(ref frame, ref frameTick, 4, 7, true);
			for (int i = 0; i < 5; i++)
			{
				float alpha = 1f - (i * 0.2f);
				float scale = 1f + (i * 0.1f);

				Color color = new Color(alpha * npc.Opacity, alpha * npc.Opacity, alpha * npc.Opacity, alpha * npc.Opacity);

				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/ZephyrSpiritSheet"), npc.Center - Main.screenPosition, rect, color, npc.oldRot[i + i], npc.frame.Size() * scale / 2, scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/PrimordialCaelus/ZephyrSpiritSheet"), npc.Center - Main.screenPosition, rect, new Color(npc.Opacity, npc.Opacity, npc.Opacity, npc.Opacity), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
				{
					Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
					Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed);
					Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f));
					Dust.NewDustPerfect(npc.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f));
				}
				if (Main.npc[(int)npc.ai[0]] != null)
					Main.npc[(int)npc.ai[0]].ai[2]++;
			}
		}
		public override Color? GetAlpha(Color drawColor) => Color.White;
	}
}
