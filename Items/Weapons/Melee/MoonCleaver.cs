using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria.Localization;
using System.Reflection;

namespace UnbiddenMod.Items.Weapons.Melee
{
    public class MoonCleaver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moon Cleaver");
            Tooltip.SetDefault("\"A sword forged from the pure essence of the Cosmos\"");
        }

        public override void SetDefaults()
        {
            item.damage = 450;
            item.width = 90;
            item.height = 90;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 12;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useTurn = true;
            item.useStyle = 1;
            item.scale = 1.0f;
            item.melee = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("MoonBlast");
            item.shootSpeed = 16f;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] split = tt.text.Split(' ');
                tt.text = split.First() + " fire " + split.Last();
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4; // 4 or 5 shots
            float pos = -40f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 vector2 = new Vector2((float) (player.position.X + player.width * 0.5 + -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X)), player.MountedCenter.Y);
                vector2.X = (float) ((vector2.X + player.Center.X) / 2.0);
                vector2.Y += (float) (pos * i); 
                float x = (float) Main.mouseX + Main.screenPosition.X - vector2.X;
                float y = (float) Main.mouseY + Main.screenPosition.Y - vector2.Y;
                float hyp = (float) Math.Sqrt((double) x * (double) x + (double) y * (double) y); 
                float num1 = 20;
                float num8 = num1 / hyp;
                float x2 = x * num8;
                float y2 = y * num8;
                Vector2 speed = new Vector2(speedX, speedY);
                int index3 = Projectile.NewProjectile(vector2.X, vector2.Y, x2, y2, type, damage, knockBack, player.whoAmI, 0.0f, (float) Main.rand.Next(3));
            }
            return false; // return false because we don't want tModContent to shoot projectile
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
            player.statLife += healingAmount;
            player.HealEffect(healingAmount, true);
            // Color cyan = Color.Cyan;
            // string str = ("fire!");
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/Weapons/Melee/MoonCleaverGlow"); //loads our glowmask
            spriteBatch.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - tex.Height * 0.5f + 2f), tex.Frame(), Color.White, rotation, tex.Size() * 0.5f, scale, 0, 0); //draws our glowmask in the inventory. To see how to draw it in the world, see the ModifyDrawLayers method in ExamplePlayer.
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
        

        /*public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.GetTexture("Items/Weapons/MoonCleaver/MoonCleaverGlow"); //loads our glowmask
            spriteBatch.Draw(tex, position, tex.Frame(), Color.White, 0, origin, scale, 0, 0); //draws our glowmask in the inventory. To see how to draw it in the world, see the ModifyDrawLayers method in ExamplePlayer.
        }*/
    }
}