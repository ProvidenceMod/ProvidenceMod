using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;

namespace UnbiddenMod.Code.Items
{
    public class StardustUltimus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Ultimus");
            Tooltip.SetDefault("\"A sword forged from the pure essence of the Cosmos\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenSword);
            item.width = 20;
            item.height = 20;
            item.value = 100000;
            item.rare = 12;
            item.useTime = 13;
            item.scale = 1.5f;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("StarBlast");
            // item.shoot = true; // Commenting this until we have a projectile to shoot
        }

        public bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int spread = 10;
            float spreadMult = 0.2f;
            for(int i = 0 ; i < 5 ; i++)
            {
                float vX = speedX +(float)Main.rand.Next(-spread,spread+1) * spreadMult;
                float vY = speedY +(float)Main.rand.Next(-spread,spread+1) * spreadMult;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
            }
            return false;
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.DirtBlock, 10); //Adds ingredients
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
        public override bool Shoot()
        {
            return true;

        }
    }
}