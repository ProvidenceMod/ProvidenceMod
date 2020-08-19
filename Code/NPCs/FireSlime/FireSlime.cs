using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.NPC;
using static Terraria.Player;
using static Terraria.ModLoader.ModContent;
using static UnbiddenMod.UnbiddenNPC;

namespace UnbiddenMod.Code.NPCs.FireSlime
{
  // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
  [AutoloadHead]
  public class FireSlime : ModNPC
  {
    public int[] resists = new int[7] {0, 200, 100, 100, 100, 100, 100};
    public override string Texture => "UnbiddenMod/Code/NPCs/SolarCultist/SolarCultist";

    public override bool Autoload(ref string name)
    {
      name = "FireSlime";
      return mod.Properties.Autoload;
    }

    public override void SetStaticDefaults()
    {
      // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
      DisplayName.SetDefault("Fire Slime");
    }

    public override void SetDefaults()
    {
      npc.townNPC = false;
      npc.width = 24;
      npc.height = 18;
      npc.aiStyle = 1;
      npc.damage = 1;
      npc.defense = 2;
      npc.lifeMax = 5000;
      npc.HitSound = SoundID.NPCHit1;
      npc.DeathSound = SoundID.NPCDeath1;
      npc.value = 25;
      animationType = 1;
    }

  }
}