using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static UnbiddenMod.UnbiddenUtils;
using UnbiddenMod.Items.Materials;
using UnbiddenMod.NPCs.HarpyQueen;

namespace UnbiddenMod.Items.Consumables
{
  public class HarpyCrown : ModItem
  {
    public override string Texture => "Terraria/Item_" + ItemID.GoldCrown;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Harpy's Crown");
      Tooltip.SetDefault("\"You feel the looming presence of Her Majesty holding this crown.\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.SuspiciousLookingEye);
      item.maxStack = 20;
    }

    public override bool CanUseItem(Player player)
    {
      return !IsThereABoss();
    }

    public override bool UseItem(Player player)
    {
      NPC.NewNPC((int)player.position.X, (int)(player.position.Y - (37 * 16)), ModContent.NPCType<HarpyQueen>());
      return true;
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