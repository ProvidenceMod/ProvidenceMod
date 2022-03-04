using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Providence.Content.NPCs.Caelus;

namespace Providence.Content.Items.BossSpawners
{
	public class ZephyrStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Stone");
			Tooltip.SetDefault("Summons Primordial Caelus\nIt's light, it almost feels crushable...");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 44;
			Item.maxStack = 20;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{
			return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
		}
		public override bool? UseItem(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
				NPC.SpawnOnPlayer(player.whoAmI, NPCType<Caelus>());
			else
				NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, NPCType<Caelus>(), 0.0f, 0.0f, 0, 0, 0);
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cloud, 10)
				.AddIngredient(ItemID.RainCloud, 10)
				.AddIngredient(ItemID.SunplateBlock, 20)
				.AddTile(TileID.SkyMill)
				.Register();
		}
	}
}
