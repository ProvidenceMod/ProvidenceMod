using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Weapons.Ranged
{
	public class OnyxDracolith : ModItem
	{
		public int frame;
		public int frameCounter;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Onyx Dracolith");
			Tooltip.SetDefault("It hums with void energy");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(2, 13));
		}
		public override void SetDefaults()
		{
			item.damage = 200;
			item.width = 150;
			item.height = 72;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = (int)ProvidenceRarity.Purple;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useTurn = false;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.scale = 1.0f;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.BlackBolt;
			item.shootSpeed = 10f;
			item.UseSound = SoundID.DD2_PhantomPhoenixShot;
			// item.noUseGraphic = true;
			item.Providence().animated = true;
			item.Providence().animationTexture = GetTexture("ProvidenceMod/Items/Weapons/Ranged/OnyxDracolithAnimated");
      item.Providence().animatedGlowmask = true;
      item.Providence().animatedGlowmaskTexture = GetTexture("ProvidenceMod/Items/Weapons/Ranged/OnyxDracolithGlow");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
                                                                                                                                                  			for (int i = 0; i < 5; i++) {
				_ = Projectile.NewProjectile(player.Center + new Vector2(150f, 0f).RotatedBy(player.AngleTo(Main.MouseWorld)), new Vector2(10f, 0f).RotateTo(player.AngleTo(Main.MouseWorld)).RotatedByRandom(10f.InRadians()), ProjectileID.BlackBolt, item.damage, 1f, player.whoAmI);
			}
			return false;
		}
	}
}