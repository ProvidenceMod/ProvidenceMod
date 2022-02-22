using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Buffs.DamageOverTime;
using static ProvidenceMod.ProvidenceUtils;
using System.Linq;

namespace ProvidenceMod.Projectiles.Magic
{
	public class VaporaMiasma : ModProjectile
	{
		private float RotationForce;
		private bool rotSet = false;
		private bool alreadyDamaged = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vapora Miasma");
		}
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 3f.InTicks();
		}
		public override void AI()
		{
			if (!rotSet) { RotationForce = Main.rand.NextFloat(-5, 5); rotSet = true; }
			Projectile.rotation += RotationForce;

			if (Projectile.velocity.Length() > 1f)
				Projectile.velocity /= 0.9f;
			else Projectile.velocity = Vector2.Zero;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			knockback = 0;
			if (!alreadyDamaged)
				alreadyDamaged = true;
			else damage = 0;
		}
	}
}
