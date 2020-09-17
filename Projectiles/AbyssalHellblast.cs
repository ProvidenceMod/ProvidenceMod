using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UnbiddenMod.Projectiles
{
    public class AbyssalHellblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Hellblast");
        }

        public override void SetDefaults()
        {
            projectile.width = 45;
            projectile.height = 22;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.damage = 100;
            projectile.GetGlobalProjectile<UnbiddenProjectile>().element = 0; // Fire
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[0]];
            projectile.ai[1] += 1f;
			projectile.localAI[0] += 1f;
			SetDirection(npc);
            
        }

        private void SetDirection(NPC npc)
        {
            IList<int> targets = ((NPCs.FireAncient.FireAncient)npc.modNPC).targets;
            bool needsRotation = true;
            if (targets.Count > 0)
            {
                int player = targets[0];
                Vector2 offset = Main.player[player].Center - projectile.Center;
                if (offset != Vector2.Zero)
                {
                    projectile.rotation = (float) Math.Atan2(offset.Y, offset.X);
                    needsRotation = false;
                }
            }
            if (needsRotation) 
            {
				projectile.rotation = -(float)Math.PI / 2f;
			}
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit) 
        {
			target.AddBuff((BuffID.OnFire), 10);
		}
    }
}