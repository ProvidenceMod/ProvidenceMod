using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Buffs.DamageOverTime
{
	public class Miasma : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Pressure Spike");
			Description.SetDefault("Lungs are failing");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.Providence().miasma = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.Providence().miasma = true;
		}
	}
}