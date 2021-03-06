using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Aura;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Materials
{
  public class SolarSigil : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Solar Sigil");
      Tooltip.SetDefault("Material\nA wordless contract with the gods which allow for the impossible to be possessed by mortals");
    }
    public override void SetDefaults()
    {
      item.material = true;
    }
  }
}