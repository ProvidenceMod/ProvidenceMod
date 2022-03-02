using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.Rarities.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.Projectiles.ProvidenceGlobalProjectileAI;
using static Terraria.ModLoader.ModContent;
using Providence.Content.Dusts;
using Providence.Content.Projectiles.Boss;

namespace Providence.Content.NPCs.PrimordialCaelus
{
	public class ZephyrSentinel : ModNPC
	{
		public int frame;
		public int frameTick;
		public float rotation;
		public Rectangle rect = new Rectangle(0, 0, 118, 118);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Sentinel");
		}
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.width = 118;
			NPC.height = 118;
			NPC.Opacity = 0f;
			NPC.damage = 25;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 200;
			NPC.townNPC = false;
			NPC.scale = 1f;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.chaseable = true;
			NPC.knockBackResist = 0f;
		}
		public override void AI()
		{
			NPC.position = Main.npc[(int)NPC.ai[1]].Center + new Vector2(128f, 0f).RotatedBy(NPC.ai[2]).RotatedBy(rotation.InRadians());
			rotation += 4;
			if (NPC.ai[0] < 60)
			{
				NPC.ai[0]++;
				NPC.Opacity += 1f / 60f;
			}
			if (NPC.ai[0] >= 60)
			{
				NPC.Opacity = 1f;
				if (NPC.ai[0] % 120 == 0)
				{
					for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
					{
						Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
						Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed);
						Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f));
						Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f));
					}
					Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(10f, 0f).RotatedBy(NPC.AngleTo(NPC.ClosestPlayer().position)), ProjectileType<ZephyrDart>(), 25, 2f, default, (int)ZephyrDartAI.Normal, 1);
				}
				NPC.ai[0]++;
			}
			if (NPC.ai[0] == 600)
			{
				NPC.life = 0;
				HitEffect(0, 0);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			spriteBatch.Draw(Request<Texture2D>("Providence/NPCs/PrimordialCaelus/ZephyrSentinelSheet").Value, NPC.position - Main.screenPosition, NPC.AnimationFrame(ref frame, ref frameTick, 5, 20, true), new Color(NPC.Opacity, NPC.Opacity, NPC.Opacity, NPC.Opacity), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life > 0)
				return;
			for (float i = 0; i < MathHelper.TwoPi; i += MathHelper.PiOver4)
			{
				Vector2 speed = new Vector2(0f, 4f).RotatedBy(i);
				Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed, default, default, 5f);
				Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / 2f), default, default, 5f);
				Dust.NewDustPerfect(NPC.Center, DustType<CloudDust>(), speed.RotatedBy(i / -2f), default, default, 5f);
			}
			if (WorldFlags.lament && !WorldFlags.wrath)
				for (int i = 0; i < 8; i += 2)
					Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(0f, 10f).RotatedBy(i * MathHelper.PiOver4), ProjectileType<SentinelShard>(), 25, 0f, default, i);
			if (WorldFlags.wrath)
				for (int i = 0; i < 8; i++)
					Projectile.NewProjectile(new ProjectileSource_NPC(NPC), NPC.Center, new Vector2(0f, 10f).RotatedBy(i * MathHelper.PiOver4), ProjectileType<SentinelShard>(), 25, 0f, default, i);
		}
	}
}
