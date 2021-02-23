using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework;
using ProvidenceMod.Projectiles.Aura;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Items.Accessories
{
  public class ChaosCatalyst : ModItem
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Chaos Catalyst");
      Tooltip.SetDefault("Material\nToken proof of a taboo creation in physical form; exactly what is needed to expand beyond laws of reality");
    }
    public override void SetDefaults()
    {
      item.material = true;
    }
  }
}