using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod.Projectiles.Magic;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class CirrusEdge : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cirrus Edge");
      Tooltip.SetDefault("Light as a feather");
    }

    public override void SetDefaults()
    {
      item.Providence().element = (int)ElementID.Air;
      item.damage = 46;
      item.width = 44;
      item.height = 44;
      item.useTime = 20;
      item.useAnimation = 20;
      item.scale = 1.5f;
      item.useStyle = ItemUseStyleID.SwingThrow;
      item.rare = ItemRarityID.Orange;
      item.autoReuse = true;
      item.shoot = ProjectileType<ZephyrSpirit>();
      item.shootSpeed = 6f;
      item.UseSound = SoundID.Item1;
    }

    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
        Main.PlaySound(SoundID.Item45, player.position);
        Vector2 velocity = new Vector2(speedX, speedY);
        Projectile.NewProjectile(position, velocity, ProjectileType<ZephyrSpirit>(), 25 + (damage / 2), 0.0f, player.whoAmI);
      return false;
    }
    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);

      r.AddIngredient(ItemType<ZephyrBar>(), 10);
      r.AddIngredient(ItemType<HarpyQueenFeather>(), 3);
      r.AddIngredient(ItemType<HarpyQueenTalon>(), 2);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}