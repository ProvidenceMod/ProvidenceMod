using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Placeables.Ores;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;

namespace ProvidenceMod.Items.Materials
{
	public class ZephyrBar : ModItem
	{
		public int frameNumber;
		public int frameTick;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zephyr Bar");
			Tooltip.SetDefault("Even condensed, it's still as light as air.");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 15));
		}

		public override void SetDefaults()
		{
			Item.material = true;
			Item.width = 66;
			Item.height = 66;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemType<ZephyrOre>(), 4)
				.AddTile(TileID.SkyMill)
				.Register();
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
