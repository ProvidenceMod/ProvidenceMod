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
  public class RadiantRelic : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Radiant Relic");
      Tooltip.SetDefault("Material\nA primal substance which forms the base building blocks of the universe");
    }
    public override void SetDefaults()
    {
      item.material = true;
    }
  }
}