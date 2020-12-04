using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace UnbiddenMod
{
    public class UnbiddenGlobalItem : GlobalItem
  {

    // Elemental variables for Items
    public bool inverseKB = false;
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
      }
		}
  }
}