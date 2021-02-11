using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Materials
{
	public class ClockworkGear : ModItem
	{
    // Delete this later, when we have a texture
		public override void SetStaticDefaults()
		{
      DisplayName.SetDefault("Clockwork Gear");
      Tooltip.SetDefault("\"You can hear a faint clock ticking if you put this to your ear.\"");
      Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(15, 4));
		}

		public override void SetDefaults()
		{
      item.CloneDefaults(ItemID.IronBar);
      item.width = 16;
      item.height = 16;
		}
    public override bool CanUseItem(Player player) => false;

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);
      r.AddIngredient(ItemType<BronzeBar>(), 1);
      r.AddTile(TileID.MythrilAnvil);
      r.SetResult(this, 2);
      r.AddRecipe();
    }
	}
}
