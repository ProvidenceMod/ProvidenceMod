using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ProvidenceMod;

namespace ProvidenceMod
{
  [BackgroundColor(30, 58, 75)]
  [Label("Client Settings")]
  public class ProvidenceClientConfig : ModConfig
  {
    public override ConfigScope Mode => ConfigScope.ClientSide;

		[BackgroundColor(41, 122, 138)]
		[Label("Texture Pack")]
    [Tooltip("Enables the texture pack. Requires a Reload.")]
    [DefaultValue(false)]
    [ReloadRequired]
    public bool texturePack;

		[BackgroundColor(41, 122, 138)]
		[Label("Boss Health Bar HP / Percentage")]
		[Tooltip("Enables HP percentage and amount to show above the boss health bar.")]
		[DefaultValue(true)]
		public bool bossHP;

		public override void OnChanged()
		{
			ProvidenceMod mod = ModContent.GetInstance<ProvidenceMod>();
			mod.texturePack = texturePack;
			mod.bossHP = bossHP;
		}
	}
	public class ProvidenceServerConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[BackgroundColor(41, 122, 138)]
		[Label("Vote for Subworld Entrance")]
		[Tooltip("Enables voting prior to entering a Subworld.")]
		[DefaultValue(true)]
		public bool subworldVote;

		public override void OnChanged()
		{
			ProvidenceMod mod = ModContent.GetInstance<ProvidenceMod>();
			mod.subworldVote = subworldVote;
		}
	}
}