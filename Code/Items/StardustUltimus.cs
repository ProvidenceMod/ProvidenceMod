using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod.Code.Items
{
    public class StardustUltimus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Ultimus");
            Tooltip.SetDefault("A sword forged from the pure essence of the Cosmos");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenSword);
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 100000;
            item.rare = 12;
            item.useTime = 13;
            item.scale = 1.5f;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = true;
            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void Shoort()
        {

            
        }
    }
}