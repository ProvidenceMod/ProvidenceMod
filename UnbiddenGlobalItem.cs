using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using static UnbiddenMod.UnbiddenUtils;

namespace UnbiddenMod
{
  public class UnbiddenGlobalItem : GlobalItem
  {
    // Elemental variables for Items
    public bool inverseKB = false;
    public int element = -1, weakEl = -1; // -1 means Typeless, meaning we don't worry about this in the first place
    // Elemental variables also contained within GlobalProjectile, GlobalNPC, and Player
    public int elementDef = 0, weakElDef = 0;
    public override bool InstancePerEntity => true;

    public UnbiddenGlobalItem()
    {
      element = -1;
      elementDef = 0;
      weakEl = -1;
      weakElDef = 0;
    }

    public override GlobalItem Clone(Item item, Item itemClone)
    {
      UnbiddenGlobalItem myClone = (UnbiddenGlobalItem)base.Clone(item, itemClone);
      myClone.element = element;
      myClone.elementDef = elementDef;
      myClone.weakEl = weakEl;
      myClone.weakElDef = weakElDef;
      return myClone;
    }
    public override void SetDefaults(Item item)
    {
      /*Mod magicStorage = ModLoader.GetMod("MagicStorage");
			if (magicStorage != null) 
      {
        switch (item.type)
        {
        case magicStorage.ItemType<CraftingAccess>():
          item.maxStack = 999;
          break;
        }
			}*/
      switch (item.type)
      {
        case ItemID.StickyBomb:
          item.maxStack = 999;
          break;
        case ItemID.StickyDynamite:
          item.maxStack = 999;
          break;
        case ItemID.StickyGlowstick:
          item.maxStack = 999;
          break;
        case ItemID.StickyGrenade:
          item.maxStack = 999;
          break;
        case ItemID.LifeCrystal:
          item.maxStack = 999;
          break;
        case ItemID.FallenStar:
          item.maxStack = 999;
          break;
        case ItemID.Torch:
          item.maxStack = 999;
          break;
        case ItemID.BlueTorch:
          item.maxStack = 999;
          break;
        case ItemID.TikiTorch:
          item.maxStack = 999;
          break;
        case ItemID.BoneTorch:
          item.maxStack = 999;
          break;
        case ItemID.CursedTorch:
          item.maxStack = 999;
          break;
        case ItemID.DemonTorch:
          item.maxStack = 999;
          break;
        case ItemID.GreenTorch:
          item.maxStack = 999;
          break;
        case ItemID.IceTorch:
          item.maxStack = 999;
          break;
        case ItemID.IchorTorch:
          item.maxStack = 999;
          break;
        case ItemID.OrangeTorch:
          item.maxStack = 999;
          break;
        case ItemID.PinkTorch:
          item.maxStack = 999;
          break;
        case ItemID.PurpleTorch:
          item.maxStack = 999;
          break;
        case ItemID.RainbowTorch:
          item.maxStack = 999;
          break;
        case ItemID.RedTorch:
          item.maxStack = 999;
          break;
        case ItemID.UltrabrightTorch:
          item.maxStack = 999;
          break;
        case ItemID.WhiteTorch:
          item.maxStack = 999;
          break;
        case ItemID.YellowTorch:
          item.maxStack = 999;
          break;
        case ItemID.AdamantiteBar:
          item.maxStack = 999;
          break;
        case ItemID.ChlorophyteBar:
          item.maxStack = 999;
          break;
        case ItemID.CobaltBar:
          item.maxStack = 999;
          break;
        case ItemID.CopperBar:
          item.maxStack = 999;
          break;
        case ItemID.CrimtaneBar:
          item.maxStack = 999;
          break;
        case ItemID.DemoniteBar:
          item.maxStack = 999;
          break;
        case ItemID.GoldBar:
          item.maxStack = 999;
          break;
        case ItemID.HallowedBar:
          item.maxStack = 999;
          break;
        case ItemID.HellstoneBar:
          item.maxStack = 999;
          break;
        case ItemID.IronBar:
          item.maxStack = 999;
          break;
        case ItemID.LeadBar:
          item.maxStack = 999;
          break;
        case ItemID.LunarBar:
          item.maxStack = 999;
          break;
        case ItemID.MeteoriteBar:
          item.maxStack = 999;
          break;
        case ItemID.MythrilBar:
          item.maxStack = 999;
          break;
        case ItemID.OrichalcumBar:
          item.maxStack = 999;
          break;
        case ItemID.PalladiumBar:
          item.maxStack = 999;
          break;
        case ItemID.PlatinumBar:
          item.maxStack = 999;
          break;
        case ItemID.ShroomiteBar:
          item.maxStack = 999;
          break;
        case ItemID.SilverBar:
          item.maxStack = 999;
          break;
        case ItemID.SpectreBar:
          item.maxStack = 999;
          break;
        case ItemID.TinBar:
          item.maxStack = 999;
          break;
        case ItemID.TitaniumBar:
          item.maxStack = 999;
          break;
        case ItemID.TungstenBar:
          item.maxStack = 999;
          break;
        case ItemID.AmmoReservationPotion:
          item.maxStack = 999;
          break;
        case ItemID.ArcheryPotion:
          item.maxStack = 999;
          break;
        case ItemID.BattlePotion:
          item.maxStack = 999;
          break;
        case ItemID.BuilderPotion:
          item.maxStack = 999;
          break;
        case ItemID.CalmingPotion:
          item.maxStack = 999;
          break;
        case ItemID.CratePotion:
          item.maxStack = 999;
          break;
        case ItemID.EndurancePotion:
          item.maxStack = 999;
          break;
        case ItemID.FeatherfallPotion:
          item.maxStack = 999;
          break;
        case ItemID.FishingPotion:
          item.maxStack = 999;
          break;
        case ItemID.FlipperPotion:
          item.maxStack = 999;
          break;
        case ItemID.GenderChangePotion:
          item.maxStack = 999;
          break;
        case ItemID.GillsPotion:
          item.maxStack = 999;
          break;
        case ItemID.GravitationPotion:
          item.maxStack = 999;
          break;
        case ItemID.GreaterHealingPotion:
          item.maxStack = 999;
          break;
        case ItemID.GreaterManaPotion:
          item.maxStack = 999;
          break;
        case ItemID.HealingPotion:
          item.maxStack = 999;
          break;
        case ItemID.HeartreachPotion:
          item.maxStack = 999;
          break;
        case ItemID.HunterPotion:
          item.maxStack = 999;
          break;
        case ItemID.InfernoPotion:
          item.maxStack = 999;
          break;
        case ItemID.InvisibilityPotion:
          item.maxStack = 999;
          break;
        case ItemID.IronskinPotion:
          item.maxStack = 999;
          break;
        case ItemID.LesserHealingPotion:
          item.maxStack = 999;
          break;
        case ItemID.LesserManaPotion:
          item.maxStack = 999;
          break;
        case ItemID.LesserRestorationPotion:
          item.maxStack = 999;
          break;
        case ItemID.LifeforcePotion:
          item.maxStack = 999;
          break;
        case ItemID.LovePotion:
          item.maxStack = 999;
          break;
        case ItemID.MagicPowerPotion:
          item.maxStack = 999;
          break;
        case ItemID.ManaPotion:
          item.maxStack = 999;
          break;
        case ItemID.ManaRegenerationPotion:
          item.maxStack = 999;
          break;
        case ItemID.MiningPotion:
          item.maxStack = 999;
          break;
        case ItemID.NightOwlPotion:
          item.maxStack = 999;
          break;
        case ItemID.ObsidianSkinPotion:
          item.maxStack = 999;
          break;
        case ItemID.RagePotion:
          item.maxStack = 999;
          break;
        case ItemID.RecallPotion:
          item.maxStack = 999;
          break;
        case ItemID.RedPotion:
          item.maxStack = 999;
          break;
        case ItemID.RegenerationPotion:
          item.maxStack = 999;
          break;
        case ItemID.RestorationPotion:
          item.maxStack = 999;
          break;
        case ItemID.ShinePotion:
          item.maxStack = 999;
          break;
        case ItemID.SonarPotion:
          item.maxStack = 999;
          break;
        case ItemID.SpelunkerPotion:
          item.maxStack = 999;
          break;
        case ItemID.StinkPotion:
          item.maxStack = 999;
          break;
        case ItemID.SummoningPotion:
          item.maxStack = 999;
          break;
        case ItemID.SuperHealingPotion:
          item.maxStack = 999;
          break;
        case ItemID.SuperManaPotion:
          item.maxStack = 999;
          break;
        case ItemID.SwiftnessPotion:
          item.maxStack = 999;
          break;
        case ItemID.TeleportationPotion:
          item.maxStack = 999;
          break;
        case ItemID.ThornsPotion:
          item.maxStack = 999;
          break;
        case ItemID.TitanPotion:
          item.maxStack = 999;
          break;
        case ItemID.TrapsightPotion:
          item.maxStack = 999;
          break;
        case ItemID.WarmthPotion:
          item.maxStack = 999;
          break;
        case ItemID.WaterWalkingPotion:
          item.maxStack = 999;
          break;
        case ItemID.WormholePotion:
          item.maxStack = 999;
          break;
        case ItemID.WrathPotion:
          item.maxStack = 999;
          break;
        case ItemID.Chest:
          item.maxStack = 999;
          break;
        case ItemID.BlueDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.BoneChest:
          item.maxStack = 999;
          break;
        case ItemID.BorealWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.CactusChest:
          item.maxStack = 999;
          break;
        case ItemID.CorruptionChest:
          item.maxStack = 999;
          break;
        case ItemID.CrimsonChest:
          item.maxStack = 999;
          break;
        case ItemID.CrystalChest:
          item.maxStack = 999;
          break;
        case ItemID.DynastyChest:
          item.maxStack = 999;
          break;
        case ItemID.EbonwoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_BlueDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_BoneChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_BorealWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_CactusChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_Chest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_CorruptionChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_CrimsonChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_CrystalChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_DynastyChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_EbonwoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_FleshChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_FrozenChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_GlassChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_GoldChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_GoldenChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_GraniteChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_GreenDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_HallowedChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_HoneyChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_IceChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_IvyChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_JungleChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_LihzahrdChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_LivingWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_MarbleChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_MartianChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_MeteoriteChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_MushroomChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_ObsidianChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_PalmWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_PearlwoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_PinkDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_PumpkinChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_RichMahoganyChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_ShadewoodChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_ShadowChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_SkywareChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_SlimeChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_SpookyChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_SteampunkChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_WaterChest:
          item.maxStack = 999;
          break;
        case ItemID.Fake_WebCoveredChest:
          item.maxStack = 999;
          break;
        case ItemID.FleshChest:
          item.maxStack = 999;
          break;
        case ItemID.FrozenChest:
          item.maxStack = 999;
          break;
        case ItemID.GlassChest:
          item.maxStack = 999;
          break;
        case ItemID.GoldChest:
          item.maxStack = 999;
          break;
        case ItemID.GoldenChest:
          item.maxStack = 999;
          break;
        case ItemID.GraniteChest:
          item.maxStack = 999;
          break;
        case ItemID.GreenDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.HallowedChest:
          item.maxStack = 999;
          break;
        case ItemID.HoneyChest:
          item.maxStack = 999;
          break;
        case ItemID.IceChest:
          item.maxStack = 999;
          break;
        case ItemID.IvyChest:
          item.maxStack = 999;
          break;
        case ItemID.JungleChest:
          item.maxStack = 999;
          break;
        case ItemID.LihzahrdChest:
          item.maxStack = 999;
          break;
        case ItemID.LivingWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.MarbleChest:
          item.maxStack = 999;
          break;
        case ItemID.MartianChest:
          item.maxStack = 999;
          break;
        case ItemID.MeteoriteChest:
          item.maxStack = 999;
          break;
        case ItemID.MushroomChest:
          item.maxStack = 999;
          break;
        case ItemID.ObsidianChest:
          item.maxStack = 999;
          break;
        case ItemID.PalmWoodChest:
          item.maxStack = 999;
          break;
        case ItemID.PearlwoodChest:
          item.maxStack = 999;
          break;
        case ItemID.PinkDungeonChest:
          item.maxStack = 999;
          break;
        case ItemID.PumpkinChest:
          item.maxStack = 999;
          break;
        case ItemID.RichMahoganyChest:
          item.maxStack = 999;
          break;
        case ItemID.ShadewoodChest:
          item.maxStack = 999;
          break;
        case ItemID.ShadowChest:
          item.maxStack = 999;
          break;
        case ItemID.SkywareChest:
          item.maxStack = 999;
          break;
        case ItemID.SlimeChest:
          item.maxStack = 999;
          break;
        case ItemID.SpookyChest:
          item.maxStack = 999;
          break;
        case ItemID.WaterChest:
          item.maxStack = 999;
          break;
        case ItemID.WebCoveredChest:
          item.maxStack = 999;
          break;

        ////////// VANILLA ELEMENTAL DEFENSES //////////
        // Prehardmode
        case ItemID.EbonwoodHelmet:
          // Provides a boost to Necrotic and a penalty to Radiant (defenses)
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.EbonwoodBreastplate:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.EbonwoodGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.ShadewoodHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.ShadewoodBreastplate:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.ShadewoodGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.RainCoat:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;
        case ItemID.RainHat:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;
        case ItemID.EskimoCoat:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.EskimoHood:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.EskimoPants:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.PinkEskimoCoat:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.PinkEskimoHood:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.PinkEskimoPants:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.AnglerHat:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;
        case ItemID.AnglerVest:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;
        case ItemID.AnglerPants:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;

        case ItemID.FossilHelm:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.FossilShirt:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.FossilPants:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeeHeadgear:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeeBreastplate:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeeGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.JungleHat:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.JungleShirt:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.JunglePants:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.MeteorHelmet:
          item.SetElementalTraits(ElementID.Air, item.defense, ElementID.Ice, item.defense / 2);
          break;
        case ItemID.MeteorSuit:
          item.SetElementalTraits(ElementID.Air, item.defense, ElementID.Ice, item.defense / 2);
          break;
        case ItemID.MeteorLeggings:
          item.SetElementalTraits(ElementID.Air, item.defense, ElementID.Ice, item.defense / 2);
          break;
        case ItemID.NecroHelmet:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.NecroBreastplate:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.NecroGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.ShadowHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.ShadowScalemail:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.ShadowGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.AncientShadowHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.AncientShadowScalemail:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.AncientShadowGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.CrimsonHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.CrimsonScalemail:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.CrimsonGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;

        case ItemID.MoltenHelmet:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;
        case ItemID.MoltenBreastplate:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;
        case ItemID.MoltenGreaves:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;

        // Hardmode
        case ItemID.PearlwoodHelmet:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense);
          break;
        case ItemID.PearlwoodBreastplate:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense);
          break;
        case ItemID.PearlwoodGreaves:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense);
          break;

        case ItemID.SpiderMask:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.SpiderBreastplate:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.SpiderGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.FrostHelmet:
          item.SetElementalTraits(ElementID.Ice, item.defense, ElementID.Water, -item.defense / 2);
          break;
        case ItemID.FrostBreastplate:
          item.SetElementalTraits(ElementID.Ice, item.defense, ElementID.Water, -item.defense / 2);
          break;
        case ItemID.FrostLeggings:
          item.SetElementalTraits(ElementID.Ice, item.defense, ElementID.Water, -item.defense / 2);
          break;

        case ItemID.AncientBattleArmorHat:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Earth, -item.defense / 2);
          break;
        case ItemID.AncientBattleArmorShirt:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Earth, -item.defense / 2);
          break;
        case ItemID.AncientBattleArmorPants:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Earth, -item.defense / 2);
          break;

        case ItemID.HallowedPlateMail:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;
        case ItemID.HallowedGreaves:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;
        case ItemID.HallowedHeadgear:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;
        case ItemID.HallowedHelmet:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;
        case ItemID.HallowedMask:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;

        case ItemID.ChlorophytePlateMail:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ChlorophyteGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ChlorophyteHeadgear:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ChlorophyteHelmet:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ChlorophyteMask:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TikiMask:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TikiShirt:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TikiPants:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TurtleHelmet:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TurtleScaleMail:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.TurtleLeggings:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeetleHelmet:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeetleScaleMail:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeetleHusk:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.BeetleLeggings:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ShroomiteBreastplate:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ShroomiteLeggings:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ShroomiteHelmet:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ShroomiteMask:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;
        case ItemID.ShroomiteHeadgear:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.SpectreRobe:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpectrePants:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpectreHood:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpectreMask:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpookyBreastplate:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpookyLeggings:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;
        case ItemID.SpookyHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;

        case ItemID.SolarFlareHelmet:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;
        case ItemID.SolarFlareBreastplate:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;
        case ItemID.SolarFlareLeggings:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;
      }
    }

    public override void UpdateEquip(Item item, Player player)
    {
      if (item.Unbidden().element != -1)
        player.Unbidden().resists[item.Unbidden().element] += item.Unbidden().elementDef;
      if (item.Unbidden().weakEl != -1)
        player.Unbidden().resists[item.Unbidden().weakEl] -= item.Unbidden().weakElDef;
      base.UpdateEquip(item, player);
    }
  }
}