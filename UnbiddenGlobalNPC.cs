using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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

    public override void SetDefaults(NPC npc)
    {
      switch (npc.type)
      {
        // Underworld Enemies
        case NPCID.FireImp:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RedDevil:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentHead:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentBody:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentTail:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Demon:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VoodooDemon:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Lavabat:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Hellbat:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.LavaSlime:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.WallofFlesh:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.WallofFleshEye:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonTaxCollector:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 1f, 2f, 1f, 1f, 1f, 1f };
          break;

        // Dungeon Enemies
        case NPCID.AngryBones:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.CursedSkull:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.AngryBonesBig:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.AngryBonesBigHelmet:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.AngryBonesBigMuscle:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.DarkCaster:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BlueArmoredBones:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BlueArmoredBonesMace:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BlueArmoredBonesNoPants:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BlueArmoredBonesSword:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BoneLee:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.Paladin:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.DiabolistRed:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.DiabolistWhite:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.GiantCursedSkull:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.HellArmoredBones:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.HellArmoredBonesMace:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.HellArmoredBonesSpikeShield:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.HellArmoredBonesSword:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RustyArmoredBonesAxe:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RustyArmoredBonesFlail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RustyArmoredBonesSword:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RustyArmoredBonesSwordNoArmor:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RaggedCaster:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.RaggedCasterOpenCoat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SkeletonSniper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.TacticalSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.DungeonSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.DungeonSpirit:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;

        // Zombies (Yes, there's that fucking many)
        
        case NPCID.Zombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Zom:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Surface        
        case NPCID.BlueSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GreenSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RedSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Pinky:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonEye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonEye2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        // case NPCID.DemonEye2:
        //   npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
        //   break;
        // case NPCID.DemonEye2:
        //   npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
        //   break;
      }
    }
  }
}