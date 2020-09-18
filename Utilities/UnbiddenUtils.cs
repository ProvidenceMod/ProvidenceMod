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

namespace UnbiddenMod
{
    public static class UnbiddenUtils
    {
        public static UnbiddenPlayer Unbidden(this Player player) => (UnbiddenPlayer) player.GetModPlayer<UnbiddenPlayer>();
        public static UnbiddenGlobalNPC Calamity(this NPC npc) => (UnbiddenGlobalNPC) npc.GetGlobalNPC<UnbiddenGlobalNPC>();
        public static UnbiddenGlobalItem Calamity(this Item item) => (UnbiddenGlobalItem) item.GetGlobalItem<UnbiddenGlobalItem>();
        public static UnbiddenGlobalProjectile Calamity(this Projectile proj) => (UnbiddenGlobalProjectile) proj.GetGlobalProjectile<UnbiddenGlobalProjectile>();


        public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
        {
            if (!condition)
            return;
            list.Add(type);
        }
    }
}