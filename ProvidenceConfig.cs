using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ProvidenceMod;

namespace ProvidenceMod
{
  [BackgroundColor(54, 30, 53)]
  [Label("Client Settings")]
  public class ProvidenceConfig : ModConfig
  {
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [BackgroundColor(101, 31, 51)]
    [Label("Use the custom texture pack")]
    [Tooltip("Enables the custom texture pack that comes packaged with ProvidenceMod. Requires a Reload. ")]
    [DefaultValue(false)]
    [ReloadRequired]
    public bool texturePackEnabled;

    public override void OnChanged()
    {
      ProvidenceMod mod = ModContent.GetInstance<ProvidenceMod>();
      mod.texturePackEnabled = texturePackEnabled;
    }
  }
}