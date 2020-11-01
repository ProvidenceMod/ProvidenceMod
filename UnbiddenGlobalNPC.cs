using Terraria;
using Terraria.ModLoader;

namespace UnbiddenMod
{
    public class UnbiddenGlobalNPC : GlobalNPC
  {
    // Elemental variables for NPC's

    public float[] resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }; // Fire, Ice, Lightning, Water, Earth, Air, Holy, Unholy

    // Elemental variables also contained within GlobalItem, GlobalProjectile, and Player
    public override bool InstancePerEntity => true;
    public override bool CloneNewInstances => true;
    public int contactDamageEl = -1; // Contact damage element, -1 by default for typeless
    public bool hypodermia = false;


    public override void ResetEffects(NPC npc)
    {
      npc.Unbidden().hypodermia = false;
    }


    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
    {
      if (hypodermia)
      {
        damage = (int)(damage * 1.20f); // 20% damage increase
      }


      int weapEl = item.GetGlobalItem<UnbiddenGlobalItem>().element; // Determine the element (will always be between 0-6 for array purposes)
      if (weapEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.GetGlobalNPC<UnbiddenGlobalNPC>().resists[weapEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
      }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      if (hypodermia)
      {
        damage = (int)(damage * 1.20f); // 20% damage increase
      }
      
      int projEl = projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.GetGlobalNPC<UnbiddenGlobalNPC>().resists[projEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
      }
    }
  }
}