using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Buffs.StatBuffs
{
  public class BoosterShot : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Booster Shot");
      Description.SetDefault("Next potion has double healing");
      Main.debuff[Type] = false;
      Main.pvpBuff[Type] = false;
      Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
      ProvidencePlayer modPlayer = player.Providence();
      modPlayer.boosterShot = true;
    }
  }
}