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
using ProvidenceMod.Metaballs;
using ProvidenceMod.Items.Dyes;
using ProvidenceMod.TexturePack;
using ProvidenceMod.NPCs.FireAncient;
using ProvidenceMod.Items.Weapons.Melee;
using static ProvidenceMod.ModSupport.ModCalls;
using static ProvidenceMod.TexturePack.ProvidenceTextureManager;
using ProvidenceMod.NPCs.PrimordialCaelus;
using ProvidenceMod.Items.BossSpawners;

namespace ProvidenceMod
{
	public class ProvidenceMod : Mod
	{
		internal static ProvidenceMod Instance;

		private UserInterface bossHealthUI;
		private UserInterface parityUI;
		internal BossHealth BossHealth;
		internal ParityUI ParityUI;

		public static MaskManager Metaballs;

		public ProvidenceEvents providenceEvents;

		public static DynamicSpriteFont bossHealthFont;
		public static DynamicSpriteFont mouseTextFont;
		public static Effect divinityEffect;

		public static ModHotKey CycleParity;

		public bool texturePack;
		public bool bossHP;
		public bool bossPercentage;
		public bool subworldVote;

		private Vector2 lastScreenSize;
		private Vector2 lastViewSize;

		public override void Load()
		{
			Instance = this;

			MaskEvents.Initialize();
			providenceEvents = new ProvidenceEvents();
			providenceEvents.Initialize();

			if (Main.netMode != NetmodeID.Server && texturePack)
				ProvidenceTextureManager.Load();
			if (Main.netMode != NetmodeID.Server)
			{
				Main.OnPreDraw += DrawMasks;
				InitializeMasks();
			}
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

				if (FontExists("Fonts/BossHealthFont"))
					bossHealthFont = GetFont("Fonts/BossHealthFont");
				//if (FontExists("Fonts/MouseTextFont"))
				//	mouseTextFont = GetFont("Fonts/MouseTextFont");

				//ProvidenceTextureManager.LoadFonts();
			}
			if (Main.netMode != NetmodeID.Server)
			{
				divinityEffect = Instance.GetEffect("Effects/DivinityShader");
				divinityEffect.Parameters["SwirlTexture"].SetValue(GetTexture("Effects/SwirlTexture"));
			}
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
			Metaballs = null;
			if (!Main.dedServ)
			{
				ProvidenceTextureManager.Unload();
				//ProvidenceTextureManager.UnloadFonts();
			}
			MaskEvents.Unload();
			providenceEvents.Unload();
			SubworldManager.Unload();
			Instance = null;
			base.Unload();
		}
		public override void PreUpdateEntities()
		{
			if (!Main.dedServ)
			{
				if (lastViewSize != Main.ViewSize && Metaballs != null)
					Metaballs.Initialize(Main.graphics.GraphicsDevice);

				lastScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
				lastViewSize = Main.ViewSize;
			}
		}
		public static void InitializeMasks()
		{
			Metaballs = new MaskManager();
			Metaballs.LoadContent();
			Metaballs.Initialize(Main.graphics.GraphicsDevice);

			var friendlyDust = (FriendlyMetaball)ModContent.GetModDust(ModContent.DustType<FriendlyMetaball>());
			var enemyDust = (EnemyMetaball)ModContent.GetModDust(ModContent.DustType<EnemyMetaball>());

			friendlyDust.Reset();
			enemyDust.Reset();
		}
		private void DrawMasks(GameTime obj)
		{
			if (!Main.gameMenu && Metaballs != null && Main.graphics.GraphicsDevice != null && Main.spriteBatch != null)
				Metaballs.DrawToTarget(Main.spriteBatch, Main.graphics.GraphicsDevice);
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
		public override void HandlePacket(BinaryReader reader, int whoAmI) => ProvidenceNetcode.HandlePacket(this, reader, whoAmI);

		public override void PostSetupContent()
		{
			SubworldManager.Load();
			BossChecklist();
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
