using Microsoft.Xna.Framework;
using Providence.Rarities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;

namespace Providence.Content.Items.Weapons.Magic
{
	public class VoidGrasper : ModItem
	{
		private bool MineMode = false;
		private List<int> mines = new List<int>();
		private const byte mineLimit = 5;
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.NebulaBlaze);
			Item.damage = 500;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.rare = RarityType<Developer>();
		}
		public override void UpdateInventory(Player player)
		{
			for (int i = 0; i < mines.Count; i++)
			{
				if (!Main.projectile[mines[i]].active)
				{
					mines.RemoveAt(i);
				}
			}
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				MineMode = !MineMode;
				string status = MineMode ? "En" : "Dis";
				Talk($"Mine Mode: {status}abled", Color.White);
			}
			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (MineMode)
			{
				mines.Add(Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, 0, knockback, default, 1));
				if (mines.Count > 5) { Main.projectile[mines[0]].active = false; mines.RemoveAt(0); } // Remove the oldest
				return false;
			}
			if (player.altFunctionUse == 2) return false;
			return true;
		}
	}
}
