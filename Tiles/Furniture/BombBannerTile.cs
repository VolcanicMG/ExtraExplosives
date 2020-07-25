using ExtraExplosives.Items.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombBannerTile : ModTile
    {
        public override void SetDefaults() 
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            dustType = -1;
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Banner");
            AddMapEntry(new Color(13, 88, 130), name);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY) 
        {
            int style = frameX / 18;
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("BombBannerItem"));
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects) 
        {
            if (i % 2 == 1) {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}