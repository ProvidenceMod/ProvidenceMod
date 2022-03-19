using Microsoft.Xna.Framework;
using Providence.Rarities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;

namespace Providence.Content.Projectiles.Magic
{
	public class VoidGrasperBomb : ModProjectile
	{
		public bool MineMode;
		public override string Texture => $"Terraria/Projectile_{ProjectileID.NebulaBlaze1}";
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.NebulaBlaze1);
			MineMode = Projectile.ai[0] == 1;
			Projectile.damage = 750;
			AIType = 0;
		}
		public override void AI()
		{
			Projectile.velocity = Vector2.Zero;
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.townNPC &&
					npc.type != NPCID.DD2EterniaCrystal && npc.type != NPCID.DD2LanePortal &&
					npc.Center.IsInRadiusOf(Projectile.position, 100f))
				{
					Projectile.ai[0] = 2;
				}
			}
			if (Projectile.ai[0] == 2) { Projectile.Kill(); Projectile.active = false; }
		}
		public override void Kill(int timeLeft)
		{
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.townNPC &&
					npc.type != NPCID.DD2EterniaCrystal && npc.type != NPCID.DD2LanePortal &&
					npc.Center.IsInRadiusOf(Projectile.position, 300f))	
				{
					_ = npc.StrikeNPC(750, 1f, -npc.direction, Projectile.OwnerPlayer().GetCritChance(DamageClass.Magic).PercentChance());
				}
			}
		}
	}
}
