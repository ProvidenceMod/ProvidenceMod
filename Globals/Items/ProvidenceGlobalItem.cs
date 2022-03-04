using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using static Providence.ProvidenceUtils;
using System.Collections.Generic;
using System.Linq;
using Providence;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Providence.TexturePack;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using Providence.Rarities;

namespace Providence
{
	public class ProvidenceGlobalItem : GlobalItem
	{
		public bool highlight;

		public bool cleric;
		public bool wraith;

		public override bool InstancePerEntity => true;
		public override GlobalItem Clone(Item item, Item itemClone)
		{
			ProvidenceGlobalItem myClone = (ProvidenceGlobalItem)base.Clone(item, itemClone);
			myClone.cleric = cleric;
			myClone.wraith = wraith;
			myClone.highlight = highlight;
			return myClone;
		}
		public override bool OnPickup(Item item, Player player)
		{
			highlight = false;
			return true;
		}
		public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			RarityAuras.DrawAuras(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			return true;
		}
		public override void SetDefaults(Item item)
		{
			if (item.maxStack == 99 || item.maxStack == 30)
				item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			Mod.CreateRecipe(ItemID.SkyMill)
				.AddIngredient(ItemID.SunplateBlock, 15)
				.AddIngredient(ItemID.Cloud, 10)
				.AddIngredient(ItemID.RainCloud, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}