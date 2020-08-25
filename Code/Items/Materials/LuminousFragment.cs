using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnbiddenMod.Code.Items.Materials
{
    public class LuminousFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminous Fragment");
            Tooltip.SetDefault("\"It shimmers with luminous energy.\"");
            item.maxStack = 999;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 5));
        }

        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 29;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI)     
        {
            Texture2D texture = ModContent.GetTexture("UnbiddenMod/Code/Items/Materials/LuminousFragment");
            spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
    }
}