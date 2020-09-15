using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod.Items.Weapons
{
    public class PoisonSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Sword");
            Tooltip.SetDefault("\"A sword to poison your enemies\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumBroadsword);
            item.GetGlobalItem<UnbiddenItem>().element = 4; // Poison
            item.autoReuse = true;
            // item.shoot = true; // Commenting this until we have a projectile to shoot
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(5) == 0) // 20% chance
            {
                target.AddBuff(20, 300, true); // Poisoned for 5 seconds
            }
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe.AddIngredient(ItemID.Stinger, 20);
            recipe.AddIngredient(ItemID.JungleSpores, 20);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod

            ModRecipe recipe2 = new ModRecipe(mod);
            
            recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe2.AddIngredient(ItemID.Stinger, 20);
            recipe2.AddIngredient(ItemID.JungleSpores, 20);
            recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe2.SetResult(this); //Sets the result of this recipe to this item
            recipe2.AddRecipe(); //Adds the recipe to the mod
        }
    }
}