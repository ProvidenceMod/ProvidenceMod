using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Providence.Content.NPCs.FireAncient;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence.Content.Items.BossSpawners
{
	public class ScintillatingObsidian : ModItem
	{
		public int frame;
		public int frameNumber;
		public int frameTick;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scintillating Obsidian");
			Tooltip.SetDefault("It pulses with an eerie fiery glow.");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Lime;
			frameNumber = frame;
			Item.consumable = true;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
		}

		public override bool CanUseItem(Player player)
		{
			// No other bosses active and on at least the surface layer
			return !IsThereABoss().Item1 && player.position.Y <= Main.worldSurface * 16;
		}

		public override bool? UseItem(Player player)
		{
			NPC.NewNPC(new EntitySource_BossSpawn(Item), (int)player.position.X, (int)(player.position.Y - (37 * 16)), NPCType<FireAncient>());
			return true;
		}
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D texture = Request<Texture2D>("Providence/Items/BossSpawners/ScintillatingObsidianAnimated").Value;
			spriteBatch.Draw(texture, position, Item.AnimationFrame(ref frameNumber, ref frameTick, 8, 13, true), Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = Request<Texture2D>("Providence/Items/BossSpawners/ScintillatingObsidianGlow").Value;
			spriteBatch.Draw(texture, new Vector2(Item.position.X - Main.screenPosition.X, Item.position.Y - Main.screenPosition.Y + 2), Item.AnimationFrame(ref frame, ref frameTick, 8, 13, true), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Feather, 3)
				.AddIngredient(ItemID.SunplateBlock, 20)
				.AddTile(TileID.SkyMill)
				.Register();
		}
	}
}
