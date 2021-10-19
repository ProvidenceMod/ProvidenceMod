using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Buffs.DamageOverTime
{
  public class PressureSpike : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Pressure Spike");
      Description.SetDefault("Imploding");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
    }
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.Providence().pressureSpike = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.Providence().pressureSpike = true;
		}
	}
}