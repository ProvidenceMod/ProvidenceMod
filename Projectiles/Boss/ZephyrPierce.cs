using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrPierce : ModProjectile
	{
		public int counter = 0;
		public bool fadeOut;
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Pierce");
			Main.projFrames[projectile.type] = 10;
		}
		public override void SetDefaults()
		{
			projectile.width = 56;
			projectile.height = 18;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.damage = 0;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.Opacity = 0f;
			projectile.Providence().element = (int)ElementID.Air; // Typeless
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			if(counter < 20)
			{
				counter++;
				projectile.Opacity += 0.05f;
				color.X += 0.05f;
				color.Y += 0.05f;
				color.Z += 0.05f;
				color.W += 0.05f;
			}
			if(counter == 20)
			{
				projectile.damage = 15;
			}
			if(fadeOut && counter < 40)
			{
				counter++;
				projectile.Opacity -= 0.05f;
				color.X -= 0.05f;
				color.Y -= 0.05f;
				color.Z -= 0.05f;
				color.W -= 0.05f;
			}
			if (++projectile.frameCounter >= 6) // Frame time
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 10) //Frame number
				{
					projectile.frame = 0;
				}
			}
			if(color.W == 0f)
			{
				projectile.Kill();
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			projectile.damage = 0;
			fadeOut = true;
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
