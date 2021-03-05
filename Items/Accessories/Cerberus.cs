using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Aura;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Accessories
{
  public class Cerberus : ModItem
  {
    public int frame;
    public int frameNumber;
    public int frameTick;
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cerberus");
      Tooltip.SetDefault("Material\nA blessing from the guardian of Hell");
    }

    public override void SetDefaults()
    {
      item.width = 34;
      item.height = 34;
      item.accessory = true;
      item.defense = 10;
      item.material = true;
      item.maxStack = 999;
      item.noUseGraphic = true;
      frameNumber = frame;
      item.rare = (int)ProvidenceRarity.Celestial;
    }

    public override void UpdateEquip(Player player)
    {
      ProvidencePlayer proPlayer = player.Providence();
      player.fireWalk = true;
      proPlayer.auraType = AuraType.CerberusAura;
      proPlayer.cerberus = true;
      proPlayer.hasClericSet = true;
      proPlayer.cerberusAura = true;
      proPlayer.dashMod = 1;
      proPlayer.auraStyle = AuraStyle.CerberusStyle;
      Vector2 spawnPosition = player.MountedCenter + new Vector2(proPlayer.clericAuraRadius, 0f);
      if (!proPlayer.cerberusAuraSpawned)
      {
        for (int i = 0; i < 3; i++)
        {
          _ = Projectile.NewProjectile(spawnPosition, new Vector2(0f, 0f), ProjectileType<BurnProjectile>(), 0, 0);
          _ = Projectile.NewProjectile(spawnPosition, new Vector2(0f, 0f), ProjectileType<BurnProjectile2>(), 0, 0);
          _ = Projectile.NewProjectile(spawnPosition, new Vector2(0f, 0f), ProjectileType<BurnProjectile3>(), 0, 0);
        }
        proPlayer.cerberusAuraSpawned = true;
      }
    }

    public override void AddRecipes()
    {
      // shield of cthulhu
      // titanium or adamantite
      // anklet of the wind
      // aglet
      // cloud
      // soul of light
      // soul of night
      // brimstone
      // hellstone bar
      // gold bar or platinum bar
      // ectoplasm
      // chaos catalyst

      // Combine Elemental Essence with Ectoplasm to create am Elemental Catalyst of the same type as the Essence used to craft it. 
      // Combine Elemental Catalysts of opposing elemental types to get Chaos Relics. 
      // Mix Chaos Relics with Lunar Fragments to create Lunar Relics of the same type as the fragment used to craft it.

      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(ItemID.EoCShield, 1);
      recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
      recipe.AddIngredient(ItemID.Aglet, 1);
      recipe.AddIngredient(ItemID.Cloud, 15);
      recipe.AddIngredient(ItemID.SoulofLight, 1);
      recipe.AddIngredient(ItemID.SoulofNight, 1);
      // recipe.AddIngredient(ItemID.Brimstone, 10);
      recipe.AddIngredient(ItemID.HellstoneBar, 10);
      recipe.AddIngredient(ItemID.GoldBar, 5);
      recipe.AddIngredient(ItemID.Ectoplasm, 5);
      recipe.AddTile(TileID.MythrilAnvil);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}