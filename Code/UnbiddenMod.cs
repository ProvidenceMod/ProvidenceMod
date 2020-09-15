using System.IO;
using Terraria;
using Terraria.ModLoader;
using UnbiddenMod.Code.NPCs.FireAncient;
using UnbiddenMod.UI;
using static Terraria.ModLoader.Mod;
using static UnbiddenMod.UnbiddenNPC;
using static UnbiddenMod.UnbiddenPlayer;
using static UnbiddenMod.UnbiddenProjectile;

namespace UnbiddenMod
{
	public class UnbiddenMod : Mod
	{
		public override void Load()
        {
            // this makes sure that the UI doesn't get opened on the server
            // the server can't see UI, can it? it's just a command prompt
            /*if (!Main.dedServ)
            {
                HealthUI = new HealthUI();
                somethingUI.Initialize();
                somethingInterface = new UserInterface();
                somethingInterface.SetState(somethingUI);
            }*/
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) 
        {
			UnbiddenModMessageType msgType = (UnbiddenModMessageType)reader.ReadByte();
			switch (msgType) 
			{
				case UnbiddenModMessageType.FireAncient:
					if (Main.npc[reader.ReadInt32()].modNPC is FireAncient ancient && ancient.npc.active) 
					{
						ancient.HandlePacket(reader);
					}
					break;

				// This message syncs UnbiddenPlayer.tearCount
				case UnbiddenModMessageType.UnbiddenPlayerSyncPlayer:
					byte playernumber = reader.ReadByte();
					UnbiddenPlayer unbiddenPlayer = Main.player[playernumber].GetModPlayer<UnbiddenPlayer>();
					int tearCount = reader.ReadInt32();
					unbiddenPlayer.tearCount = tearCount;
					// SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
					break;

				default:
					Logger.WarnFormat("UnbiddenMod: Unknown Message type: {0}", msgType);
					break;
			}
		}
	}

    internal enum UnbiddenModMessageType : byte
	    {
		    FireAncient,
            UnbiddenPlayerSyncPlayer
	    }
}