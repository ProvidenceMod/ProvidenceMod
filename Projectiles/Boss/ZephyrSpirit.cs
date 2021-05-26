using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Projectiles.Boss
{
	public class ZephyrSpirit : ModProjectile
	{
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public int cooldown = 5;
		public int frame;
		public int frameTick;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Spirit");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 34;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.scale = 1f;
			projectile.damage = 25;
			projectile.friendly = true;
			projectile.Opacity = 0f;
			projectile.Providence().element = -1; // Typeless
			projectile.Providence().homingID = (int)HomingID.Natural;
		}

		public override void AI()
		{
			projectile.ai[1]++;
			projectile.localAI[0]++;
			NPC npc = (NPC)ClosestEntity(projectile, true);
			projectile.Homing(npc, 16f, default, default, 25f, 300f);

			if (projectile.Opacity < 1f)
			{
				projectile.Opacity += color.X += color.Y += color.Z += color.W += 0.05f;
			}
			projectile.rotation = projectile.velocity.ToRotation();
			if (++projectile.frameCounter >= 5) // Frame time
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5) //Frame number
				{
					projectile.frame = 0;
				}
			}
			Dust.NewDust(projectile.TrueCenter(), 6, 6, ModContent.DustType<ColdDust>());
			Dust.NewDust(projectile.TrueCenter(), 6, 6, ModContent.DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			if (cooldown > 0)
				cooldown--;
			if (cooldown == 0)
			{
				cooldown = 5;
			}
			Color lighting = ColorShift(new Color(0, 255, 255), new Color(0, 192, 255), 3f);
			Lighting.AddLight(projectile.Center, lighting.ToVector3());
		}
		// public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		// {
		//   Texture2D texture = ModContent.GetTexture("ProvidenceMod/Projectiles/Magic/ZephyrSpirit");
		//   spriteBatch.Draw(texture, projectile.position - Main.screenPosition, projectile.AnimationFrame(ref frame, ref frameTick, 6, 5, true), Color.White, projectile.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
		//   return false;
		// }
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.OnHitPlayer(target, damage, crit);
			projectile.Kill();
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(color.X, color.Y, color.Z, color.W);
		}
	}
}