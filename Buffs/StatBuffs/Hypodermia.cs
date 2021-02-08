using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Buffs.StatBuffs
{
  public class Hypodermia : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Hypodermia");
      Description.SetDefault("20% increased damage");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
      longerExpertDebuff = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
      ProvidenceGlobalNPC modNPC = npc.Providence();
      modNPC.hypodermia = true;
    }
  }
}