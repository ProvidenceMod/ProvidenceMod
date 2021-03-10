using Terraria;

namespace ProvidenceMod
{
  public enum ProvidenceRarity
  {
    //
    // Summary:
    //     (Not yet implemented) Minus thirteen (-13)\nMaster: Fiery\mFlag: item.master
    //     This special tier contains Master Mode-exclusive items dropped by bosses in Master Mode.
    Master = -13,
    //
    // Summary:
    //     Minus twelve (-12)\nExpert: Rainbow\nFlag: item.expert\n
    //     This special tier contains Expert mode-exclusive items obtained by opening Treasure Bags dropped by Expert Mode and Master Mode bosses,\n
    //     as well as the Treasure Bags themselves. All Expert-exclusive items from Treasure Bags have this rarity, with the exception of 0x33's Aviators and developer items.\n
    //     In the game code, these items contain other tier numbers, despite displaying the animated Rainbow rarity color in-game.
    Expert = -12,
    //
    // Summary:
    //     Minus eleven (-11)\nQuest: Amber\nFlag: item.quest\n
    //     This special tier contains only "quest items" to be turned over to quest-giving NPCs (currently the Dye Trader and Angler) for rewards.\n
    //     Quest items have no other use, though the Strange Plants can be sold for 20 each, and can be used as decoration.
    Quest = -11,
    //
    // Summary:
    //     Minus one (-1)\n
    //     The lowest tier. Only "junk" fishing items have this as a base rarity: Tin Can, Old Shoe, and Seaweed.\n
    //     This tier mainly accommodates White or Blue-tier equipment with poor modifiers, such as a Shameful Iron Broadsword.
    Gray = -1,
    //
    // Summary:
    //     Zero (0)\n
    //     Items without a rarity value specified in Terraria's game code default to this tier.\n
    //     It is by far the rarity containing the most items, many of which could be considered the most common items in the game.\n
    //     Coins, Hearts and Mana Stars are in this tier. It includes most furniture; building materials (blocks and walls); early tools, weapons,\n
    //     and armor (made from wood or low-tier ores); common crafting materials like Gel and herbs; paints; and most vanity items.\n
    //     Items from this tier will be destroyed in lava, with a few exceptions.\n
    White = 0,
    //
    // Summary:
    //     One (1)\n
    //     Weapons and armor crafted from early ores, along with early dropped/looted items like the Shackle and Lucky Horseshoe.\n
    //     Also includes banners, trophies, masks, fish,and pre-Hardmode dyes. Items in this tier and above will not be destroyed in lava.
    Blue = 1,
    //
    // Summary:
    //     Two (2)\n
    //     Midway pre-Hardmode items. These are mostly looted, dropped, or purchased (non-craftable) items, with the exception of Necro armor,\n
    //     Sandgun, Spinal Tap, Sunglasses, Obsidian Skull, Star Cannon, Diamond Staff, and Tinkerer's Workshop combinations.
    Green = 2,
    //
    // Summary:
    //     Three (3)\n
    //     Late-stage pre-Hardmode items: Weapons and armor made of Hellstone, Jungle and Underground Jungle items, Underworld items.\n
    //     Several Hardmode ores, crafting materials, and consumables (Greater Healing Potion and some ammunition items) also fall into this category.
    Orange = 3,
    //
    // Summary:
    //     Four (4)\n
    //     Early Hardmode items, including those crafted from the six Hardmode ores spawned from destroying Altars, and item drops from early and/or common Hardmode enemies.\n
    //     Also includes flasks, and some items that are rare, but still obtainable very early on, such as the Golden Bug Net and Slime Staff.\n
    //     Also includes some late pre-Hardmode items such as Mana Flower, and most of the items crafted with the Shiny Red Balloon.
    LightRed = 4,
    //
    // Summary:
    //     Five (5)\n
    //     Mid-Hardmode (pre-Plantera) items, including those acquired after defeating mechanical bosses, e.g. Flamethrower, Optic Staff, and Hallowed armor.\n
    //     Also includes the more expensive Hardmode NPC purchases like the Clentaminator, and the rarer Hardmode drops,\n
    //     including the special mid-tier wing ingredients (Tattered Bee Wing, Fire Feather, etc).\n
    //     Also includes certain pre-Hardmode vanity items such as the Winter Cape.
    Pink = 5,
    //
    // Summary:
    //     Six (6)\n
    //     A smaller tier consisting of the rarest pre-Plantera items, mostly purchased or dropped, like the Death Sickle and Coin Gun.\n
    //     Also some higher-tier Tinkerer's Workshop combinations, like the Ankh Charm, and the Ammo Box (obtainable pre-Hardmode).
    LightPurple = 6,
    //
    // Summary:
    //     Seven (7)\n
    //     Items acquired around Plantera and Golem, and the Hardmode Underground Jungle.\n
    //     Chlorophyte tools and weapons, Ankh Shield, Temple Key, some Frost Moon drops, and the Candy Cane Hook.\n
    //     Also some powerful yet pre-Hardmode Tinkerer's Workshop combinations, like the Cell Phone, Frostspark Boots, Lava Waders, and Terraspark Boots.
    Lime = 7,
    //
    // Summary:
    //     Eight (8)\n
    //     Items acquired or crafted from loot obtained in the post-Plantera Dungeon: Spectre/Ectoplasm items, Biome Chest items like Vampire Knives;\n
    //     Certain Mimic drops with high modifiers such as the Unreal Daedalus Stormbow; Shroomite items;\n
    //     Drops from the late-game events: Martian Madness, Pumpkin Moon, Frost Moon, Betsy; and Duke Fishron drops.\n
    //     Also Beetle armor, Terra Blade, Portal Gun, and all mounts (except for the Minecart).
    Yellow = 8,
    //
    // Summary:
    //     Nine (9)\n
    //     This smaller tier contains early items acquired from the Lunar Events, primarily Lunar Fragments and Monoliths, and some Moon Lord drops.\n
    //     Also contains the developer items that can be obtained rarely from Treasure Bags in Expert Mode, and streamer items. 
    Cyan = 9,
    //
    // Summary:
    //     Ten (10)\n
    //     Items crafted at the Ancient Manipulator from Lunar Fragments and/or Luminite.\n
    //     Also some direct Moon Lord drops, Yellow items with high-level modifiers, and the Ancient Manipulator crafting station itself.
    Red = 10,
    //
    // Summary:
    //     Eleven (11)\n
    //     This tier consists of Cyan and Red items that have high-level modifiers. No items currently have this as a base rarity.
    Purple = 11,
    Celestial = 12,
    // Summary:
    //     20 (Twenty)\n
    //     This tier consists of items that are attributed to Developers.
    Developer = 999,
  }
}