using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Dusts;
using ProvidenceMod.Projectiles.Boss;


namespace ProvidenceMod.NPCs.EndlessSea
{
	public class RubyRibbontail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby Ribbontail");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lifeMax = 20;
			npc.townNPC = false;
			npc.scale = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.chaseable = true;
			npc.width = 70;
			npc.height = 28;
			npc.Opacity = 0f;
			npc.knockBackResist = 5f;
		}
		public override void AI()
		{
			npc.ai[0]++;
			npc.spriteDirection = npc.direction;
			npc.rotation = npc.velocity.X >= 0 && npc.velocity.Y <= 0 ? Utils.Clamp(npc.velocity.ToRotation(), 0f.InRadians(), 5F.InRadians()) : Utils.Clamp(npc.velocity.ToRotation(), 355f.InRadians(), 360F.InRadians());
			Player player = (Player)ClosestEntity(npc, false);
			bool isInRange = player.Center.IsInRadiusOf(npc.Center, 256);
			if(isInRange)
			{

			}
			else
			{

			}
		}
	}
}
