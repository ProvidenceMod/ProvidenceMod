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
            Tooltip.SetDefault("+100% Cleric damage!\nCreates a cleric aura!");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            UnbiddenPlayer unbiddenPlayer = player.Unbidden();
            unbiddenPlayer.cleric += 1f;
            unbiddenPlayer.hasClericSet = true;
            player.dash = 20;
        }
    }
}