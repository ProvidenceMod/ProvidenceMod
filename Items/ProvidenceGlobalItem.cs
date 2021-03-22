using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using static ProvidenceMod.ProvidenceUtils;
using System.Collections.Generic;
using System.Linq;
using ProvidenceMod;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using ProvidenceMod.TexturePack;
using static Terraria.ModLoader.ModContent;
using ProvidenceMod.Items.Materials;

namespace ProvidenceMod
{
  public class ProvidenceGlobalItem : GlobalItem
  {
    public bool   animated;
    public bool cleric;
    public bool glowmask;
    public bool texturePackEnabled;
    public bool frameTickIncrease;
    public int element = -1;
    public int weakElement = -1;
    public int elementDefense;
    public int frame;
    public int frameTick;
    public int frameNumber;
    public int frameTime;
    public int frameCount;
    public int overrideGlowmaskPositionX;
    public int overrideGlowmaskPositionY;
    public int weakElementDefense;
    public Texture2D glowmaskTexture;
    public Texture2D animationTexture;
    public override bool InstancePerEntity => true;
    public ProvidenceGlobalItem()
    {
      element = -1;
      elementDefense = 0;
      weakElement = -1;
      weakElementDefense = 0;
      cleric = false;
    }

    public override void PostUpdate(Item item)
    {
      if (!texturePackEnabled)
      {
        item.InitializeItemGlowMasks();
        texturePackEnabled = true;
      }
      // if(!LocalPlayer().HeldItem.IsAir && LocalPlayer().HeldItem.type == item.type)
      // {
      //   item.ChangeFrameToElement();
      // }
    }
    public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
      if (item.Providence().glowmask && !item.Providence().animated)
      {
        spriteBatch.Draw(glowmaskTexture, new Vector2(item.position.X - Main.screenPosition.X + overrideGlowmaskPositionX, item.position.Y - Main.screenPosition.Y + 2 + overrideGlowmaskPositionY), new Rectangle(0, 0, item.width, item.height), Color.White, rotation, item.Center, 1f, SpriteEffects.None, 0.0f);
      }
      if(item.Providence().glowmask && item.Providence().animated)
      {
        spriteBatch.Draw(glowmaskTexture, new Vector2(item.position.X - Main.screenPosition.X + overrideGlowmaskPositionX, item.position.Y - Main.screenPosition.Y + 2 + overrideGlowmaskPositionY), item.AnimationFrame(ref frameNumber, ref frameTick, frameTime, frameCount, frameTickIncrease), Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      }
      if(item.type == ItemType<Starfiber>() && glowmaskTexture != null)
      {
        spriteBatch.Draw(glowmaskTexture, new Vector2(item.position.X - Main.screenPosition.X + overrideGlowmaskPositionX, item.position.Y - Main.screenPosition.Y + 2 + overrideGlowmaskPositionY), item.AnimationFrame(ref frameNumber, ref frameTick, frameTime, frameCount, frameTickIncrease), Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      }
    }

    public override GlobalItem Clone(Item item, Item itemClone)
    {
      ProvidenceGlobalItem myClone = (ProvidenceGlobalItem)base.Clone(item, itemClone);
      myClone.element = element;
      myClone.elementDefense = elementDefense;
      myClone.weakElement = weakElement;
      myClone.weakElementDefense = weakElementDefense;
      myClone.cleric = cleric;
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
        case ItemID.StickyDynamite:
        case ItemID.StickyGlowstick:
        case ItemID.StickyGrenade:
        case ItemID.LifeCrystal:
        case ItemID.FallenStar:
        case ItemID.Torch:
        case ItemID.BlueTorch:
        case ItemID.TikiTorch:
        case ItemID.BoneTorch:
        case ItemID.CursedTorch:
        case ItemID.DemonTorch:
        case ItemID.GreenTorch:
        case ItemID.IceTorch:
        case ItemID.IchorTorch:
        case ItemID.OrangeTorch:
        case ItemID.PinkTorch:
        case ItemID.PurpleTorch:
        case ItemID.RainbowTorch:
        case ItemID.RedTorch:
        case ItemID.UltrabrightTorch:
        case ItemID.WhiteTorch:
        case ItemID.YellowTorch:
        case ItemID.AdamantiteBar:
        case ItemID.ChlorophyteBar:
        case ItemID.CobaltBar:
        case ItemID.CopperBar:
        case ItemID.CrimtaneBar:
        case ItemID.DemoniteBar:
        case ItemID.GoldBar:
        case ItemID.HallowedBar:
        case ItemID.HellstoneBar:
        case ItemID.IronBar:
        case ItemID.LeadBar:
        case ItemID.LunarBar:
        case ItemID.MeteoriteBar:
        case ItemID.MythrilBar:
        case ItemID.OrichalcumBar:
        case ItemID.PalladiumBar:
        case ItemID.PlatinumBar:
        case ItemID.ShroomiteBar:
        case ItemID.SilverBar:
        case ItemID.SpectreBar:
        case ItemID.TinBar:
        case ItemID.TitaniumBar:
        case ItemID.TungstenBar:
        case ItemID.AmmoReservationPotion:
        case ItemID.ArcheryPotion:
        case ItemID.BattlePotion:
        case ItemID.BuilderPotion:
        case ItemID.CalmingPotion:
        case ItemID.CratePotion:
        case ItemID.EndurancePotion:
        case ItemID.FeatherfallPotion:
        case ItemID.FishingPotion:
        case ItemID.FlipperPotion:
        case ItemID.GenderChangePotion:
        case ItemID.GillsPotion:
        case ItemID.GravitationPotion:
        case ItemID.GreaterHealingPotion:
        case ItemID.GreaterManaPotion:
        case ItemID.HealingPotion:
        case ItemID.HeartreachPotion:
        case ItemID.HunterPotion:
        case ItemID.InfernoPotion:
        case ItemID.InvisibilityPotion:
        case ItemID.IronskinPotion:
        case ItemID.LesserHealingPotion:
        case ItemID.LesserManaPotion:
        case ItemID.LesserRestorationPotion:
        case ItemID.LifeforcePotion:
        case ItemID.LovePotion:
        case ItemID.MagicPowerPotion:
        case ItemID.ManaPotion:
        case ItemID.ManaRegenerationPotion:
        case ItemID.MiningPotion:
        case ItemID.NightOwlPotion:
        case ItemID.ObsidianSkinPotion:
        case ItemID.RagePotion:
        case ItemID.RecallPotion:
        case ItemID.RedPotion:
        case ItemID.RegenerationPotion:
        case ItemID.RestorationPotion:
        case ItemID.ShinePotion:
        case ItemID.SonarPotion:
        case ItemID.SpelunkerPotion:
        case ItemID.StinkPotion:
        case ItemID.SummoningPotion:
        case ItemID.SuperHealingPotion:
        case ItemID.SuperManaPotion:
        case ItemID.SwiftnessPotion:
        case ItemID.TeleportationPotion:
        case ItemID.ThornsPotion:
        case ItemID.TitanPotion:
        case ItemID.TrapsightPotion:
        case ItemID.WarmthPotion:
        case ItemID.WaterWalkingPotion:
        case ItemID.WormholePotion:
        case ItemID.WrathPotion:
        case ItemID.Chest:
        case ItemID.BlueDungeonChest:
        case ItemID.BoneChest:
        case ItemID.BorealWoodChest:
        case ItemID.CactusChest:
        case ItemID.CorruptionChest:
        case ItemID.CrimsonChest:
        case ItemID.CrystalChest:
        case ItemID.DynastyChest:
        case ItemID.EbonwoodChest:
        case ItemID.Fake_BlueDungeonChest:
        case ItemID.Fake_BoneChest:
        case ItemID.Fake_BorealWoodChest:
        case ItemID.Fake_CactusChest:
        case ItemID.Fake_Chest:
        case ItemID.Fake_CorruptionChest:
        case ItemID.Fake_CrimsonChest:
        case ItemID.Fake_CrystalChest:
        case ItemID.Fake_DynastyChest:
        case ItemID.Fake_EbonwoodChest:
        case ItemID.Fake_FleshChest:
        case ItemID.Fake_FrozenChest:
        case ItemID.Fake_GlassChest:
        case ItemID.Fake_GoldChest:
        case ItemID.Fake_GoldenChest:
        case ItemID.Fake_GraniteChest:
        case ItemID.Fake_GreenDungeonChest:
        case ItemID.Fake_HallowedChest:
        case ItemID.Fake_HoneyChest:
        case ItemID.Fake_IceChest:
        case ItemID.Fake_IvyChest:
        case ItemID.Fake_JungleChest:
        case ItemID.Fake_LihzahrdChest:
        case ItemID.Fake_LivingWoodChest:
        case ItemID.Fake_MarbleChest:
        case ItemID.Fake_MartianChest:
        case ItemID.Fake_MeteoriteChest:
        case ItemID.Fake_MushroomChest:
        case ItemID.Fake_ObsidianChest:
        case ItemID.Fake_PalmWoodChest:
        case ItemID.Fake_PearlwoodChest:
        case ItemID.Fake_PinkDungeonChest:
        case ItemID.Fake_PumpkinChest:
        case ItemID.Fake_RichMahoganyChest:
        case ItemID.Fake_ShadewoodChest:
        case ItemID.Fake_ShadowChest:
        case ItemID.Fake_SkywareChest:
        case ItemID.Fake_SlimeChest:
        case ItemID.Fake_SpookyChest:
        case ItemID.Fake_SteampunkChest:
        case ItemID.Fake_WaterChest:
        case ItemID.Fake_WebCoveredChest:
        case ItemID.FleshChest:
        case ItemID.FrozenChest:
        case ItemID.GlassChest:
        case ItemID.GoldChest:
        case ItemID.GoldenChest:
        case ItemID.GraniteChest:
        case ItemID.GreenDungeonChest:
        case ItemID.HallowedChest:
        case ItemID.HoneyChest:
        case ItemID.IceChest:
        case ItemID.IvyChest:
        case ItemID.JungleChest:
        case ItemID.LihzahrdChest:
        case ItemID.LivingWoodChest:
        case ItemID.MarbleChest:
        case ItemID.MartianChest:
        case ItemID.MeteoriteChest:
        case ItemID.MushroomChest:
        case ItemID.ObsidianChest:
        case ItemID.PalmWoodChest:
        case ItemID.PearlwoodChest:
        case ItemID.PinkDungeonChest:
        case ItemID.PumpkinChest:
        case ItemID.RichMahoganyChest:
        case ItemID.ShadewoodChest:
        case ItemID.ShadowChest:
        case ItemID.SkywareChest:
        case ItemID.SlimeChest:
        case ItemID.SpookyChest:
        case ItemID.WaterChest:
        case ItemID.WebCoveredChest:
          item.maxStack = 999;
          break;

        /// VANILLA ELEMENTAL DEFENSES ///
        // Prehardmode
        case ItemID.EbonwoodHelmet:
          // Provides a boost to Necrotic and a penalty to Radiant (defenses)
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.EbonwoodBreastplate:
        case ItemID.EbonwoodGreaves:
        case ItemID.ShadewoodHelmet:
        case ItemID.ShadewoodBreastplate:
        case ItemID.ShadewoodGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense);
          break;
        case ItemID.RainCoat:
        case ItemID.RainHat:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;
        case ItemID.EskimoCoat:
        case ItemID.EskimoHood:
        case ItemID.EskimoPants:
        case ItemID.PinkEskimoCoat:
        case ItemID.PinkEskimoHood:
        case ItemID.PinkEskimoPants:
          item.SetElementalTraits(ElementID.Ice, item.defense);
          break;
        case ItemID.AnglerHat:
        case ItemID.AnglerVest:
        case ItemID.AnglerPants:
          item.SetElementalTraits(ElementID.Water, item.defense);
          break;

        case ItemID.FossilHelm:
        case ItemID.FossilShirt:
        case ItemID.FossilPants:
        case ItemID.BeeHeadgear:
        case ItemID.BeeBreastplate:
        case ItemID.BeeGreaves:
        case ItemID.JungleHat:
        case ItemID.JungleShirt:
        case ItemID.JunglePants:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.MeteorHelmet:
        case ItemID.MeteorSuit:
        case ItemID.MeteorLeggings:
          item.SetElementalTraits(ElementID.Air, item.defense, ElementID.Ice, item.defense / 2);
          break;
        case ItemID.NecroHelmet:
        case ItemID.NecroBreastplate:
        case ItemID.NecroGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.ShadowHelmet:
        case ItemID.ShadowScalemail:
        case ItemID.ShadowGreaves:
        case ItemID.AncientShadowHelmet:
        case ItemID.AncientShadowScalemail:
        case ItemID.AncientShadowGreaves:
        case ItemID.CrimsonHelmet:
        case ItemID.CrimsonScalemail:
        case ItemID.CrimsonGreaves:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;

        case ItemID.MoltenHelmet:
        case ItemID.MoltenBreastplate:
        case ItemID.MoltenGreaves:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;

        // Hardmode
        case ItemID.PearlwoodHelmet:
        case ItemID.PearlwoodBreastplate:
        case ItemID.PearlwoodGreaves:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense);
          break;

        case ItemID.SpiderMask:
        case ItemID.SpiderBreastplate:
        case ItemID.SpiderGreaves:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.FrostHelmet:
        case ItemID.FrostBreastplate:
        case ItemID.FrostLeggings:
          item.SetElementalTraits(ElementID.Ice, item.defense, ElementID.Water, -item.defense / 2);
          break;

        case ItemID.AncientBattleArmorHat:
        case ItemID.AncientBattleArmorShirt:
        case ItemID.AncientBattleArmorPants:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Earth, -item.defense / 2);
          break;

        case ItemID.HallowedPlateMail:
        case ItemID.HallowedGreaves:
        case ItemID.HallowedHeadgear:
        case ItemID.HallowedHelmet:
        case ItemID.HallowedMask:
          item.SetElementalTraits(ElementID.Radiant, item.defense, ElementID.Necrotic, item.defense / 2);
          break;

        case ItemID.ChlorophytePlateMail:
        case ItemID.ChlorophyteGreaves:
        case ItemID.ChlorophyteHeadgear:
        case ItemID.ChlorophyteHelmet:
        case ItemID.ChlorophyteMask:
        case ItemID.TikiMask:
        case ItemID.TikiShirt:
        case ItemID.TikiPants:
        case ItemID.TurtleHelmet:
        case ItemID.TurtleScaleMail:
        case ItemID.TurtleLeggings:
        case ItemID.BeetleHelmet:
        case ItemID.BeetleScaleMail:
        case ItemID.BeetleHusk:
        case ItemID.BeetleLeggings:
        case ItemID.ShroomiteBreastplate:
        case ItemID.ShroomiteLeggings:
        case ItemID.ShroomiteHelmet:
        case ItemID.ShroomiteMask:
        case ItemID.ShroomiteHeadgear:
          item.SetElementalTraits(ElementID.Earth, item.defense, ElementID.Air, item.defense / 2);
          break;

        case ItemID.SpectreRobe:
        case ItemID.SpectrePants:
        case ItemID.SpectreHood:
        case ItemID.SpectreMask:
        case ItemID.SpookyBreastplate:
        case ItemID.SpookyLeggings:
        case ItemID.SpookyHelmet:
          item.SetElementalTraits(ElementID.Necrotic, item.defense, ElementID.Radiant, item.defense / 2);
          break;

        case ItemID.SolarFlareHelmet:
        case ItemID.SolarFlareBreastplate:
        case ItemID.SolarFlareLeggings:
          item.SetElementalTraits(ElementID.Fire, item.defense, ElementID.Water, item.defense / 2);
          break;

        ///// VANILLA WEAPON ELEMENTS /////
        case ItemID.Flamarang:
        case ItemID.FieryGreatsword:
        case ItemID.HelFire:
        case ItemID.DayBreak:
        case ItemID.SolarEruption:
        case ItemID.MolotovCocktail:
        case ItemID.HellwingBow:
        case ItemID.MoltenFury:
        case ItemID.DD2PhoenixBow:
        case ItemID.PhoenixBlaster:
        case ItemID.FlareGun:
        case ItemID.Flamethrower:
        case ItemID.EldMelter:
        case ItemID.WandofSparking:
        case ItemID.Flamelash:
        case ItemID.FlowerofFire:
        case ItemID.ApprenticeStaffT3:
        case ItemID.SpiritFlame:
        case ItemID.ImpStaff:
        case ItemID.DD2FlameburstTowerT1Popper:
        case ItemID.DD2FlameburstTowerT2Popper:
        case ItemID.DD2FlameburstTowerT3Popper:
        case ItemID.DD2SquireDemonSword:
        case ItemID.Sunfury:
        case ItemID.HeatRay:
          item.SetElementalTraits(ElementID.Fire);
          break;

        case ItemID.IceBlade:
        case ItemID.Frostbrand:
        case ItemID.IceSickle:
        case ItemID.Amarok:
        case ItemID.NorthPole:
        case ItemID.IceBoomerang:
        case ItemID.IceBow:
        case ItemID.SnowballCannon:
        case ItemID.SnowmanCannon:
        case ItemID.Snowball:
        case ItemID.FlowerofFrost:
        case ItemID.IceRod:
        case ItemID.FrostStaff:
        case ItemID.BlizzardStaff:
        case ItemID.StaffoftheFrostHydra:
        case ItemID.FrostDaggerfish:
        case ItemID.SnowballLauncher:
          item.SetElementalTraits(ElementID.Ice);
          break;

        case ItemID.DD2LightningAuraT1Popper:
        case ItemID.DD2LightningAuraT2Popper:
        case ItemID.DD2LightningAuraT3Popper:
          item.SetElementalTraits(ElementID.Lightning);
          break;

        case ItemID.Muramasa:
        case ItemID.WaterBolt:
        case ItemID.PurpleClubberfish:
        case ItemID.Kraken:
        case ItemID.Swordfish:
        case ItemID.ObsidianSwordfish:
        case ItemID.BlueMoon:
        case ItemID.Flairon:
        case ItemID.BubbleGun:
        case ItemID.AquaScepter:
        case ItemID.NimbusRod:
        case ItemID.RazorbladeTyphoon:
        case ItemID.TempestStaff:
          item.SetElementalTraits(ElementID.Water);
          break;

        case ItemID.BeeKeeper:
        case ItemID.AntlionClaw:
        case ItemID.BladeofGrass:
        case ItemID.ChlorophyteSaber:
        case ItemID.ChlorophyteClaymore:
        case ItemID.ChlorophytePartisan:
        case ItemID.Seedler:
        case ItemID.JungleYoyo:
        case ItemID.Yelets:
        case ItemID.MushroomSpear:
        case ItemID.ThornChakram:
        case ItemID.PoisonDart:
        case ItemID.PoisonedKnife:
        case ItemID.Beenade:
        case ItemID.VenusMagnum:
        case ItemID.Stynger:
        case ItemID.JackOLanternLauncher:
        case ItemID.Blowgun:
        case ItemID.Blowpipe:
        case ItemID.PoisonStaff:
        case ItemID.StaffofEarth:
        case ItemID.BeeGun:
        case ItemID.WaspGun:
        case ItemID.LeafBlower:
        case ItemID.ToxicFlask:
        case ItemID.SlimeStaff:
        case ItemID.HornetStaff:
        case ItemID.SpiderStaff:
        case ItemID.PygmyStaff:
        case ItemID.QueenSpiderStaff:
        case ItemID.BoneJavelin:
        case ItemID.BoneDagger:
          // case ItemID.BoneSword:
          item.SetElementalTraits(ElementID.Earth);
          break;

          // case ItemID.BoneDagger:
          //   item.SetElementalTraits(ElementID.Air);
          //   break;
      }
    }

    private string DetermineDamagetip(Item item)
    {
      string el;
      string dmgType = item.melee ? "melee" :
                       item.ranged ? "ranged" :
                       item.magic ? "magic" :
                       item.summon ? "summon" :
                       item.thrown ? "throwing" :
                       item.Providence().cleric ? "cleric" :
                       "";
      switch (item.Providence().element)
      {
        case ElementID.Fire:
          el = "fire ";
          break;
        case ElementID.Ice:
          el = "ice ";
          break;
        case ElementID.Lightning:
          el = "lightning ";
          break;
        case ElementID.Water:
          el = "water ";
          break;
        case ElementID.Earth:
          el = "earth ";
          break;
        case ElementID.Air:
          el = "air ";
          break;
        case ElementID.Radiant:
          el = "radiant ";
          break;
        case ElementID.Necrotic:
          el = "necrotic ";
          break;
        default:
          el = "";
          break;
      }
      return $" {el}{dmgType} "; // The space between is added implicitly in el's assignment
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
      TooltipLine tooltip2 = tooltips.Find(x => x.Name == "ItemName" && x.mod == "Terraria");
      if (tooltip2 != null)
      {
        // if(item.type == ModContent.ItemType<MoonCleaver>())
        //   tooltip2.overrideColor = ColorShift(new Color (166, 46, 61), new Color(227, 79, 79), 2f);
        switch (item.rare)
        {
          case (int)ProvidenceRarity.Celestial:
            tooltip2.overrideColor = ColorShift(new Color(119, 37, 100), new Color(246, 121, 133), 5f);
            break;
          case (int)ProvidenceRarity.Developer:
            tooltip2.overrideColor = ColorShift(new Color(166, 46, 61), new Color(227, 79, 79), 5f);
            break;
        }
      }
      TooltipLine tooltip1 = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
      if (tooltip1 != null)
      {
        string[] split = tooltip1.text.Split(' ');
        tooltip1.text = split[0] + DetermineDamagetip(item) + split.Last();
      }
    }
    public override void AddRecipes()
    {
      ModRecipe skymill = new ModRecipe(mod);
      skymill.AddIngredient(ItemID.SunplateBlock, 15);
      skymill.AddIngredient(ItemID.Cloud, 10);
      skymill.AddIngredient(ItemID.RainCloud, 5);
      skymill.AddTile(TileID.WorkBenches);
      skymill.SetResult(ItemID.SkyMill, 1);
      skymill.AddRecipe();
    }

    public override void UpdateEquip(Item item, Player player)
    {
      if (item.Providence().element != -1)
        player.Providence().resists[item.Providence().element] += item.Providence().elementDefense;
      if (item.Providence().weakElement != -1)
        player.Providence().resists[item.Providence().weakElement] -= item.Providence().weakElementDefense;

      base.UpdateEquip(item, player);
    }

    public void Unload()
    {
    }
  }
}