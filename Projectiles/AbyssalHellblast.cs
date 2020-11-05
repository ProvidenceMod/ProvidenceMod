using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UnbiddenMod.NPCs.FireAncient;

namespace UnbiddenMod.Projectiles
{
    public class AbyssalHellblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Hellblast");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            projectile.width = 45;
            projectile.height = 22;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.damage = 100;
            projectile.hostile = true;
            projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Fire
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[0]];
            projectile.ai[1] += 1f;
            projectile.localAI[0] += 1f;
            SetDirection(npc);
            // int npcType = ModContent.NPCType<NPCs.FireAncient.FireAncient>();
            // FireAncient npc2 = (FireAncient)ModContent.GetModNPC(npcType);
            // IList<int> targets = npc2.targets;
            IList<int> targets = ((FireAncient)npc.modNPC).targets;
            int player2 = targets[0];
            Player player = Main.player[player2];
            /*Vector2 directionTo = projectile.DirectionTo(player.Center);
            projectile.velocity.Y = directionTo.Y * 10f;
            projectile.velocity.X = directionTo.X * 10f;
            */
            if (++projectile.frameCounter >= 3) // Frame time
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 9) //Frame number
                {
                    projectile.frame = 0;
                }
            }
            //Vector2 offset = Main.player[player].position - projectile.position;
            /*////
            if(offset.X > 0)
                projectile.velocity.X += 0.1f;
            else if (offset.X < 0)
                projectile.velocity.X -= 0.1f;
            else if (offset.X == 0)
                projectile.velocity.X = 0f;
            /////
            if(offset.Y > 0)
                projectile.velocity.Y += 0.1f;
            else if (offset.Y < 0)
                projectile.velocity.Y -= 0.1f;
            else if (offset.Y == 0)
                projectile.velocity.Y = 0f;
            */
        }

        private void SetDirection(NPC npc)
        {
            // int npcType = ModContent.NPCType<NPCs.FireAncient.FireAncient>();
            // FireAncient npc2 = (FireAncient)ModContent.GetModNPC(npcType);
            // IList<int> targets = npc2.targets;
            IList<int> targets = ((FireAncient)npc.modNPC).targets;
            bool needsRotation = true;
            if (targets.Count > 0)
            {
                int player = targets[0];
                Vector2 offset = Main.player[player].Center - projectile.Center;
                if (offset != Vector2.Zero)
                {
                    projectile.rotation = (float)Math.Atan2(offset.Y, offset.X);
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
            UnbiddenPlayer unbiddenPlayer = target.Unbidden();
            int projEl = projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element;
            if (projEl != -1) // if not typeless (and implicitly within 0-6)
            {
                float damageFloat = (float)damage, // And the damage we already have, converted to float
                resistMod = unbiddenPlayer.resists[projEl];
                if (resistMod > 0f)
                {
                    damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
                    damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
                }
                else
                {
                    damage = 1;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.White;
            return (color);
        }
    }
}