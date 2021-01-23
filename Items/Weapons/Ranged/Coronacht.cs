using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles.Ranged;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Items.Weapons.Ranged
{
    public class Coronacht : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coronacht");
            Tooltip.SetDefault("\"Almost Crashed My PC.\"");
        }

        public override void SetDefaults()
        {
            item.shoot = ModContent.ProjectileType<CoronachtArrow>();
            item.CloneDefaults(ItemID.WoodenBow);
            item.ranged = true;
            item.damage = 55;
            item.scale = 1.1f;
            item.useTime = 9;
            item.useAnimation = 9;
            item.reuseDelay = 12;
            item.autoReuse = true;
            item.knockBack = 5;
            item.shootSpeed = 12f;
            item.rare = ItemRarityID.Cyan;
            item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projs = 4;
            for (int i = 0; i < projs; i++)
            {
                Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(5f.InRadians());
                Projectile.NewProjectile(position, speed, ModContent.ProjectileType<CoronachtArrow>(), damage, knockBack, player.whoAmI);
            }

            //  for (float rotation = Main.rand.NextFloat(-5f, +5f); rotation < 90f; rotation += 45f)
            //  {
            //    Vector2 speed = new Vector2(speedX, speedY).RotateTo(rotation.InRadians());
            //      Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI);
            //  }

            return false;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}