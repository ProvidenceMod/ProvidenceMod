using Terraria;
using Terraria.ModLoader;

namespace UnbiddenMod.Buffs.StatBuffs
{
  public class TankParryBoost : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("D.R. Boost");
      Description.SetDefault("Increased D.R. from parry absorption!");
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
      player.Unbidden().tankParryOn = true;
    }
  }
}