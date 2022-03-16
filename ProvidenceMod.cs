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
using Providence.UI;
using Providence.TexturePack;
using static Providence.Content.ModSupport.ModCalls;
using Providence.Particles;
using Providence.UI.Developer;
using Providence.PrimitiveTrails;
using Providence.RenderTargets;
using Providence.Content.Items.Armor;
using Providence.Content.Items.Dyes;

namespace Providence
{
	public class ProvidenceMod : Mod
	{
		internal static ProvidenceMod Instance;

		public static RenderTargetManager Targets;
		public static TrailHelper Trails;
		public Detours Detours;

		private UserInterface bossHealthUI;
		private UserInterface parityUI;
		private UserInterface quantum;
		private UserInterface structureDev;

		internal BossHealth BossHealth;
		internal Quantum Quantum;
		internal ParityUI ParityUI;
		internal StructureDev StructureDev;

		public static DynamicSpriteFont bossHealthFont;
		public static DynamicSpriteFont mouseTextFont;
		public static Ref<Effect> quantumShader;
		public static ArmorShaderData quantumShaderData;
		public static Ref<Effect> divinityEffect;

		public static ModKeybind CycleParity;
		public static ModKeybind UseQuantum;

		public static bool TexturePack;
		public bool bossHP;
		public bool bossPercentage;
		public bool subworldVote;

		public override void Load()
		{
			Instance = this;

			if (!Main.dedServ)
				LoadCLient();

			BossHealthBarManager.Initialize();
		}
		public void LoadCLient()
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

			CycleParity = KeybindLoader.RegisterKeybind(this, "Cycle Parity Element", "C");
			UseQuantum = KeybindLoader.RegisterKeybind(this, "Activate Quantum Flux", "C");

			bossHealthFont = ModContent.Request<DynamicSpriteFont>("Providence/Assets/Fonts/BossHealthFont").Value;
			//if (FontExists("Fonts/MouseTextFont"))
			//	mouseTextFont = GetFont("Fonts/MouseTextFont");

			//ProvidenceTextureManager.LoadFonts();

			quantumShader = new Ref<Effect>(ModContent.Request<Effect>("Providence/Assets/Effects/Quantum").Value);
			quantumShaderData = new ArmorShaderData(divinityEffect, "Quantum");
			GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverHelm>(), quantumShaderData);
			GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverBreastplate>(), quantumShaderData);
			GameShaders.Armor.BindShader(ModContent.ItemType<StarreaverLeggings>(), quantumShaderData);
			GameShaders.Armor.BindShader(ModContent.ItemType<DivinityDye>(), quantumShaderData);
		}
		public override void PostSetupContent()
		{
			//SubworldManager.Load();
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
			//SubworldManager.Unload();
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

		// Move this to a Mod System
		//public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		//{
		//	int accbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Builder Accessories Bar"));
		//	if (accbarIndex != -1)
		//	{
		//		layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("Providence: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
		//		layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("Providence: Parity", DrawParityUI, InterfaceScaleType.UI));
		//		layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("Providence: Quantum", DrawQuantum, InterfaceScaleType.UI));
		//		layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("Providence: Structure Dev", DrawStructureDev, InterfaceScaleType.UI));
		//	}
		//}
		// Move this to a Mod System
		//public override void UpdateUI(GameTime gameTime)
		//{
		//	BossHealthBarManager.Update();
		//	bossHealthUI?.Update(gameTime);
		//	parityUI?.Update(gameTime);
		//	quantum?.Update(gameTime);
		//	structureDev?.Update(gameTime);
		//}

		public override void HandlePacket(BinaryReader reader, int whoAmI) => Netcode.HandlePacket(this, reader, whoAmI);

		//public override void UpdateMusic(ref int music, ref MusicPriority priority)
		//{
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
		//}
	}
}
