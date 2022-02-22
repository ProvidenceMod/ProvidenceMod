using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using System.Linq;

namespace ProvidenceMod.Projectiles.Magic
{
	public class CumulusCloud : ModProjectile
	{
		private byte magicTickCD = 3;
		public override string Texture => $"Terraria/Projectile_{ProjectileID.BloodCloudRaining}";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cumulus Cloud");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BloodCloudRaining);
			Projectile.timeLeft = 15f.InTicks();
		}
		public override bool PreAI()
		{
			// Only one cloud per player
			Player p = Main.player[Projectile.owner];
			if (p.ownedProjectileCounts[Projectile.type] > 1)
			{
				Projectile oldest = Enumerable.Aggregate(Main.projectile, (withShortestLeft, current) =>
				{
					if (withShortestLeft.type == Projectile.type && current.type == Projectile.type && withShortestLeft.owner == current.owner)
					{
						return withShortestLeft.timeLeft > current.timeLeft ? current : withShortestLeft;
					}
					else { return withShortestLeft; }
				});
				oldest.active = false;
			}
			return true;
		}
		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			// If rightclicking and proj is within 1 tile's range of the mouse, don't move, otherwise, creep it towards the mouse.
			if (p.altFunctionUse == 2 && !Projectile.Center.IsInRadiusOf(Main.MouseWorld, 16f))
			{
				Projectile.velocity = new Vector2(6f, 0f).RotateTo(Projectile.AngleTo(Main.MouseWorld));

				p.statMana--;
			}
			else
			{
				Projectile.velocity = Vector2.Zero;
			}
			foreach (NPC npc in Main.npc)
			{
				if (npc.Center.IsInRadiusOf(Projectile.Center, 150f))
				{
					npc.position += new Vector2(0.15f, 0f).RotateTo(npc.AngleTo(Projectile.Center));
				}
			}
		}
	}
}
