using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using ProvidenceMod.NPCs.FireAncient;
using ProvidenceMod.UI;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod
{
  public class ProvidenceMod : Mod
  {
    private UserInterface elemDefUI, focusUI, bossHealthUI;
    internal ElemDefUI ElemDefUI;
    internal Focus focusBar;
    internal BossHealth BossHealth;
    public static ModHotKey ParryHotkey;

    public override void Load()
    {
      ElemDefUI = new ElemDefUI();
      ElemDefUI.Initialize();
      elemDefUI = new UserInterface();
      elemDefUI.SetState(ElemDefUI);

      focusBar = new Focus();
      focusBar.Initialize();
      focusUI = new UserInterface();
      focusUI.SetState(focusBar);

      BossHealth = new BossHealth();
      BossHealth.Initialize();
      bossHealthUI = new UserInterface();
      bossHealthUI.SetState(BossHealth);

      ParryHotkey = RegisterHotKey("Parry", "F");
    }
    public override void Unload()
    {
      ElemDefUI = null;
      elemDefUI = null;
      focusBar = null;
      focusUI = null;
      BossHealth = null;
      bossHealthUI = null;
      ParryHotkey = null;
    }
    private bool DrawElemDefUI()
    {
      if (ElemDefUI.visible && Main.playerInventory)
        elemDefUI.Draw(Main.spriteBatch, new GameTime());
      return true;
    }
    private bool DrawFocusUI()
    {
      if (Focus.visible)
        focusUI.Draw(Main.spriteBatch, new GameTime());
      return true;
    }
    private bool DrawBossHealthUI()
    {
      if (BossHealth.visible)
        bossHealthUI.Draw(Main.spriteBatch, new GameTime());
      return true;
    }
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      int accbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Builder Accessories Bar"));
      if (accbarIndex != -1)
      {
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Elemental Affinities", DrawElemDefUI, InterfaceScaleType.UI));
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("ProvidenceMod: Focus Meter", DrawFocusUI, InterfaceScaleType.UI));
      }
    }
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
      ProvidenceModMessageType msgType = (ProvidenceModMessageType)reader.ReadByte();
      switch (msgType)
      {
        case ProvidenceModMessageType.FireAncient:
          if (Main.npc[reader.ReadInt32()].modNPC is FireAncient ancient && ancient.npc.active)
          {
            ancient.HandlePacket(reader);
          }
          break;

        // This message syncs ProvidencePlayer.tearCount
        case ProvidenceModMessageType.ProvidencePlayerSyncPlayer:
          byte playernumber = reader.ReadByte();
          ProvidencePlayer ProvidencePlayer = Main.player[playernumber].Providence();
          int tearCount = reader.ReadInt32();
          ProvidencePlayer.tearCount = tearCount;
          // SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
          break;

        default:
          Logger.WarnFormat("ProvidenceMod: Unknown Message type: {0}", msgType);
          break;
      }
    }

    public override void PostSetupContent()
    {
      // Showcases mod support with Boss Checklist without referencing the mod
      Mod bossChecklist = ModLoader.GetMod("BossChecklist");
      bossChecklist?.Call(
          "AddBoss",
          10.5f,
          new List<int> { ModContent.NPCType<FireAncient>() },
          this, // Mod
          "$Mods.ProvidenceMod.NPCName.FireAncient",
          (Func<bool>)(() => ProvidenceWorld.downedFireAncient),
          ModContent.ItemType<Items.Weapons.Melee.AirSword>(),
          new List<int> { ModContent.ItemType<Items.Weapons.Melee.AirSword>(), ModContent.ItemType<Items.Weapons.Melee.AirSword>() },
          new List<int> { ModContent.ItemType<Items.Weapons.Melee.AirSword>(), ModContent.ItemType<Items.Weapons.Melee.AirSword>() },
          "$Mods.ProvidenceMod.BossSpawnInfo.FireAncient"
        );
      Mod magicStorage = ModLoader.GetMod("MagicStorage");
      if (magicStorage != null)
      {
        ModItem craftingAccess = magicStorage.GetItem("CraftingAccess");
        ModItem creativeStorageUnit = magicStorage.GetItem("CreativeStorageUnit");
        ModItem radiantJewel = magicStorage.GetItem("RadiantJewel");
        ModItem shadowDiamond = magicStorage.GetItem("ShadowDiamond");
        ModItem storageAccess = magicStorage.GetItem("StorageAccess");
        ModItem storageComponent = magicStorage.GetItem("StorageComponent");
        ModItem storageConnector = magicStorage.GetItem("StorageConnector");
        ModItem storageHeart = magicStorage.GetItem("StorageHeart");
        ModItem storageUnit = magicStorage.GetItem("StorageUnit");
        ModItem storageUnitBlueChlorophyte = magicStorage.GetItem("StorageUnitBlueChlorophyte");
        ModItem storageUnitCrimtane = magicStorage.GetItem("StorageUnitCrimtane");
        ModItem storageUnitDemonite = magicStorage.GetItem("StorageUnitDemonite");
        ModItem storageUnitHallowed = magicStorage.GetItem("StorageUnitHallowed");
        ModItem storageUnitHellstone = magicStorage.GetItem("StorageUnitHellstone");
        ModItem storageUnitLuminite = magicStorage.GetItem("StorageUnitLuminite");
        ModItem storageUnitTerra = magicStorage.GetItem("StorageUnitTerra");
        ModItem storageUnitTiny = magicStorage.GetItem("StorageUnitTiny");
        ModItem upgradeBlueChlorophyte = magicStorage.GetItem("UpgradeBlueChlorophyte");
        ModItem upgradeCrimtane = magicStorage.GetItem("UpgradeCrimtane");
        ModItem upgradeDemonite = magicStorage.GetItem("UpgradeDemonite");
        ModItem upgradeHallowed = magicStorage.GetItem("UpgradeHallowed");
        ModItem upgradeHellstone = magicStorage.GetItem("UpgradeHellstone");
        ModItem upgradeLuminite = magicStorage.GetItem("UpgradeLuminite");
        ModItem upgradeTerra = magicStorage.GetItem("UpgradeTerra");
        if (upgradeTerra != null)
        {
          craftingAccess.item.maxStack = 999;
          creativeStorageUnit.item.maxStack = 999;
          radiantJewel.item.maxStack = 999;
          shadowDiamond.item.maxStack = 999;
          storageAccess.item.maxStack = 999;
          storageComponent.item.maxStack = 999;
          storageConnector.item.maxStack = 999;
          storageHeart.item.maxStack = 999;
          storageUnit.item.maxStack = 999;
          storageUnitBlueChlorophyte.item.maxStack = 999;
          storageUnitCrimtane.item.maxStack = 999;
          storageUnitDemonite.item.maxStack = 999;
          storageUnitHallowed.item.maxStack = 999;
          storageUnitHellstone.item.maxStack = 999;
          storageUnitLuminite.item.maxStack = 999;
          storageUnitTerra.item.maxStack = 999;
          storageUnitTiny.item.maxStack = 999;
          upgradeBlueChlorophyte.item.maxStack = 999;
          upgradeCrimtane.item.maxStack = 999;
          upgradeDemonite.item.maxStack = 999;
          upgradeHallowed.item.maxStack = 999;
          upgradeHellstone.item.maxStack = 999;
          upgradeLuminite.item.maxStack = 999;
          upgradeTerra.item.maxStack = 999;
        }
      }
    }
    public override void UpdateMusic(ref int music, ref MusicPriority priority)
    {
      if (NPC.AnyNPCs(NPCID.BrainofCthulhu))
      {
        music = GetSoundSlot(SoundType.Music, "Sounds/Music/Brainiac");
        priority = MusicPriority.BossMedium;
      }
    }
    public override void UpdateUI(GameTime gameTime)
    {
      elemDefUI?.Update(gameTime);
      focusUI?.Update(gameTime);
      bossHealthUI?.Update(gameTime);
    }
  }

  internal enum ProvidenceModMessageType : byte
  {
    FireAncient,
    ProvidencePlayerSyncPlayer
  }
}
