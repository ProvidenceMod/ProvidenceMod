﻿using Terraria;
using Terraria.ModLoader;

namespace Providence.Content.Items.Materials
{
	public class AetherShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aether Shard");
			Tooltip.SetDefault("Material");
		}
		public override void SetDefaults()
		{
			Item.material = true;
			Item.width = 34;
			Item.height = 28;
			Item.rare = (int)ProvidenceRarity.Orange;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 3, 0);
		}
	}
}
