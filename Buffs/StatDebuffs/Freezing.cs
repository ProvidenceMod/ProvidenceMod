using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Buffs.StatDebuffs
{
  public class Freezing : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Freezing");
      Description.SetDefault("Slowing down!");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
      longerExpertDebuff = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
      ProvidenceGlobalNPC modNPC = npc.Providence();
      modNPC.freezing = true;
    }
  }
}