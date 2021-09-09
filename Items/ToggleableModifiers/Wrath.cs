using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProvidenceMod.Items.ToggleableModifiers
{
	public class Wrath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wrath");
			Tooltip.SetDefault("Activates Wrath Mode\nRequires Lament to be active\nToggleable\nDO NOT USE while fighting a boss");
		}
		public override void SetDefaults()
		{
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.width = 80;
			item.height = 110;
			item.consumable = false;
			item.maxStack = 1;
			item.Providence().customRarity = ProvidenceRarity.Wrath;
		}
		public override bool ConsumeItem(Player player) => false;
		public override bool CanUseItem(Player player) => Main.expertMode && ProvidenceWorld.wrath;
		public override bool UseItem(Player player)
		{
			Main.PlaySound(SoundID.DD2_BetsyDeath);
			if (IsThereABoss().Item1)
			{
				// Funny punishment
				for (int i = 0; i < Main.player.Length; i++)
				{
					if (Main.player[i]?.active == true && !Main.player[i].dead)
						Main.player[i]?.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("You tempted fate."), 999999, 0);
				}
			}
			ProvidenceWorld.wrath = !ProvidenceWorld.wrath;
			BrinewastesWorld.wrath = !BrinewastesWorld.wrath;
			if (ProvidenceWorld.wrath)
				Talk("You are overwhelmed with dread.", Color.Purple, player.whoAmI);
			else
				Talk("Sadness will find you.", Color.Purple, player.whoAmI);
			return true;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			for (int i = 0; i < 6; i++)
			{
				float alpha = 1f - (i * (1f / 7f));
				float mult = (float) ((Math.Sin(Main.GlobalTime * 12f) * 0.0625f) + 1f);
				float newScale = (1f + ((i + 1f) * (1f / 14f))) * mult;
				Color[] colors = new Color[6]
				{
					new Color(245, 197, 128, 255).ColorRGBAIntToFloat(),
					new Color(243, 132, 95, 255).ColorRGBAIntToFloat(),
					new Color(218, 70, 70, 255).ColorRGBAIntToFloat(),
					new Color(158, 47, 63, 255).ColorRGBAIntToFloat(),
					new Color(82, 32, 62, 255).ColorRGBAIntToFloat(),
					new Color(41, 16, 41, 255).ColorRGBAIntToFloat(),
				};
				Color color = new Color(1f * alpha * (mult * 1.1f), 1f * alpha * (mult * 1.1f), 1f * alpha * (mult * 1.1f), alpha);
				spriteBatch.Draw(GetTexture("ProvidenceMod/Items/ToggleableModifiers/Wrath_Glow"), item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), color, rotation, new Vector2(item.width / 2, item.height / 2), newScale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(GetTexture("ProvidenceMod/Items/ToggleableModifiers/Wrath"), item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), lightColor, rotation, new Vector2(item.width / 2, item.height / 2), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(GetTexture("ProvidenceMod/Items/ToggleableModifiers/Wrath_Glow"), item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), Color.White, rotation, new Vector2(item.width / 2, item.height / 2), 1f, SpriteEffects.None, 0f);
			return false;
		}
	}
}
