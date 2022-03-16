using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.ToggleableModifiers
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
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.width = 80;
			Item.height = 110;
			Item.consumable = false;
			Item.maxStack = 1;
			Item.rare = ModContent.RarityType<Rarities.Lament>();
		}
		public override bool ConsumeItem(Player player) => false;
		public override bool CanUseItem(Player player) => Main.expertMode && WorldFlags.lament;
		public override bool? UseItem(Player player)
		{
			SoundEngine.PlaySound(SoundID.DD2_BetsyDeath);
			if (IsThereABoss().Item1)
			{
				// Funny punishment
				for (int i = 0; i < Main.player.Length; i++)
				{
					if (Main.player[i]?.active == true && !Main.player[i].dead)
						Main.player[i]?.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("You tempted fate."), 999999, 0);
				}
			}
			WorldFlags.wrath = !WorldFlags.wrath;
			if (WorldFlags.wrath)
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
				float mult = (float)((Math.Sin(Main.GlobalTimeWrappedHourly * 12f) * 0.0625f) + 1f);
				float newScale = (1f + ((i + 1f) * (1f / 14f))) * mult;
				Color[] colors = new Color[6]
				{
					new Color(245, 197, 128, 255).RGBAIntToFloat(),
					new Color(243, 132, 95, 255).RGBAIntToFloat(),
					new Color(218, 70, 70, 255).RGBAIntToFloat(),
					new Color(158, 47, 63, 255).RGBAIntToFloat(),
					new Color(82, 32, 62, 255).RGBAIntToFloat(),
					new Color(41, 16, 41, 255).RGBAIntToFloat(),
				};
				Color color = new(1f * alpha * (mult * 1.1f), 1f * alpha * (mult * 1.1f), 1f * alpha * (mult * 1.1f), alpha);
				spriteBatch.Draw(Request<Texture2D>("Providence/Items/ToggleableModifiers/Wrath_Glow").Value, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), color, rotation, new Vector2(Item.width / 2, Item.height / 2), newScale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(Request<Texture2D>("Providence/Items/ToggleableModifiers/Wrath").Value, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), lightColor, rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(Request<Texture2D>("Providence/Items/ToggleableModifiers/Wrath_Glow").Value, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), Color.White, rotation, new Vector2(Item.width / 2, Item.height / 2), 1f, SpriteEffects.None, 0f);
			return false;
		}
	}
}
