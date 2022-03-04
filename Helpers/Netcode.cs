using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence
{
	public class Netcode : ModSystem
	{
		public static void HandlePacket(Mod mod, BinaryReader reader, int whoAmI)
		{
		}
		public static void SyncWorld()
		{
			if (Main.netMode != NetmodeID.Server)
				return;
			NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
		}
		public enum ProvidenceMessageType : byte
		{

		}
	}
}