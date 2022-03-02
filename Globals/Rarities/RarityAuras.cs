using Providence.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Providence.Rarities
{
	public static class RarityAuras
	{
		public static void DrawAuras(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (item.rare == ItemRarityID.Master)
				MasterAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Expert)
				ExpertAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Quest)
				QuestAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Gray)
				GrayAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.White)
				WhiteAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Blue)
				BlueAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Green)
				GreenAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Orange)
				OrangeAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.LightRed)
				LightRedAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Pink)
				PinkAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.LightPurple)
				LightPurpleAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Lime)
				LimeAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Yellow)
				YellowAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Cyan)
				CyanAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Red)
				RedAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ItemRarityID.Purple)
				PurpleAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ModContent.RarityType<Lament>())
				LamentAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ModContent.RarityType<Wrath>())
				WrathAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
			else if (item.rare == ModContent.RarityType<Developer>())
				DeveloperAura(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
		}
		public static void MasterAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void ExpertAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void QuestAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void GrayAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void WhiteAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void BlueAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void GreenAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void OrangeAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void LightRedAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void PinkAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void LightPurpleAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void LimeAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void YellowAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void CyanAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void RedAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void PurpleAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void LamentAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void WrathAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void SupporterAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
		public static void DeveloperAura(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{

		}
	}
}
