using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class NukePosterTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            
            TileID.Sets.FramesOnKillWall[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            //TileID.Sets.DisableSmartCursor[Type] = true;
            //TileID.Sets.DisableSmartCursor[Type] = true;
            
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Origin = new Point16(1,0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            
            
            AddMapEntry(new Color(200, 200, 200), name);
            AdjTiles = new int[] { TileID.Tables };
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        /*public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j),i * 16, j * 16, 48, 64, ModContent.ItemType<Items.Tiles.Furniture.NukePosterItem>());
        }*/
    }
}