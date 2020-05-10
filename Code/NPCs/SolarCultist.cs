using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.NPC;
using static Terraria.Player;
using static Terraria.ModLoader.ModContent;

namespace UnbiddenMod.Code.NPCs
{
  // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
  [AutoloadHead]
  public class SolarCultist : ModNPC
  {
    public override bool Autoload(ref string name)
    {
      name = "SolarCultist";
      return mod.Properties.Autoload;
    }

    public override void SetStaticDefaults()
    {
      // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
      // DisplayName.SetDefault("Example Person");
      Main.npcFrameCount[npc.type] = 25;
      NPCID.Sets.ExtraFramesCount[npc.type] = 9;
      NPCID.Sets.AttackFrameCount[npc.type] = 4;
      NPCID.Sets.DangerDetectRange[npc.type] = 700;
      NPCID.Sets.AttackType[npc.type] = 0;
      NPCID.Sets.AttackTime[npc.type] = 90;
      NPCID.Sets.AttackAverageChance[npc.type] = 30;
      NPCID.Sets.HatOffsetY[npc.type] = 4;
    }

    public override void SetDefaults()
    {
      npc.townNPC = true;
      npc.friendly = true;
      npc.width = 18;
      npc.height = 40;
      npc.aiStyle = 7;
      npc.damage = 10;
      npc.defense = 15;
      npc.lifeMax = 250;
      npc.HitSound = SoundID.NPCHit1;
      npc.DeathSound = SoundID.NPCDeath1;
      npc.knockBackResist = 0.5f;
      animationType = NPCID.Guide;
    }

    public override void HitEffect(int hitDirection, double damage)
    {
      int num = npc.life > 0 ? 1 : 5;
      for (int k = 0; k < num; k++)
      {
        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("StarBlastDust"));
      }
    }

    public override bool CanTownNPCSpawn(int numTownNPCs, int money)
    {
      for (int k = 0; k < 255; k++)
      {
        Player player = Main.player[k];
        if (!player.active)
        {
          continue;
        }

        foreach (Item item in player.inventory)
        {
          if (item.type == 3539);
          {
            return true;
          }
        }
      }
      return false;
    }

    // Example Person needs a house built out of ExampleMod tiles. You can delete this whole method in your townNPC for the regular house conditions.
    public override bool CheckConditions(int left, int right, int top, int bottom)
    {
      // NOTE: FOR TESTING PURPOSES ONLY, WOOD (NO PREFIXES, SIMPLE GRASSLAND WOOD) WORKS TO COUNT TOWARDS SCORE.
      // THE ONLY NON-WOOD THING THAT IS VALID FOR NOW SHOULD BE THE SOLAR MONOLITH
      int score = 0, monolith = 3539, woodChair = 34, woodBench = 36, woodTable = 32, basicBed = 224, woodDoor = 25, woodWall = 93;
      for (int x = left; x <= right; x++)
      {
        for (int y = top; y <= bottom; y++)
        {
          int type = Main.tile[x, y].type;
          if (type == monolith || type == woodChair || type == woodBench || type == basicBed || type == woodDoor)
          {
            score++;
          }
          if (Main.tile[x, y].wall == woodWall)
          {
            score++;
          }
        }
      }
      return score >= (right - left) * (bottom - top) / 2;
    }

    public override string TownNPCName()
    {
      return "Solar Cultist";
    }

    public override void FindFrame(int frameHeight)
    {
      /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
    }

    public override string GetChat()
    {
      switch (Main.rand.Next(4))
      {
        case 0:
          return "[BLANK]";
        case 1:
          return "Have you seen my demonized sibling? He's a Lunie, I suggest keeping your distance.";
        case 2:
          return "[BLANK]";
        default:
          return "'Tranquillitas, frater'. I'm not one of those Lunies.";
      }
    }

    /* 
		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4))
			{
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}
			chat.Add("Sometimes I feel like I'm different from everyone else here.");
			chat.Add("What's your favorite color? My favorite colors are white and black.");
			chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
			chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
			chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}
		*/

    public override void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
      button2 = "Awesomeify";
      if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
        button = "Upgrade " + Lang.GetItemNameValue(ItemID.HiveBackpack);
    }

    public override void OnChatButtonClicked(bool firstButton, ref bool shop)
    {
      if (firstButton)
      {
        shop = true;
      }
      else
      {
        // If the 2nd button is pressed, open the inventory...
        Main.playerInventory = true;
        // remove the chat window...
        Main.npcChatText = "";
        // and start an instance of our UIState.
        // Note that even though we remove the chat window, Main.LocalPlayer.talkNPC will still be set correctly and we are still technically chatting with the npc.
      }
    }

    public override void SetupShop(Chest shop, ref int nextSlot)
    {
      shop.item[nextSlot].SetDefaults(mod.ItemType("StardustUltimus"));
    }

    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      damage = 20;
      knockback = 4f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
      cooldown = 30;
      randExtraCooldown = 30;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
      projType = mod.ProjectileType("StarBlast");
      attackDelay = 1;
    }
  }
}