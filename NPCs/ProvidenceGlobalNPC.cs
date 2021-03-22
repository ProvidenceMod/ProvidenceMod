using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.TexturePack;

namespace ProvidenceMod
{
  public class ProvidenceGlobalNPC : GlobalNPC
  {
    // Elemental variables for NPC's
    public int contactDamageEl = -1; // Contact damage element, -1 by default for typeless
    public float[] resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }; // Fire, Ice, Lightning, Water, Earth, Air, Radiant, Necrotic
    // Elemental variables also contained within GlobalItem, GlobalProjectile, and Player
    public override bool InstancePerEntity => true;
    public override bool CloneNewInstances => true;
    // Status effect bools
    public bool hypodermia;
    public bool freezing;
    public bool frozen;
    public bool spawnReset = true;
    public bool texturePackEnabled;
    public bool maxSpawnsTempSet;
    public int maxSpawnsTemp;
    public int armorMax = 1000;
    public int armor = 1000;
    public int buffCount;
    public float armorEfficiency = 0.5f;
    public bool Armored
    {
      get => armor > 0;
    }
    public override void ResetEffects(NPC npc)
    {
      npc.Providence().hypodermia = false;
      npc.Providence().freezing = false;
      npc.Providence().frozen = false;
    }

    public override void AI(NPC npc)
    {
      if (npc.Providence().freezing)
      {
        npc.velocity.X /= 1.1f;
      }
      if (npc.Providence().frozen)
      {
        npc.velocity.X = 0; // Frozen in place. Keep in mind they can still shoot if they could already.
        npc.velocity.Y = 0;
      }
    }
    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
    {
      if (!texturePackEnabled)
      {
        NPCManager.InitializeNPCTextures();
        texturePackEnabled = true;
      }
      spriteBatch.Draw(ModContent.GetTexture("ProvidenceMod/Items/Weapons/Melee/CirrusEdge"), npc.Center - Main.screenPosition + new Vector2(0.0f, npc.gfxOffY), new Rectangle(0, 0, 44, 44), Color.White, 0f, npc.Center, 1f, SpriteEffects.None, 0f);
      SpriteBatch spriteBatch2 = new SpriteBatch(Main.graphics.GraphicsDevice);
      spriteBatch2.Begin();
      spriteBatch2.Draw(ModContent.GetTexture("ProvidenceMod/Items/Weapons/Melee/CirrusEdge"), npc.Center - Main.screenPosition + new Vector2(0.0f, npc.gfxOffY), new Rectangle(0, 0, 44, 44), Color.White, 0f, npc.Center, 1f, SpriteEffects.None, 0f);
      spriteBatch2.End();
    }

    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
    {
      if (hypodermia)
      {
        damage = (int)(damage * 1.20f); // 20% damage increase
      }

      damage = item.CalcEleDamage(npc, ref damage);
      damage = ArmorCalculation(npc, ref damage, ref crit);
      // if (npc.Providence().Armored)
      // {
      //   npc.Providence().armor -= damage;
      //   if (!crit)
      //   {
      //     npc.life++;
      //   }
      //   else if (crit)
      //   {
      //     npc.life += 2;
      //   }
      //   for (int index = 0; index < 100; index++)
      //   {
      //     CombatText combatText = Main.combatText[index];
      //     if(combatText.text == "1" || combatText.text == "2")
      //       combatText.active = false;
      //   }
      //   CombatText.UpdateCombatText();
      //   CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 10, default, default), Color.White, damage, false, false);
      //   damage = 0;
      // }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      if (hypodermia)
      {
        damage = (int)(damage * 1.20f); // 20% damage increase
      }

      damage = projectile.CalcEleDamage(npc, ref damage);
      damage = ArmorCalculation(npc, ref damage, ref crit);
      if (projectile.Providence().inverseKB)
      {
        npc.StrikeNPC(0, knockback, -projectile.direction, crit);
      }
    }
    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
      ProvidencePlayer proPlayer = player.Providence();
      if (proPlayer.intimidated)
      {
        if (!maxSpawnsTempSet)
        {
          maxSpawnsTemp = maxSpawns;
          maxSpawnsTempSet = true;
        }
        spawnRate = 2147483647;
        maxSpawns = 0;
        spawnReset = false;
      }
      if (!proPlayer.intimidated && !spawnReset)
      {
        if (maxSpawnsTempSet)
        {
          maxSpawns = maxSpawnsTemp;
          maxSpawnsTemp = 0;
          maxSpawnsTempSet = false;
        }
        spawnRate = 0;
        spawnReset = true;
      }
    }
    public override void SetDefaults(NPC npc)
    {
      switch (npc.type)
      {
        // Underworld Enemies
        case NPCID.FireImp:
        case NPCID.RedDevil:
        case NPCID.BoneSerpentHead:
        case NPCID.BoneSerpentBody:
        case NPCID.BoneSerpentTail:
        case NPCID.Demon:
        case NPCID.VoodooDemon:
        case NPCID.Lavabat:
        case NPCID.Hellbat:
        case NPCID.LavaSlime:
        case NPCID.DemonTaxCollector:
          npc.Providence().resists = new float[8] { 0.25f, 1.25f, 0.25f, 1.5f, 1f, 1f, 1f, 1f };
          break;

        // Dungeon Enemies
        case NPCID.AngryBones:
        case NPCID.CursedSkull:
        case NPCID.AngryBonesBig:
        case NPCID.AngryBonesBigHelmet:
        case NPCID.AngryBonesBigMuscle:
        case NPCID.DarkCaster:
        case NPCID.BlueArmoredBones:
        case NPCID.BlueArmoredBonesMace:
        case NPCID.BlueArmoredBonesNoPants:
        case NPCID.BlueArmoredBonesSword:
        case NPCID.BoneLee:
        case NPCID.Paladin:
        case NPCID.DiabolistRed:
        case NPCID.DiabolistWhite:
        case NPCID.GiantCursedSkull:
        case NPCID.HellArmoredBones:
        case NPCID.HellArmoredBonesMace:
        case NPCID.HellArmoredBonesSpikeShield:
        case NPCID.HellArmoredBonesSword:
        case NPCID.RustyArmoredBonesAxe:
        case NPCID.RustyArmoredBonesFlail:
        case NPCID.RustyArmoredBonesSword:
        case NPCID.RustyArmoredBonesSwordNoArmor:
        case NPCID.RaggedCaster:
        case NPCID.RaggedCasterOpenCoat:
        case NPCID.SkeletonSniper:
        case NPCID.TacticalSkeleton:
        case NPCID.DungeonSlime:
        case NPCID.DungeonSpirit:
        case NPCID.Necromancer:
        case NPCID.NecromancerArmored:
        case NPCID.CultistDevote:
        case NPCID.CultistArcherBlue:

        // Zombies (Yes, there's that fucking many)
        case NPCID.Zombie:
        case NPCID.ZombieDoctor:
        case NPCID.ZombieElf:
        case NPCID.ZombieElfBeard:
        case NPCID.ZombieElfGirl:
        case NPCID.ZombieMushroom:
        case NPCID.ZombieMushroomHat:
        case NPCID.ZombiePixie:
        case NPCID.ZombieRaincoat:
        case NPCID.ZombieSuperman:
        case NPCID.ZombieSweater:
        case NPCID.ZombieXmas:
        case NPCID.ArmedZombie:
        case NPCID.ArmedZombieCenx:
        case NPCID.ArmedZombiePincussion:
        case NPCID.ArmedZombieSlimed:
        case NPCID.ArmedZombieSwamp:
        case NPCID.ArmedZombieTwiggy:
        case NPCID.BaldZombie:
        case NPCID.BigBaldZombie:
        case NPCID.BigFemaleZombie:
        case NPCID.BigPincushionZombie:
        case NPCID.BigRainZombie:
        case NPCID.BigSlimedZombie:
        case NPCID.BigSwampZombie:
        case NPCID.BigTwiggyZombie:
        case NPCID.BigZombie:
        case NPCID.BloodZombie:
        case NPCID.FemaleZombie:
        case NPCID.PincushionZombie:
        case NPCID.SlimedZombie:
        case NPCID.SmallBaldZombie:
        case NPCID.SmallFemaleZombie:
        case NPCID.SmallPincushionZombie:
        case NPCID.SmallRainZombie:
        case NPCID.SmallSlimedZombie:
        case NPCID.SmallSwampZombie:
        case NPCID.SmallTwiggyZombie:
        case NPCID.SmallZombie:
        case NPCID.SwampZombie:
        case NPCID.TwiggyZombie:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;

        // Surface        
        case NPCID.BlueSlime:
        case NPCID.GreenSlime:
        case NPCID.RedSlime:
        case NPCID.PurpleSlime:
        case NPCID.Pinky:
          npc.Providence().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DemonEye:
        case NPCID.DemonEye2:
        case NPCID.DemonEyeOwl:
        case NPCID.DemonEyeSpaceship:
        case NPCID.PossessedArmor:
        case NPCID.WanderingEye:
        case NPCID.Wraith:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.50f, 0.25f };
          break;
        case NPCID.HoppinJack:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;
        case NPCID.Werewolf:
        case NPCID.GoblinScout:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Snow Biome
        case NPCID.IceSlime:
          npc.Providence().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 1f, 1f, 1f };
          break;
        case NPCID.ZombieEskimo:
        case NPCID.ArmedZombieEskimo:
        case NPCID.CorruptPenguin:
        case NPCID.CrimsonPenguin:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.IceElemental:
          npc.Providence().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 1f, 1f, 1f };
          break;
        case NPCID.Wolf:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.IceGolem:
          npc.Providence().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;

        // Desert
        case NPCID.SandSlime:
        case NPCID.Vulture:
        case NPCID.Antlion:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Mummy:
        case NPCID.DarkMummy:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.LightMummy:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;

        //Evil
        case NPCID.EaterofSouls:
        case NPCID.BigEater:
        case NPCID.LittleEater:
        case NPCID.Corruptor:
        case NPCID.BigMimicCorruption:
        case NPCID.SandsharkCorrupt:
        case NPCID.CorruptGoldfish:
        case NPCID.CorruptSlime:
        case NPCID.DesertGhoulCorruption:
        case NPCID.Slimeling:
        case NPCID.CorruptBunny:
        case NPCID.CursedHammer:
        case NPCID.Clinger:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertDjinn:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Slimer:
        case NPCID.Slimer2:
        case NPCID.SandsharkCrimson:
        case NPCID.BloodCrawler:
        case NPCID.CrimsonGoldfish:
        case NPCID.FaceMonster:
        case NPCID.Crimera:
        case NPCID.BigCrimera:
        case NPCID.LittleCrimera:
        case NPCID.Herpling:
        case NPCID.Crimslime:
        case NPCID.BloodJelly:
        case NPCID.BloodFeeder:
        case NPCID.CrimsonAxe:
        case NPCID.IchorSticker:
        case NPCID.FloatyGross:
        case NPCID.BigMimicCrimson:
        case NPCID.DesertGhoulCrimson:
        case NPCID.PigronCrimson:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Jungle
        case NPCID.JungleBat:
        case NPCID.JungleCreeper:
        case NPCID.Derpling:
        case NPCID.GiantFlyingFox:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.JungleSlime:
        case NPCID.BigMimicJungle:
        case NPCID.Snatcher:
        case NPCID.AngryTrapper:
        case NPCID.GiantTortoise:
          npc.Providence().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.Piranha:
        case NPCID.AnglerFish:
        case NPCID.Arapaima:

        // Ocean
        case NPCID.PinkJellyfish:
        case NPCID.Crab:
        case NPCID.Squid:
        case NPCID.SeaSnail:
        case NPCID.Shark:
          npc.Providence().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;

        // Mushroom 
        case NPCID.AnomuraFungus:
        case NPCID.FungiBulb:
        case NPCID.GiantFungiBulb:
        case NPCID.MushiLadybug:
        case NPCID.Spore:
        case NPCID.FungiSpore:
        case NPCID.FungoFish:
          npc.Providence().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Underground Desert 
        case NPCID.WalkingAntlion:
        case NPCID.FlyingAntlion:
        case NPCID.TombCrawlerBody:
        case NPCID.TombCrawlerHead:
        case NPCID.TombCrawlerTail:
        case NPCID.DesertScorpionWalk:
        case NPCID.DesertScorpionWall:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DesertLamiaDark:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.DesertLamiaLight:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.DesertBeast:

        //Underground Jungle 
        case NPCID.Hornet:
        case NPCID.HornetFatty:
        case NPCID.HornetHoney:
        case NPCID.HornetLeafy:
        case NPCID.HornetSpikey:
        case NPCID.HornetStingy:
        case NPCID.BigHornetFatty:
        case NPCID.BigHornetHoney:
        case NPCID.BigHornetLeafy:
        case NPCID.BigHornetSpikey:
        case NPCID.BigHornetStingy:
        case NPCID.BigMossHornet:
        case NPCID.GiantMossHornet:
        case NPCID.LittleHornetFatty:
        case NPCID.LittleHornetHoney:
        case NPCID.LittleHornetLeafy:
        case NPCID.LittleHornetSpikey:
        case NPCID.LittleHornetStingy:
        case NPCID.LittleMossHornet:
        case NPCID.MossHornet:
        case NPCID.TinyMossHornet:
        case NPCID.ManEater:
        case NPCID.SpikedJungleSlime:
          npc.Providence().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;
        case NPCID.LacBeetle:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.DoctorBones:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.Bee:
        case NPCID.BeeSmall:
        case NPCID.JungleCreeperWall:
        case NPCID.Moth:
        case NPCID.ArmoredViking:
        case NPCID.CyanBeetle:
        case NPCID.Nymph:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Ice Biome 
        case NPCID.IceBat:
        case NPCID.SnowFlinx:
        case NPCID.SpikedIceSlime:
        case NPCID.IceTortoise:
        case NPCID.IcyMerman:
          npc.Providence().resists = new float[8] { 1.5f, 0.25f, 1f, 1f, 0.25f, 0.5f, 1f, 1f };
          break;
        case NPCID.UndeadViking:
        case NPCID.UndeadMiner:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.25f, 0.25f };
          break;

        // Hallow
        case NPCID.Pixie:
        case NPCID.Unicorn:
        case NPCID.RainbowSlime:
        case NPCID.Gastropod:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.LightningBug:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        //Underground Hallow
        case NPCID.IlluminantSlime:
        case NPCID.IlluminantBat:
        case NPCID.BigMimicHallow:
        case NPCID.PigronHallow:
        case NPCID.DesertGhoulHallow:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 0.25f, 1.5f };
          break;
        case NPCID.ChaosElemental:
        case NPCID.EnchantedSword:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Granite Cave
        case NPCID.GraniteFlyer:
        case NPCID.GraniteGolem:

        // Marble Cave
        case NPCID.GreekSkeleton:
        case NPCID.Medusa:
          npc.Providence().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Jungle Temple
        case NPCID.FlyingSnake:
        case NPCID.Lihzahrd:
        case NPCID.LihzahrdCrawler:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Meteorite
        case NPCID.MeteorHead:
          npc.Providence().resists = new float[8] { 1f, 0.25f, 0.5f, 1f, 0.25f, 1.5f, 1f, 1f };
          break;

        // Spider Cave
        case NPCID.WallCreeper:
        case NPCID.WallCreeperWall:
        case NPCID.BlackRecluse:
        case NPCID.BlackRecluseWall:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Space
        case NPCID.Harpy:
        case NPCID.WyvernBody:
        case NPCID.WyvernBody2:
        case NPCID.WyvernBody3:
        case NPCID.WyvernHead:
        case NPCID.WyvernLegs:
        case NPCID.WyvernTail:
          npc.Providence().resists = new float[8] { 1f, 1.5f, 1f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;

        // Underground
        case NPCID.Skeleton:
        case NPCID.SkeletonArcher:
        case NPCID.HeavySkeleton:
        case NPCID.SmallSkeleton:
        case NPCID.BigSkeleton:
        case NPCID.HeadacheSkeleton:
        case NPCID.BigHeadacheSkeleton:
        case NPCID.SmallHeadacheSkeleton:
        case NPCID.MisassembledSkeleton:
        case NPCID.BigMisassembledSkeleton:
        case NPCID.SmallMisassembledSkeleton:
        case NPCID.PantlessSkeleton:
        case NPCID.BigPantlessSkeleton:
        case NPCID.SmallPantlessSkeleton:
        case NPCID.SkeletonTopHat:
        case NPCID.SkeletonAlien:
        case NPCID.SkeletonAstonaut:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;
        case NPCID.GiantWormBody:
        case NPCID.GiantWormHead:
        case NPCID.GiantWormTail:
        case NPCID.DiggerBody:
        case NPCID.DiggerHead:
        case NPCID.DiggerTail:
        case NPCID.ToxicSludge:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.YellowSlime:
          npc.Providence().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.BlueJellyfish:
        case NPCID.GreenJellyfish:
          npc.Providence().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;

        // Cavern
        case NPCID.BlackSlime:
        case NPCID.BabySlime:
        case NPCID.MotherSlime:
          npc.Providence().resists = new float[8] { 1.25f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Salamander:
        case NPCID.Salamander2:
        case NPCID.Salamander3:
        case NPCID.Salamander4:
        case NPCID.Salamander5:
        case NPCID.Salamander6:
        case NPCID.Salamander7:
        case NPCID.Salamander8:
        case NPCID.Salamander9:
        case NPCID.CaveBat:
        case NPCID.CochinealBeetle:
        case NPCID.GiantBat:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;
        case NPCID.Crawdad:
        case NPCID.Crawdad2:
        case NPCID.GiantShelly:
        case NPCID.GiantShelly2:
          npc.Providence().resists = new float[8] { 0.5f, 1f, 1.5f, 0.25f, 1f, 0.25f, 1f, 1f };
          break;
        case NPCID.Tim:
        case NPCID.RuneWizard:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Blood Moon 
        case NPCID.Clown:
        case NPCID.Drippler:
        case NPCID.TheGroom:
        case NPCID.TheBride:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.25f };
          break;

        // Rain 
        case NPCID.AngryNimbus:
        case NPCID.FlyingFish:
          npc.Providence().resists = new float[8] { 0.5f, 1.5f, 1.5f, 0.25f, 0.5f, 0.25f, 1f, 1f };
          break;
        case NPCID.UmbrellaSlime:
          npc.Providence().resists = new float[8] { 0.75f, 1f, 1.5f, 0.75f, 1f, 1f, 1f, 1f };
          break;

        // Sandstorm
        case NPCID.Tumbleweed:
        case NPCID.SandElemental:
        case NPCID.SandsharkHallow:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Goblin Invasion
        case NPCID.GoblinArcher:
        case NPCID.GoblinPeon:
        case NPCID.GoblinSorcerer:
        case NPCID.GoblinSummoner:
        case NPCID.GoblinThief:
        case NPCID.GoblinWarrior:
        case NPCID.ShadowFlameApparition:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Old Ones Army 
        case NPCID.DD2LightningBugT3:
        case NPCID.DD2AttackerTest:
        case NPCID.DD2Bartender:
        case NPCID.DD2Betsy:
        case NPCID.DD2DarkMageT1:
        case NPCID.DD2DarkMageT3:
        case NPCID.DD2DrakinT2:
        case NPCID.DD2DrakinT3:
        case NPCID.DD2GoblinBomberT1:
        case NPCID.DD2GoblinBomberT2:
        case NPCID.DD2GoblinBomberT3:
        case NPCID.DD2GoblinT1:
        case NPCID.DD2GoblinT2:
        case NPCID.DD2GoblinT3:
        case NPCID.DD2JavelinstT1:
        case NPCID.DD2JavelinstT2:
        case NPCID.DD2JavelinstT3:
        case NPCID.DD2KoboldFlyerT2:
        case NPCID.DD2KoboldFlyerT3:
        case NPCID.DD2KoboldWalkerT2:
        case NPCID.DD2KoboldWalkerT3:
        case NPCID.DD2OgreT2:
        case NPCID.DD2OgreT3:
        case NPCID.DD2SkeletonT1:
        case NPCID.DD2SkeletonT3:
        case NPCID.DD2WitherBeastT2:
        case NPCID.DD2WitherBeastT3:
        case NPCID.DD2WyvernT1:
        case NPCID.DD2WyvernT2:
        case NPCID.DD2WyvernT3:

        // Frost Legion 
        case NPCID.MisterStabby:
        case NPCID.SnowBalla:
        case NPCID.SnowmanGangsta:

        // Pirate Invasion 
        case NPCID.Parrot:
        case NPCID.PirateCaptain:
        case NPCID.PirateCorsair:
        case NPCID.PirateCrossbower:
        case NPCID.PirateDeadeye:
        case NPCID.PirateDeckhand:
        case NPCID.PirateShip:
        case NPCID.PirateShipCannon:

        // Solar Eclipse
        case NPCID.Mothron:
        case NPCID.MothronEgg:
        case NPCID.MothronSpawn:
        case NPCID.CreatureFromTheDeep:
        case NPCID.Butcher:
        case NPCID.DeadlySphere:
        case NPCID.DrManFly:
        case NPCID.Eyezor:
        case NPCID.Frankenstein:
        case NPCID.Fritz:
        case NPCID.Nailhead:
        case NPCID.Psycho:
        case NPCID.Reaper:
        case NPCID.SwampThing:
        case NPCID.ThePossessed:
        case NPCID.Vampire:

        // Martian Madness
        case NPCID.BrainScrambler:
        case NPCID.GigaZapper:
        case NPCID.GrayGrunt:
        case NPCID.MartianEngineer:
        case NPCID.MartianOfficer:
        case NPCID.MartianTurret:
        case NPCID.MartianWalker:
        case NPCID.RayGunner:
        case NPCID.Scutlix:
        case NPCID.ScutlixRider:

        // Pumpkin Moon 
        case NPCID.HeadlessHorseman:
        case NPCID.Hellhound:
        case NPCID.Poltergeist:
        case NPCID.Scarecrow1:
        case NPCID.Scarecrow2:
        case NPCID.Scarecrow3:
        case NPCID.Scarecrow4:
        case NPCID.Scarecrow5:
        case NPCID.Scarecrow6:
        case NPCID.Scarecrow7:
        case NPCID.Scarecrow8:
        case NPCID.Scarecrow9:
        case NPCID.Scarecrow10:
        case NPCID.Splinterling:

        // Frost Moon 
        case NPCID.ElfArcher:
        case NPCID.ElfCopter:
        case NPCID.Flocko:
        case NPCID.GingerbreadMan:
        case NPCID.Krampus:
        case NPCID.Nutcracker:
        case NPCID.NutcrackerSpinning:
        case NPCID.PresentMimic:
        case NPCID.Yeti:

        // Nebula Pillar
        case NPCID.NebulaBeast:
        case NPCID.NebulaBrain:
        case NPCID.NebulaHeadcrab:
        case NPCID.NebulaSoldier:

        //Solar Pillar
        case NPCID.SolarCorite:
        case NPCID.SolarCrawltipedeBody:
        case NPCID.SolarCrawltipedeHead:
        case NPCID.SolarCrawltipedeTail:
        case NPCID.SolarDrakomire:
        case NPCID.SolarDrakomireRider:
        case NPCID.SolarSolenian:
        case NPCID.SolarSroller:

        // Stardust Pillar 
        case NPCID.StardustCellBig:
        case NPCID.StardustCellSmall:
        case NPCID.StardustJellyfishBig:
        case NPCID.StardustJellyfishSmall:
        case NPCID.StardustSoldier:
        case NPCID.StardustSpiderBig:
        case NPCID.StardustSpiderSmall:
        case NPCID.StardustWormBody:
        case NPCID.StardustWormHead:
        case NPCID.StardustWormTail:

        //Vortex Pillar
        case NPCID.VortexHornet:
        case NPCID.VortexHornetQueen:
        case NPCID.VortexLarva:
        case NPCID.VortexRifleman:
        case NPCID.VortexSoldier:
        // Bosses //

        // King Slime
        case NPCID.KingSlime:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.SlimeSpiked:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Eye of Cthulhu
        case NPCID.EyeofCthulhu:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.ServantofCthulhu:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          break;

        // Eater of Worlds
        case NPCID.EaterofWorldsBody:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.EaterofWorldsHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.EaterofWorldsTail:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        // Brain of Cthulhu
        case NPCID.BrainofCthulhu:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Creeper:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Queen Bee
        case NPCID.QueenBee:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        //Skeletron
        case NPCID.SkeletronHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.SkeletronHand:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Wall of Flesh
        case NPCID.WallofFlesh:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.WallofFleshEye:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheHungry:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheHungryII:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechBody:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.LeechTail:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Skeletron Prime
        case NPCID.SkeletronPrime:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeCannon:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeLaser:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeSaw:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PrimeVice:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        //The Destroyer
        case NPCID.TheDestroyer:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheDestroyerBody:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.TheDestroyerTail:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Probe:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // The Twins
        case NPCID.Retinazer:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Spazmatism:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Plantera
        case NPCID.Plantera:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PlanterasHook:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.PlanterasTentacle:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Golem
        case NPCID.Golem:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemFistLeft:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemFistRight:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.GolemHeadFree:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Lunatic Cultist
        case NPCID.CultistBoss:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistBossClone:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody1:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody2:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody3:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonBody4:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.CultistDragonTail:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientCultistSquidhead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientDoom:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.AncientLight:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        //Duke Fishron
        case NPCID.DukeFishron:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Sharkron:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.Sharkron2:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        // Moonlord
        case NPCID.MoonLordCore:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordFreeEye:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordHand:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordHead:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MoonLordLeechBlob:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;

        // Martian Madness Bosses
        case NPCID.MartianSaucer:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerCannon:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerCore:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
        case NPCID.MartianSaucerTurret:
          npc.Providence().resists = new float[8] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
          npc.buffImmune[mod.BuffType("Freezing")] = true;
          npc.buffImmune[mod.BuffType("Frozen")] = true;
          break;
      }
    }
  }
}