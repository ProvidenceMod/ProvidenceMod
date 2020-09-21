using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod
{
    public class UnbiddenGlobalItem : GlobalItem
  {

    // Elemental variables for Items

    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place

    // Elemental variables also contained within GlobalProjectile, GlobalNPC, and Player
    public override bool InstancePerEntity => true;

    public UnbiddenGlobalItem()
    {
      element = -1;
    }
    public override GlobalItem Clone(Item item, Item itemClone)
    {
      UnbiddenGlobalItem myClone = (UnbiddenGlobalItem)base.Clone(item, itemClone);
      myClone.element = element;
      return myClone;
    }
  }
}