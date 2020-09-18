using Terraria.ModLoader;
using Terraria;

namespace UnbiddenMod
{
    public class UnbiddenGlobalItem : GlobalItem
  {
    public override bool InstancePerEntity => true;

    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place

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