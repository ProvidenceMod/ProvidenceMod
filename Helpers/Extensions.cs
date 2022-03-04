using Providence.Globals.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Providence
{
	public static class Extensions
	{
		/// <summary>References the ProvidencePlayer instance. Shorthand for ease of use.</summary>
		public static ProvidencePlayer Providence(this Player player) => player.GetModPlayer<ProvidencePlayer>();
		public static ClericPlayer Cleric(this Player player) => player.GetModPlayer<ClericPlayer>();
		public static WraithPlayer Wraith(this Player player) => player.GetModPlayer<WraithPlayer>();
		/// <summary>References the ProvidenceGlobalNPC instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalNPC Providence(this NPC npc) => npc.GetGlobalNPC<ProvidenceGlobalNPC>();
		/// <summary>References the ProvidenceGlobalItem instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalItem Providence(this Item item) => item.GetGlobalItem<ProvidenceGlobalItem>();
		/// <summary>References the ProvidenceGlobalProjectile instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalProjectile Providence(this Projectile proj) => proj.GetGlobalProjectile<ProvidenceGlobalProjectile>();
		/// <summary>References the Player owner of a projectile instance. Shorthand for ease of use.</summary>
		public static Player OwnerPlayer(this Projectile projectile) => Main.player[projectile.owner];
		/// <summary>References the NPC owner of a projectile instance. Shorthand for ease of use.</summary>
		public static NPC OwnerNPC(this Projectile projectile) => Main.npc[projectile.owner];
		public static void AddIfTrue(this List<string> list, bool boolean, string item)
		{
			if (boolean)
				list.Add(item);
		}
		public static void AddIfFalse(this List<string> list, bool boolean, string item)
		{
			if (!boolean)
				list.Add(item);
		}
	}
}
