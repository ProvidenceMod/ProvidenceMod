using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.Projectiles.ProvidenceGlobalProjectileAI;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.NPCs.PrimordialCaelus
{
	public class ZephyrSentinel : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Sentinel");
		}
		public override void SetDefaults()
		{
			npc.width = 86;
			npc.height = 86;
			npc.Opacity = 0f;
			npc.damage = 25;
			npc.aiStyle = -1;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 200;
			npc.townNPC = false;
			npc.scale = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.chaseable = true;
			npc.knockBackResist = 0f;
		}
		public override void AI()
		{
			if (npc.ai[0] < 60)
			{
				npc.ai[0]++;
				npc.Opacity += 1f / 60f;
			}
			if (npc.ai[0] >= 60)
			{
				npc.Opacity = 1f;
				if (npc.ai[0] % 120 == 0)
				{
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(npc.AngleTo(ProvidenceUtils.ClosestEntity(npc, false).position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int) ZephyrDartAI.WeakHoming, 1);
				}
				npc.ai[0]++;
			}
			if (npc.ai[0] == 600)
			{
				for (double i = MathHelper.PiOver4; i < MathHelper.TwoPi; i += MathHelper.PiOver2)
				{
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(i), ProjectileType<ZephyrDart>(), 25, 2f, default, 6, 1);
				}
				npc.life = 0;
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (double i = MathHelper.PiOver4; i < MathHelper.TwoPi; i += MathHelper.PiOver2)
				{
					Projectile.NewProjectile(npc.Center, new Vector2(10f, 0f).RotatedBy(i), ProjectileType<ZephyrDart>(), 25, 2f, default, 6, 1);
				}
			}
		}
	}
}
