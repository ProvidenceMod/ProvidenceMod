using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Ranged
{
  public class Tempest : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Tempest");
      Tooltip.SetDefault("The power of a storm resides within this weapom.");
    }
    public override void SetDefaults()
    {
      item.scale = 1f;
      item.width = 30;
      item.damage = 25;
      item.height = 72;
      item.useTime = 15;
      item.useAnimation = 15;
      item.shootSpeed = 15f;
      item.ranged = true;
      item.noMelee = true;
      item.autoReuse = true;
      item.knockBack = 4.5f;
      item.useAmmo = AmmoID.Arrow;
      item.UseSound = SoundID.Item102;
      item.rare = ItemRarityID.Orange;
      item.value = Item.buyPrice(0, 10, 0, 0);
      item.useStyle = ItemUseStyleID.HoldingOut;
      item.Providence().element = (int)ElementID.Air;
      item.shoot = ProjectileID.WoodenArrowFriendly;
    }
  }
}