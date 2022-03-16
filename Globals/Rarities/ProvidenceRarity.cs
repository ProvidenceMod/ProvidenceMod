namespace Providence
{
	public enum ProvidenceRarity
	{
		///
		/// <summary>
		/// <para>Minus thirteen (-13)</para>
		/// <para>Master: Fiery</para>
		/// <para>Flag: item.master</para>
		/// <para>This special tier contains Master Mode-exclusive items dropped by bosses in Master Mode.</para>
		Master = -13,
		///
		/// <summary>
		/// <para>Minus twelve (-12)</para>
		/// <para>Expert: Rainbow</para>
		/// <para>Flag: item.expert</para>
		/// <para>This special tier contains Expert mode-exclusive items obtained by opening Treasure Bags dropped by Expert Mode and Master Mode bosses, 
		/// as well as the Treasure Bags themselves.</para>
		/// <para>All Expert-exclusive items from Treasure Bags have this rarity, with the exception of 0x33's Aviators and developer items.
		/// In the game code, these items contain other tier numbers, despite displaying the animated Rainbow rarity color in-game.</para>
		/// </summary>
		Expert = -12,
		///
		/// <summary>
		/// <para>Minus eleven (-11)</para>
		/// <para>Quest: Amber</para>
		/// <para>Flag: item.quest</para>
		/// <para>This special tier contains only "quest items" to be turned over to quest-giving NPCs (currently the Dye Trader and Angler) for rewards.</para>
		/// <para>Quest items have no other use, though the Strange Plants can be sold for 20 each, and can be used as decoration.</para>
		/// </summary>
		Quest = -11,
		///
		/// <summary>
		/// <para>Minus one (-1)</para>
		/// <para>The lowest tier. Only "junk" fishing items have this as a base rarity: Tin Can, Old Shoe, and Seaweed.</para>
		/// <para>This tier mainly accommodates White or Blue-tier equipment with poor modifiers, such as a Shameful Iron Broadsword.</para>
		/// </summary>
		Gray = -1,
		///
		/// <summary>
		/// <para>Zero (0)</para>
		/// <para>Items without a rarity value specified in Terraria's game code default to this tier.</para>
		/// <para>It is by far the rarity containing the most items, many of which could be considered the most common items in the game.</para>
		/// <para>Coins, Hearts and Mana Stars are in this tier. It includes most furniture; building materials (blocks and walls); early tools, weapons,
		/// and armor (made from wood or low-tier ores); common crafting materials like Gel and herbs; paints; and most vanity items.</para>
		/// <para>Items from this tier will be destroyed in lava, with a few exceptions.</para>
		/// </summary>
		White = 0,
		///
		/// <summary>
		/// <para>One (1)</para>
		/// <para>Weapons and armor crafted from early ores, along with early dropped/looted items like the Shackle and Lucky Horseshoe.</para>
		/// <para>Also includes banners, trophies, masks, fish,and pre-Hardmode dyes. Items in this tier and above will not be destroyed in lava.</para>
		/// </summary>
		Blue = 1,
		///
		/// <summary>
		/// <para>Two (2)</para>
		/// <para>Midway pre-Hardmode items. These are mostly looted, dropped, or purchased (non-craftable) items, with the exception of Necro armor,
		/// Sandgun, Spinal Tap, Sunglasses, Obsidian Skull, Star Cannon, Diamond Staff, and Tinkerer's Workshop combinations.</para>
		/// </summary>
		Green = 2,
		///
		/// <summary>
		/// <para>Three (3)</para>
		/// <para>Late-stage pre-Hardmode items: Weapons and armor made of Hellstone, Jungle and Underground Jungle items, Underworld items</para>.
		/// <para>Several Hardmode ores, crafting materials, and consumables (Greater Healing Potion and some ammunition items) also fall into this category.</para>
		/// </summary>
		Orange = 3,
		///
		/// <summary>
		/// <para>Four (4)</para>
		/// <para>Early Hardmode items, including those crafted from the six Hardmode ores spawned from destroying Altars, and item drops from early and/or common Hardmode enemies.</para>
		/// <para>Also includes flasks, and some items that are rare, but still obtainable very early on, such as the Golden Bug Net and Slime Staff.</para>
		/// <para>Also includes some late pre-Hardmode items such as Mana Flower, and most of the items crafted with the Shiny Red Balloon.</para>
		/// </summary>
		LightRed = 4,
		///
		/// <summary>
		/// <para>Five (5)</para>
		/// <para>Mid-Hardmode (pre-Plantera) items, including those acquired after defeating mechanical bosses, e.g. Flamethrower, Optic Staff, and Hallowed armor.</para>
		/// <para>Also includes the more expensive Hardmode NPC purchases like the Clentaminator, and the rarer Hardmode drops,
		/// including the special mid-tier wing ingredients (Tattered Bee Wing, Fire Feather, etc).</para>
		/// <para>Also includes certain pre-Hardmode vanity items such as the Winter Cape.</para>
		/// </summary>
		Pink = 5,
		///
		/// <summary>
		/// <para>Six (6)</para>
		/// <para>A smaller tier consisting of the rarest pre-Plantera items, mostly purchased or dropped, like the Death Sickle and Coin Gun.</para>
		/// <para>Also some higher-tier Tinkerer's Workshop combinations, like the Ankh Charm, and the Ammo Box (obtainable pre-Hardmode).</para>
		/// </summary>
		LightPurple = 6,
		///
		/// <summary>
		/// <para>Seven (7)</para>
		/// <para>Items acquired around Plantera and Golem, and the Hardmode Underground Jungle.</para>
		/// <para>Chlorophyte tools and weapons, Ankh Shield, Temple Key, some Frost Moon drops, and the Candy Cane Hook.</para>
		/// <para>Also some powerful yet pre-Hardmode Tinkerer's Workshop combinations, like the Cell Phone, Frostspark Boots, Lava Waders, and Terraspark Boots.</para>
		/// </summary>
		Lime = 7,
		///
		/// <summary>
		/// <para>Eight (8)</para>
		/// <para>Items acquired or crafted from loot obtained in the post-Plantera Dungeon: Spectre/Ectoplasm items, Biome Chest items like Vampire Knives;
		/// Certain Mimic drops with high modifiers such as the Unreal Daedalus Stormbow; Shroomite items;
		/// Drops from the late-game events: Martian Madness, Pumpkin Moon, Frost Moon, Betsy; and Duke Fishron drops.</para>
		/// <para>Also Beetle armor, Terra Blade, Portal Gun, and all mounts (except for the Minecart).</para>
		/// </summary>
		Yellow = 8,
		///
		/// <summary>
		/// <para>Nine (9)</para>
		/// <para>This smaller tier contains early items acquired from the Lunar Events, primarily Lunar Fragments and Monoliths, and some Moon Lord drops.</para>
		/// <para>Also contains the developer items that can be obtained rarely from Treasure Bags in Expert Mode, and streamer items.</para>
		/// </summary>
		Cyan = 9,
		///
		/// <summary>
		/// <para>Ten (10)</para>
		/// <para>Items crafted at the Ancient Manipulator from Lunar Fragments and/or Luminite.</para>
		/// <para>Also some direct Moon Lord drops, Yellow items with high-level modifiers, and the Ancient Manipulator crafting station itself.</para>
		/// </summary>
		Red = 10,
		///
		/// <summary>
		/// <para>Eleven (11)</para>
		/// <para>This tier consists of Cyan and Red items that have high-level modifiers. No items currently have this as a base rarity.</para>
		/// </summary>
		Purple = 11,
		///
		/// <summary>
		/// <para>Twenty seven (27)</para>
		/// <para>This special tier contains Lament Mode-exclusive items.</para>
		/// </summary>
		Lament = 27,
		///
		/// <summary>
		/// <para>Twenty eight (28)</para>
		/// <para>This special tier contains Wrath Mode-exclusive items.</para>
		/// </summary>
		Wrath = 28,
		///
		/// <summary>
		/// <para>Twenty nine (29)</para>
		/// <para>This tier consists of items that are attributed to Supporters.</para>
		/// </summary>
		Supporter = 29,
		///
		/// <summary>
		/// <para>Thirty (30)</para>
		/// <para>This tier consists of items that are attributed to Developers.</para>
		/// </summary>
		Developer = 30
	}
}