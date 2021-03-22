using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ProvidenceMod.Items.Weapons.Melee
{
  public class Ugadachi : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ugadachi");
      Tooltip.SetDefault("Holding this weapon activated Petal Mode.\nPrimary attack builds petals, secondary releases all of them\nPetals increase the amount of damage you deal on your next successful hit.");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.WoodenSword);
      item.damage = 150;
      item.width = 48;
      item.height = 52;
      item.rare = (int)ProvidenceRarity.Celestial;
      item.useTime = 13;
      item.useAnimation = 13;
      item.scale = 1.0f;
      item.melee = true;
      item.autoReuse = true;
      item.useTurn = true;
    }
    public override bool AltFunctionUse(Player player) => true;
    public override bool CanUseItem(Player player)
    {
      return (player.altFunctionUse == 2 && player.Providence().petalCount > 0) || player.altFunctionUse != 2;
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
      ProvidencePlayer p = player.Providence();
      if (player.altFunctionUse == 2)
      {
        if (p.petalCount == 8) damage = int.MaxValue;
        else damage += item.damage * p.petalCount;
        p.petalCount = 0;
      }
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      if (player.altFunctionUse != 2)
      {
        player.Providence().petalCount++;
      }
    }
  }
}