using Terraria;
using Terraria.ModLoader;

namespace UnbiddenMod.Buffs.StatDebuffs
{
  public class Frozen : ModBuff
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
      UnbiddenGlobalNPC modNPC = npc.Unbidden();
      modNPC.frozen = true;
    }
  }
}