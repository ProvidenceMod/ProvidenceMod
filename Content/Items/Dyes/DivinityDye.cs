﻿using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Providence.Content.Items.Dyes
{
	public class DivinityDye : ModItem
	{
		public ArmorShaderData ShaderDataToBind => new ArmorShaderData(new Ref<Effect>(ModContent.Request<Effect>("Effects/Quantum").Value), "Quantum");
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Divinity Dye");
			Tooltip.SetDefault("The strands of fate seemingly reach from the bottle...");
		}
		public override void SetDefaults()
		{
			int dye = Item.dye;
			Item.CloneDefaults(ItemID.GelDye);
			Item.dye = dye;
			Item.width = 36;
			Item.height = 36;
		}
	}
}
