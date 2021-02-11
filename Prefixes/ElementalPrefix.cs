using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.Prefixes
{
  public class ElementalPrefix : ModPrefix
  {
    private readonly float _power;
    private readonly byte _element;
    public override PrefixCategory Category => PrefixCategory.AnyWeapon;
    public override float RollChance(Item item) => 2.5f;
		public override bool CanRoll(Item item) => item.Providence().element == _element;

    public ElementalPrefix() {}
    public ElementalPrefix(byte power, byte element) { _power = power; _element = element; }
    public override bool Autoload(ref string name)
    {
      if (!base.Autoload(ref name)) return false;

      mod.AddPrefix("Burning", new ElementalPrefix(6, ElementID.Fire));
      mod.AddPrefix("Searing", new ElementalPrefix(12, ElementID.Fire));
      mod.AddPrefix("Scorching", new ElementalPrefix(18, ElementID.Fire));

      mod.AddPrefix("Chilling", new ElementalPrefix(6, ElementID.Ice));
      mod.AddPrefix("Freezing", new ElementalPrefix(12, ElementID.Ice));
      mod.AddPrefix("Frostbiting", new ElementalPrefix(18, ElementID.Ice));

      mod.AddPrefix("Sparking", new ElementalPrefix(6, ElementID.Lightning));
      mod.AddPrefix("Shocking", new ElementalPrefix(12, ElementID.Lightning));
      mod.AddPrefix("Electrocuting", new ElementalPrefix(18, ElementID.Lightning));

      mod.AddPrefix("Dampening", new ElementalPrefix(6, ElementID.Water));
      mod.AddPrefix("Dousing", new ElementalPrefix(12, ElementID.Water));
      mod.AddPrefix("Drenching", new ElementalPrefix(18, ElementID.Water));

      mod.AddPrefix("Cracking", new ElementalPrefix(6, ElementID.Earth));
      mod.AddPrefix("Crushing", new ElementalPrefix(12, ElementID.Earth));
      mod.AddPrefix("Shattering", new ElementalPrefix(18, ElementID.Earth));

      mod.AddPrefix("Breezy", new ElementalPrefix(6, ElementID.Air));
      mod.AddPrefix("Gale-Force", new ElementalPrefix(12, ElementID.Air));
      mod.AddPrefix("Tempestuous", new ElementalPrefix(18, ElementID.Air));

      mod.AddPrefix("Pious", new ElementalPrefix(6, ElementID.Radiant));
      mod.AddPrefix("Cleansing", new ElementalPrefix(12, ElementID.Radiant));
      mod.AddPrefix("Purging", new ElementalPrefix(18, ElementID.Radiant));

      mod.AddPrefix("Afflicting", new ElementalPrefix(6, ElementID.Necrotic));
      mod.AddPrefix("Pestilent", new ElementalPrefix(12, ElementID.Necrotic));
      mod.AddPrefix("Abhorrent", new ElementalPrefix(18, ElementID.Necrotic));
      return false;
    }
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
      damageMult += 0.01f * _power;
    }
  }
}