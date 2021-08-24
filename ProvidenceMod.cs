using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using ProvidenceMod.UI;
using ProvidenceMod.TexturePack;
using ProvidenceMod.NPCs.FireAncient;
using ProvidenceMod.Items.Weapons.Melee;
using static ProvidenceMod.TexturePack.ProvidenceTextureManager;

namespace ProvidenceMod
{
	public class ProvidenceMod : Mod
	{
		internal static ProvidenceMod Instance;

		private UserInterface bossHealthUI;
		private UserInterface parityUI;
		internal BossHealth BossHealth;
		internal ParityUI ParityUI;

		public static DynamicSpriteFont providenceFont;
		public static DynamicSpriteFont bossHealthFont;

		public static ModHotKey CycleParity;

		public bool texturePack;
		public bool bossHP;
		public bool subworldVote;

		public override void Load()
		{
			Instance = this;
			if (!Main.dedServ)
			{
				BossHealth = new BossHealth();
				BossHealth.Initialize();
				bossHealthUI = new UserInterface();
				bossHealthUI.SetState(BossHealth);

				ParityUI = new ParityUI();
				ParityUI.Initialize();
				parityUI = new UserInterface();
				parityUI.SetState(ParityUI);

				CycleParity = RegisterHotKey("Cycle Parity Element", "C");

				if (FontExists("Fonts/ProvidenceFont"))
				{
					providenceFont = GetFont("Fonts/ProvidenceFont");
				}
				if (FontExists("Fonts/BossHealthFont"))
				{
					bossHealthFont = GetFont("Fonts/BossHealthFont");
				}
			}
			if (Main.netMode != NetmodeID.Server)
			{
				Ref<Effect> forcefield = new Ref<Effect>(GetEffect("Effects/Forcefield")); // The path to the compiled shader file.
				Filters.Scene["Forcefield"] = new Filter(new ScreenShaderData(forcefield, "Forcefield"), EffectPriority.VeryHigh);
				Filters.Scene["Forcefield"].Load();
			}
		}
		public override void Unload()
		{
			ParityUI = null;
			parityUI = null;
			BossHealth = null;
			bossHealthUI = null;
			CycleParity = null;
			ProvidenceTextureManager.Unload();
			SubworldManager.Unload();
			Instance = null;
			base.Unload();
		}
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
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Parity Meter", DrawParityUI, InterfaceScaleType.UI));
			}
		}
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
		}

		public override void PostSetupContent()
		{
			if (Main.netMode != NetmodeID.Server)
			{
				if (texturePack)
				{
					Load();
				}
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
			//else if (NPC.AnyNPCs(ModContent.NPCType<Caelus>()))
			//{
			//	music = "Sounds/Music/HighInTheSky".AsMusicSlot(this);
			//	priority = MusicPriority.BossMedium;
			//}
		}
		public override void UpdateUI(GameTime gameTime)
		{
			bossHealthUI?.Update(gameTime);
			parityUI?.Update(gameTime);
		}
	}
}
