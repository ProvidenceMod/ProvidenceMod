using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
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
			projectile.width = 40;
			projectile.height = 2;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.scale = 1f;
			projectile.hide = false;
			projectile.aiStyle = 0;
			projectile.ranged = true;
			projectile.timeLeft = 30f.InTicks();
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();//this is a method, they have to have () <-- those things
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
					projectile.Kill();
			}
			foreach (NPC npc in Main.npc) {
				float distance = Vector2.Distance(projectile.position, npc.position);
				if (distance <= 128) {
					Vector2 Succ = new Vector2(30f, 0f);
					npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
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
				float distance = Vector2.Distance(projectile.position, npc.position);
				if (distance <= 128) {
					Vector2 Succ = new Vector2(30f, 0f);
					npc.velocity = Succ.RotateTo(npc.AngleTo(projectile.position));
				}
			}
			return false;
		}
	}
}