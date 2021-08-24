using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProvidenceMod.Subworld
{
  public class TestingSubworldEnter : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.Extractinator;

		public override void SetStaticDefaults() => Tooltip.SetDefault("Use to enter a subworld. Only works with 'SubworldLibrary' Mod enabled");

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 34;
			item.height = 38;
			item.rare = 12;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item1;
		}

		public override bool UseItem(Player player)
		{
			//Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				SubworldManager.Enter(SubworldManager.endlessSeaID);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
  }
}