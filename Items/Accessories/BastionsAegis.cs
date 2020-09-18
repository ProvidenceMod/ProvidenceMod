using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace UnbiddenMod.Items.Accessories
{
    public class BastionsAegis : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {

        }

        public override void UpdateEquip(Player player)
        {
            UnbiddenPlayer unbiddenPlayer = player.Unbidden();
        }
    }
}