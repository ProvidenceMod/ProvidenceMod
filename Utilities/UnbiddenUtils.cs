using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using UnbiddenMod.Buffs;

namespace UnbiddenMod
{
    public static class UnbiddenUtils
    {
        public static UnbiddenPlayer Unbidden(this Player player) => (UnbiddenPlayer) player.GetModPlayer<UnbiddenPlayer>();
        public static UnbiddenGlobalNPC Unbidden(this NPC npc) => (UnbiddenGlobalNPC) npc.GetGlobalNPC<UnbiddenGlobalNPC>();
        public static UnbiddenGlobalItem Unbidden(this Item item) => (UnbiddenGlobalItem) item.GetGlobalItem<UnbiddenGlobalItem>();
        public static UnbiddenGlobalProjectile Unbidden(this Projectile proj) => (UnbiddenGlobalProjectile) proj.GetGlobalProjectile<UnbiddenGlobalProjectile>();

        public static Vector2 RandomPointInHitbox(this Rectangle hitbox) 
        {
            Vector2 v = new Vector2();
            int semiAxisX = Main.rand.Next(hitbox.Left, hitbox.Right), 
                semiAxisY = Main.rand.Next(hitbox.Top, hitbox.Bottom);
            v.X = semiAxisX;
            v.Y = semiAxisY;
            return v;
        }

        public static void Parry(Player player, Rectangle hitbox)
        {
            int NoOfProj = Main.projectile.Length;
			int affectedProjs = 0;
			for (int i = 0; i < NoOfProj; i++)
			{
				Projectile currProj = Main.projectile[i];
				if (!player.HasBuff(ModContent.BuffType<CantDeflect>()) && currProj.active && currProj.hostile && hitbox.Intersects(currProj.Hitbox))
				{
					// Add your melee damage multiplier to the damage so it has a little more oomph
					currProj.damage = (int)(currProj.damage * player.meleeDamageMult);
					
					// If Micit Bangle is equipped, add that multiplier.
					currProj.damage = player.Unbidden().micitBangle ? (int)(currProj.damage * 2.5) : currProj.damage;
					// Convert the proj so you own it and reverse its trajectory
					currProj.owner = player.whoAmI;
					currProj.hostile = false;
					currProj.friendly = true;
					currProj.Unbidden().deflected = true;
					currProj.velocity.X = -currProj.velocity.X;
					currProj.velocity.Y = -currProj.velocity.Y;
					affectedProjs++;
				}
			}
			if (affectedProjs > 0)
			{
				// Give a cooldown; 1 second per projectile reflected
				// CantDeflect is a debuff, separate from this code block
				player.AddBuff(ModContent.BuffType<CantDeflect>(), affectedProjs * 60, true);
			}
        }
        public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
        {
            if (!condition)
            return;
            list.Add(type);
        }
    }
}