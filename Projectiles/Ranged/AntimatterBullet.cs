using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;
namespace ProvidenceMod.Projectiles.Ranged
{
	public class AntimatterBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Antimatter Bullet");
		}
		public override void SetDefaults()
		{
			projectile.width = 100;
			projectile.height = 2;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.scale = 1f;
			projectile.hide = false;
			projectile.aiStyle = 0;
			projectile.ranged = true;
			projectile.timeLeft = 30.InTicks();
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();//this is a method, they have to have () <-- those things
			Lighting.AddLight(projectile.position, new Vector3(109,242,196).ColorRGBIntToFloat());
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
					projectile.Kill();
			}
			foreach (NPC npc in Main.npc) {
					if (!npc.friendly)
				{
						float distance = Vector2.Distance(projectile.position, npc.position);
					if (distance <= 256) {
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
					}
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate-=10;
			if (projectile.penetrate <= 0)
			{
					projectile.Kill();
			}
			foreach (NPC npc in Main.npc) {
				if (!npc.friendly)
				{
						float distance = Vector2.Distance(projectile.position, npc.position);
					if (distance <= 256) {
						Vector2 Succ = new Vector2(30f, 0f);
						npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
					}
				}
				
			}
			return false;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}