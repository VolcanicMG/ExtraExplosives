using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace ExtraExplosives.Tiles.Furniture
{
    // TODO can currently be stacked on top of itself, thats not right
    // Additionally due to the size of the sprite, the hitbox is off
    // Need to redo the sprite to align it correctly with the hitbox
    public class BombTableTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            
            
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;
            
            // TODO the line is correct but tml fails to compile when its included
            // TileID.Sets.DisableSmartCursor[Type] = true;

            DustType = DustID.Asphalt; // TODO Wrong dust
            AdjTiles = new int[] { TileID.Tables };
            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Table"));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}