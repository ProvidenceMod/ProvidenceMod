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
using ProvidenceMod.Items.Dyes;
using ProvidenceMod.TexturePack;
using ProvidenceMod.NPCs.FireAncient;
using ProvidenceMod.Items.Weapons.Melee;
using static ProvidenceMod.ModSupport.ModCalls;
using static ProvidenceMod.TexturePack.ProvidenceTextureManager;
using ProvidenceMod.NPCs.PrimordialCaelus;
using ProvidenceMod.Items.BossSpawners;
using ProvidenceMod.Particles;
using ProvidenceMod.Items.Armor;
using ProvidenceMod.UI.Developer;

namespace ProvidenceMod
{
	public class ProvidenceMod : Mod
	{
		internal static ProvidenceMod Instance;

		private UserInterface bossHealthUI;
		private UserInterface parityUI;
		private UserInterface quantum;
		private UserInterface structureDev;

		internal BossHealth BossHealth;
		internal Quantum Quantum;
		internal ParityUI ParityUI;
		internal StructureDev StructureDev;

		public ProvidenceDetours providenceHooks;
		public static ParticleManager particleManager;

		public static DynamicSpriteFont bossHealthFont;
		public static DynamicSpriteFont mouseTextFont;
		public static Ref<Effect> quantumShader;
		public static ArmorShaderData quantumShaderData;
		public static Ref<Effect> divinityEffect;

		public static ModHotKey CycleParity;
		public static ModHotKey UseQuantum;

		public bool texturePack;
		public bool bossHP;
		public bool bossPercentage;
		public bool subworldVote;

		public override void Load()
		{
			Instance = this;

			providenceHooks = new ProvidenceDetours();
			providenceHooks.Initialize();

			LoadCLient();

			BossHealthBarManager.Initialize();
		}
		public void LoadCLient()
		{
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

				Quantum = new Quantum();
				Quantum.Initialize();

				StructureDev = new StructureDev();
				StructureDev.Initialize();
				structureDev = new UserInterface();
				structureDev.SetState(StructureDev);

				CycleParity = RegisterHotKey("Cycle Parity Element", "C");
				UseQuantum = RegisterHotKey("Activate Quantum Flux", "C");

				if (FontExists("Fonts/BossHealthFont"))
					bossHealthFont = GetFont("Fonts/BossHealthFont");
				//if (FontExists("Fonts/MouseTextFont"))
				//	mouseTextFont = GetFont("Fonts/MouseTextFont");

				//ProvidenceTextureManager.LoadFonts();

				if (texturePack)
					ProvidenceTextureManager.Load();
				particleManager = new ParticleManager();
				particleManager.Load();

				quantumShader = new Ref<Effect>(GetEffect("Effects/Quantum"));
				quantumShaderData = new ArmorShaderData(divinityEffect, "Quantum");
				GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverHelm>(), quantumShaderData);
				GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverBreastplate>(), quantumShaderData);
				GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverLeggings>(), quantumShaderData);
				GameShaders.Armor.BindShader(ModContent.ItemType<DivinityDye>(), quantumShaderData);

				//quantumShader = Instance.GetEffect("Effects/Quantum");
				//divinityEffect = Instance.GetEffect("Effects/DivinityShader");
				//divinityEffect.Parameters["SwirlTexture"].SetValue(GetTexture("Effects/SwirlTexture"));
			}
		}
		public override void PostSetupContent()
		{
			SubworldManager.Load();
			BossChecklist();
		}
		public override void Unload()
		{
			ParityUI = null;
			parityUI = null;
			BossHealth = null;
			bossHealthUI = null;
			CycleParity = null;
			bossHealthFont = null;
			mouseTextFont = null;
			divinityEffect = null;
			if (!Main.dedServ)
			{
				ProvidenceTextureManager.Unload();
				//ProvidenceTextureManager.UnloadFonts();
			}
			try
			{
				particleManager.Unload();
			}
			catch (Exception ex)
			{

			}
			providenceHooks.Unload();
			SubworldManager.Unload();
			Instance = null;
			base.Unload();
		}

		private bool DrawBossHealthUI()
		{
			if (BossHealth.visible) bossHealthUI.Draw(Main.spriteBatch, new GameTime());
			return true;
		}
		private bool DrawParityUI()
		{
			if (ParityUI.visible) parityUI.Draw(Main.spriteBatch, new GameTime());
			return true;
		}
		private bool DrawQuantum()
		{
			Quantum.Draw(Main.spriteBatch);
			return true;
		}
		private bool DrawStructureDev()
		{
			if (StructureDev.visible) structureDev.Draw(Main.spriteBatch, new GameTime());
			return true;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int accbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Builder Accessories Bar"));
			if (accbarIndex != -1)
			{
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Parity", DrawParityUI, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Quantum", DrawQuantum, InterfaceScaleType.UI));
				layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Structure Dev", DrawStructureDev, InterfaceScaleType.UI));
			}
		}
		public override void UpdateUI(GameTime gameTime)
		{
			BossHealthBarManager.Update();
			bossHealthUI?.Update(gameTime);
			parityUI?.Update(gameTime);
			quantum?.Update(gameTime);
			structureDev?.Update(gameTime);
		}

		public override void PreSaveAndQuit()
		{
			particleManager.Dispose();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) => ProvidenceNetcode.HandlePacket(this, reader, whoAmI);

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
	}
}
