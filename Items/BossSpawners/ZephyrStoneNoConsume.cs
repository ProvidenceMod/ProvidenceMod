using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.NPCs.PrimordialCaelus;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;

namespace ProvidenceMod.Items.BossSpawners
{
	public class ZephyrStoneNoConsume : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Stone");
			Tooltip.SetDefault("Not Consumable\nSummons Primordial Caelus\nIt's light, it almost feels crushable...");
		}
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 38;
			item.maxStack = 1;
			item.rare = (int)ProvidenceRarity.Orange;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
		}
		public override bool UseItem(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.SpawnOnPlayer(player.whoAmI, NPCType<PrimordialCaelus>());
				Talk("Primordial Caelus has awoken!", new Color(56, 196, 166), NPCType<PrimordialCaelus>());
			}
			else
			{
				NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, NPCType<PrimordialCaelus>(), 0.0f, 0.0f, 0, 0, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<ZephyrBar>(), 10);
			recipe.AddTile(TileID.SkyMill);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}