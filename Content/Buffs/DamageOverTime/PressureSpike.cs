using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Buffs.DamageOverTime
{
	public class PressureSpike : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(0, "Pressure Spike");
			Description.AddTranslation(0, "Imploding");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.Providence().pressureSpike = true;
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			npc.lifeRegen -= 5;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.Providence().pressureSpike = true;
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.lifeRegen -= 3;
		}
	}
}