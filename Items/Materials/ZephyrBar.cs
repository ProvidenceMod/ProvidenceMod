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
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 15));
		}

		public override void SetDefaults()
		{
			item.material = true;
			item.width = 66;
			item.height = 66;
			item.rare = (int)ProvidenceRarity.Orange;
			item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			ModRecipe r = new ModRecipe(mod);
			r.AddIngredient(ItemType<ZephyrOre>(), 4);
			r.AddTile(TileID.SkyMill);
			r.SetResult(this);
			r.AddRecipe();
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
	}
}
