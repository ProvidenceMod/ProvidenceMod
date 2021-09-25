using Terraria;
using Terraria.ID;
namespace ProvidenceMod
{
	public class ProvidenceHooks
	{
		public ProvidenceHooks Instance;
		//public interface IProvidenceGlobalNPC
		//{
		//	int NPC_NewNPC(On.Terraria.NPC.orig_NewNPC orig, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target);
		//}
		// Initialize subscribed methods
		// These are called every time the hooks they're subscribed to are called
		public void Initialize()
		{
			Instance = this;
			On.Terraria.NPC.AddBuff += NPC_AddBuff;
			On.Terraria.NPC.FindBuffIndex += NPC_FindBuffIndex;
			On.Terraria.NPC.DelBuff += NPC_DelBuff;
			On.Terraria.NPC.UpdateNPC_BuffSetFlags += NPC_UpdateNPC_BuffSetFlags;
			On.Terraria.NPC.UpdateNPC_BuffClearExpiredBuffs += NPC_UpdateNPC_BuffClearExpiredBuffs;
			On.Terraria.Main.DrawHealthBar += Main_DrawHealthBar;
		}
		// Unload hooks, aka unsubscribe methods
		public void Unload()
		{
			Instance = null;
			On.Terraria.NPC.AddBuff -= NPC_AddBuff;
			On.Terraria.NPC.FindBuffIndex -= NPC_FindBuffIndex;
			On.Terraria.NPC.DelBuff -= NPC_DelBuff;
			On.Terraria.NPC.UpdateNPC_BuffSetFlags -= NPC_UpdateNPC_BuffSetFlags;
			On.Terraria.NPC.UpdateNPC_BuffClearExpiredBuffs -= NPC_UpdateNPC_BuffClearExpiredBuffs;
			On.Terraria.Main.DrawHealthBar -= Main_DrawHealthBar;
		}
		public virtual void NPC_AddBuff(On.Terraria.NPC.orig_AddBuff orig, NPC self, int type, int time, bool quiet)
		{
			if (self.buffType.Length != 10 || self.buffTime.Length != 10)
			{
				self.buffType = new int[10];
				self.buffTime = new int[10];
			}
			if (self.buffImmune[type])
			{
				return;
			}
			if (!quiet)
			{
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.AddNPCBuff, -1, -1, null, self.whoAmI, type, time, 0f, 0, 0, 0);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.SendNPCBuffs, -1, -1, null, self.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			int num = -1;
			for (int i = 0; i < 10; i++)
			{
				if (self.buffType[i] == type)
				{
					if (self.buffTime[i] < time)
					{
						self.buffTime[i] = time;
					}
					return;
				}
			}
			while (num == -1)
			{
				int num2 = -1;
				for (int j = 0; j < 10; j++)
				{
					if (!Main.debuff[self.buffType[j]])
					{
						num2 = j;
						break;
					}
				}
				if (num2 == -1)
				{
					return;
				}
				for (int k = num2; k < 10; k++)
				{
					if (self.buffType[k] == 0)
					{
						num = k;
						break;
					}
				}
				if (num == -1)
				{
					self.DelBuff(num2);
				}
			}
			self.buffType[num] = type;
			self.buffTime[num] = time;
		}
		public virtual int NPC_FindBuffIndex(On.Terraria.NPC.orig_FindBuffIndex orig, NPC self, int type)
		{
			if (self.buffImmune[type])
			{
				return -1;
			}
			for (int i = 0; i < 10; i++)
			{
				if (self.buffTime[i] >= 1 && self.buffType[i] == type)
				{
					return i;
				}
			}
			return -1;
		}
		public virtual void NPC_DelBuff(On.Terraria.NPC.orig_DelBuff orig, NPC self, int buffIndex)
		{
			self.buffTime[buffIndex] = 0;
			self.buffType[buffIndex] = 0;
			for (int i = 0; i < 10; i++)
			{
				if (self.buffTime[i] == 0 || self.buffType[i] == 0)
				{
					for (int j = i + 1; j < 10; j++)
					{
						self.buffTime[j - 1] = self.buffTime[j];
						self.buffType[j - 1] = self.buffType[j];
						self.buffTime[j] = 0;
						self.buffType[j] = 0;
					}
				}
			}
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.SendNPCBuffs, -1, -1, null, self.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}
		public virtual void NPC_UpdateNPC_BuffSetFlags(On.Terraria.NPC.orig_UpdateNPC_BuffSetFlags orig, NPC self)
		{
			if (self.buffType.Length != 10 || self.buffTime.Length != 10)
			{
				self.buffType = new int[10];
				self.buffTime = new int[10];
			}
			for (int i = 0; i < 10; i++)
			{
				if (self.buffType[i] > 0 && self.buffTime[i] > 0)
				{
					self.buffTime[i]--;
					if (self.buffType[i] == 20)
					{
						self.poisoned = true;
					}
					if (self.buffType[i] == 70)
					{
						self.venom = true;
					}
					if (self.buffType[i] == 24)
					{
						self.onFire = true;
					}
					if (self.buffType[i] == 72)
					{
						self.midas = true;
					}
					if (self.buffType[i] == 69)
					{
						self.ichor = true;
					}
					if (self.buffType[i] == 31)
					{
						self.confused = true;
					}
					if (self.buffType[i] == 39)
					{
						self.onFire2 = true;
					}
					if (self.buffType[i] == 44)
					{
						self.onFrostBurn = true;
					}
					if (self.buffType[i] == 103)
					{
						self.dripping = true;
					}
					if (self.buffType[i] == 137)
					{
						self.drippingSlime = true;
					}
					if (self.buffType[i] == 119)
					{
						self.loveStruck = true;
					}
					if (self.buffType[i] == 120)
					{
						self.stinky = true;
					}
					if (self.buffType[i] == 151)
					{
						self.soulDrain = true;
					}
					if (self.buffType[i] == 153)
					{
						self.shadowFlame = true;
					}
					if (self.buffType[i] == 165)
					{
						self.dryadWard = true;
					}
					if (self.buffType[i] == 169)
					{
						self.javelined = true;
					}
					if (self.buffType[i] == 183)
					{
						self.celled = true;
					}
					if (self.buffType[i] == 186)
					{
						self.dryadBane = true;
					}
					if (self.buffType[i] == 189)
					{
						self.daybreak = true;
					}
					if (self.buffType[i] == 203)
					{
						self.betsysCurse = true;
					}
					if (self.buffType[i] == 204)
					{
						self.oiled = true;
					}
				}
			}
		}
		public virtual void NPC_UpdateNPC_BuffClearExpiredBuffs(On.Terraria.NPC.orig_UpdateNPC_BuffClearExpiredBuffs orig, NPC self)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				for (int i = 0; i < 10; i++)
				{
					if (self.buffType[i] > 0 && self.buffTime[i] <= 0)
					{
						self.DelBuff(i);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.SendNPCBuffs, -1, -1, null, self.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
		}
		public virtual void Main_DrawHealthBar(On.Terraria.Main.orig_DrawHealthBar orig, Main self, float X, float Y, int Health, int MaxHealth, float alpha, float scale)
		{
		}
	}
}
