using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProvidenceMod.Items.Materials;
using ProvidenceMod.Projectiles.Magic;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Magic
{
  public class Cumulus : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cumulus");
      Tooltip.SetDefault("Conjures a localized tempest that steadily follows the cursor and slowly pulls in enemies");
      Item.staff[item.type] = true;
    }

    public override void SetDefaults()
    {
      item.width = 52;
      item.height = 52;
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
			item.shoot = ProjectileType<CumulusCloud>();
			item.shootSpeed = 0f;
    }
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player) => player.altFunctionUse != 2;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
			position = Main.MouseWorld;
      return true;
    }

    public override void AddRecipes()
    {
      ModRecipe r = new ModRecipe(mod);

      r.AddIngredient(ItemType<ZephyrBar>(), 10);
      r.AddTile(TileID.SkyMill);
      r.SetResult(this);
      r.AddRecipe();
    }
  }
}