using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using static ProvidenceMod.ProvidenceUtils;
using System.Collections.Generic;
using System.Linq;
using ProvidenceMod;
using Microsoft.Xna.Framework.Graphics;
using static ProvidenceMod.ItemHelper;
using ProvidenceMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;

namespace ProvidenceMod
{
  public class ProvidenceGlobalItem : GlobalItem
  {
    // Elemental variables for Items
    public bool cleric;
    public bool glowmask;
    public bool animated;
    public int customRarity;
    public int element = -1, weakEl = -1; // -1 means Typeless, meaning we don't worry about this in the first place
    // Elemental variables also contained within GlobalProjectile, GlobalNPC, and Player
    public int elementDef, weakElDef;
    public Texture2D glowmaskTexture;
    public Texture2D animationTexture;
    public override bool InstancePerEntity => true;
    public ProvidenceGlobalItem()
    {
      element = -1;
      elementDef = 0;
      weakEl = -1;
      weakElDef = 0;
      cleric = false;
    }

    public override GlobalItem Clone(Item item, Item itemClone)
    {
      ProvidenceGlobalItem myClone = (ProvidenceGlobalItem)base.Clone(item, itemClone);
      myClone.element = element;
      myClone.elementDef = elementDef;
      myClone.weakEl = weakEl;
      myClone.weakElDef = weakElDef;
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
      SetItemDefaults(item);
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
      TooltipLine tooltip1 = tooltips.Find(x => x.Name == "Damage" && x.mod == "Terraria");
      if (tooltip1 != null)
      {
        string[] split = tooltip1.text.Split(' ');
        tooltip1.text = split[0] + DetermineDamagetip(item) + split.Last();
      }
      TooltipLine tooltip2 = tooltips.Find(x => x.Name == "ItemName" && x.mod == "Terraria");
      if (tooltip2 != null)
      {
        switch (customRarity)
        {
          case (int)ProvidenceRarity.Celestial:
            tooltip2.overrideColor = ColorShift(new Color(119, 37, 100), new Color(246, 121, 133), 5f);
            break;
          case (int)ProvidenceRarity.Developer:
            tooltip2.overrideColor = ColorShift(new Color(166, 46, 61), new Color(227, 79, 79), 5f);
            break;
        }
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
        player.Providence().resists[item.Providence().element] += item.Providence().elementDef;
      if (item.Providence().weakEl != -1)
        player.Providence().resists[item.Providence().weakEl] -= item.Providence().weakElDef;

      base.UpdateEquip(item, player);
    }
  }
}