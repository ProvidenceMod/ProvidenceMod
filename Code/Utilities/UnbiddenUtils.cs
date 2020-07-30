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
        public static UnbiddenPlayer Unbidden(this Player player)
        {
            return (UnbiddenPlayer) player.GetModPlayer<UnbiddenPlayer>();
        }

        public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
        {
            if (!condition)
            return;
            list.Add(type);
        }
    }
}