using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using UnbiddenMod.Code.Items.Consumables.IchorFromBeyond;
using static Terraria.ModLoader.ModContent;


namespace UnbiddenMod
{
  public class UnbiddenPlayer : ModPlayer
  {
    public bool ichor;
    public int ichorCount;

    public virtual void Initialize()
    {
      this.ichor = false;
      this.ichorCount = 0;
    }

    public virtual TagCompound Save()
    {
      return new TagCompound {
        {"ichor", this.ichor},
        {"ichorCount", this.ichorCount}
      };
    }

    public override void ResetEffects()
    {
      // statLifeMax2 += this.ichorCount * 20;
    }

    public virtual void Load(TagCompound tag)
    {
      ichor = tag.GetBool("ichor");
      ichorCount = tag.GetInt("ichorCount");
    }
  }
}