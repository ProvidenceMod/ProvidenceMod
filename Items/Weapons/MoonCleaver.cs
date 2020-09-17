using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UnbiddenMod.Items.Weapons
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
            // item.shoot = true; // Commenting this until we have a projectile to shoot
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
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .1f);
                // perturbedSpeed = perturbedSpeed * scale;

                Vector2 vector2 = new Vector2((float) (player.position.X + player.width * 0.5 + -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X)), player.MountedCenter.Y);
                // The Player's X position + the Player's hitbox * 0.5 + negative Player direction + the X of the Mouse -the X of the Player
                // Y = the Mount center of the Player (This raises where the projectiles spawn enough to be on level with the Player)
                // So I assume it gets the position of the player and makes sure the angle adjustments dont get screwed up by adjusting the direction as well
                // I don't know what the hitbox modification is for, but I assume it adds the Mouse position so that the stars spawn relative to the Mouse position

                vector2.X = (float) ((vector2.X + player.Center.X) / 2.0);
                // I don't know why this is taking the X center of the Player and dividing it by 2.0
                // This makes the projectile X spawn position move
                
                vector2.Y += (float) (pos * i);
                // I assume this functions the way I had already made it, where the Y position of the projectile
                // increments with the continuous spawning, except it subtracts the additive position fron the MountedCenter

                
                float x = (float) Main.mouseX + Main.screenPosition.X - vector2.X;
                // X position of the Mouse - the...true center? Of the Player

                float y = (float) Main.mouseY + Main.screenPosition.Y - vector2.Y;
                // Y position of the Mouse

                float hyp = (float) Math.Sqrt((double) x * (double) x + (double) y * (double) y); 
                // Using Pythag to calculate the hypotenuse for a triangle with Mouse X and Mouse Y as the sides

                float num1 = 20;
                // Some sort of speed
                // Used to be float num1 = (float) Main.rand.Next(22, 30);
                // Probably a speed randomizer

                float num8 = num1 / hyp;
                // After some testing I've figured that this takes tbe speed and divides it by the hypotenuse for X and Y of the Mouse
                // We have a Speed of 20, this means that the larger the distance from (0, 0) the Mouse gets, the larger the hypotenuse,
                // which means that this number actually gets...smaller?
                // Right, so this is how it programs in the X spawn position. 
                // The farther the Mouse gets from the player, the less of a distance the X spawn moves
                // Actually, after editing the next variable, it could very well be that this contributes to the projectiles shooting to the Mouse
                // Which leads me to believe that this probably has a value of lower than 1.0f since it is used as a multiplicative

                float x2 = x * num8;
                // This gives the correct X velocity so that the projectiles shoot towards the cursor

                float y2 = y * num8;
                // This gives the correct Y velocity so that the projectiles shoot towards the cursor

                Vector2 speed = new Vector2(speedX, speedY);
                // Vector2 speed = new Vector2(speedX, speedY).RotatedBy((i * 1f) * Main.mouseX);
                // The end part for rotation seems to make no difference surprisingly

                int index3 = Projectile.NewProjectile(vector2.X, vector2.Y, x2, y2, type, damage, knockBack, player.whoAmI, 0.0f, (float) Main.rand.Next(3));
                //Projectile.NewProjectile(position.X, position.Y + (pos * i), speed.X, speed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tModContent to shoot projectile
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
            player.statLife += healingAmount;
            player.HealEffect(healingAmount, true);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/Weapons/MoonCleaver/MoonCleaverGlow"); //loads our glowmask
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