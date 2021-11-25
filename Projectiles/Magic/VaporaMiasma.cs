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
			projectile.width = 16;
			projectile.height = 16;
			projectile.tileCollide = true;
			projectile.magic = true;
			projectile.timeLeft = 3f.InTicks();
		}
		public override void AI()
		{
			if (!rotSet) { RotationForce = Main.rand.NextFloat(-5, 5); rotSet = true; }
			projectile.rotation += RotationForce;

			if (projectile.velocity.Length() > 1f)
				projectile.velocity /= 0.9f;
			else projectile.velocity = Vector2.Zero;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			knockback = 0;
			if (!alreadyDamaged)
				alreadyDamaged = true;
			else damage = 0;
			target.AddBuff(BuffType<Miasma>(), 3f.InTicks());
		}
	}
}
