using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Items.Materials;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using ProvidenceMod;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class ClockworkCog : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Clockwork Cog");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.WoodenBoomerang);
      item.damage = 35;
      item.width = 46;
      item.height = 46;
      item.useTime = 13;
      item.useAnimation = 13;
      item.rare = ItemRarityID.Orange;
      item.autoReuse = true;
      item.shoot = ProjectileType<Projectiles.Melee.ClockworkCog>();
      item.shootSpeed = 10f;
    }
    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);

      r.AddIngredient(ItemType<BronzeBar>(), 10);
      r.AddIngredient(ItemID.GoldBar, 3);
      r.AddTile(TileID.MythrilAnvil);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}