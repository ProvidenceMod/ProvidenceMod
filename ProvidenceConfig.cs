using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ProvidenceMod;

namespace ProvidenceMod
{
  [BackgroundColor(16, 23, 40)]
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
		[Label("Boss Health Bar HP")]
		[Tooltip("Enables HP amount to show above the boss health bar.")]
		[DefaultValue(true)]
		public bool bossHP;

		[BackgroundColor(41, 122, 138)]
		[Label("Boss Health Bar Percentage")]
		[Tooltip("Enables percentage amount to show above the boss health bar.")]
		[DefaultValue(true)]
		public bool bossPercentage;

		public override void OnChanged()
		{
			ProvidenceMod mod = ModContent.GetInstance<ProvidenceMod>(); 
			mod.texturePack = texturePack;
			mod.bossHP = bossHP;
			mod.bossPercentage = bossPercentage;
		}
	}
	[BackgroundColor(41, 16, 41)]
	[Label("Server Settings")]
	public class ProvidenceServerConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[BackgroundColor(158, 47, 63)]
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