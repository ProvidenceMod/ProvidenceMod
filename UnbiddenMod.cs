using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using UnbiddenMod.NPCs.FireAncient;
using UnbiddenMod.UI;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod
{
  public class UnbiddenMod : Mod
  {
    private UserInterface elemDefUI, focusUI, bossHealthUI;
    internal ElemDefUI ElemDefUI;
    internal Focus focusBar;
    internal BossHealth BossHealth;
    public static ModHotKey ParryHotkey;

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
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("UnbiddenMod: Elemental Affinities", DrawElemDefUI, InterfaceScaleType.UI));
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("UnbiddenMod: Boss Health Bar", DrawBossHealthUI, InterfaceScaleType.UI));
        layers.Insert(accbarIndex, new LegacyGameInterfaceLayer("UnbiddenMod: Focus Meter", DrawFocusUI, InterfaceScaleType.UI));
      }
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

    public override void PostSetupContent()
    {
      // Showcases mod support with Boss Checklist without referencing the mod
      Mod bossChecklist = ModLoader.GetMod("BossChecklist");
      bossChecklist?.Call(
          "AddBoss",
          10.5f,
          new List<int> { ModContent.NPCType<NPCs.FireAncient.FireAncient>() },
          this, // Mod
          "$Mods.UnbiddenMod.NPCName.FireAncient",
          (Func<bool>)(() => UnbiddenGlobalNPC.downedFireAncient),
          ModContent.ItemType<Items.Weapons.Melee.AirSword>(),
          new List<int> { ModContent.ItemType<Items.Weapons.Melee.AirSword>(), ModContent.ItemType<Items.Weapons.Melee.AirSword>() },
          new List<int> { ModContent.ItemType<Items.Weapons.Melee.AirSword>(), ModContent.ItemType<Items.Weapons.Melee.AirSword>() },
          "$Mods.UnbiddenMod.BossSpawnInfo.FireAncient"
        );
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

  internal enum UnbiddenModMessageType : byte
  {
    FireAncient,
    UnbiddenPlayerSyncPlayer
  }
}
