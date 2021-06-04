using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.NPCs.AirElemental;

namespace ProvidenceMod.Items.Consumables
{
  public class NonConsumableHarpyCrown : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.GoldCrown;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Harpy's Crown (Infinite)");
      Tooltip.SetDefault("You feel the looming presence of Her Majesty holding this crown.");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.SuspiciousLookingEye);
      item.maxStack = 1;
    }

    public override bool CanUseItem(Player player)
    {
      // No other bosses active and on at least the surface layer
      return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
    }

    public override bool UseItem(Player player)
    {
      _ = NPC.NewNPC((int)player.position.X, (int)(player.position.Y - (37 * 16)), NPCType<AirElemental>());
      return true;
    }
    public override bool ConsumeItem(Player player)
    {
      return false;
    }
    public override void AddRecipes()
    {
      // Recipes here. See Basic Recipe Guide2
      ModRecipe recipe = new ModRecipe(mod);

      recipe.AddIngredient(ItemID.Feather, 3);
      recipe.AddIngredient(ItemID.SunplateBlock, 20);
      recipe.AddTile(TileID.SkyMill); // Ancient Manipulator
      recipe.SetResult(this); //Sets the result of this recipe to this item
      recipe.AddRecipe(); //Adds the recipe to the mod
    }
  }
}