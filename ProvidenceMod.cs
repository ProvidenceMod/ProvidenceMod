using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using ProvidenceMod.NPCs.FireAncient;
using ProvidenceMod.UI;
using ProvidenceMod.TexturePack;
using static ProvidenceMod.TexturePack.ProvidenceTextureManager;
using ProvidenceMod.NPCs.AirElemental;
using ReLogic.Graphics;
using ProvidenceMod.Items.Weapons.Melee;

namespace ProvidenceMod
{
	public class ProvidenceMod : Mod
	{
		internal static ProvidenceMod Instance;
		public static DynamicSpriteFont providenceFont;
		//public static ModHotKey ParryHotkey;
		public static ModHotKey CycleParity;
		//private UserInterface elemDefUI;
		//private UserInterface focusUI;
		private UserInterface bossHealthUI;
		private UserInterface parityUI;
		internal ParityUI ParityUI;
		//internal ElemDefUI ElemDefUI;
		//internal FocusUI FocusUI;
		internal BossHealth BossHealth;
		public bool texturePackEnabled;

		public override void Load()
		{
			Instance = this;
			if (!Main.dedServ)
			{
				//ElemDefUI = new ElemDefUI();
				//ElemDefUI.Initialize();
				//elemDefUI = new UserInterface();
				//elemDefUI.SetState(ElemDefUI);

				//FocusUI = new FocusUI();
				//FocusUI.Initialize();
				//focusUI = new UserInterface();
				//focusUI.SetState(FocusUI);

				BossHealth = new BossHealth();
				BossHealth.Initialize();
				bossHealthUI = new UserInterface();
				bossHealthUI.SetState(BossHealth);

				ParityUI = new ParityUI();
				ParityUI.Initialize();
				parityUI = new UserInterface();
				parityUI.SetState(ParityUI);

				//ParryHotkey = RegisterHotKey("Parry", "F");
				CycleParity = RegisterHotKey("Cycle Parity Element", "C");

				if (FontExists("Fonts/ProvidenceFont"))
				{
					providenceFont = GetFont("Fonts/ProvidenceFont");
				}
			}
		}
		public override void Unload()
		{
			//ElemDefUI = null;
			//FocusUI = null;
			BossHealth = null;
			ParityUI = null;
			/*elemDefUI = focusUI = */
			bossHealthUI = parityUI = null;
			/*ParryHotkey = */
			CycleParity = null;
			ModContent.GetInstance<ProvidencePlayer>().texturePackEnabled = false;
			ModContent.GetInstance<ProvidenceTile>().texturePackEnabled = false;
			ModContent.GetInstance<ProvidenceGlobalProjectile>().texturePackEnabled = false;
			ModContent.GetInstance<ProvidenceGlobalNPC>().texturePackEnabled = false;
			ModContent.GetInstance<ProvidenceGlobalItem>().texturePackEnabled = false;
			ModContent.GetInstance<ProvidenceGlobalItem>().Unload();
			ModContent.GetInstance<ProvidenceWall>().texturePackEnabled = false;
			ProvidenceUtils.elememtalAffinityDefense = null;
			ItemManager.Unload();
			UIManager.Unload();
			SubworldManager.Unload();
			Instance = null;
			base.Unload();
		}
		//private bool DrawElemDefUI()
		//{
		//	if (ElemDefUI.visible && Main.playerInventory)
		//		elemDefUI.Draw(Main.spriteBatch, new GameTime());
		//	return true;
		//}
		//private bool DrawFocusUI()
		//{
		//	if (FocusUI.visible)
		//		focusUI.Draw(Main.spriteBatch, new GameTime());
		//	return true;
		//}
		private bool DrawBossHealthUI()
		{
			if (BossHealth.visible)
				bossHealthUI.Draw(Main.spriteBatch, new GameTime());
			return true;
		}
		private bool DrawParityUI()
		{
			if (ParityUI.visible) parityUI.Draw(Main.spriteBatch, new GameTime());
			return true;
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int accbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Builder Accessories Bar"));
			if (accbarIndex != -1)
			{
				//layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Elemental Affinities", DrawElemDefUI, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
				//layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Focus Meter", DrawFocusUI, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Parity Meter", DrawParityUI, InterfaceScaleType.UI));
			}
		}
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
		}

		public override void PostSetupContent()
		{
			if (texturePackEnabled)
			{
				Initialize();
			}

			SubworldManager.Load();

			// Showcases mod support with Boss Checklist without referencing the mod
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			bossChecklist?.Call(
					"AddBoss",
					10.5f,
					new List<int> { ModContent.NPCType<FireAncient>() },
					this, // Mod
					"$Mods.ProvidenceMod.NPCName.FireAncient",
					(Func<bool>)(() => ProvidenceWorld.downedFireAncient),
					ModContent.ItemType<CirrusEdge>(),
					new List<int> { ModContent.ItemType<CirrusEdge>(), ModContent.ItemType<CirrusEdge>() },
					new List<int> { ModContent.ItemType<CirrusEdge>(), ModContent.ItemType<CirrusEdge>() },
					"$Mods.ProvidenceMod.BossSpawnInfo.FireAncient"
				);
		}
		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			//if (NPC.AnyNPCs(NPCID.BrainofCthulhu))
			//{
			//	music = "Sounds/Music/Brainiac".AsMusicSlot(this);
			//	priority = MusicPriority.BossMedium;
			//}
			//else if (NPC.AnyNPCs(ModContent.NPCType<AirElemental>()))
			//{
			//	music = "Sounds/Music/HighInTheSky".AsMusicSlot(this);
			//	priority = MusicPriority.BossMedium;
			//}
		}
		public override void UpdateUI(GameTime gameTime)
		{
			//elemDefUI?.Update(gameTime);
			//focusUI?.Update(gameTime);
			bossHealthUI?.Update(gameTime);
			parityUI?.Update(gameTime);
		}
	}
}
