using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod.Items.Weapons.Melee
{
    public class FireSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Sword");
            Tooltip.SetDefault("\"A sword of pure flame\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumBroadsword);
            item.GetGlobalItem<UnbiddenGlobalItem>().element = 0; // Fire
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 15;
            // item.shoot = true; // Commenting this until we have a projectile to shoot
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(10) == 0) // 10% chance
            {
                target.AddBuff(BuffID.OnFire, 300, true); // Burn for 5 seconds
            }
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);
        
            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe.AddIngredient(ItemID.Torch, 25);
            recipe.AddIngredient(ItemID.Gel, 99);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod

            ModRecipe recipe2 = new ModRecipe(mod);
            
            recipe2.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe2.AddIngredient(ItemID.Torch, 25);
            recipe2.AddIngredient(ItemID.Gel, 99);
            recipe2.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe2.SetResult(this); //Sets the result of this recipe to this item
            recipe2.AddRecipe(); //Adds the recipe to the mod
        }
    }
}