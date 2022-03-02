using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Buffs.StatDebuffs
{
	public class Intimidated : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Intimidated");
			Description.SetDefault("Enemies will not spawn.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			ProvidencePlayer proPlayer = player.Providence();
			proPlayer.intimidated = true;
		}
	}
}
