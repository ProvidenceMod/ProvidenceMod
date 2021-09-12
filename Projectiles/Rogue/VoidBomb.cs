using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Metaballs;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.Metaballs.MaskManager;

namespace ProvidenceMod.Projectiles.Rogue
{
	public class VoidBomb : ModProjectile, IGalaxySprite
	{
		private NPC contactTarget;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Bomb");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 36;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 3f.InTicks();
			projectile.penetrate = 1;
			//projectile.damage = 50; A soft damage, don't want the proj itself dealing damage
		}
		public override void AI()
		{
			if (projectile.ai[0] == 0)
				ProvidenceMod.Metaballs.FriendlyLayer.Sprites.Add(this);
			projectile.velocity.Y += 0.3f;
			if (projectile.velocity.Y > 16f)
				projectile.velocity.Y = 16f;
			projectile.ai[0]++;
		}
		public override bool PreDraw(SpriteBatch sb, Color color) => false;
		public void DrawGalaxyMappedSprite(SpriteBatch sB)
		{
			if (projectile.type == ProjectileType<VoidBomb>() && projectile.active)
			{
				Texture2D tex = GetTexture("ProvidenceMod/Projectiles/Rogue/VoidBomb");
				sB.Draw(tex, (projectile.Center - Main.screenPosition + new Vector2(0, projectile.gfxOffY)) / 2, new Rectangle(0, 0, projectile.width, projectile.height), Color.White, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), projectile.scale / 2f, SpriteEffects.None, 0);
			}
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			contactTarget = target;
			damage += (int)Math.Floor((decimal)(projectile.timeLeft / 10));
			projectile.Kill();
			projectile.active = false;
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) => projHitbox.Intersects(targetHitbox);
		public override void Kill(int timeLeft)
		{
			for (byte i = 0; i < 15; i++)
			{
				Vector2 randPointWI100f = projectile.Center;
				randPointWI100f.X += Main.rand.NextFloat(-50f, 50f);
				randPointWI100f.Y += Main.rand.NextFloat(-50f, 50f);
				_ = Dust.NewDustPerfect(randPointWI100f, DustType<FriendlyMetaball>(), new Vector2(projectile.velocity.X, -MathHelper.TwoPi), Scale: 5f);
			}
			Player owner = projectile.OwnerPlayer();
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.townNPC && npc.Center.IsInRadiusOf(projectile.position, 100f) && npc != contactTarget)
				{
					npc.StrikeNPC(50 + (int)Math.Floor((decimal)(timeLeft / 10)), 1f, -npc.direction, owner.thrownCrit.PercentChance());
				}
			}
			ProvidenceMod.Metaballs.FriendlyLayer.Sprites.Remove(this);
		}

	}
}