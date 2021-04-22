using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Items.Weapons.Joke
{
  public class BonkStick : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Bonk Stick");
      Tooltip.SetDefault("\"Go to horny jail.\"");
    }

    public override void SetDefaults()
    {
      item.CloneDefaults(ItemID.WoodenSword);
      item.damage = 1;
      item.width = 48;
      item.height = 52;
      item.rare = (int)ProvidenceRarity.Developer;
      item.useTime = 5;
      item.useAnimation = 5;
      item.scale = 1.0f;
      item.melee = true;
      item.autoReuse = true;
      item.useTurn = true;
      item.knockBack = 500;
      item.crit = 100;
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
      CombatText combatText = Main.combatText[0];
      combatText.text = "-1";
      combatText.Update();
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
      LegacySoundStyle bonk = "Sounds/NPCHit/bonk".AsLegacy(mod);
      Main.PlaySound(bonk, target.position);
    }
  }
}