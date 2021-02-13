using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ProvidenceMod.Items;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod.Tiles
{
  public class EssenceCondenser : ModTile
  {
    public override void SetDefaults()
    {
      Main.tileFrameImportant[Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
      TileObjectData.newTile.Height = 2;
      TileObjectData.newTile.Width = 2;
      TileObjectData.newTile.Origin = new Point16(1, 1);
      TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
      TileObjectData.addTile(Type);
      ModTranslation name = CreateMapEntryName();
      name.SetDefault("Essence Condenser");
      AddMapEntry(new Color(175, 13, 166), name);
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY){
      Item.NewItem(i * 16, j * 16, 32, 32, ItemType<Items.Placeable.EssenceCondenser>());
    }
  }
}