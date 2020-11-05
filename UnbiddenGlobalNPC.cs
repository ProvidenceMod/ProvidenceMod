using Terraria;
using Terraria.ID;
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
    public bool freezing = false;
    public bool frozen = false;


    public override void ResetEffects(NPC npc)
    {
      npc.Unbidden().hypodermia = false;
      npc.Unbidden().freezing = false;
      npc.Unbidden().frozen = false;
    }

    public override void AI(NPC npc)
    {
      if (npc.Unbidden().freezing)
      {
        npc.velocity.X /= 1.1f;
      }
      if (npc.Unbidden().frozen)
      {
        npc.velocity.X = 0; // Frozen in place. Keep in mind they can still shoot if they could already.
        npc.velocity.Y = 0; 
      }
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
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RedDevil:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentHead:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentBody:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BoneSerpentTail:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Demon:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VoodooDemon:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Lavabat:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Hellbat:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.LavaSlime:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonTaxCollector:
          npc.Unbidden().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
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
        case NPCID.Necromancer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.NecromancerArmored:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.CultistDevote:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.CultistArcherBlue:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;

        // Zombies (Yes, there's that fucking many)
        case NPCID.Zombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieDoctor:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieElf:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieElfBeard:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieElfGirl:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieMushroom:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieMushroomHat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombiePixie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieRaincoat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieSuperman:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieSweater:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ZombieXmas:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombieCenx:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombiePincussion:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombieSlimed:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombieSwamp:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.ArmedZombieTwiggy:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BaldZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigBaldZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigFemaleZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigPincushionZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigRainZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigSlimedZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigSwampZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigTwiggyZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BigZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.BloodZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.FemaleZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.PincushionZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SlimedZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallBaldZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallFemaleZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallPincushionZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallRainZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallSlimedZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallSwampZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallTwiggyZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SmallZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.SwampZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.TwiggyZombie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;

        // Surface        
        case NPCID.BlueSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GreenSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RedSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PurpleSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Pinky:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonEye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.DemonEye2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.DemonEyeOwl:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.DemonEyeSpaceship:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.PossessedArmor:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.WanderingEye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.Wraith:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.Werewolf:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.HoppinJack:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.GoblinScout:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Snow Biome
        case NPCID.IceSlime:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 1f, 1f, 1f };
          break;
        case NPCID.ZombieEskimo:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.ArmedZombieEskimo:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CorruptPenguin:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CrimsonPenguin:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.IceElemental:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 1f, 1f, 1f };
          break;
        case NPCID.Wolf:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.IceGolem:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;

        // Desert
        case NPCID.SandSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Vulture:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Antlion:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Mummy:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DarkMummy:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.LightMummy:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;

        //Evil
        case NPCID.EaterofSouls:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigEater:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.LittleEater:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Corruptor:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigMimicCorruption:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SandsharkCorrupt:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CorruptGoldfish:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CorruptSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertGhoulCorruption:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Slimeling:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CorruptBunny:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CursedHammer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Clinger:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertDjinn:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Slimer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Slimer2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SandsharkCrimson:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BloodCrawler:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CrimsonGoldfish:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.FaceMonster:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Crimera:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigCrimera:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.LittleCrimera:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Herpling:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Crimslime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BloodJelly:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BloodFeeder:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CrimsonAxe:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.IchorSticker:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.FloatyGross:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigMimicCrimson:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertGhoulCrimson:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.PigronCrimson:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Jungle
        case NPCID.JungleBat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.JungleSlime:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.JungleCreeper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BigMimicJungle:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.Piranha:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Snatcher:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.AngryTrapper:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.Derpling:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GiantFlyingFox:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.AnglerFish:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Arapaima:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.GiantTortoise:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Ocean
        case NPCID.PinkJellyfish:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Crab:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Squid:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.SeaSnail:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Shark:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;

        // Mushroom 
        case NPCID.AnomuraFungus:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.FungiBulb:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.GiantFungiBulb:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.MushiLadybug:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.Spore:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.FungiSpore:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.FungoFish:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Underground Desert 
        case NPCID.WalkingAntlion:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.FlyingAntlion:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.TombCrawlerBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.TombCrawlerHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.TombCrawlerTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DesertLamiaDark:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertLamiaLight:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.DesertBeast:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.DesertScorpionWalk:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DesertScorpionWall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Underground Jungle 
        case NPCID.Hornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.HornetFatty:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.HornetHoney:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.HornetLeafy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.HornetSpikey:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.HornetStingy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigHornetFatty:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigHornetHoney:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigHornetLeafy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigHornetSpikey:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigHornetStingy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.BigMossHornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.GiantMossHornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleHornetFatty:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleHornetHoney:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleHornetLeafy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleHornetSpikey:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleHornetStingy:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LittleMossHornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.MossHornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.TinyMossHornet:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.ManEater:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.SpikedJungleSlime:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LacBeetle:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DoctorBones:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Bee:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BeeSmall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.JungleCreeperWall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Moth:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Ice Biome 
        case NPCID.IceBat:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;
        case NPCID.SnowFlinx:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;
        case NPCID.SpikedIceSlime:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;
        case NPCID.ArmoredViking:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.UndeadViking:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.UndeadMiner:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.CyanBeetle:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Nymph:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.IceTortoise:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;
        case NPCID.IcyMerman:
          npc.Unbidden().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;

        //Hallow
        case NPCID.Pixie:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.Unicorn:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.RainbowSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.Gastropod:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.LightningBug:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Underground Hallow
        case NPCID.IlluminantSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.IlluminantBat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.ChaosElemental:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.EnchantedSword:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BigMimicHallow:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.PigronHallow:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.DesertGhoulHallow:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;

        // Granite Cave
        case NPCID.GraniteFlyer:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.GraniteGolem:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Marble Cave
        case NPCID.GreekSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.Medusa:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Jungle Temple
        case NPCID.FlyingSnake:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Lihzahrd:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.LihzahrdCrawler:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Meteorite
        case NPCID.MeteorHead:
          npc.Unbidden().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Spider Cave
        case NPCID.WallCreeper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.WallCreeperWall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BlackRecluse:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BlackRecluseWall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Space
        case NPCID.Harpy:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernBody:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernBody2:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernBody3:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernHead:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernLegs:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.WyvernTail:
          npc.Unbidden().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;

        // Underground
        case NPCID.Skeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SkeletonArcher:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.HeavySkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SmallSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.HeadacheSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigHeadacheSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SmallHeadacheSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.MisassembledSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigMisassembledSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SmallMisassembledSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.PantlessSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.BigPantlessSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SmallPantlessSkeleton:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SkeletonTopHat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SkeletonAlien:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.SkeletonAstonaut:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.GiantWormBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GiantWormHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GiantWormTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.YellowSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BlueJellyfish:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.DiggerBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DiggerHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DiggerTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.ToxicSludge:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GreenJellyfish:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;

        // Cavern
        case NPCID.BlackSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BabySlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MotherSlime:
          npc.Unbidden().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander4:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander5:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander6:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander7:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander8:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander9:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Crawdad:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Crawdad2:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.CaveBat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GiantShelly:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.GiantShelly2:
          npc.Unbidden().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Tim:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.CochinealBeetle:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GiantBat:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RuneWizard:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Blood Moon 
        case NPCID.Clown:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Drippler:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.TheGroom:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.TheBride:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Rain 
        case NPCID.AngryNimbus:
          npc.Unbidden().resists = new float[8] { 0.5f, 1.5f, 1.5f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.FlyingFish:
          npc.Unbidden().resists = new float[8] { 0.5f, 1.5f, 1.5f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.UmbrellaSlime:
          npc.Unbidden().resists = new float[8] { 0.75f, 1f, 1.5f, 0.75f, 1f, 1f, 1f, 1f };
          break;

        // Sandstorm
        case NPCID.Tumbleweed:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SandElemental:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SandsharkHallow:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Goblin Invasion
        case NPCID.GoblinArcher:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GoblinPeon:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GoblinSorcerer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GoblinSummoner:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GoblinThief:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GoblinWarrior:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.ShadowFlameApparition:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Old Ones Army 
        case NPCID.DD2LightningBugT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2AttackerTest:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2Bartender:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2Betsy:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2DarkMageT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2DarkMageT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2DrakinT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2DrakinT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinBomberT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinBomberT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinBomberT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2GoblinT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2JavelinstT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2JavelinstT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2JavelinstT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2KoboldFlyerT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2KoboldFlyerT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2KoboldWalkerT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2KoboldWalkerT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2OgreT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2OgreT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2SkeletonT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2SkeletonT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2WitherBeastT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2WitherBeastT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2WyvernT1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2WyvernT2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DD2WyvernT3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Frost Legion 
        case NPCID.MisterStabby:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SnowBalla:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SnowmanGangsta:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Pirate Invasion 
        case NPCID.Parrot:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateCaptain:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateCorsair:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateCrossbower:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateDeadeye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateDeckhand:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateShip:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PirateShipCannon:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Solar Eclipse
        case NPCID.Mothron:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MothronEgg:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MothronSpawn:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.CreatureFromTheDeep:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Butcher:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DeadlySphere:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DrManFly:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Eyezor:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Frankenstein:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Fritz:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Nailhead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Psycho:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Reaper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SwampThing:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.ThePossessed:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Vampire:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Martian Madness
        case NPCID.BrainScrambler:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GigaZapper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GrayGrunt:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MartianEngineer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MartianOfficer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MartianTurret:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.MartianWalker:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.RayGunner:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scutlix:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.ScutlixRider:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Pumpkin Moon 
        case NPCID.HeadlessHorseman:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Hellhound:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Poltergeist:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow4:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow5:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow6:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow7:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow8:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow9:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Scarecrow10:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Splinterling:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Frost Moon 
        case NPCID.ElfArcher:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.ElfCopter:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Flocko:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.GingerbreadMan:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Krampus:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Nutcracker:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.NutcrackerSpinning:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.PresentMimic:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Yeti:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Nebula Pillar
        case NPCID.NebulaBeast:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.NebulaBrain:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.NebulaHeadcrab:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.NebulaSoldier:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Solar Pillar
        case NPCID.SolarCorite:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarCrawltipedeBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarCrawltipedeHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarCrawltipedeTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarDrakomire:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarDrakomireRider:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarSolenian:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.SolarSroller:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Stardust Pillar 
        case NPCID.StardustCellBig:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustCellSmall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustJellyfishBig:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustJellyfishSmall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustSoldier:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustSpiderBig:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustSpiderSmall:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustWormBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustWormHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.StardustWormTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Vortex Pillar
        case NPCID.VortexHornet:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VortexHornetQueen:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VortexLarva:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VortexRifleman:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.VortexSoldier:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;


        // Bosses //

        // King Slime
        case NPCID.KingSlime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.SlimeSpiked:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Eye of Cthulhu
        case NPCID.EyeofCthulhu:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.ServantofCthulhu:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Eater of Worlds
        case NPCID.EaterofWorldsBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.EaterofWorldsHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.EaterofWorldsTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
          
        // Brain of Cthulhu
        case NPCID.BrainofCthulhu:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          
          break;
        case NPCID.Creeper:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Queen Bee
        case NPCID.QueenBee:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        //Skeletron
        case NPCID.SkeletronHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.SkeletronHand:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Wall of Flesh
        case NPCID.WallofFlesh:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.WallofFleshEye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheHungry:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheHungryII:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Skeletron Prime
        case NPCID.SkeletronPrime:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeCannon:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeLaser:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeSaw:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeVice:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        //The Destroyer
        case NPCID.TheDestroyer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheDestroyerBody:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheDestroyerTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Probe:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // The Twins
        case NPCID.Retinazer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Spazmatism:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Plantera
        case NPCID.Plantera:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PlanterasHook:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PlanterasTentacle:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Golem
        case NPCID.Golem:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemFistLeft:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemFistRight:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemHeadFree:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Lunatic Cultist
        case NPCID.CultistBoss:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistBossClone:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody1:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody3:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody4:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonTail:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientCultistSquidhead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientDoom:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientLight:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        //Duke Fishron
        case NPCID.DukeFishron:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Sharkron:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Sharkron2:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        
        // Moonlord
        case NPCID.MoonLordCore:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordFreeEye:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordHand:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordHead:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordLeechBlob:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Martian Madness Bosses
        case NPCID.MartianSaucer:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerCannon:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerCore:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerTurret:
          npc.Unbidden().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
      }
    }
  }
}