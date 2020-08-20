using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using UnbiddenMod.Code.Items.Consumables.AngelTear;
using static Terraria.ModLoader.ModContent;


namespace UnbiddenMod
{
  public class UnbiddenPlayer : ModPlayer
  {
    public bool angelTear;
    public int tearCount;
    public string[] elements = new string[7] {"fire", "ice", "lightning", "poison", "acid", "holy", "unholy"};
    public int[] resists = new int[7] {100, 100, 100, 100, 100, 100, 100};
    public bool brimHeart = false;

    public override TagCompound Save()
    {
      return new TagCompound {
        {"angelTear", this.angelTear},
        {"tearCount", this.tearCount},
        {"resists", this.resists},
        {"brimHeart", this.brimHeart}
      };
    }

    public override void ResetEffects()
    {
      player.statLifeMax2 += tearCount * 20;
    }

    public override void Load(TagCompound tag)
    {
      angelTear = tag.GetBool("angelTear");
      tearCount = tag.GetInt("tearCount");
      resists = tag.GetIntArray("resists");
      brimHeart = tag.GetBool("brimHeart");
    }
    public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
    {
      if (brimHeart)
      {
          player.buffImmune[BuffID.OnFire] = true;
          if (item.GetGlobalItem<UnbiddenItem>().element == 0) // If the weapon is fire-based
          {
            // Reduce cost by 15%
            reduce -= 0.15f;
          }
      }
    }

  }
}