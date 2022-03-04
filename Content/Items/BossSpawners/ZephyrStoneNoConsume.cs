using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Providence.ProvidenceUtils;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Providence.Content.Items.Materials;
using Providence.Content.NPCs.Caelus;
using Terraria.DataStructures;

namespace Providence.Content.Items.BossSpawners
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
			Item.width = 46;
			Item.height = 44;
			Item.maxStack = 1;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
		}
		public override bool? UseItem(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
				NPC.NewNPC(new EntitySource_BossSpawn(Item), (int)player.Center.X, (int)player.Center.Y - 256, NPCType<Caelus>());
			else
				NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, NPCType<Caelus>(), 0.0f, 0.0f, 0, 0, 0);
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemType<ZephyrBar>(), 10)
				.AddTile(TileID.SkyMill)
				.Register();
		}
	}
}
