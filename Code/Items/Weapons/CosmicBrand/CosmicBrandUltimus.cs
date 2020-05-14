using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod.Code.Items.Weapons.CosmicBrand
{
    public class CosmicBrandUltimus : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Brand Ultimus");
            Tooltip.SetDefault("\"A sword forged from the pure essence of the Cosmos\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenSword);
            item.damage = 150;
            item.width = 20;
            item.height = 20;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 12;
            item.useTime = 13;
            item.scale = 1.5f;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("StarBlast");
            // item.shoot = true; // Commenting this until we have a projectile to shoot
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int spread = 10;
            float spreadMult = 0.2f;
            for(int i = 0 ; i < 5 ; i++)
            {
                float width = Screen.PrimaryScreen.WorkingArea.Width;
                float height = Screen.PrimaryScreen.WorkingArea.Height;
                float centerX = width / 2;
                float centerY = height / 2;
                float vX = Main.mouseX - centerX;
                float vY = Main.mouseY - centerY;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
            }
            return false;
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