using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod.Code.Items.Weapons.FieryBlade
{
    public class FieryBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fiery Blade");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumBroadsword);
            item.GetGlobalItem<UnbiddenItem>().element = 0; // Fire
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
          if (Main.rand.Next(10) == 0) // 10% chance
          {
            target.AddBuff(BuffID.OnFire, 300, true);
          }
          
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.DirtBlock, 10); //Adds ingredients
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}