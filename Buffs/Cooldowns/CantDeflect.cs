using Terraria;
using Terraria.ModLoader;

namespace UnbiddenMod.Buffs.Cooldowns
{
  public class CantDeflect : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Can't Deflect");
      Description.SetDefault("Waiting for recharge...");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
      longerExpertDebuff = false;
    }
    public override void Update(Player player, ref int buffIndex)
    {
      player.Unbidden().cantdeflect = true;
    }
  }
}