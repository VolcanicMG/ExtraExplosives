using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombBookshelfTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 18 };
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.CoordinatePadding = 2;
            //TileObjectData.newTile.CoordinateWidth = 16;
            //TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Bomb Bookcase");
            AddMapEntry(new Color(200, 200, 200), name);
            disableSmartCursor = true;
            AdjTiles = new int[] { TileID.Bookcases };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 64, ModContent.ItemType<Items.Tiles.Furniture.BombBookshelfItem>());
        }
    }
}