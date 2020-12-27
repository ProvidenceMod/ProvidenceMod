using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace UnbiddenMod.NPCs
{
    // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
  public class SolarCultist : ModNPC
  {

    public override string Texture => "UnbiddenMod/NPCs/SolarCultist";

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
        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("MoonBlastDust"));
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
          if (item.type == ItemID.SolarMonolith)
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
          if (type == monolith || type == woodChair || type == woodBench || type == basicBed || type == woodDoor || type == woodTable)
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

    // Random name generator when the NPC arrives
    public override string TownNPCName()
    {
      switch (Main.rand.Next(4)) {
        case 1:
          return "Jenova";
        case 2:
          return "Tel'slara";
        case 3:
          return "Hoshinko";
        default:
          return "Sighard";
      }
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

		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("'Tranquillitas, frater'. I'm not one of those Lunies.");
      if (NPC.downedAncientCultist) {
        chat.Add("So you've killed my brother. I thank you for ridding of him, but now his soul will be forever toyed with.");
        if (NPC.downedTowerSolar) {
          chat.Add("I think this is the one time I will agree with you destroying my creed. Those pillars are a menace.");
        }
        if (NPC.downedMoonlord) {
          chat.Add("I can only fear the imbalances of sun and moon killing one of the lords.");
        }
      } else {
			  chat.Add("Have you seen my demonized sibling? He's a Lunie, I suggest keeping your distance.");
      }
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}

    public override void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
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

    // Setting up shop for when the "Shop" button is clicked
    public override void SetupShop(Chest shop, ref int nextSlot)
    {
      shop.item[nextSlot].SetDefaults(mod.ItemType("MoonCleaver"));
    }

    // Gives them a way to defend themselves: How hard they hit
    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      damage = 20;
      knockback = 4f;
    }

    // Gives them a way to defend themselves: The cooldown between attacks
    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
      cooldown = 30;
      randExtraCooldown = 30;
    }

    // Gives them a way to defend themselves: The projectile itself
    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
      projType = mod.ProjectileType("MoonBlast");
      attackDelay = 1;
    }
    
    // So he can actually throw the projectile instead of letting it spawn and fall through the floor
    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
  }
}