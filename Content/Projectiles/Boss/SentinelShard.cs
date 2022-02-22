using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Globals.Systems;

namespace ProvidenceMod.Projectiles.Boss
{
	public class SentinelShard : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sentinel Shard");
		}
		public override void SetDefaults()
		{
			Projectile.scale = 1f;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.penetrate = 1;
			Projectile.width = 30;
			Projectile.height = 16;
		}
		public override void AI()
		{
			Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), Vector2.Zero, default, default, 5f);
			if (Projectile.ai[1] == 1)
			{
				Projectile.friendly = true;
			}
			else
			{
				Projectile.hostile = true;
				Projectile.velocity.Y += 0.3f;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Request<Texture2D>("ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGore" + Projectile.ai[0]).Value;
			Projectile.width = tex.Width;
			Projectile.height = tex.Height;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), lightColor, Projectile.ai[0] % 2 != 0 ? Projectile.velocity.ToRotation() + MathHelper.PiOver4 : Projectile.velocity.ToRotation(), tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			if (Projectile.friendly)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, Projectile.ai[1], Main.rand.Next(0, 3));
			}
			Projectile.penetrate--;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			if (WorldFlags.wrath && Projectile.hostile)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, Projectile.ai[1], Main.rand.Next(0, 3));
			}
			Projectile.penetrate--;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), speed, default, default, 5f);
				Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f), default, default, 5f);
				Dust.NewDustPerfect(Projectile.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f), default, default, 5f);
			}
			if (WorldFlags.wrath || Projectile.friendly)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, Projectile.ai[1], Main.rand.Next(0, 3));
			}
			return true;
		}
	}
}
