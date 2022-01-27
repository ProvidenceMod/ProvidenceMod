using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProvidenceMod.Items.Materials
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
			item.material = true;
			item.width = 34;
			item.height = 28;
			item.rare = (int)ProvidenceRarity.Orange;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 3, 0);
		}
	}
}
