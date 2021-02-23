// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using System;
// using static Terraria.ModLoader.ModContent;
// using ProvidenceMod.Tiles;

// namespace ProvidenceMod.Items.Materials
// {
//   public enum EssenceNames
//   {
//     FireEssence = 0,
//     IceEssence = 1,
//     LightningEssence = 2,
//     WaterEssence = 3,
//     EarthEssence = 4,
//     AirEssence = 5,
//     RadiantEssence = 6,
//     NecroticEssence = 7
//   }

//   public class IceEssence : ModItem
//   {
//     // {ID of item, # needed, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[5, 3] {
//       {ItemID.Snowball, 999, 1},
//       {ItemID.IceRod, 1, 5},
//       {ItemID.Amarok, 1, 10},
//       {ItemID.SnowballCannon, 1, 5},
//       {ItemID.SnowmanCannon, 1, 10}
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/IceEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Ice Essence");
//       Tooltip.SetDefault("This rune is freezing cold. Don't get frostbite!");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }

//   public class LightningEssence : ModItem
//   {
//     // {ID of item, # needed, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[4, 3] {
//       {ItemID.NimbusRod, 2, 1},
//       {ItemID.DD2LightningAuraT1Popper, 1, 3},
//       {ItemID.DD2LightningAuraT2Popper, 1, 9},
//       {ItemID.DD2LightningAuraT3Popper, 1, 15},
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/LightningEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Lightning Essence");
//       Tooltip.SetDefault("This rune makes your hair stand on end. Shocking!");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }
//   public class WaterEssence : ModItem
//   {
//     // {ID of item, # needed, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[7, 3] {
//       {ItemID.NimbusRod, 2, 1},
//       {ItemID.WaterBolt, 1, 5},
//       {ItemID.AquaScepter, 1, 5},
//       {ItemID.Flairon, 1, 10},
//       {ItemID.BubbleGun, 1, 10},
//       {ItemID.RazorbladeTyphoon, 1, 10},
//       {ItemID.TempestStaff, 1, 10},
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/WaterEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Water Essence");
//       Tooltip.SetDefault("This rune is permanantly drenched for whatever reason.");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }

//   public class EarthEssence : ModItem
//   {
//     // {ID of item, # needed, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[,] {
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/EarthEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Earth Essence");
//       Tooltip.SetDefault("This rune is extraordinarily heavy.");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }

//   public class AirEssence : ModItem
//   {
//     // {ID of item, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[,] {
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/AirEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Air Essence");
//       Tooltip.SetDefault("This rune is amazingly light!");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }

//   public class RadiantEssence : ModItem
//   {
//     // {ID of item, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[,] {
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/RadiantEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Radiant Essence");
//       Tooltip.SetDefault("This rune makes you feel like the sun is basking on your arm.");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }
//   public class NecroticEssence : ModItem
//   {
//     // {ID of item, amount of essence recieved}
//     private readonly int[,] smeltableItems = new int[,] {
//       };
//     public override string Texture => "ProvidenceMod/Items/Materials/Essences/NecroticEssence";
//     public override void SetStaticDefaults()
//     {
//       DisplayName.SetDefault("Necrotic Essence");
//       Tooltip.SetDefault("This rune makes your skin itchy and irritable, like it's rotting away.");
//     }

//     public override void SetDefaults()
//     {
//       item.CloneDefaults(ItemID.FallenStar);
//       item.ammo = AmmoID.None;
//     }

//     public override void AddRecipes()
//     {
//       for (int i = 0; i < smeltableItems.GetLength(0); i++)
//       {
//         ModRecipe r = new ModRecipe(mod);
//         r.AddIngredient(smeltableItems[i, 0], smeltableItems[i, 1]);
//         r.AddTile(TileType<EssenceCondenser>());
//         r.SetResult(this, smeltableItems[i, 2]);
//         r.AddRecipe();
//       }
//       for (int i = 0; i < 8; i++)
//       {
//         if (Name != ((EssenceNames)i).ToString())
//         {
//           ModRecipe r = new ModRecipe(mod);
//           r.AddIngredient(mod.ItemType(((EssenceNames)i).ToString()), 1);
//           r.AddTile(TileType<EssenceCondenser>());
//           r.SetResult(this, 1);
//           r.AddRecipe();
//         }
//       }
//     }
//   }
// }
