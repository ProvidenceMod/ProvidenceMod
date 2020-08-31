using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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
            item.damage = 150;
            item.width = 20;
            item.height = 20;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 12;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useTurn = true;
            item.useStyle = 1;
            item.scale = 1.0f;
            item.melee = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("StarBlast");
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
            float pos = 40f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .1f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y - (pos * i), perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tModContent to shoot projectile
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
            player.statLife += healingAmount;
            player.HealEffect(healingAmount, true);
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
            Texture2D tex = ModContent.GetTexture("Code/Items/Weapons/CosmicBrand/CosmicBrandUltimusGlowmask"); //loads our glowmask
            spriteBatch.Draw(tex, position, tex.Frame(), Color.White, 0, origin, scale, 0, 0); //draws our glowmask in the inventory. To see how to draw it in the world, see the ModifyDrawLayers method in ExamplePlayer.
        }*/
    }
}