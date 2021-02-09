using Terraria.ID;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Materials
{
	public class BronzeBar : ModItem
	{
    // Delete this later, when we have a texture
		public override void SetStaticDefaults()
		{
      DisplayName.SetDefault("Bronze Bar");
      Tooltip.SetDefault("\"It smells faintly of ozone. And, well, metal.\"");
		}

		public override void SetDefaults()
		{
      item.CloneDefaults(ItemID.IronBar);
		}

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemID.CopperOre, 1);
      r.AddIngredient(ItemID.TinOre, 1);
      r.AddTile(TileID.Furnaces);
      r.SetResult(this, 2);
      r.AddRecipe();
    }
	}
}
