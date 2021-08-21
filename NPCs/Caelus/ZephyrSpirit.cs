using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Dusts;
using ProvidenceMod.Projectiles.Boss;

namespace ProvidenceMod.NPCs.Caelus
{
	public class ZephyrSpirit : ModNPC
	{
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public Vector2[] oldPos = new Vector2[10] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

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
			npc.width = 42;
			npc.height = 34;
			npc.Opacity = 0f;
			npc.knockBackResist = 0f;
		}

		public override void AI()
		{
			npc.ai[0]++;
			npc.rotation = npc.velocity.ToRotation();
			if (npc.Opacity != 1f)
			{
				npc.Opacity += 0.05f;
				color.X += 0.05f;
				color.Y += 0.05f;
				color.Z += 0.05f;
				color.W += 0.05f;
			}
			oldPos[9] = oldPos[8];
			oldPos[8] = oldPos[7];
			oldPos[7] = oldPos[6];
			oldPos[6] = oldPos[5];
			oldPos[5] = oldPos[4];
			oldPos[4] = oldPos[3];
			oldPos[3] = oldPos[2];
			oldPos[2] = oldPos[1];
			oldPos[1] = oldPos[0];
			oldPos[0] = npc.Center;
			Player player = (Player)ClosestEntity(npc, false);
			Vector2 unitY = npc.DirectionTo(new Vector2(player.Center.X, player.Center.Y));
			npc.velocity = ((npc.velocity * 30f) + (unitY * 3f)) / (30f + 1f);
			Dust.NewDust(new Vector2(npc.Hitbox.X + Main.rand.NextFloat(0, npc.Hitbox.Width + 1), npc.Hitbox.Y + Main.rand.NextFloat(0, npc.Hitbox.Height + 1)), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			Color lighting = ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f);
			Lighting.AddLight(npc.Center, lighting.ToVector3());
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			for (int i = 0; i < 10; i++)
			{
				float alpha = 1f - (i * 0.1f);
				spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/Caelus/ZephyrSpirit"), oldPos[i] - Main.screenPosition, npc.frame, new Color(alpha, alpha, alpha, alpha), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/NPCs/Caelus/ZephyrSpirit"), npc.Center - Main.screenPosition, npc.frame, new Color(1f, 1f, 1f, 1f), npc.rotation, npc.frame.Size() / 2, npc.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override Color? GetAlpha(Color drawColor) => Color.White;
	}
}
