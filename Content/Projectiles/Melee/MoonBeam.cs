using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ProvidenceMod.Particles;
using ParticleLibrary;

namespace ProvidenceMod.Projectiles.Melee
{
	public class MoonBeam : ModProjectile
	{
		public override string Texture => "ProvidenceMod/ExtraTextures/EmptyPixel";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Beam");
		}
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 300;
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.Opacity = 0f;
			Projectile.extraUpdates = 100;
			Projectile.aiStyle = 0;
			AIType = 0;
			Projectile.penetrate = 3;
		}
		public override void AI()
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] % 2.5f == 0)
			{
				//Dust.NewDust(projectile.Center, 6, 6, 130, default, default, default, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Green);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Blue);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Yellow);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Pink);
				ParticleManager.NewParticle(Projectile.Center, new Vector2(0f, 1f), new MoonBlastParticle(), Color.White, Main.rand.NextFloat(1f, 4f) / 10f, 0f, 0f, 0f, 0f, Main.rand.NextFloat(20, 61));
			}
			NPC target = Projectile.ClosestNPC();
			if (target?.Distance(Projectile.Center) <= 750f)
			{
				Vector2 unitY = Projectile.DirectionTo(target.Center);
				Projectile.velocity = ((Projectile.velocity * 55f) + (unitY * 4f)) / (55f + 1f);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Projectile.OwnerPlayer();
			int healingAmount = damage / 60 >= player.statLifeMax * 0.5f ? player.statLifeMax / 2 : damage / 60;
			player.statLife += healingAmount;
			player.HealEffect(healingAmount, true);
			Projectile.penetrate--;
			target.immune[Projectile.owner] = 3;
		}
	}
}
