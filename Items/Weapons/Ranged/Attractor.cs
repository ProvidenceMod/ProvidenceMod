using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria.Localization;

namespace UnbiddenMod.Items.Weapons.Ranged
{
    public class Attractor : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Attractor");
            Tooltip.SetDefault("\"Gimme a hug!\"");
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.width = 54;
            item.height = 16;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 5;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useTurn = false;
            item.useStyle = 5;
            item.scale = 1.0f;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Gel;
            item.shoot = mod.ProjectileType("AttractorSphere"); 
            item.shootSpeed = 10f;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] split = tt.text.Split(' ');
                tt.text = split.First() + " ice " + split.Last();
            }
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.IceTorch, 1);
            recipe.AddIngredient(ItemID.Switch, 1);
            recipe.AddIngredient(ItemID.CobaltBar, 10);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
        

        /*public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.GetTexture("Items/Weapons/MoonCleaver/MoonCleaverGlow"); //loads our glowmask
            spriteBatch.Draw(tex, position, tex.Frame(), Color.White, 0, origin, scale, 0, 0); //draws our glowmask in the inventory. To see how to draw it in the world, see the ModifyDrawLayers method in ExamplePlayer.
        }*/
    }
}