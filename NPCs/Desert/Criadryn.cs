using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.NPCs.Desert
{
	public class Criadryn : ModNPC
	{
		private const int volleyTimerUpper = 15, volleyCDUpper = 120;
		private int volleyTimer = 0, volleyCD = volleyCDUpper;
		private Entity fullTarget = null;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Criadryn");
		}
		public override void SetDefaults()
		{
			npc.width = 104;
			npc.height = 64;
			npc.friendly = false;
			npc.defense = 30;
			npc.lifeMax = 350;
			npc.noTileCollide = false;
			npc.knockBackResist = 0.2f;
			npc.HitSound = SoundID.NPCHit1;
		}
		private void CountTimers()
		{
			if (volleyCD <= 0) volleyCD = volleyCDUpper;
			else volleyCD = volleyCDUpper;
			if (volleyTimer >= volleyTimerUpper) volleyTimer = 0;
			else volleyTimer++;
		}
		public override void AI()
		{
			CountTimers();
			fullTarget = npc.ClosestPlayer();
			npc.target = fullTarget.whoAmI;
			float direction = fullTarget.position.X - npc.position.X > 0 ? 1 : -1;
			npc.velocity.X += direction * 0.15f;
			if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y + (npc.height / 2)), 1, 1))
			{
				if (npc.velocity.X > 3f)
					npc.velocity.X = 3f;
				if (npc.velocity.X < -3f)
					npc.velocity.X = -3f;
			}
		}
	}
}