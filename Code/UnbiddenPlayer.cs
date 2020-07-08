using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Microsoft.Xna.Framework;
using UnbiddenMod.Code.Items.Consumables.IchorFromBeyond;


namespace UnbiddenMod.UnbiddenPlayer
{
  public class UnbiddenPlayer : ModPlayer
  {
    public bool ichor;

    public virtual void Initialize()
    {
      this.ichor = false;
    }

    public virtual TagCompound Save()
    {
      return new TagCompound {
        {"ichor", this.ichor}
      };
    }

    public virtual void Load(TagCompound tag)
    {
      this.ichor = tag.GetBool("ichor");
    }
  }
}