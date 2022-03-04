using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Providence.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using Providence.TexturePack;
using Terraria.Audio;
using ReLogic.Graphics;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Providence
{
	public class ProvidenceGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public int[] oldLife = new int[10];
		public float[] extraAI = new float[4];
		public Vector2[] oldCen = new Vector2[10];

		public bool spawnReset = true;
		public bool maxSpawnsTempSet;
		public int maxSpawnsTemp;

		public float DR;

		// Debuffs
		public bool pressureSpike;

		// Health Bars
		public int comboDMG;
		public int comboBarCooldown = 75;
		public int comboDMGCooldown = 75;
		public int comboDMGCounter = 120;
		public Rectangle comboRect = new(0, 0, 36, 8);

		public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
		{
			if (ProvidenceMod.TexturePack)
			{
				npc.DrawHealthBarCustom(ref comboDMG, ref comboBarCooldown, ref comboDMGCooldown, ref comboDMGCounter, ref scale, ref position, ref comboRect);
				return true;
			}
			return null;
		}
		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= (1f - DR);
			return true;
			//if (armor <= 0)
			//	return true;
			//else
			//{
			//	armor -= damage;
			//  CombatText.NewText(npc.frame, Color.White, (int) damage, false, false);
			//}
			//return false;
		}
		public override void AI(NPC npc)
		{
			if (oldLife[9] == 0)
			{
				for (int i = 0; i < oldLife.Length; i++)
					oldLife[i] = npc.life;
			}
		}
		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			byte buffCount = 0;
			byte buffArrCounter = 0;
			byte debuffArrCounter = 0;
			Texture2D[] buffs = new Texture2D[10];
			Texture2D[] debuffs = new Texture2D[10];
			Vector2 drawPos = npc.position - screenPos;

			foreach (int buffID in npc.buffType)
			{
				if (buffID != 0)
				{
					Texture2D buffTexture = Terraria.GameContent.TextureAssets.Buff[buffID].Value;
					buffCount++;
					if (Main.debuff[buffID])
					{
						debuffs[debuffArrCounter] = buffTexture;
						debuffArrCounter++;
					}
					else
					{
						buffs[buffArrCounter] = buffTexture;
						buffArrCounter++;
					}
				}
			}
			int offset = 0;
			int counter = 0;

			foreach (Texture2D t in buffs)
			{
				if (t != null)
				{
					counter++;
					spriteBatch.Draw(t, drawPos + new Vector2(counter < 6 ? ((npc.width - 80f) / 2f) + offset + 8f : ((npc.width - 80f) / 2f) + (offset - 80f) + 8f, counter < 6 ? npc.height + 40f : npc.height + 56f), new Rectangle(0, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 0.5f, SpriteEffects.None, 0f);
					offset += 16;
				}
			}
			foreach (Texture2D t in debuffs)
			{
				if (t != null)
				{
					counter++;
					spriteBatch.Draw(t, drawPos + new Vector2(counter < 6 ? ((npc.width - 80f) / 2f) + offset + 8f : ((npc.width - 80f) / 2f) + (offset - 80f) + 8f, counter < 6 ? npc.height + 40f : npc.height + 56f), new Rectangle(0, 0, 32, 32), Color.White, 0, new Vector2(16, 16), 0.5f, SpriteEffects.None, 0f);
					offset += 16;
				}
			}
		}
		public string GetBossTitle(string name)
		{
			return name switch
			{
				"Primordial Sentinel" => "Caelus",
				_ => "",
			};
		}
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			// If any subworld from our mod is loaded, disable spawns
			//if (SubworldManager.AnyActive<Providence>())
			//	pool.Clear();
		}
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (player.Providence().intimidated)
			{
				spawnRate *= 3;
				maxSpawns = (int)(maxSpawns * 0.0001);
			}
		}
	}
}