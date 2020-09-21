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
            DisplayName.SetDefault("Bastion's Aegis");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            UnbiddenPlayer unbiddenPlayer = player.Unbidden();
            player.dash = 20;
        }
    }
}