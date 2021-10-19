using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace ProvidenceMod.Items.Dyes
{
	public class DivinityDye : ModItem
	{
		public ArmorShaderData ShaderDataToBind => new ArmorShaderData(new Ref<Effect>(mod.GetEffect("Effects/DivinityShader")), "DivinityShader");
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Divinity Dye");
			Tooltip.SetDefault("The strands of fate seemingly reach from the bottle...");
		}
		public override void SetDefaults()
		{
			byte dye = item.dye;
			item.CloneDefaults(ItemID.GelDye);
			item.dye = dye;
			item.width = 36;
			item.height = 36;
		}
	}
}
