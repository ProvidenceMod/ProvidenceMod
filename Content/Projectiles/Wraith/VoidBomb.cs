using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Providence.Globals.Systems.Particles;
using ParticleLibrary;

namespace Providence.Content.Projectiles.Wraith
{
	public class VoidBomb : ModProjectile
	{
		private NPC contactTarget;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Bomb");
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 36;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 3f.InTicks();
			Projectile.penetrate = 1;
			Projectile.damage = 50;
		}
		public override void AI()
		{
			if (Projectile.ai[0] > 30f)
				Projectile.velocity.Y += 0.3f;
			if (Projectile.velocity.Y > 16f)
				Projectile.velocity.Y = 16f;
			Projectile.ai[0] += 1f;
		}
		public override bool PreDraw(ref Color color) {
			ParticleManager.NewParticle(Projectile.Center, new Vector2(Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-1f, 2f)), new Metaball(), Color.White, 0.2f);
			return false;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			contactTarget = target;
			damage += (int)Math.Floor((decimal)(Projectile.timeLeft / 10));
			Projectile.Kill();
			Projectile.active = false;
		}
		public override void Kill(int timeLeft)
		{
			for (byte i = 0; i < 30; i++)
			{
				Vector2 randPointWI100f = Projectile.Center;
				randPointWI100f.X += Main.rand.NextFloat(-50f, 50f);
				randPointWI100f.Y += Main.rand.NextFloat(-50f, 50f);
				ParticleManager.NewParticle(randPointWI100f, new Vector2(Main.rand.NextFloat(-3f, 4f), Main.rand.NextFloat(-3f, 4f)), new Metaball(), Color.White, Main.rand.NextFloat(0.5f, 1f));
				Lighting.AddLight(randPointWI100f, new Vector3(0.5f, 0.5f, 0.5f));
			}
			Player owner = Projectile.OwnerPlayer();
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.townNPC && 
					npc.type != NPCID.DD2EterniaCrystal && npc.type != NPCID.DD2LanePortal &&
					npc.Center.IsInRadiusOf(Projectile.position, 100f) && npc != contactTarget)
				{
					_ = npc.StrikeNPC(50, 1f, -npc.direction, owner.GetCritChance(DamageClass.Throwing).PercentChance());
				}
			}
		}
	}
}
