using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

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
			projectile.scale = 1f;
			projectile.timeLeft = 300;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.penetrate = 1;
			projectile.width = 30;
			projectile.height = 16;
		}
		public override void AI()
		{
			Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), Vector2.Zero, default, default, 5f);
			if (projectile.ai[1] == 1)
			{
				projectile.friendly = true;
			}
			else
			{
				projectile.hostile = true;
				projectile.velocity.Y += 0.3f;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = GetTexture(projectile.ai[0] == 0 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreA" :
																	projectile.ai[0] == 1 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreB" :
																	projectile.ai[0] == 2 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreC" :
																	projectile.ai[0] == 3 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreD" :
																	projectile.ai[0] == 4 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreE" :
																	projectile.ai[0] == 5 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreF" :
																	projectile.ai[0] == 6 ? "ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreG" :
																	"ProvidenceMod/Gores/PrimordialCaelus/ZephyrSentinelGoreH");
			projectile.width = tex.Width;
			projectile.height = tex.Height;
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), lightColor, projectile.ai[0] % 2 != 0 ? projectile.velocity.ToRotation() + MathHelper.PiOver4 : projectile.velocity.ToRotation(), tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.Item27, projectile.Center);
			if (projectile.friendly)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, projectile.ai[1], Main.rand.Next(0, 3));
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			Main.PlaySound(SoundID.Item27, projectile.Center);
			if (ProvidenceWorld.wrath && projectile.hostile)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, projectile.ai[1], Main.rand.Next(0, 3));
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item27, projectile.Center);
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), speed, default, default, 5f);
				Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f), default, default, 5f);
				Dust.NewDustPerfect(projectile.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f), default, default, 5f);
			}
			if (ProvidenceWorld.wrath || projectile.friendly)
			{
				for (int i = 0; i < 3; i++)
					Projectile.NewProjectile(projectile.Center, new Vector2(4f, 0f).RotatedBy(Main.rand.NextFloat(-MathHelper.Pi, 0f)), ProjectileType<SentinelShrapnel>(), 5, 0f, default, projectile.ai[1], Main.rand.Next(0, 3));
			}
			return true;
		}
	}
}
