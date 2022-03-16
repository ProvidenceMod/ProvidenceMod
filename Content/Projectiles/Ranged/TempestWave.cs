using Microsoft.Xna.Framework;
using Providence.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Projectiles.Ranged
{
	public class TempestWave : ModProjectile
	{
		public bool fadeOut;
		public Vector4 color = new Vector4(0f, 0f, 0f, 0f);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tempest Wave");
			Main.projFrames[Projectile.type] = 11;
		}
		public override void SetDefaults()
		{
			Projectile.damage = 10;
			Projectile.width = 78;
			Projectile.height = 28;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 300;
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.damage = 0;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.Opacity = 0f;
			Projectile.Providence().element = (int)ProvidenceEnums.ElementID.Air; // Typeless
		}
		public override void AI()
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] == 3)
			{
				Projectile.ai[0] = 0;
				Dust.NewDust(new Vector2(Projectile.Hitbox.X + Main.rand.NextFloat(0, Projectile.Hitbox.Width + 1), Projectile.Hitbox.Y + Main.rand.NextFloat(0, Projectile.Hitbox.Height + 1)), 5, 5, DustType<CloudDust>(), Main.rand.NextFloat(-1f, 2f), Main.rand.NextFloat(-3f, 4f), default, Color.White, 3f);
			}
			Projectile.rotation = Projectile.velocity.ToRotation();
			Lighting.AddLight(Projectile.Center, ProvidenceColor.ColorShift(new Color(71, 74, 145), new Color(114, 164, 223), 3f).ToVector3());
			if (Projectile.ai[1] < 20)
			{
				Projectile.ai[1]++;
				Projectile.Opacity += 0.05f;
				color.X += 0.05f;
				color.Y += 0.05f;
				color.Z += 0.05f;
				color.W += 0.05f;
			}
			if (Projectile.ai[1] == 20)
			{
				Projectile.damage = 50;
			}
			if (Projectile.timeLeft == 20)
			{
				fadeOut = true;
			}
			if (fadeOut)
			{
				Projectile.ai[1]++;
				Projectile.Opacity -= 0.05f;
				color.X -= 0.05f;
				color.Y -= 0.05f;
				color.Z -= 0.05f;
				color.W -= 0.05f;
			}
			if (++Projectile.frameCounter >= 6) // Frame time
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 11) //Frame number
				{
					Projectile.frame = 0;
				}
			}
			if (color.W == 0f)
			{
				Projectile.Kill();
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			Projectile.damage = 0;
			fadeOut = true;
		}
		public override Color? GetAlpha(Color lightColor) => new Color(color.X, color.Y, color.Z, color.W);
	}
}
