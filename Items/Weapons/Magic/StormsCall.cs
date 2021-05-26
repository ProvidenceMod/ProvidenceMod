using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static ProvidenceMod.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Projectiles.Boss;
using ProvidenceMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Boss;

namespace ProvidenceMod.Items.Weapons.Magic
{
  public class StormsCall : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Storm's Call");
      Tooltip.SetDefault("Shoots slow-moving but homing harpy feathers.\nThe down you used to make the crown is heavier then the rod itself.");
      Item.staff[item.type] = true;
    }

    public override void SetDefaults()
    {
      item.width = 64;
      item.height = 34;
      item.value = Item.buyPrice(0, 10, 0, 0);
      item.rare = 12;
      item.useStyle = ItemUseStyleID.HoldingOut;
      item.useTime = 20;
      item.useAnimation = 20;
      item.scale = 1.0f;
      item.magic = true;
      item.mana = 6;
      item.autoReuse = true;
      item.rare = ItemRarityID.Orange;
      item.UseSound = SoundID.Item45;
      item.Providence().element = (int)ElementID.Air; // Fire
      item.shoot = ProjectileType<ZephyrSpirit>();
      item.shootSpeed = 6f;
    }
    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
      for (float index = -10; index < 10; index += 10)
      {
        Vector2 velocity = new Vector2(speedX, speedY).RotatedBy(index.InRadians());
        Projectile.NewProjectile(position, velocity, ProjectileType<ZephyrSpirit>(), 25, 0.0f, player.whoAmI);
      }
      return false;
    }

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);

      r.AddIngredient(ItemType<ZephyrBar>(), 10);
      r.AddIngredient(ItemType<HarpyQueenFeather>(), 3);
      r.AddIngredient(ItemType<HarpyQueenTalon>(), 3);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}