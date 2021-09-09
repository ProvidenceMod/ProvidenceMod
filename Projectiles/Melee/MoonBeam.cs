using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
using Terraria.ID;
using Microsoft.Xna.Framework;
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
			projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.Opacity = 0f;
			projectile.extraUpdates = 100;
			projectile.aiStyle = 0;
			aiType = 0;
			projectile.penetrate = 3;
		}
		public override void AI()
		{
			projectile.ai[0]++;
			if (projectile.ai[0] % 2.5f == 0)
			{
				Dust.NewDust(projectile.Center, 6, 6, 130, default, default, default, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Green);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Blue);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Yellow);
				//Dust.NewDust(projectile.Center, 6, 6, DustID.Firework_Pink);
			}
			NPC target = (NPC)ClosestEntity(projectile, true);
			if (target?.Distance(projectile.Center) <= 750f)
			{
				Vector2 unitY = projectile.DirectionTo(target.Center);
				projectile.velocity = ((projectile.velocity * 55f) + (unitY * 4f)) / (55f + 1f);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = projectile.OwnerPlayer();
			int healingAmount = damage / 60 >= player.statLifeMax * 0.5f ? player.statLifeMax / 2 : damage / 60;
			player.statLife += healingAmount;
			player.HealEffect(healingAmount, true);
			projectile.penetrate--;
			target.immune[projectile.owner] = 3;
		}
	}
}
