using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Microsoft.Xna.Framework;


namespace Providence.Content.NPCs.Brinewastes
{
	public class RubyRibbontail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby Ribbontail");
		}
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 20;
			NPC.townNPC = false;
			NPC.scale = 1f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.chaseable = true;
			NPC.width = 70;
			NPC.height = 28;
			NPC.knockBackResist = 5f;
		}
		public override void AI()
		{
			NPC.ai[0]++;
			NPC.spriteDirection = NPC.direction;
			// This can be done better
			// npc.rotation = npc.velocity.X >= 0 && npc.velocity.Y <= 0 ? Utils.Clamp(npc.velocity.ToRotation(), 0d.InRadians(), 5d.InRadians()) : Utils.Clamp(npc.velocity.ToRotation(), 355d.InRadians(), 360d.InRadians());

			NPC.rotation = Utils.Clamp(NPC.velocity.Y * 0.05f, -0.5f, 0.5f);

			// Player player = (Player)ClosestEntity(npc, false);
			// if(player.Center.IsInRadiusOf(npc.Center, 256))
			// {

			// }
			// else
			// {

			// }
			if (!NPC.wet)
			{
				NPC.velocity.Y++;
			}
			foreach (Player player in Main.player)
			{
				if (NPC.active && NPC.wet && player.active && !player.dead && player.wet && player.Center.IsInRadiusOf(NPC.Center, 50f))
				{
					NPC.velocity = player.AngleTo(NPC.Center).ToRotationVector2() * 16;
				}
			}
		}
	}
}
